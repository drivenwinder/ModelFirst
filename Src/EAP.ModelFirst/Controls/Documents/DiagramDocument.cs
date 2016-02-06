using System;
using System.Linq;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.DynamicMenu;
using EAP.ModelFirst.Controls.Editors.DiagramEditor;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Explorers;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using System.Collections.Generic;
using System.Collections;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Connections;
using EAP.ModelFirst.Core.Project.Relationships;
using System.Drawing;

namespace EAP.ModelFirst.Controls.Documents
{
    public partial class DiagramDocument : DocumentBase, IDocument, IPropertyConfigurable, IPropertyGroup
    {
        public event EventHandler SelectionChanged;

        public DiagramDocument(IDiagram document, IDockForm dockForm)
        {
            InitializeComponent();
            DockForm = dockForm;
            canvas.Diagram = document;
            Text = canvas.Diagram.Name;
            TabText = Text;
            canvas.Diagram.Renamed += new EventHandler(diagram_StateChanged);
            Style.CurrentStyleChanged += new EventHandler(Style_CurrentStyleChanged);
            canvas.Diagram.StateChanged += new EventHandler(diagram_StateChanged);
            canvas.Diagram.SelectionChanged += new EventHandler(Diagram_SelectionChanged);
            Handler.ZoomHandler = canvas;
            Handler.PrintHandler = canvas.Diagram;
            Handler.EditHandler = canvas.Diagram;
            Handler.VisualizerHandler = canvas;
            Handler.LayoutHandler = canvas.Diagram;
            Handler.GenerateCode += new EventHandler(Handler_GenerateCode);
            UpdateTexts();
            Load += DiagramDocument_Load;
        }

        void DiagramDocument_Load(object sender, EventArgs e)
        {
            canvas.Offset = new Point(500, 400);
        }

        void Diagram_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, EventArgs.Empty);
        }

        void Style_CurrentStyleChanged(object sender, EventArgs e)
        {
            canvas.Diagram.Redraw();
        }

        void Handler_GenerateCode(object sender, EventArgs e)
        {
            if (canvas.Diagram != null)
            {
                var diagram = canvas.Diagram as Diagram;
                var types = diagram.GetSelectedShapes().Select(p => p.Entity).OfType<TypeBase>();
                if (!types.Any())
                    types = diagram.Entities.OfType<TypeBase>();
                Generator.Show(types);
            }
        }

        public IDocumentItem DocumentItem
        {
            get { return canvas.Diagram; }
        }

        public string GetStatus()
        {
            return canvas.Diagram.GetStatus();
        }

        public static IDocument LoadForm(string id, DocumentManager manager)
        {
            foreach (var p in manager.DockForm.Workspace.Projects)
            {
                IProjectDocument item = p.FindItem(id.ConvertTo<Guid>());
                if (item != null && item is IDiagram)
                {
                    return new DiagramDocument((IDiagram)item, manager.DockForm);
                }
            }
            return (IDocument)null;
        }

        void diagram_StateChanged(object sender, EventArgs e)
        {
            Text = canvas.Diagram.IsDirty ? canvas.Diagram.Name + "*" : canvas.Diagram.Name;
            TabText = Text;
        }

        private void DiagramDocument_FormClosing(object sender, FormClosingEventArgs e)
        {
            canvas.Diagram.Renamed -= new EventHandler(diagram_StateChanged);
            canvas.Diagram.StateChanged -= new EventHandler(diagram_StateChanged);
            Style.CurrentStyleChanged -= new EventHandler(Style_CurrentStyleChanged);
            canvas.Diagram.SelectionChanged -= new EventHandler(Diagram_SelectionChanged);
        }

        protected override string GetPersistString()
        {
            return base.GetPersistString() + "|" + canvas.Diagram.Id;
        }

        private void DiagramDocument_Activated(object sender, EventArgs e)
        {
            DockForm.Workspace.ActiveProject = canvas.Diagram.ProjectInfo;
        }

        public override IDynamicMenu GetDynamicMenu()
        {
            DiagramDynamicMenu dynamicMenu = DiagramDynamicMenu.Default;
            dynamicMenu.SetReference(canvas.Diagram);
            return dynamicMenu;
        }

        public void Save()
        {
            DockForm.Workspace.SaveProject(canvas.Diagram.ProjectInfo);
        }

        public bool IsDirty
        {
            get { return canvas.Diagram.ProjectInfo.IsDirty; }
        }

        public void SaveAs()
        {
            DockForm.Workspace.SaveProjectAs(canvas.Diagram.ProjectInfo);
        }

        public object PropertyObject
        {
            get { return this; }
        }

        public void Select(object obj)
        {
            var diagram = canvas.Diagram as Diagram;
            diagram.DeselectAll();
            if (obj is IEntity)
            {
                var e = diagram.ShapeList.FirstOrDefault(p => p.Entity == obj);
                if (e != null)
                {
                    e.IsSelected = true;
                    var x = e.Left - (canvas.VisibleArea.Width - e.Width) / 2;
                    var y = e.Top - (canvas.VisibleArea.Height - e.Height) / 2;
                    canvas.Offset = new Point(Math.Max(x, 0), Math.Max(y, 0));
                }
            }
            else if (obj is Relationship)
            {
                var r = diagram.ConnectionList.FirstOrDefault(p => p.Relationship == obj);
                if (r != null)
                {
                    r.IsSelected = true;
                    var e = diagram.ShapeList.FirstOrDefault(p => p.Entity == r.Relationship.First);
                    if (e != null)
                    {
                        var x = e.Left - (canvas.VisibleArea.Width - e.Width) / 2;
                        var y = e.Top - (canvas.VisibleArea.Height - e.Height) / 2;
                        canvas.Offset = new Point(Math.Max(x, 0), Math.Max(y, 0));
                    }
                }
            }
            diagram.Redraw();
        }

        public IEnumerable ObjectList
        {
            get
            {
                var diagram = canvas.Diagram as Diagram;
                foreach (var e in diagram.Entities)
                    yield return e;
                foreach (var e in diagram.Relationships)
                    yield return e;
                yield return diagram;
            }
        }

        public object[] SelectedObjects
        {
            get
            {
                var diagram = canvas.Diagram as Diagram;
                var s = diagram.GetSelectedElements();
                if (s.Any())
                {
                    List<object> lst = new List<object>();
                    lst.AddRange(s.OfType<Shape>().Select(p => p.Entity));
                    lst.AddRange(s.OfType<Connection>().Select(p => p.Relationship));
                    return lst.ToArray();
                }
                return new[] { diagram };
            }
        }
    }
}
