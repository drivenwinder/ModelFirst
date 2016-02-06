using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Connections;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Controls.Editors.FloatingEditors;
using EAP.ModelFirst.Controls.KryptonContextMenus;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Relationships;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram
{
    public class Diagram : Model, IDiagram, IEditable, IPrintable
    {
        private enum State
        {
            Normal,
            Multiselecting,
            CreatingShape,
            CreatingConnection,
            Dragging
        }

        #region Fields

        const int DiagramPadding = 10;
        const int PrecisionSize = 10;
        const int MaximalPrecisionDistance = 500;
        const float DashSize = 3;
        static readonly Size MinSize = new Size(3000, 2000);
        public static readonly Pen SelectionPen;

        ElementList<Shape> shapes = new ElementList<Shape>();
        ElementList<Connection> connections = new ElementList<Connection>();
        DiagramElement activeElement = null;
        Point offset = Point.Empty;
        float zoom = 1.0F;
        Size size = MinSize;

        State state = State.Normal;
        bool selectioning = false;
        RectangleF selectionFrame = RectangleF.Empty;
        PointF mouseLocation = PointF.Empty;
        bool redrawSuspended = false;
        int selectedShapeCount = 0;
        int selectedConnectionCount = 0;
        Rectangle shapeOutline = Rectangle.Empty;
        EntityType newShapeType = EntityType.Class;
        ConnectionCreator connectionCreator = null;
        IEntity entity;

        public event EventHandler OffsetChanged;
        public event EventHandler SizeChanged;
        public event EventHandler ZoomChanged;
        public event EventHandler StatusChanged;
        public event EventHandler SelectionChanged;
        public event EventHandler NeedsRedraw;
        public event EventHandler EditStateChanged;
        public event PopupWindowEventHandler ShowingWindow;
        public event PopupWindowEventHandler HidingWindow;
        DiagramEditStack editStack = new DiagramEditStack();
        //XmlElement saved;

        #endregion

        static Diagram()
        {
            SelectionPen = new Pen(Color.FromArgb(255, 59, 97, 156));
            SelectionPen.DashPattern = new float[] { DashSize, DashSize };
        }

        protected Diagram()
        {
        }

        public static Diagram Create(string name)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentException("Name cannot empty string.");
            return new Diagram() { Id = Guid.NewGuid(), Name = name };
        }

        #region Property

        [System.ComponentModel.Browsable(false)]
        public string DocumentName
        {
            get { return Name; }
        }

        [System.ComponentModel.Browsable(false)]
        public IEnumerable<Shape> Shapes
        {
            get { return shapes; }
        }

        protected internal ElementList<Shape> ShapeList
        {
            get { return shapes; }
        }

        [System.ComponentModel.Browsable(false)]
        public IEnumerable<Connection> Connections
        {
            get { return connections; }
        }

        protected internal ElementList<Connection> ConnectionList
        {
            get { return connections; }
        }

        [System.ComponentModel.Browsable(false)]
        public Point Offset
        {
            get
            {
                return offset;
            }
            set
            {
                if (value.X < 0) value.X = 0;
                if (value.Y < 0) value.Y = 0;

                if (offset != value)
                {
                    offset = value;
                    OnOffsetChanged(EventArgs.Empty);
                }
            }
        }

        [System.ComponentModel.Browsable(false)]
        public Size Size
        {
            get
            {
                return size;
            }
            protected set
            {
                if (value.Width < MinSize.Width) value.Width = MinSize.Width;
                if (value.Height < MinSize.Height) value.Height = MinSize.Height;

                if (size != value)
                {
                    size = value;
                    OnSizeChanged(EventArgs.Empty);
                }
            }
        }

        [System.ComponentModel.Browsable(false)]
        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                if (value < Canvas.MinZoom) value = Canvas.MinZoom;
                if (value > Canvas.MaxZoom) value = Canvas.MaxZoom;

                if (zoom != value)
                {
                    zoom = value;
                    OnZoomChanged(EventArgs.Empty);
                }
            }
        }

        [System.ComponentModel.Browsable(false)]
        public Color BackColor
        {
            get { return Style.CurrentStyle.BackgroundColor; }
        }

        [System.ComponentModel.Browsable(false)]
        public bool RedrawSuspended
        {
            get
            {
                return redrawSuspended;
            }
            set
            {
                if (redrawSuspended != value)
                {
                    redrawSuspended = value;
                    if (!redrawSuspended)
                    {
                        RecalculateSize();
                        RequestRedrawIfNeeded();
                    }
                }
            }
        }

        [System.ComponentModel.Browsable(false)]
        public bool CanUndo
        {
            get { return editStack.CanUndo; }
        }

        [System.ComponentModel.Browsable(false)]
        public bool CanRedo
        {
            get { return editStack.CanRedo; }
        }

        [System.ComponentModel.Browsable(false)]
        public bool CanDelete
        {
            get { return HasSelectedElement; }
        }

        [System.ComponentModel.Browsable(false)]
        public bool CanCutToClipboard
        {
            get { return SelectedShapeCount > 0; }
        }

        [System.ComponentModel.Browsable(false)]
        public bool CanCopyToClipboard
        {
            get { return SelectedShapeCount > 0; }
        }

        [System.ComponentModel.Browsable(false)]
        public bool CanPasteFromClipboard
        {
            get { return Clipboard.Item is ElementContainer; }
        }

        [System.ComponentModel.Browsable(false)]
        public int ShapeCount
        {
            get { return shapes.Count; }
        }

        [System.ComponentModel.Browsable(false)]
        public int ConnectionCount
        {
            get { return connections.Count; }
        }

        [System.ComponentModel.Browsable(false)]
        public DiagramElement ActiveElement
        {
            get
            {
                return activeElement;
            }
            private set
            {
                if (activeElement != null)
                {
                    activeElement.IsActive = false;
                }
                activeElement = value;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public DiagramElement TopSelectedElement
        {
            get
            {
                if (SelectedConnectionCount > 0)
                    return connections.FirstValue;
                else if (SelectedShapeCount > 0)
                    return shapes.FirstValue;
                else
                    return null;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public bool HasSelectedElement
        {
            get
            {
                return SelectedElementCount > 0;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public int SelectedElementCount
        {
            get { return selectedShapeCount + selectedConnectionCount; }
        }

        [System.ComponentModel.Browsable(false)]
        public int SelectedShapeCount
        {
            get { return selectedShapeCount; }
        }

        [System.ComponentModel.Browsable(false)]
        public int SelectedConnectionCount
        {
            get { return selectedConnectionCount; }
        }

        #endregion

        #region Operation

        public void CloseWindows()
        {
            if (ActiveElement != null)
                ActiveElement.HideEditor();
        }

        public void Undo()
        {
            RedrawSuspended = true;

            Deserialize(editStack.Undo());

            RedrawSuspended = false;
        }

        public void Redo()
        {
            RedrawSuspended = true;

            Deserialize(editStack.Redo());

            RedrawSuspended = false;
        }

        public void Delete()
        {
            DeleteSelectedElements();
        }

        public void Cut()
        {
            if (CanCutToClipboard)
            {
                Copy();
                DeleteSelectedElements(false);
            }
        }

        public void Copy()
        {
            if (CanCopyToClipboard)
            {
                ElementContainer elements = new ElementContainer();
                foreach (Shape shape in GetSelectedShapes())
                {
                    elements.AddShape(shape);
                }
                foreach (Connection connection in GetSelectedConnections())
                {
                    elements.AddConnection(connection);
                }
                Clipboard.Item = elements;
            }
        }

        public void Paste()
        {
            if (CanPasteFromClipboard)
            {
                DeselectAll();
                RedrawSuspended = true;
                Clipboard.Paste(this);
                RedrawSuspended = false;
                OnEditStateChanged(EventArgs.Empty);
            }
        }

        public void Display(Graphics g)
        {
            RectangleF clip = g.ClipBounds;

            // Draw diagram elements
            IGraphics graphics = new GdiGraphics(g);
            foreach (DiagramElement element in GetElementsInReversedDisplayOrder())
            {
                if (clip.IntersectsWith(element.GetVisibleArea(Zoom)))
                    element.Draw(graphics, true);
                element.NeedsRedraw = false;
            }
            if (state == State.CreatingShape)
            {
                g.DrawRectangle(SelectionPen,
                    shapeOutline.X, shapeOutline.Y, shapeOutline.Width, shapeOutline.Height);
            }
            else if (state == State.CreatingConnection)
            {
                connectionCreator.Draw(g);
            }

            // Draw selection lines
            GraphicsState savedState = g.Save();
            g.ResetTransform();
            g.SmoothingMode = SmoothingMode.None;
            foreach (Shape shape in shapes.GetSelectedElementsReversed())
            {
                if (clip.IntersectsWith(shape.GetVisibleArea(Zoom)))
                    shape.DrawSelectionLines(g, Zoom, Offset);
            }
            foreach (Connection connection in connections.GetSelectedElementsReversed())
            {
                if (clip.IntersectsWith(connection.GetVisibleArea(Zoom)))
                    connection.DrawSelectionLines(g, Zoom, Offset);
            }

            if (state == State.Multiselecting)
            {
                RectangleF frame = RectangleF.FromLTRB(
                    Math.Min(selectionFrame.Left, selectionFrame.Right),
                    Math.Min(selectionFrame.Top, selectionFrame.Bottom),
                    Math.Max(selectionFrame.Left, selectionFrame.Right),
                    Math.Max(selectionFrame.Top, selectionFrame.Bottom));
                g.DrawRectangle(SelectionPen,
                    frame.X * Zoom - Offset.X,
                    frame.Y * Zoom - Offset.Y,
                    frame.Width * Zoom,
                    frame.Height * Zoom);
            }

            // Draw diagram border
            clip = g.ClipBounds;
            float borderWidth = Size.Width * Zoom;
            float borderHeight = Size.Height * Zoom;
            if (clip.Right > borderWidth || clip.Bottom > borderHeight)
            {
                SelectionPen.DashOffset = Offset.Y - Offset.X;
                g.DrawLines(SelectionPen, new PointF[] {
					new PointF(borderWidth, 0),
					new PointF(borderWidth, borderHeight),
					new PointF(0, borderHeight)
				});
                SelectionPen.DashOffset = 0;
            }

            // Restore original state
            g.Restore(savedState);
        }

        public void CopyAsImage()
        {
            ImageCreator.CopyAsImage(this);
        }

        public void CopyAsImage(bool selectedOnly)
        {
            ImageCreator.CopyAsImage(this, selectedOnly);
        }

        public void SaveAsImage()
        {
            ImageCreator.SaveAsImage(this);
        }

        public void SaveAsImage(bool selectedOnly)
        {
            ImageCreator.SaveAsImage(this, selectedOnly);
        }

        public void Print(IGraphics g)
        {
            Print(g, false, Style.CurrentStyle);
        }

        public void Print(IGraphics g, bool selectedOnly, Style style)
        {
            foreach (Shape shape in shapes.GetReversedList())
            {
                if (!selectedOnly || shape.IsSelected)
                    shape.Draw(g, false, style);
            }
            foreach (Connection connection in connections.GetReversedList())
            {
                if (!selectedOnly || connection.IsSelected)
                    connection.Draw(g, false, style);
            }
        }

        private void RecalculateSize()
        {
            const int Padding = 500;
            int rightMax = MinSize.Width, bottomMax = MinSize.Height;

            foreach (Shape shape in shapes)
            {
                Rectangle area = shape.GetLogicalArea();
                if (area.Right + Padding > rightMax)
                    rightMax = area.Right + Padding;
                if (area.Bottom + Padding > bottomMax)
                    bottomMax = area.Bottom + Padding;
            }
            foreach (Connection connection in connections)
            {
                Rectangle area = connection.GetLogicalArea();
                if (area.Right + Padding > rightMax)
                    rightMax = area.Right + Padding;
                if (area.Bottom + Padding > bottomMax)
                    bottomMax = area.Bottom + Padding;
            }

            this.Size = new Size(rightMax, bottomMax);
        }

        public void SetShapeBackColor(Color color)
        {
            RedrawSuspended = true;
            foreach (Shape shape in shapes.GetSelectedElements())
                shape.BackColor = color;
            RedrawSuspended = false;
        }

        public void SetShapeForeColor(Color color)
        {
            RedrawSuspended = true;
            foreach (Shape shape in shapes.GetSelectedElements())
                shape.ForeColor = color;
            RedrawSuspended = false;
        }

        public void AlignLeft()
        {
            if (SelectedShapeCount >= 2)
            {
                int left = Size.Width;
                RedrawSuspended = true;

                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    left = Math.Min(left, shape.Left);
                }
                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    shape.Left = left;
                }

                RedrawSuspended = false;
            }
        }

        public void AlignRight()
        {
            if (SelectedShapeCount >= 2)
            {
                int right = 0;
                RedrawSuspended = true;

                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    right = Math.Max(right, shape.Right);
                }
                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    shape.Right = right;
                }

                RedrawSuspended = false;
            }
        }

        public void AlignTop()
        {
            if (SelectedShapeCount >= 2)
            {
                int top = Size.Height;
                RedrawSuspended = true;

                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    top = Math.Min(top, shape.Top);
                }
                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    shape.Top = top;
                }

                RedrawSuspended = false;
            }
        }

        public void AlignBottom()
        {
            if (SelectedShapeCount >= 2)
            {
                int bottom = 0;
                RedrawSuspended = true;

                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    bottom = Math.Max(bottom, shape.Bottom);
                }
                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    shape.Bottom = bottom;
                }

                RedrawSuspended = false;
            }
        }

        public void AlignHorizontal()
        {
            if (SelectedShapeCount >= 2)
            {
                int center = 0;
                RedrawSuspended = true;

                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    center += (shape.Top + shape.Bottom) / 2;
                }
                center /= SelectedShapeCount;

                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    shape.Top = center - shape.Height / 2;
                }

                RedrawSuspended = false;
            }
        }

        public void AlignVertical()
        {
            if (SelectedShapeCount >= 2)
            {
                int center = 0;
                RedrawSuspended = true;

                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    center += (shape.Left + shape.Right) / 2;
                }
                center /= SelectedShapeCount;

                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    shape.Left = center - shape.Width / 2;
                }

                RedrawSuspended = false;
            }
        }

        public void AdjustToSameWidth()
        {
            if (SelectedShapeCount >= 2)
            {
                int maxWidth = 0;
                RedrawSuspended = true;

                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    maxWidth = Math.Max(maxWidth, shape.Width);
                }
                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    shape.Width = maxWidth;
                }
                RedrawSuspended = false;
            }
        }

        public void AdjustToSameHeight()
        {
            if (SelectedShapeCount >= 2)
            {
                int maxHeight = 0;
                RedrawSuspended = true;

                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    maxHeight = Math.Max(maxHeight, shape.Height);
                }
                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    shape.Height = maxHeight;
                }

                RedrawSuspended = false;
            }
        }

        public void AdjustToSameSize()
        {
            if (SelectedShapeCount >= 2)
            {
                Size maxSize = Size.Empty;
                RedrawSuspended = true;

                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    maxSize.Width = Math.Max(maxSize.Width, shape.Width);
                    maxSize.Height = Math.Max(maxSize.Height, shape.Height);
                }
                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    shape.Size = maxSize;
                }

                RedrawSuspended = false;
            }
        }

        public void AutoSizeOfShapes()
        {
            RedrawSuspended = true;
            var lst = shapes.GetSelectedElements();
            if (!lst.Any())
                lst = shapes;
            foreach (Shape shape in lst)
            {
                shape.AutoWidth();
                shape.AutoHeight();
            }
            RedrawSuspended = false;
        }

        public void AutoWidthOfShapes()
        {
            RedrawSuspended = true;
            var lst = shapes.GetSelectedElements();
            if (!lst.Any())
                lst = shapes;
            foreach (Shape shape in lst)
            {
                shape.AutoWidth();
            }
            RedrawSuspended = false;
        }

        public void AutoHeightOfShapes()
        {
            RedrawSuspended = true;
            var lst = shapes.GetSelectedElements();
            if (!lst.Any())
                lst = shapes;
            foreach (Shape shape in lst)
            {
                shape.AutoHeight();
            }
            RedrawSuspended = false;
        }

        public void CollapseAll()
        {
            bool selectedOnly = HasSelectedElement;
            CollapseAll(selectedOnly);
        }

        public void CollapseAll(bool selectedOnly)
        {
            RedrawSuspended = true;

            foreach (Shape shape in shapes)
            {
                if (shape.IsSelected || !selectedOnly)
                    shape.Collapse();
            }

            RedrawSuspended = false;
        }

        public void ExpandAll()
        {
            bool selectedOnly = HasSelectedElement;
            ExpandAll(selectedOnly);
        }

        public void ExpandAll(bool selectedOnly)
        {
            RedrawSuspended = true;

            foreach (Shape shape in shapes)
            {
                if (shape.IsSelected || !selectedOnly)
                    shape.Expand();
            }

            RedrawSuspended = false;
        }

        public void SelectAll()
        {
            RedrawSuspended = true;
            selectioning = true;

            foreach (Shape shape in shapes)
            {
                shape.IsSelected = true;
            }
            foreach (Connection connection in connections)
            {
                connection.IsSelected = true;
            }

            selectedShapeCount = shapes.Count;
            selectedConnectionCount = connections.Count;

            OnSelectionChanged(EventArgs.Empty);
            OnEditStateChanged(EventArgs.Empty);
            OnSatusChanged(EventArgs.Empty);

            selectioning = false;
            RedrawSuspended = false;
        }

        public void AcceptEdit()
        {
            if (IsEditing && state == State.Normal)
                editStack.Push(editStack.Serialize(this));
        }

        [System.ComponentModel.Browsable(false)]
        public bool IsEditing { get; private set; }

        public void BeginEdit()
        {
            IsEditing = true;
            //saved = editStack.Serialize(this);
            //editStack.Init(saved);
        }

        public void EndEdit()
        {
            IsEditing = false;
        }

        public void CancelEdit()
        {
            //Deserialize(saved);
        }

        #endregion

        #region Selection

        public string GetSelectedElementName()
        {
            if (HasSelectedElement && SelectedElementCount == 1)
            {
                foreach (Shape shape in shapes)
                {
                    if (shape.IsSelected)
                        return shape.Entity.Name;
                }
            }

            return null;
        }

        public IEnumerable<Shape> GetSelectedShapes()
        {
            return shapes.GetSelectedElements();
        }

        public IEnumerable<Connection> GetSelectedConnections()
        {
            return connections.GetSelectedElements();
        }

        public IEnumerable<DiagramElement> GetSelectedElements()
        {
            foreach (Shape shape in shapes)
            {
                if (shape.IsSelected)
                    yield return shape;
            }
            foreach (Connection connection in connections)
            {
                if (connection.IsSelected)
                    yield return connection;
            }
        }

        public void DeleteSelectedElements()
        {
            DeleteSelectedElements(true);
        }

        private void DeleteSelectedElements(bool showConfirmation)
        {
            if (HasSelectedElement && (!showConfirmation || ConfirmDelete()))
            {
                if (selectedShapeCount > 0)
                {
                    foreach (Shape shape in shapes.GetModifiableList())
                    {
                        if (shape.IsSelected)
                            RemoveEntity(shape.Entity);
                    }
                }
                if (selectedConnectionCount > 0)
                {
                    foreach (Connection connection in connections.GetModifiableList())
                    {
                        if (connection.IsSelected)
                        {
                            RemoveRelationship(connection.Relationship);
                        }
                    }
                }
                Redraw();
            }
        }

        private void SelectElements(AbsoluteMouseEventArgs e)
        {
            DiagramElement firstElement = null;
            bool multiSelection = (Control.ModifierKeys == Keys.Control);

            foreach (DiagramElement element in GetElementsInDisplayOrder())
            {
                bool isSelected = element.IsSelected;
                element.MousePressed(e);
                if (e.Handled && firstElement == null)
                {
                    firstElement = element;
                    if (isSelected)
                        multiSelection = true;
                }
            }

            if (firstElement != null && !multiSelection)
            {
                DeselectAllOthers(firstElement);
            }

            if (!e.Handled)
            {
                if (!multiSelection)
                    DeselectAll();

                if (e.Button == MouseButtons.Left)
                {
                    state = State.Multiselecting;
                    selectionFrame.Location = e.Location;
                    selectionFrame.Size = Size.Empty;
                }
            }
        }

        private void TrySelectElements()
        {
            selectionFrame = RectangleF.FromLTRB(
                Math.Min(selectionFrame.Left, selectionFrame.Right),
                Math.Min(selectionFrame.Top, selectionFrame.Bottom),
                Math.Max(selectionFrame.Left, selectionFrame.Right),
                Math.Max(selectionFrame.Top, selectionFrame.Bottom));
            selectioning = true;

            foreach (Shape shape in shapes)
            {
                if (shape.TrySelect(selectionFrame))
                    selectedShapeCount++;
            }
            foreach (Connection connection in connections)
            {
                if (connection.TrySelect(selectionFrame))
                    selectedConnectionCount++;
            }

            OnSelectionChanged(EventArgs.Empty);
            OnEditStateChanged(EventArgs.Empty);
            OnSatusChanged(EventArgs.Empty);
            Redraw();

            selectioning = false;
        }

        #endregion

        public void Redraw()
        {
            OnNeedsRedraw(EventArgs.Empty);
        }

        #region Menu

        public KryptonContextMenu GetKryptonContextMenu(AbsoluteMouseEventArgs e)
        {
            KryptonContextMenuBase.MenuStrip.Items.Clear();
            if (HasSelectedElement)
            {
                Intersector<KryptonContextMenuItemBase> intersector = new Intersector<KryptonContextMenuItemBase>();

                foreach (Shape shape in GetSelectedShapes())
                    intersector.AddSet(shape.GetKryptonContextMenuItems(this).Items);
                foreach (Connection connection in GetSelectedConnections())
                    intersector.AddSet(connection.GetKryptonContextMenuItems(this).Items);

                KryptonContextMenuBase.MenuStrip.Items.Add(new KryptonContextMenuItems(intersector.GetIntersection().ToArray()));
                return KryptonContextMenuBase.MenuStrip;
            }
            else
            {
                KryptonContextMenuBase.MenuStrip.Items.Add(BlankKryptonContextMenu.Default.GetMenuItems(this));
            }
            return KryptonContextMenuBase.MenuStrip;
        }

        #endregion

        public string GetStatus()
        {
            if (SelectedElementCount == 1)
            {
                return TopSelectedElement.ToString();
            }
            else if (SelectedElementCount > 1)
            {
                return string.Format(Strings.ItemsSelected, SelectedElementCount);
            }
            else
            {
                return Strings.Ready;
            }
        }

        public string GetShortDescription()
        {
            return Strings.Language + ": " + Language.ToString();
        }

        public void DeselectAll()
        {
            foreach (Shape shape in shapes)
            {
                shape.IsSelected = false;
                shape.IsActive = false;
            }
            foreach (Connection connection in connections)
            {
                connection.IsSelected = false;
                connection.IsActive = false;
            }
            ActiveElement = null;
        }

        public RectangleF GetPrintingArea(bool selectedOnly)
        {
            RectangleF area = Rectangle.Empty;
            bool first = true;

            foreach (Shape shape in shapes)
            {
                if (!selectedOnly || shape.IsSelected)
                {
                    if (first)
                    {
                        area = shape.GetPrintingClip(Zoom);
                        first = false;
                    }
                    else
                    {
                        area = RectangleF.Union(area, shape.GetPrintingClip(Zoom));
                    }
                }
            }
            foreach (Connection connection in connections)
            {
                if (!selectedOnly || connection.IsSelected)
                {
                    if (first)
                    {
                        area = connection.GetPrintingClip(Zoom);
                        first = false;
                    }
                    else
                    {
                        area = RectangleF.Union(area, connection.GetPrintingClip(Zoom));
                    }
                }
            }

            return area;
        }

        #region Mouse & Key Event

        public void MouseUp(AbsoluteMouseEventArgs e)
        {
            RedrawSuspended = true;

            if (state == State.Multiselecting)
            {
                TrySelectElements();
                state = State.Normal;
            }
            else
            {
                foreach (DiagramElement element in GetElementsInDisplayOrder())
                {
                    element.MouseUpped(e);
                }
            }

            RedrawSuspended = false;
        }

        public void MouseDown(AbsoluteMouseEventArgs e)
        {
            RedrawSuspended = true;

            if (state == State.CreatingShape)
            {
                AddCreatedShape();
            }
            else if (state == State.CreatingConnection)
            {
                connectionCreator.MouseDown(e);
                if (connectionCreator.Created)
                    state = State.Normal;
            }
            else
            {
                SelectElements(e);
            }

            if (e.Button == MouseButtons.Right)
            {
                ActiveElement = null;
            }

            RedrawSuspended = false;
        }

        public void MouseMove(AbsoluteMouseEventArgs e)
        {
            RedrawSuspended = true;

            mouseLocation = e.Location;
            if (state == State.Multiselecting)
            {
                selectionFrame = RectangleF.FromLTRB(
                    selectionFrame.Left, selectionFrame.Top, e.X, e.Y);
                Redraw();
            }
            else if (state == State.CreatingShape)
            {
                shapeOutline.Location = new Point((int)e.X, (int)e.Y);
                Redraw();
            }
            else if (state == State.CreatingConnection)
            {
                connectionCreator.MouseMove(e);
            }
            else
            {
                foreach (DiagramElement element in GetElementsInDisplayOrder())
                {
                    element.MouseMoved(e);
                }
            }

            RedrawSuspended = false;
        }

        public void DragOver(AbsoluteMouseEventArgs e)
        {
            RedrawSuspended = true;
            if (state == State.CreatingShape)
            {
                shapeOutline.Location = new Point((int)e.X, (int)e.Y);
                Redraw();
            }
            else if (state == State.CreatingConnection)
            {
                connectionCreator.MouseMove(e);
            }
            RedrawSuspended = false;
        }

        public void DragDrop(AbsoluteMouseEventArgs e)
        {
            RedrawSuspended = true;
            if (state == State.CreatingShape)
            {
                AddCreatedShape();
            }
            else if (state == State.CreatingConnection)
            {
                connectionCreator.MouseDown(e);
                if (connectionCreator.Created)
                    state = State.Normal;
            }
            RedrawSuspended = false;
        }

        public void DoubleClick(AbsoluteMouseEventArgs e)
        {
            foreach (DiagramElement element in GetElementsInDisplayOrder())
            {
                element.DoubleClicked(e);
            }
        }

        public void KeyDown(KeyEventArgs e)
        {
            //TODO: ActiveElement.KeyDown()
            RedrawSuspended = true;

            // Delete
            if (e.KeyCode == Keys.Delete)
            {
                if (SelectedElementCount >= 2 || ActiveElement == null ||
                    !ActiveElement.DeleteSelectedMember())
                {
                    DeleteSelectedElements();
                }
            }
            // Escape
            else if (e.KeyCode == Keys.Escape)
            {
                state = State.Normal;
                DeselectAll();
                Redraw();
            }
            else if (e.KeyCode == Keys.F4 && ActiveElement != null)
            {
                ActiveElement.ShowEditDialog();
            }
            // Enter
            else if (e.KeyCode == Keys.Enter && ActiveElement != null)
            {
                ActiveElement.ShowEditor();
            }
            // Up
            else if (e.KeyCode == Keys.Up && ActiveElement != null)
            {
                if (e.Shift || e.Control)
                    ActiveElement.MoveUp();
                else
                    ActiveElement.SelectPrevious();
            }
            // Down
            else if (e.KeyCode == Keys.Down && ActiveElement != null)
            {
                if (e.Shift || e.Control)
                    ActiveElement.MoveDown();
                else
                    ActiveElement.SelectNext();
            }
            // Ctrl + A
            else if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                SelectAll();
            }
            // Ctrl + X
            else if (e.KeyCode == Keys.X && e.Modifiers == Keys.Control)
            {
                Cut();
            }
            // Ctrl + C
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                Copy();
            }
            // Ctrl + V
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                Paste();
            }
            // Ctrl + D
            else if (e.KeyCode == Keys.D && e.Modifiers == Keys.Control)
            {
                AutoSizeOfShapes();
            }
            // Ctrl + Shift + ?
            else if (e.Modifiers == (Keys.Control | Keys.Shift))
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        CreateShape();
                        break;

                    case Keys.C:
                        CreateShape(EntityType.Class);
                        break;

                    case Keys.S:
                        CreateShape(EntityType.Structure);
                        break;

                    case Keys.I:
                        CreateShape(EntityType.Interface);
                        break;

                    case Keys.E:
                        CreateShape(EntityType.Enum);
                        break;

                    case Keys.D:
                        CreateShape(EntityType.Delegate);
                        break;

                    case Keys.N:
                        CreateShape(EntityType.Comment);
                        break;
                }
            }
            RedrawSuspended = false;
        }

        #endregion

        #region Create Shape

        public void CancelCreating()
        {
            state = State.Normal;
            Redraw();
        }

        public void CreateShape()
        {
            CreateShape(newShapeType);
        }

        public void CreateShape(EntityType type)
        {
            state = State.CreatingShape;
            newShapeType = type;

            switch (type)
            {
                case EntityType.Class:
                case EntityType.Delegate:
                case EntityType.Enum:
                case EntityType.Interface:
                case EntityType.Structure:
                    shapeOutline = TypeShape.GetOutline(Style.CurrentStyle);
                    break;

                case EntityType.Comment:
                    shapeOutline = CommentShape.GetOutline(Style.CurrentStyle);
                    break;
            }
            shapeOutline.Location = new Point((int)mouseLocation.X, (int)mouseLocation.Y);
            entity = CreateEntity(type);
            if (entity is TypeBase)
                ((TypeBase)entity).IsUntitled = true;
            Redraw();
        }

        public void CreateShape(IEntity e)
        {
            state = State.CreatingShape;

            switch (e.EntityType)
            {
                case EntityType.Class:
                case EntityType.Delegate:
                case EntityType.Enum:
                case EntityType.Interface:
                case EntityType.Structure:
                    shapeOutline = TypeShape.GetOutline(Style.CurrentStyle);
                    break;

                case EntityType.Comment:
                    shapeOutline = CommentShape.GetOutline(Style.CurrentStyle);
                    break;
            }
            shapeOutline.Location = new Point((int)mouseLocation.X, (int)mouseLocation.Y);
            entity = e;
            Redraw();
        }

        public void CreateConnection(RelationshipType type)
        {
            connectionCreator = new ConnectionCreator(this, type);
            state = State.CreatingConnection;
        }
        public void CreateConnection(Shape first, RelationshipType type)
        {
            connectionCreator = new ConnectionCreator(this, first, type);
            state = State.CreatingConnection;
        }

        private void AddShape(Shape shape)
        {
            AddEntity(shape.Entity);
            shape.Diagram = this;
            shape.Modified += new EventHandler(element_Modified);
            shape.Activating += new EventHandler(element_Activating);
            shape.Dragging += new MoveEventHandler(shape_Dragging);
            shape.Resizing += new ResizeEventHandler(shape_Resizing);
            shape.SelectionChanged += new EventHandler(shape_SelectionChanged);
            shapes.AddFirst(shape);
            RecalculateSize();
        }

        private void AddCreatedShape()
        {
            state = State.Normal;
            DeselectAll();
            Shape shape = AddShape(entity);
            shape.Location = shapeOutline.Location;
            RecalculateSize();

            shape.IsSelected = true;
            shape.IsActive = true;
            if (shape is TypeShape && ((TypeShape)shape).TypeBase.IsUntitled)
                shape.ShowEditor();
        }

        private IEntity CreateEntity(EntityType type)
        {
            switch (type)
            {
                case EntityType.Class:
                    return Language.CreateClass(Project.GetName(Package, ProjectItemType.Type, "Class"));

                case EntityType.Delegate:
                    return Language.CreateDelegate(Project.GetName(Package, ProjectItemType.Type, "Delegate"));

                case EntityType.Enum:
                    return Language.CreateEnum(Project.GetName(Package, ProjectItemType.Type, "Enum"));

                case EntityType.Interface:
                    return Language.CreateInterface(Project.GetName(Package, ProjectItemType.Type, "Interface"));

                case EntityType.Structure:
                    return Language.CreateStructure(Project.GetName(Package, ProjectItemType.Type, "Structure"));

                case EntityType.Comment:
                    return new Comment();

                default:
                    throw new NotSupportedException(type.ToString());
            }
        }

        private Shape AddShape(IEntity entity)
        {
            switch (entity.EntityType)
            {
                case EntityType.Class:
                    var classType = (ClassType)entity;
                    AddShape(new ClassShape(classType));
                    if (!Loading)
                    {
                        //Generalization
                        if (classType.HasExplicitBase && Entities.Contains(classType.BaseClass))
                            InsertGeneralization(classType.GeneralizationRelationship);
                        foreach (var t in classType.Children)
                            if (Entities.Contains(t))
                                InsertGeneralization(t.GeneralizationRelationship);

                        //Interfaces
                        foreach (var i in classType.Interfaces)
                            if (Entities.Contains(i))
                                AddRealization(classType, i);

                        //Nesting
                        if (classType.IsNested && Entities.Contains(classType.NestingParent))
                            InsertNesting(classType.NestingRelationship);
                        foreach (var i in classType.NestedChildren)
                            if (Entities.Contains(i))
                                InsertNesting(i.NestingRelationship);

                        //Association
                        foreach (var r in classType.AssociationRelationships)
                            if (Entities.Contains(r.Second))
                                InsertAssociation(r);
                        foreach (var a in classType.Associations)
                            if (Entities.Contains(a))
                                AddAssociation(a, classType);
                    }
                    break;

                case EntityType.Delegate:
                    var delegateType = (DelegateType)entity;
                    AddShape(new DelegateShape(delegateType));
                    if (!Loading)
                    {
                        //Nesting
                        if (delegateType.IsNested && Entities.Contains(delegateType.NestingParent))
                            InsertNesting(delegateType.NestingRelationship);

                        //Association
                        foreach (var r in delegateType.AssociationRelationships)
                            if (Entities.Contains(r.Second))
                                InsertAssociation(r);
                        foreach (var a in delegateType.Associations)
                            if (Entities.Contains(a))
                                AddAssociation(a, delegateType);
                    }
                    break;

                case EntityType.Enum:
                    var enumType = (EnumType)entity;
                    AddShape(new EnumShape(enumType));
                    if (!Loading)
                    {
                        //Nesting
                        if (enumType.IsNested && Entities.Contains(enumType.NestingParent))
                            InsertNesting(enumType.NestingRelationship);

                        //Association
                        foreach (var r in enumType.AssociationRelationships)
                            if (Entities.Contains(r.Second))
                                InsertAssociation(r);
                        foreach (var a in enumType.Associations)
                            if (Entities.Contains(a))
                                AddAssociation(a, enumType);
                    }
                    break;

                case EntityType.Interface:
                    var interfaceType = (InterfaceType)entity;
                    AddShape(new InterfaceShape(interfaceType));
                    if (!Loading)
                    {
                        //Generalization
                        foreach (var b in interfaceType.Bases)
                            if (Entities.Contains(b))
                                AddGeneralization(interfaceType, b);
                        foreach (var c in interfaceType.Children)
                            if (Entities.Contains(c))
                                foreach (var t in c.GeneralizationRelationships)
                                    if (t.Second == interfaceType)
                                        InsertGeneralization(t);

                        //Realisztion
                        foreach (var b in interfaceType.Realisztions)
                            if (Entities.Contains(b))
                                AddRealization(b, interfaceType);

                        //Nesting
                        if (interfaceType.IsNested && Entities.Contains(interfaceType.NestingParent))
                            InsertNesting(interfaceType.NestingRelationship);

                        //Association
                        foreach (var r in interfaceType.AssociationRelationships)
                            if (Entities.Contains(r.Second))
                                InsertAssociation(r);
                        foreach (var a in interfaceType.Associations)
                            if (Entities.Contains(a))
                                AddAssociation(a, interfaceType);
                    }
                    break;

                case EntityType.Structure:
                    var structureType = (StructureType)entity;
                    AddShape(new StructureShape(structureType));
                    if (!Loading)
                    {
                        //Realization
                        foreach (var i in structureType.Interfaces)
                            if (Entities.Contains(i))
                                AddRealization(structureType, i);

                        //Nesting
                        if (structureType.IsNested && Entities.Contains(structureType.NestingParent))
                            InsertNesting(structureType.NestingRelationship);
                        foreach (var i in structureType.NestedChildren)
                            if (Entities.Contains(i))
                                InsertNesting(i.NestingRelationship);

                        //Association
                        foreach (var r in structureType.AssociationRelationships)
                            if (Entities.Contains(r.Second))
                                InsertAssociation(r);
                        foreach (var a in structureType.Associations)
                            if (Entities.Contains(a))
                                AddAssociation(a, structureType);
                    }
                    break;

                case EntityType.Comment:
                    AddShape(new CommentShape((Comment)entity));
                    break;
            }
            return shapes.FirstValue;
        }

        #endregion

        #region Add Entity

        public bool InsertClass(ClassType newClass)
        {
            if (newClass != null && !Entities.Contains(newClass) && newClass.Language == Language)
            {
                if (!Project.CheckName(Package, newClass.ItemType, newClass.Name))
                    newClass.Name = Project.GetName(Package, newClass.ItemType, newClass.Name);
                AddShape(newClass);
                return true;
            }
            return false;
        }

        public bool InsertStructure(StructureType newStructure)
        {
            if (newStructure != null && !Entities.Contains(newStructure) &&
                newStructure.Language == Language)
            {
                if (!Project.CheckName(Package, newStructure.ItemType, newStructure.Name))
                    newStructure.Name = Project.GetName(Package, newStructure.ItemType, newStructure.Name);
                AddShape(newStructure);
                return true;
            }
            return false;
        }

        public bool InsertInterface(InterfaceType newInterface)
        {
            if (newInterface != null && !Entities.Contains(newInterface) &&
                newInterface.Language == Language)
            {
                if (!Project.CheckName(Package, newInterface.ItemType, newInterface.Name))
                    newInterface.Name = Project.GetName(Package, newInterface.ItemType, newInterface.Name);
                AddShape(newInterface);
                return true;
            }
            return false;
        }

        public bool InsertEnum(EnumType newEnum)
        {
            if (newEnum != null && !Entities.Contains(newEnum) &&
                newEnum.Language == Language)
            {
                if (!Project.CheckName(Package, newEnum.ItemType, newEnum.Name))
                    newEnum.Name = Project.GetName(Package, newEnum.ItemType, newEnum.Name);
                AddShape(newEnum);
                return true;
            }
            return false;
        }

        public bool InsertDelegate(DelegateType newDelegate)
        {
            if (newDelegate != null && !Entities.Contains(newDelegate) &&
                newDelegate.Language == Language)
            {
                if (!Project.CheckName(Package, newDelegate.ItemType, newDelegate.Name))
                    newDelegate.Name = Project.GetName(Package, newDelegate.ItemType, newDelegate.Name);
                AddShape(newDelegate);
                return true;
            }
            return false;
        }

        public bool InsertComment(Comment comment)
        {
            if (comment != null && !Entities.Contains(comment))
            {
                AddShape(comment);
                return true;
            }
            return false;
        }

        #endregion

        #region Add Relationship

        internal Association AddAssociation(TypeBase first, TypeBase second)
        {
            foreach (var r in first.AssociationRelationships)
                if (r.Second == second && !Relationships.Contains(r))
                    return AddAssociation(r);
            return AddAssociation(new AssociationRelationship(first, second));
        }

        public bool InsertAssociation(AssociationRelationship associaton)
        {
            if (associaton != null && !Relationships.Contains(associaton) &&
                Entities.Contains(associaton.First) && Entities.Contains(associaton.Second))
            {
                AddAssociation(associaton);
                return true;
            }
            return false;
        }

        internal Association AddComposition(TypeBase first, TypeBase second)
        {
            foreach (var r in first.AssociationRelationships)
                if (r.AssociationType == AssociationType.Composition
                    && r.Second == second && !Relationships.Contains(r))
                    return AddAssociation(r);
            return AddAssociation(new AssociationRelationship(first, second, AssociationType.Composition));
        }

        internal Association AddAggregation(TypeBase first, TypeBase second)
        {
            foreach (var r in first.AssociationRelationships)
                if (r.AssociationType == AssociationType.Aggregation
                    && r.Second == second && !Relationships.Contains(r))
                    return AddAssociation(r);
            return AddAssociation(new AssociationRelationship(first, second, AssociationType.Aggregation));
        }

        Association AddAssociation(AssociationRelationship association)
        {
            AddRelationship(association);

            Shape startShape = GetShape(association.First);
            Shape endShape = GetShape(association.Second);
            return AddConnection(new Association(association, startShape, endShape));
        }

        internal Generalization AddGeneralization(CompositeType derivedType, CompositeType baseType)
        {
            if (derivedType.HasExplicitBase)
            {
                if (derivedType is SingleInharitanceType)
                {
                    var s = derivedType as SingleInharitanceType;
                    if (s.GeneralizationRelationship.Second == baseType && !Relationships.Contains(s.GeneralizationRelationship))
                    {
                        return AddGeneralization(s.GeneralizationRelationship);
                    }
                }
                else if (derivedType is InterfaceType)
                {
                    var i = derivedType as InterfaceType;
                    foreach (var g in i.GeneralizationRelationships)
                        if (g.Second == baseType && (!Relationships.Contains(g)))
                        {
                            return AddGeneralization(g);
                        }
                }
            }
            return AddGeneralization(new GeneralizationRelationship(derivedType, baseType));
        }

        public bool InsertGeneralization(GeneralizationRelationship generalization)
        {
            if (generalization != null && !Relationships.Contains(generalization) &&
                Entities.Contains(generalization.First) && Entities.Contains(generalization.Second))
            {
                AddGeneralization(generalization);
                return true;
            }
            return false;
        }

        Generalization AddGeneralization(GeneralizationRelationship generalization)
        {
            AddRelationship(generalization);

            Shape startShape = GetShape(generalization.First);
            Shape endShape = GetShape(generalization.Second);
            return AddConnection(new Generalization(generalization, startShape, endShape));
        }

        internal Realization AddRealization(TypeBase implementer, InterfaceType baseType)
        {
            if (implementer is IInterfaceImplementer)
            {
                var i = implementer as IInterfaceImplementer;
                foreach (var r in i.RealizationRelationships)
                    if (r.Second == baseType && (!Relationships.Contains(r)))
                    {
                        return AddRealization(r);
                    }
            }
            return AddRealization(new RealizationRelationship(implementer, baseType));
        }

        public bool InsertRealization(RealizationRelationship realization)
        {
            if (realization != null && !Relationships.Contains(realization) &&
                Entities.Contains(realization.First) && Entities.Contains(realization.Second))
            {
                AddRealization(realization);
                return true;
            }
            return false;
        }

        Realization AddRealization(RealizationRelationship realization)
        {
            AddRelationship(realization);

            Shape startShape = GetShape(realization.First);
            Shape endShape = GetShape(realization.Second);
            return AddConnection(new Realization(realization, startShape, endShape));
        }

        internal Dependency AddDependency(TypeBase first, TypeBase second)
        {
            return AddDependency(new DependencyRelationship(first, second));
        }

        public bool InsertDependency(DependencyRelationship dependency)
        {
            if (dependency != null && !Relationships.Contains(dependency) &&
                Entities.Contains(dependency.First) && Entities.Contains(dependency.Second))
            {
                AddDependency(dependency);
                return true;
            }
            return false;
        }

        Dependency AddDependency(DependencyRelationship dependency)
        {
            AddRelationship(dependency);

            Shape startShape = GetShape(dependency.First);
            Shape endShape = GetShape(dependency.Second);
            return AddConnection(new Dependency(dependency, startShape, endShape));
        }

        internal Nesting AddNesting(CompositeType parentType, TypeBase innerType)
        {
            if (innerType.IsNested)
            {
                if (innerType.NestingRelationship.First == parentType
                    && !Relationships.Contains(innerType.NestingRelationship))
                {
                    return AddNesting(innerType.NestingRelationship);
                }
            }
            return AddNesting(new NestingRelationship(parentType, innerType));
        }

        public bool InsertNesting(NestingRelationship nesting)
        {
            if (nesting != null && !Relationships.Contains(nesting) &&
                Entities.Contains(nesting.First) && Entities.Contains(nesting.Second))
            {
                AddNesting(nesting);
                return true;
            }
            return false;
        }

        Nesting AddNesting(NestingRelationship nesting)
        {
            AddRelationship(nesting);

            Shape startShape = GetShape(nesting.First);
            Shape endShape = GetShape(nesting.Second);
            return AddConnection(new Nesting(nesting, startShape, endShape));
        }

        internal virtual CommentConnection AddCommentRelationship(Comment comment, IEntity entity)
        {
            return AddCommentRelationship(new CommentRelationship(comment, entity));
        }

        public bool InsertCommentRelationship(CommentRelationship commentRelationship)
        {
            if (commentRelationship != null && !Relationships.Contains(commentRelationship) &&
                Entities.Contains(commentRelationship.First) && Entities.Contains(commentRelationship.Second))
            {
                AddCommentRelationship(commentRelationship);
                return true;
            }
            return false;
        }

        CommentConnection AddCommentRelationship(CommentRelationship commentRelationship)
        {
            AddRelationship(commentRelationship);

            Shape startShape = GetShape(commentRelationship.First);
            Shape endShape = GetShape(commentRelationship.Second);
            return AddConnection(new CommentConnection(commentRelationship, startShape, endShape));
        }

        private T AddConnection<T>(T connection) where T : Connection
        {
            connection.Diagram = this;
            connection.Modified += new EventHandler(element_Modified);
            connection.Activating += new EventHandler(element_Activating);
            connection.SelectionChanged += new EventHandler(connection_SelectionChanged);
            connection.RouteChanged += new EventHandler(connection_RouteChanged);
            connection.BendPointMove += new BendPointEventHandler(connection_BendPointMove);
            connections.AddFirst(connection);
            RecalculateSize();
            return connection;
        }

        #endregion

        #region OnPropertyChanged

        protected override void OnEntityRemoved(EntityEventArgs e)
        {
            Shape shape = GetShape(e.Entity);
            RemoveShape(shape);

            base.OnEntityRemoved(e);
        }

        protected override void OnRelationRemoved(RelationshipEventArgs e)
        {
            Connection connection = GetConnection(e.Relationship);
            RemoveConnection(connection);

            base.OnRelationRemoved(e);
        }

        protected virtual void OnOffsetChanged(EventArgs e)
        {
            if (OffsetChanged != null)
                OffsetChanged(this, e);
            UpdateWindowPosition();
        }

        protected virtual void OnSizeChanged(EventArgs e)
        {
            if (SizeChanged != null)
                SizeChanged(this, e);
        }

        protected virtual void OnZoomChanged(EventArgs e)
        {
            if (ZoomChanged != null)
                ZoomChanged(this, e);
            CloseWindows();
        }

        protected virtual void OnSatusChanged(EventArgs e)
        {
            if (StatusChanged != null)
                StatusChanged(this, e);
        }

        protected virtual void OnSelectionChanged(EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        protected virtual void OnNeedsRedraw(EventArgs e)
        {
            if (NeedsRedraw != null)
                NeedsRedraw(this, e);
        }

        protected virtual void OnEditStateChanged(EventArgs e)
        {
            if (EditStateChanged != null)
                EditStateChanged(this, e);
        }

        protected virtual void OnShowingWindow(PopupWindowEventArgs e)
        {
            if (ShowingWindow != null)
                ShowingWindow(this, e);
        }

        protected virtual void OnHidingWindow(PopupWindowEventArgs e)
        {
            if (HidingWindow != null)
                HidingWindow(this, e);
        }

        #endregion

        #region Save & Load

        protected override void SaveEntitites(XmlElement node)
        {
            if (node == null)
                throw new ArgumentNullException("root");

            XmlElement entitiesChild = node.OwnerDocument.CreateElement("Entities");

            foreach (Shape entity in Shapes)
            {
                XmlElement child = node.OwnerDocument.CreateElement("Entity");
                ((ISerializableElement)entity).Serialize(child);
                child.SetAttribute("type", entity.Entity.EntityType.ToString());
                if (entity.BackColor != Color.Empty)
                    child.SetAttribute("backColor", entity.BackColor.ToArgb().ToString());
                if (entity.ForeColor != Color.Empty)
                    child.SetAttribute("foreColor", entity.ForeColor.ToArgb().ToString());
                entitiesChild.AppendChild(child);
            }
            node.AppendChild(entitiesChild);
        }

        protected override void LoadEntitites(XmlNode root)
        {
            if (root == null)
                throw new ArgumentNullException("root");

            XmlNodeList nodeList = root.SelectNodes("Entities/Entity");
            shapes.Clear();
            foreach (XmlElement node in nodeList)
            {
                try
                {
                    IEntity entity = null;
                    string type = node.GetAttribute("type");
                    if (type == "Comment")
                    {
                        entity = new Comment();
                        entity.Deserialize(node);
                    }
                    else
                    {
                        Guid id = node["Id"].GetValue(Guid.Empty);
                        entity = ProjectInfo.Entities.FirstOrDefault(p => p.Id == id);
                        if (entity == null)
                            throw new AppException("Entity of ID[{0}] not found.".FormatArgs(id));
                    }
                    var shape = AddShape(entity);
                    ((ISerializableElement)shape).Deserialize(node);
                    var backColor = node.GetAttributeValue<int?>("backColor", null);
                    shape.BackColor = backColor.HasValue ? Color.FromArgb(backColor.Value) : Color.Empty;
                    var foreColor = node.GetAttributeValue<int?>("foreColor", null);
                    shape.ForeColor = foreColor.HasValue ? Color.FromArgb(foreColor.Value) : Color.Empty;
                }
                catch (BadSyntaxException ex)
                {
                    throw new AppException("Invalid entity.", ex);
                }
            }
        }

        protected override void SaveRelationships(XmlNode root)
        {
            if (root == null)
                throw new ArgumentNullException("root");

            XmlElement relationsChild = root.OwnerDocument.CreateElement("Relationships");

            foreach (var connection in Connections)
            {
                XmlElement child = root.OwnerDocument.CreateElement("Relationship");

                connection.Relationship.Serialize(child);
                ((ISerializableElement)connection).Serialize(child);
                child.SetAttribute("type", connection.Relationship.RelationshipType.ToString());
                child.SetAttribute("first", connection.Relationship.First.Id.ToString());
                child.SetAttribute("second", connection.Relationship.Second.Id.ToString());
                relationsChild.AppendChild(child);
            }
            root.AppendChild(relationsChild);
        }

        protected override void LoadRelationships(XmlNode root)
        {
            if (root == null)
                throw new ArgumentNullException("root");

            XmlNodeList nodeList = root.SelectNodes("Relationships/Relationship");
            connections.Clear();
            foreach (XmlElement node in nodeList)
            {
                string type = node.GetAttribute("type");
                var firstId = node.GetAttribute("first").ConvertTo<Guid>();
                var secondId = node.GetAttribute("second").ConvertTo<Guid>();

                try
                {
                    IEntity first = Entities.First(p => p.Id == firstId);
                    IEntity second = Entities.First(p => p.Id == secondId);
                    Connection connection;

                    switch (type)
                    {
                        case "Association":
                            connection = AddAssociation(first as TypeBase, second as TypeBase);
                            break;

                        case "Generalization":
                            connection = AddGeneralization(first as CompositeType, second as CompositeType);
                            break;

                        case "Realization":
                            connection = AddRealization(first as TypeBase, second as InterfaceType);
                            break;

                        case "Dependency":
                            connection = AddDependency(first as TypeBase, second as TypeBase);
                            break;

                        case "Nesting":
                            connection = AddNesting(first as CompositeType, second as TypeBase);
                            break;

                        case "Comment":
                            if (first is Comment)
                                connection = AddCommentRelationship(first as Comment, second);
                            else
                                connection = AddCommentRelationship(second as Comment, first);
                            break;

                        default:
                            throw new InvalidException(Strings.ErrorCorruptSaveFormat);
                    }
                    connection.Relationship.Deserialize(node);
                    ((ISerializableElement)connection).Deserialize(node);
                }
                catch (ArgumentNullException ex)
                {
                    throw new InvalidException("Invalid relationship.", ex);
                }
                catch (RelationshipException ex)
                {
                    throw new InvalidException("Invalid relationship.", ex);
                }
            }
        }

        #endregion

        #region Private Methods

        private void DeselectAllOthers(DiagramElement onlySelected)
        {
            foreach (Shape shape in shapes)
            {
                if (shape != onlySelected)
                {
                    shape.IsSelected = false;
                    shape.IsActive = false;
                }
            }
            foreach (Connection connection in connections)
            {
                if (connection != onlySelected)
                {
                    connection.IsSelected = false;
                    connection.IsActive = false;
                }
            }
        }

        private bool ConfirmDelete()
        {
            DialogResult result = Client.ShowConfirm(Strings.DeleteElementsConfirmation);
            return result == DialogResult.OK;
        }

        private void RequestRedrawIfNeeded()
        {
            if (Loading)
                return;

            foreach (Shape shape in shapes)
            {
                if (shape.NeedsRedraw)
                {
                    OnNeedsRedraw(EventArgs.Empty);
                    return;
                }
            }
            foreach (Connection connection in connections)
            {
                if (connection.NeedsRedraw)
                {
                    OnNeedsRedraw(EventArgs.Empty);
                    return;
                }
            }
        }

        private void connection_BendPointMove(object sender, BendPointEventArgs e)
        {
            if (e.BendPoint.X < DiagramPadding)
                e.BendPoint.X = DiagramPadding;
            if (e.BendPoint.Y < DiagramPadding)
                e.BendPoint.Y = DiagramPadding;

            // Snap bend points to others if possible
            if (Settings.Default.UsePrecisionSnapping && Control.ModifierKeys != Keys.Shift)
            {
                foreach (Connection connection in connections.GetSelectedElements())
                {
                    foreach (BendPoint point in connection.BendPoints)
                    {
                        if (point != e.BendPoint && !point.AutoPosition)
                        {
                            int xDist = Math.Abs(e.BendPoint.X - point.X);
                            int yDist = Math.Abs(e.BendPoint.Y - point.Y);

                            if (xDist <= Connection.PrecisionSize)
                            {
                                e.BendPoint.X = point.X;
                            }
                            if (yDist <= Connection.PrecisionSize)
                            {
                                e.BendPoint.Y = point.Y;
                            }
                        }
                    }
                }
            }
        }

        private void connection_RouteChanged(object sender, EventArgs e)
        {
            Connection connection = (Connection)sender;
            connection.ValidatePosition(DiagramPadding);

            RecalculateSize();
        }

        private void connection_SelectionChanged(object sender, EventArgs e)
        {
            if (!selectioning)
            {
                Connection connection = (Connection)sender;

                if (connection.IsSelected)
                {
                    selectedConnectionCount++;
                    connections.ShiftToFirstPlace(connection);
                }
                else
                {
                    selectedConnectionCount--;
                }

                OnSelectionChanged(EventArgs.Empty);
                OnEditStateChanged(EventArgs.Empty);
                OnSatusChanged(EventArgs.Empty);
            }
        }

        private void shape_SelectionChanged(object sender, EventArgs e)
        {
            if (!selectioning)
            {
                Shape shape = (Shape)sender;

                if (shape.IsSelected)
                {
                    selectedShapeCount++;
                    shapes.ShiftToFirstPlace(shape);
                }
                else
                {
                    selectedShapeCount--;
                }

                OnSelectionChanged(EventArgs.Empty);
                OnEditStateChanged(EventArgs.Empty);
                OnSatusChanged(EventArgs.Empty);
            }
        }

        private void UpdateWindowPosition()
        {
            if (ActiveElement != null)
                ActiveElement.MoveWindow();
        }

        internal void ShowWindow(PopupWindow window)
        {
            Redraw();
            OnShowingWindow(new PopupWindowEventArgs(window));
        }

        internal void HideWindow(PopupWindow window)
        {
            window.Closing();
            OnHidingWindow(new PopupWindowEventArgs(window));
        }

        private void element_Modified(object sender, EventArgs e)
        {
            if (!RedrawSuspended)
                RequestRedrawIfNeeded();
            OnModified(EventArgs.Empty);
        }

        private void element_Activating(object sender, EventArgs e)
        {
            foreach (Shape shape in shapes)
            {
                if (shape != sender)
                    shape.IsActive = false;
            }
            foreach (Connection connection in connections)
            {
                if (connection != sender)
                    connection.IsActive = false;
            }
            ActiveElement = (DiagramElement)sender;
        }

        private void shape_Dragging(object sender, MoveEventArgs e)
        {
            Size offset = e.Offset;

            // Align to other shapes
            if (Settings.Default.UsePrecisionSnapping && Control.ModifierKeys != Keys.Shift)
            {
                Shape shape = (Shape)sender;

                foreach (Shape otherShape in shapes.GetUnselectedElements())
                {
                    int xDist = otherShape.X - (shape.X + offset.Width);
                    int yDist = otherShape.Y - (shape.Y + offset.Height);

                    if (Math.Abs(xDist) <= PrecisionSize)
                    {
                        int distance1 = Math.Abs(shape.Top - otherShape.Bottom);
                        int distance2 = Math.Abs(otherShape.Top - shape.Bottom);
                        int distance = Math.Min(distance1, distance2);

                        if (distance <= MaximalPrecisionDistance)
                            offset.Width += xDist;
                    }
                    if (Math.Abs(yDist) <= PrecisionSize)
                    {
                        int distance1 = Math.Abs(shape.Left - otherShape.Right);
                        int distance2 = Math.Abs(otherShape.Left - shape.Right);
                        int distance = Math.Min(distance1, distance2);

                        if (distance <= MaximalPrecisionDistance)
                            offset.Height += yDist;
                    }
                }
            }

            // Get maxmimal avaiable offset for the selected elements
            foreach (Shape shape in shapes)
            {
                offset = shape.GetMaximalOffset(offset, DiagramPadding);
            }
            foreach (Connection connection in connections)
            {
                offset = connection.GetMaximalOffset(offset, DiagramPadding);
            }
            if (!offset.IsEmpty)
            {
                foreach (Shape shape in shapes.GetSelectedElements())
                {
                    shape.Offset(offset);
                }
                foreach (Connection connection in connections.GetSelectedElements())
                {
                    connection.Offset(offset);
                }
            }
            RecalculateSize();
        }

        private void shape_Resizing(object sender, ResizeEventArgs e)
        {
            if (Settings.Default.UsePrecisionSnapping && Control.ModifierKeys != Keys.Shift)
            {
                Shape shape = (Shape)sender;
                Size change = e.Change;

                // Horizontal resizing
                if (change.Width != 0)
                {
                    foreach (Shape otherShape in shapes.GetUnselectedElements())
                    {
                        if (otherShape != shape)
                        {
                            int xDist = otherShape.Right - (shape.Right + change.Width);
                            if (Math.Abs(xDist) <= PrecisionSize)
                            {
                                int distance1 = Math.Abs(shape.Top - otherShape.Bottom);
                                int distance2 = Math.Abs(otherShape.Top - shape.Bottom);
                                int distance = Math.Min(distance1, distance2);

                                if (distance <= MaximalPrecisionDistance)
                                {
                                    change.Width += xDist;
                                    break;
                                }
                            }
                        }
                    }
                }

                // Vertical resizing
                if (change.Height != 0)
                {
                    foreach (Shape otherShape in shapes.GetUnselectedElements())
                    {
                        if (otherShape != shape)
                        {
                            int yDist = otherShape.Bottom - (shape.Bottom + change.Height);
                            if (Math.Abs(yDist) <= PrecisionSize)
                            {
                                int distance1 = Math.Abs(shape.Left - otherShape.Right);
                                int distance2 = Math.Abs(otherShape.Left - shape.Right);
                                int distance = Math.Min(distance1, distance2);

                                if (distance <= MaximalPrecisionDistance)
                                {
                                    change.Height += yDist;
                                    break;
                                }
                            }
                        }
                    }
                }

                e.Change = change;
            }
        }

        private void RemoveShape(Shape shape)
        {
            if (shape.IsActive)
            {
                ActiveElement = null;
            }
            if (shape.IsSelected)
            {
                selectedShapeCount--;
                OnSelectionChanged(EventArgs.Empty);
                OnEditStateChanged(EventArgs.Empty);
                OnSatusChanged(EventArgs.Empty);
            }
            shape.Diagram = null;
            shape.Modified -= new EventHandler(element_Modified);
            shape.Activating -= new EventHandler(element_Activating);
            shape.Dragging -= new MoveEventHandler(shape_Dragging);
            shape.Resizing -= new ResizeEventHandler(shape_Resizing);
            shape.SelectionChanged -= new EventHandler(shape_SelectionChanged);
            shapes.Remove(shape);
            shape.OnRemoved();
            RecalculateSize();
        }

        //TODO: 
        private Shape GetShape(IEntity entity)
        {
            foreach (Shape shape in shapes)
            {
                if (shape.Entity == entity)
                    return shape;
            }
            return null;
        }

        private Connection GetConnection(Relationship relationship)
        {
            foreach (Connection connection in connections)
            {
                if (connection.Relationship == relationship)
                    return connection;
            }
            return null;
        }

        private void RemoveConnection(Connection connection)
        {
            if (connection == null) return;
            if (connection.IsSelected)
            {
                selectedConnectionCount--;
                OnSelectionChanged(EventArgs.Empty);
                OnEditStateChanged(EventArgs.Empty);
                OnSatusChanged(EventArgs.Empty);
            }
            connection.Diagram = null;
            connection.Modified -= new EventHandler(element_Modified);
            connection.Activating += new EventHandler(element_Activating);
            connection.SelectionChanged -= new EventHandler(connection_SelectionChanged);
            connection.RouteChanged -= new EventHandler(connection_RouteChanged);
            connection.BendPointMove -= new BendPointEventHandler(connection_BendPointMove);
            connections.Remove(connection);
            RecalculateSize();
        }

        private IEnumerable<DiagramElement> GetElementsInDisplayOrder()
        {
            foreach (Shape shape in shapes.GetSelectedElements())
                yield return shape;

            foreach (Connection connection in connections.GetSelectedElements())
                yield return connection;

            foreach (Connection connection in connections.GetUnselectedElements())
                yield return connection;

            foreach (Shape shape in shapes.GetUnselectedElements())
                yield return shape;
        }

        private IEnumerable<DiagramElement> GetElementsInReversedDisplayOrder()
        {
            foreach (Shape shape in shapes.GetUnselectedElementsReversed())
                yield return shape;

            foreach (Connection connection in connections.GetUnselectedElementsReversed())
                yield return connection;

            foreach (Connection connection in connections.GetSelectedElementsReversed())
                yield return connection;

            foreach (Shape shape in shapes.GetSelectedElementsReversed())
                yield return shape;
        }

        //protected override void OnClosing(EventArgs e)
        //{
        //    base.OnClosing(e);
        //    Project.Saved -= new EventHandler(value_Saved);
        //}

        //public override ProjectInfo Project
        //{
        //    get { return base.Project; }
        //    set
        //    {
        //        base.Project = value;
        //        value.Saved += new EventHandler(value_Saved);
        //    }
        //}

        //void value_Saved(object sender, EventArgs e)
        //{
        //    saved = editStack.Serialize(this);
        //}

        #endregion
    }
}