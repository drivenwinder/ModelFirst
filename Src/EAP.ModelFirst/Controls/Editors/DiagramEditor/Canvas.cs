using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Controls.Editors.FloatingEditors;
using EAP.ModelFirst.Controls.ProjectView;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Relationships;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor
{
    public partial class Canvas : UserControl, IDocumentVisualizer
    {
        public const float MinZoom = 0.1F;
        public const float MaxZoom = 4.0F;
        const int DiagramPadding = 10;

        static Pen borderPen;

        static Canvas()
        {
            borderPen = new Pen(Color.FromArgb(128, Color.Black));
            borderPen.DashPattern = new float[] { 5, 5 };
        }

        public event EventHandler ZoomChanged;
        public event EventHandler DocumentRedrawed;
        public event EventHandler VisibleAreaChanged;
        public event EventHandler MouseHWheel;

        IDiagram diagram = null;
        List<PopupWindow> windows = new List<PopupWindow>();

        public Canvas()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
        }

        [Browsable(false)]
        public bool HasDocument
        {
            get { return diagram != null; }
        }

        [Browsable(false)]
        public IDiagram Diagram
        {
            get
            {
                return diagram;
            }
            set
            {
                if (diagram != value)
                {
                    if (diagram != null)
                    {
                        diagram.CloseWindows();
                        diagram.OffsetChanged -= new EventHandler(document_OffsetChanged);
                        diagram.SizeChanged -= new EventHandler(document_SizeChanged);
                        diagram.ZoomChanged -= new EventHandler(document_ZoomChanged);
                        diagram.NeedsRedraw -= new EventHandler(document_NeedsRedraw);
                        diagram.ShowingWindow -= new PopupWindowEventHandler(document_ShowingWindow);
                        diagram.HidingWindow -= new PopupWindowEventHandler(document_HidingWindow);
                        //diagram.Modified -= new EventHandler(diagram_Modified);
                        diagram.EndEdit();
                    }
                    diagram = value;

                    if (diagram != null)
                    {
                        diagram.OffsetChanged += new EventHandler(document_OffsetChanged);
                        diagram.SizeChanged += new EventHandler(document_SizeChanged);
                        diagram.ZoomChanged += new EventHandler(document_ZoomChanged);
                        diagram.NeedsRedraw += new EventHandler(document_NeedsRedraw);
                        diagram.ShowingWindow += new PopupWindowEventHandler(document_ShowingWindow);
                        diagram.HidingWindow += new PopupWindowEventHandler(document_HidingWindow);
                        //diagram.Modified += new EventHandler(diagram_Modified);
                        diagram.BeginEdit();
                    }

                    SetScrolls();
                    Invalidate();

                    OnDocumentRedrawed(EventArgs.Empty);
                    OnZoomChanged(EventArgs.Empty);
                    OnVisibleAreaChanged(EventArgs.Empty);
                }
            }
        }

        //void diagram_Modified(object sender, EventArgs e)
        //{
        //    if (!Focused && !ContainsFocus)
        //        diagram.BeginEdit();
        //    else
        //        diagram.AcceptEdit();
        //}

        public override Color BackColor
        {
            get
            {
                if (HasDocument)
                    return Diagram.BackColor;
                else
                    return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        [Browsable(false)]
        public Point Offset
        {
            get
            {
                return new Point(HorizontalScroll.Value, VerticalScroll.Value);
            }
            set
            {
                AutoScrollPosition = value;
                UpdateDocumentOffset();
            }
        }

        Size IDocumentVisualizer.DocumentSize
        {
            get
            {
                if (HasDocument)
                    return Diagram.Size;
                else
                    return Size.Empty;
            }
        }

        [Browsable(false)]
        public Rectangle VisibleArea
        {
            get
            {
                if (HasDocument)
                {
                    return new Rectangle(
                        (int)(Diagram.Offset.X / Diagram.Zoom),
                        (int)(Diagram.Offset.Y / Diagram.Zoom),
                        (int)(this.ClientSize.Width / Diagram.Zoom),
                        (int)(this.ClientSize.Height / Diagram.Zoom)
                    );
                }
                else
                {
                    return Rectangle.Empty;
                }
            }
        }

        [Browsable(false)]
        public float Zoom
        {
            get
            {
                if (HasDocument)
                    return Diagram.Zoom;
                else
                    return 1.0F;
            }
            set
            {
                ChangeZoom(value);
            }
        }

        [Browsable(false)]
        public int ZoomPercentage
        {
            get
            {
                if (HasDocument)
                    return (int)Math.Round(Diagram.Zoom * 100);
                else
                    return 100;
            }
        }

        private void document_OffsetChanged(object sender, EventArgs e)
        {
            SetScrolls();
            OnVisibleAreaChanged(EventArgs.Empty);
        }

        private void document_SizeChanged(object sender, EventArgs e)
        {
            SetScrolls();
            OnVisibleAreaChanged(EventArgs.Empty);
        }

        private void document_ZoomChanged(object sender, EventArgs e)
        {
            Invalidate();
            SetScrolls();
            OnZoomChanged(EventArgs.Empty);
            OnVisibleAreaChanged(EventArgs.Empty);
        }

        private void document_NeedsRedraw(object sender, EventArgs e)
        {
            Invalidate();
            OnDocumentRedrawed(EventArgs.Empty);
        }

        private void document_ShowingWindow(object sender, PopupWindowEventArgs e)
        {
            PopupWindow window = e.Window;
            if (!windows.Contains(window))
            {
                windows.Add(window);
                if (ParentForm != null)
                {
                    ParentForm.Controls.Add(window);
                    Point point = this.PointToScreen(Point.Empty);
                    Point absPos = ParentForm.PointToClient(point);
                    window.ParentLocation = absPos;
                    window.BringToFront();
                }
            }
        }

        private void document_HidingWindow(object sender, PopupWindowEventArgs e)
        {
            PopupWindow window = e.Window;
            if (windows.Contains(window))
            {
                windows.Remove(window);
                if (ParentForm != null)
                {
                    ParentForm.Controls.Remove(window);
                }
            }
        }

        private PointF GetAbsoluteCenterPoint()
        {
            return ConvertRelativeToAbsolute(new Point(Width / 2, Height / 2));
        }

        private PointF ConvertRelativeToAbsolute(Point location)
        {
            return new PointF(
                (location.X + Offset.X) / Zoom,
                (location.Y + Offset.Y) / Zoom
            );
        }

        private Point ConvertAbsoluteToRelative(PointF location)
        {
            return new Point(
                (int)(location.X * Zoom - Offset.X),
                (int)(location.Y * Zoom - Offset.Y)
            );
        }

        public void ZoomIn()
        {
            ChangeZoom(true);
        }

        public void ZoomOut()
        {
            ChangeZoom(false);
        }

        public void ChangeZoom(bool enlarge)
        {
            if (HasDocument)
            {
                if (Diagram.HasSelectedElement)
                    ChangeZoom(enlarge, Diagram.GetPrintingArea(true));
                else
                    ChangeZoom(enlarge, GetAbsoluteCenterPoint());
            }
        }

        public void ChangeZoom(bool enlarge, PointF zoomingCenter)
        {
            if (HasDocument)
            {
                float zoomValue = CalculateZoomValue(enlarge);
                ChangeZoom(zoomValue, zoomingCenter);
            }
        }

        private void ChangeZoom(bool enlarge, RectangleF zoomingCenter)
        {
            if (HasDocument)
            {
                float zoomValue = CalculateZoomValue(enlarge);
                ChangeZoom(zoomValue, zoomingCenter);
            }
        }

        private float CalculateZoomValue(bool enlarge)
        {
            if (enlarge)
                return ((int)Math.Round(Diagram.Zoom * 100) + 10) / 10 / 10F;
            else
                return ((int)Math.Round(Diagram.Zoom * 100) - 1) / 10 / 10F;
        }

        public void ChangeZoom(float zoom)
        {
            if (HasDocument)
            {
                if (Diagram.HasSelectedElement)
                    ChangeZoom(zoom, Diagram.GetPrintingArea(true));
                else
                    ChangeZoom(zoom, GetAbsoluteCenterPoint());
            }
        }

        public void ChangeZoom(float zoomValue, PointF zoomingCenter)
        {
            if (HasDocument)
            {
                Point oldLocation = ConvertAbsoluteToRelative(zoomingCenter);
                Diagram.Zoom = zoomValue;
                Point newLocation = ConvertAbsoluteToRelative(zoomingCenter);

                this.Offset += new Size(
                    newLocation.X - oldLocation.X,
                    newLocation.Y - oldLocation.Y
                );
            }
        }

        private void ChangeZoom(float zoomValue, RectangleF zoomingCenter)
        {
            PointF centerPoint = new PointF(
                zoomingCenter.Left + zoomingCenter.Width / 2,
                zoomingCenter.Top + zoomingCenter.Height / 2);

            Diagram.Zoom = zoomValue;
            Point newLocation = ConvertAbsoluteToRelative(centerPoint);
            Point desiredLocation = new Point(Width / 2, Height / 2);

            this.Offset += new Size(
                newLocation.X - desiredLocation.X,
                newLocation.Y - desiredLocation.Y
            );
        }

        public void AutoZoom()
        {
            AutoZoom(true);
        }

        public void AutoZoom(bool selectedOnly)
        {
            if (HasDocument && !Diagram.IsEmpty)
            {
                const int Margin = Shape.SelectionMargin;
                selectedOnly &= Diagram.HasSelectedElement;

                Rectangle visibleRectangle = this.ClientRectangle;
                RectangleF diagramRectangle = diagram.GetPrintingArea(selectedOnly);
                visibleRectangle.Inflate(-Margin, -Margin);

                float scaleX = visibleRectangle.Width / diagramRectangle.Width;
                float scaleY = visibleRectangle.Height / diagramRectangle.Height;
                float scale = Math.Min(scaleX, scaleY);

                Diagram.Zoom = scale;

                float offsetX = (visibleRectangle.Width - diagramRectangle.Width * Zoom) / 2;
                float offsetY = (visibleRectangle.Height - diagramRectangle.Height * Zoom) / 2;
                Offset = new Point(
                    (int)(diagramRectangle.X * Zoom - Margin - offsetX),
                    (int)(diagramRectangle.Y * Zoom - Margin - offsetY)
                );
            }
        }

        private void SetScrolls()
        {
            if (HasDocument)
            {
                this.AutoScrollMinSize = new Size(
                    (int)Math.Ceiling(Diagram.Size.Width * Diagram.Zoom),
                    (int)Math.Ceiling(Diagram.Size.Height * Diagram.Zoom)
                );
                this.AutoScrollPosition = Diagram.Offset;
            }
            else
            {
                this.AutoScrollMinSize = Size.Empty;
                this.AutoScrollPosition = Point.Empty;
            }
        }

        protected override Point ScrollToControl(Control activeControl)
        {
            if (activeControl.Parent != null && activeControl.Parent != this)
            {
                return ScrollToControl(activeControl.Parent);
            }
            else
            {
                Point point = base.ScrollToControl(activeControl);
                if (HasDocument)
                    Diagram.Offset = new Point(-point.X, -point.Y);
                return point;
            }
        }

        void IDocumentVisualizer.DrawDocument(Graphics g)
        {
            if (HasDocument)
            {
                IGraphics graphics = new GdiGraphics(g);
                Diagram.Print(graphics, false, Style.CurrentStyle);
            }
        }

        private void DrawContent(Graphics g)
        {
            if (HasDocument)
            {
                // Set the drawing quality
                g.SmoothingMode = SmoothingMode.AntiAlias;
                if (Diagram.Zoom == 1.0F)
                {
                    if (Properties.Settings.Default.UseClearType == ClearTypeMode.Always)
                        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    else
                        g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
                }
                else
                {
                    if (Properties.Settings.Default.UseClearType == ClearTypeMode.WhenZoomed ||
                        Properties.Settings.Default.UseClearType == ClearTypeMode.Always)
                    {
                        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    }
                    else
                    {
                        g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    }
                }

                // Transform the screen to absolute coordinate system
                g.TranslateTransform(-Diagram.Offset.X, -Diagram.Offset.Y);
                g.ScaleTransform(Diagram.Zoom, Diagram.Zoom);

                // Draw contents
                Diagram.Display(g);
            }
        }

        private void ScrollHorizontally(int offset)
        {
            if (HScroll)
            {
                int posX = -DisplayRectangle.X + offset;
                int maxX = DisplayRectangle.Width - ClientRectangle.Width;

                if (posX < 0)
                    posX = 0;
                if (posX > maxX)
                    posX = maxX;

                SetDisplayRectLocation(-posX, DisplayRectangle.Y);
                AdjustFormScrollbars(true);
            }
        }

        private void UpdateDocumentOffset()
        {
            if (HasDocument)
            {
                Diagram.Offset = this.Offset;
                if (MonoHelper.IsRunningOnMono)
                    Invalidate();
            }
        }

        private void UpdateWindowPositions()
        {
            if (ParentForm != null)
            {
                Point point = this.PointToScreen(Point.Empty);
                Point absPos = ParentForm.PointToClient(point);

                foreach (PopupWindow window in windows)
                {
                    window.ParentLocation = absPos;
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys key = (keyData & ~Keys.Modifiers);

            if (key == Keys.Up || key == Keys.Down)
                return false;
            else
                return base.ProcessDialogKey(keyData);
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            OnVisibleAreaChanged(EventArgs.Empty);
        }

        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);
            UpdateDocumentOffset();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                bool enlarge = (e.Delta > 0);

                if (ClientRectangle.Contains(e.Location))
                    ChangeZoom(enlarge, ConvertRelativeToAbsolute(e.Location));
                else
                    ChangeZoom(enlarge);
            }
            else if (Control.ModifierKeys == Keys.Shift)
            {
                ScrollHorizontally(-e.Delta);
            }
            else
            {
                base.OnMouseWheel(e);
            }
            UpdateDocumentOffset();
        }

        protected virtual void OnMouseHWheel(EventArgs e) //TODO: MouseEventArgs kellene
        {
            UpdateDocumentOffset();
            Invalidate(); //TODO: SetDisplayRectLocation() kellene
            if (MouseHWheel != null)
                MouseHWheel(this, e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (HasDocument)
            {
                AbsoluteMouseEventArgs abs_e = new AbsoluteMouseEventArgs(e, Diagram);
                Diagram.MouseDown(abs_e);
                if (e.Button == MouseButtons.Right)
                {
                    //this.ContextMenuStrip = Diagram.GetContextMenu(abs_e);
                    KryptonContextMenu = Diagram.GetKryptonContextMenu(abs_e);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (HasDocument)
            {
                Diagram.MouseMove(new AbsoluteMouseEventArgs(e, Diagram));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (HasDocument)
            {
                Diagram.MouseUp(new AbsoluteMouseEventArgs(e, Diagram));
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (HasDocument)
            {
                Diagram.DoubleClick(new AbsoluteMouseEventArgs(e, Diagram));
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (diagram != null)
            {
                if (e.Modifiers == Keys.Control)
                {
                    if (e.KeyCode == Keys.Add)
                    {
                        ZoomIn();
                    }
                    else if (e.KeyCode == Keys.Subtract)
                    {
                        ZoomOut();
                    }
                    else if (e.KeyCode == Keys.Multiply || e.KeyCode == Keys.NumPad0)
                    {
                        Zoom = 1.0F;
                    }
                }

                diagram.KeyDown(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (HasDocument)
            {
                DrawContent(e.Graphics);
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            UpdateWindowPositions();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateDocumentOffset();
            UpdateWindowPositions();
        }

        private void OnZoomChanged(EventArgs e)
        {
            if (ZoomChanged != null)
                ZoomChanged(this, e);
        }

        private void OnDocumentRedrawed(EventArgs e)
        {
            if (DocumentRedrawed != null)
                DocumentRedrawed(this, e);
        }

        private void OnVisibleAreaChanged(EventArgs e)
        {
            if (VisibleAreaChanged != null)
                VisibleAreaChanged(this, e);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            if (e.Data.GetDataPresent(typeof(ListViewItem)) || e.Data.GetDataPresent(typeof(TypeNode)))
            {
                if ((e.AllowedEffect & DragDropEffects.Link) != 0)
                {
                    e.Effect = DragDropEffects.Link;
                    var p = PointToClient(new Point(e.X, e.Y));
                    var x = (p.X + diagram.Offset.X) / diagram.Zoom;
                    var y = (p.Y + diagram.Offset.Y) / diagram.Zoom;
                    diagram.DragOver(new AbsoluteMouseEventArgs(MouseButtons.Left, x, y, diagram.Zoom));
                }
            }
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
            diagram.CancelCreating();
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            if (e.Data.GetDataPresent(typeof(ListViewItem)) || e.Data.GetDataPresent(typeof(TypeNode)))
            {
                try
                {
                    e.Effect = DragDropEffects.Link;
                    if (e.Data.GetDataPresent(typeof(ListViewItem)))
                    {
                        ListViewItem t = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                        if (t.Group.Name == "TypeGroup")
                        {
                            EntityType type;
                            if (Enum.TryParse(t.Tag.ToSafeString(), out type))
                                diagram.CreateShape(type);
                        }
                        else if (t.Group.Name == "RelationshipGroup")
                        {
                            RelationshipType type;
                            if (Enum.TryParse(t.Tag.ToSafeString(), out type))
                                diagram.CreateConnection(type);
                        }
                    }
                    else if (e.Data.GetDataPresent(typeof(TypeNode)))
                    {
                        TypeNode t = (TypeNode)e.Data.GetData(typeof(TypeNode));
                        diagram.CreateShape(t.TypeBase);
                    }
                }
                catch (Exception exc)
                {
                    Client.ShowInfo(exc.Message);
                }
            }
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);
            try
            {
                var p = PointToClient(new Point(e.X, e.Y));
                var x = (p.X + diagram.Offset.X) / diagram.Zoom;
                var y = (p.Y + diagram.Offset.Y) / diagram.Zoom;
                diagram.DragDrop(new AbsoluteMouseEventArgs(MouseButtons.Left, x, y, diagram.Zoom));
            }
            catch (Exception exc)
            {
                Client.ShowInfo(exc.Message);
            }
        }

        int LOWORD(IntPtr value)
        {
            int num = ((int)value.ToInt64()) & 0xffff;
            if (num <= 0x7fff)
            {
                return num;
            }
            return (num - 0x10000);
        }

        int HIWORD(IntPtr value)
        {
            int num = (((int)value.ToInt64()) >> 0x10) & 0xffff;
            if (num <= 0x7fff)
            {
                return num;
            }
            return (num - 0x10000);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x020E) // WM_MOUSEHWHEEL
            {
                OnMouseHWheel(EventArgs.Empty);
            }
            else if ((m.Msg == 0x7b) && (KryptonContextMenu != null))
            {
                Point p = new Point(LOWORD(m.LParam), HIWORD(m.LParam));
                if (((int)((long)m.LParam)) == -1)
                {
                    p = new Point(base.Width / 2, base.Height / 2);
                }
                else
                {
                    p = base.PointToClient(p);
                    p.X--;
                    p.Y--;
                }
                if (base.ClientRectangle.Contains(p))
                {
                    this.KryptonContextMenu.Show(this, base.PointToScreen(p));
                    return;
                }
            }
            if (!base.IsDisposed)
            {
                base.WndProc(ref m);
            }
        }

        ComponentFactory.Krypton.Toolkit.KryptonContextMenu _kryptonContextMenu;
        public virtual ComponentFactory.Krypton.Toolkit.KryptonContextMenu KryptonContextMenu
        {
            get
            {
                return this._kryptonContextMenu;
            }
            set
            {
                if (this._kryptonContextMenu != value)
                {
                    this._kryptonContextMenu = value;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var form = FindForm();
            if (form != null)
            {
                form.FormClosing += new FormClosingEventHandler(form_FormClosing);
                form.FormClosed += new FormClosedEventHandler(form_FormClosed);
            }
        }

        void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            diagram.EndEdit();
            GC.Collect();
        }

        void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!e.Cancel && diagram.IsDirty)
            //{
            //    DialogResult result = Client.Show(Strings.DiscardChanges, Strings.Confirmation,
            //        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            //    if (result == DialogResult.Yes)
            //        diagram.CancelEdit();
            //    else if (result == DialogResult.Cancel)
            //        e.Cancel = true;
            //}
        }
    }
}