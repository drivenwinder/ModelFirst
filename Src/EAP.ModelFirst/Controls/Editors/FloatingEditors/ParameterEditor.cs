using System.Drawing;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Core.Project;

namespace EAP.ModelFirst.Controls.Editors.FloatingEditors
{
    [System.ComponentModel.ToolboxItem(false)]
	public partial class ParameterEditor : ItemEditor
	{
        class ParameterEditorHandler : ItemEditorHandler
        {
            DelegateShape shape = null;
            ParameterEditor editor { get { return Window as ParameterEditor; } }

            internal override void Init(DiagramElement element)
            {
                shape = (DelegateShape)element;
                base.Init(element);
            }

            internal override void Relocate(DiagramElement element)
            {
                var shape = (DelegateShape)element;

                Diagram diagram = shape.Diagram;
                if (diagram != null)
                {
                    Rectangle record = shape.GetMemberRectangle(shape.ActiveMemberIndex);

                    Point absolute = new Point(shape.Right, record.Top);
                    Size relative = new Size(
                        (int)(absolute.X * diagram.Zoom) - diagram.Offset.X + MarginSize,
                        (int)(absolute.Y * diagram.Zoom) - diagram.Offset.Y);
                    relative.Height -= (Window.Height - (int)(record.Height * diagram.Zoom)) / 2;

                    Window.Location = Window.ParentLocation + relative;
                }
            }

            protected internal override void RefreshValues()
            {
                if (shape.ActiveParameter != null)
                {
                    int cursorPosition = editor.SelectionStart;
                    editor.DeclarationText = shape.ActiveParameter.ToString();
                    editor.SelectionStart = cursorPosition;

                    editor.SetError(null);
                    editor.NeedValidation = false;
                    RefreshMoveUpDownTools();
                }
            }

            protected internal override bool ValidateDeclarationLine()
            {
                if (editor.NeedValidation && shape.ActiveParameter != null)
                {
                    try
                    {
                        shape.DelegateType.ModifyParameter(shape.ActiveParameter, editor.DeclarationText);
                        RefreshValues();
                    }
                    catch (BadSyntaxException ex)
                    {
                        editor.SetError(ex.Message);
                        return false;
                    }
                }
                return true;
            }

            protected internal override void HideEditor()
            {
                editor.NeedValidation = false;
                shape.HideEditor();
            }

            protected internal override void SelectPrevious()
            {
                if (ValidateDeclarationLine())
                {
                    shape.SelectPrevious();
                }
            }

            protected internal override void SelectNext()
            {
                if (ValidateDeclarationLine())
                {
                    shape.SelectNext();
                }
            }

            protected internal override void MoveUp()
            {
                if (ValidateDeclarationLine())
                {
                    shape.MoveUp();
                }
            }

            protected internal override void MoveDown()
            {
                if (ValidateDeclarationLine())
                {
                    shape.MoveDown();
                }
            }

            protected internal override void Delete()
            {
                shape.DeleteActiveParameter();
            }

            private void RefreshMoveUpDownTools()
            {
                int index = shape.ActiveMemberIndex;
                int parameterCount = shape.DelegateType.ArgumentCount;

                editor.toolMoveUp.Enabled = (index > 0);
                editor.toolMoveDown.Enabled = (index < parameterCount - 1);
            }
        }

        public ParameterEditor()
            : base(new ParameterEditorHandler())
        {
        }
	}
}
