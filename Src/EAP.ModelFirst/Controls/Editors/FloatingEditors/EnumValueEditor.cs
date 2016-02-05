using System.Drawing;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Core.Project;

namespace EAP.ModelFirst.Controls.Editors.FloatingEditors
{
    [System.ComponentModel.ToolboxItem(false)]
	public partial class EnumValueEditor : ItemEditor
	{
        class EnumValueEditorHandler : ItemEditorHandler
        {
            EnumShape shape = null;
            EnumValueEditor editor { get { return Window as EnumValueEditor; } }

            internal override void Init(DiagramElement element)
            {
                shape = (EnumShape)element;
                base.Init(element);
            }

            internal override void Relocate(DiagramElement element)
            {
                Relocate((EnumShape)element);
            }

            internal void Relocate(EnumShape shape)
            {
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
                if (shape.ActiveValue != null)
                {
                    int cursorPosition = editor.SelectionStart;
                    editor.DeclarationText = shape.ActiveValue.ToString();
                    editor.SelectionStart = cursorPosition;

                    editor.SetError(null);
                    editor.NeedValidation = false;
                    RefreshMoveUpDownTools();
                }
            }

            private void RefreshMoveUpDownTools()
            {
                int index = shape.ActiveMemberIndex;
                int parameterCount = shape.EnumType.ValueCount;

                editor.toolMoveUp.Enabled = (index > 0);
                editor.toolMoveDown.Enabled = (index < parameterCount - 1);
            }

            protected internal override bool ValidateDeclarationLine()
            {
                if (editor.NeedValidation && shape.ActiveValue != null)
                {
                    try
                    {
                        shape.EnumType.ModifyValue(shape.ActiveValue, editor.DeclarationText);
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
                shape.DeleteActiveValue();
            }
        }

        public EnumValueEditor()
            :base(new EnumValueEditorHandler())
        {

        }
	}
}
