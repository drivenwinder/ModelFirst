using System;
using System.ComponentModel;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using System.Drawing;

namespace EAP.ModelFirst.Controls.Editors.FloatingEditors
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class RelationShipEditor : EditorWindow
    {
        TypeShape shape = null;

        class RelationShipEditorHandler : EditorHandler
        {
            RelationShipEditor editor { get { return Window as RelationShipEditor; } }

            internal override void Init(DiagramElement element)
            {
                editor.Init((TypeShape)element);
            }

            internal override void Relocate(DiagramElement element)
            {
                var shape = (TypeShape)element;
                Diagram diagram = shape.Diagram;
                if (diagram != null)
                {
                    Point absolute = new Point(shape.Left, shape.Top - 40);
                    Size relative = new Size(
                        (int)(absolute.X * diagram.Zoom) - diagram.Offset.X,
                        (int)(absolute.Y * diagram.Zoom) - diagram.Offset.Y);
                    Window.Location = Window.ParentLocation + relative;
                }
            }

            public override void ValidateData()
            {

            }
        }

        public RelationShipEditor()
            : base(new RelationShipEditorHandler())
        {
            InitializeComponent();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
        }

        internal void Init(TypeShape element)
        {
            shape = element;
        }

        private void btnAssociation_Click(object sender, EventArgs e)
        {
            shape.Diagram.CreateConnection(shape, Core.Project.Relationships.RelationshipType.Association);
        }

        private void btnComposition_Click(object sender, EventArgs e)
        {
            shape.Diagram.CreateConnection(shape, Core.Project.Relationships.RelationshipType.Composition);
        }

        private void btnAggregation_Click(object sender, EventArgs e)
        {
            shape.Diagram.CreateConnection(shape, Core.Project.Relationships.RelationshipType.Aggregation);
        }

        private void btnGeneralization_Click(object sender, EventArgs e)
        {
            shape.Diagram.CreateConnection(shape, Core.Project.Relationships.RelationshipType.Generalization);
        }

        private void btnRealization_Click(object sender, EventArgs e)
        {
            shape.Diagram.CreateConnection(shape, Core.Project.Relationships.RelationshipType.Realization);
        }

        private void btnDependency_Click(object sender, EventArgs e)
        {
            shape.Diagram.CreateConnection(shape, Core.Project.Relationships.RelationshipType.Dependency);
        }
    }
}
