using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;

namespace EAP.ModelFirst.Controls.Editors.FloatingEditors
{
    [System.ComponentModel.ToolboxItem(false)]
    public class EditorWindow : PopupWindow
    {
        protected EditorHandler EditorHandler { get; private set; }

        private EditorWindow()
        {
            //For Windows Designer
        }

        public EditorWindow(EditorHandler handler)
        {
            EditorHandler = handler;
            if (EditorHandler != null)//For Windows Designer, in DesignMode Editor may be null
                EditorHandler.Window = this;
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (EditorHandler != null)
                EditorHandler.OnWindowPaintBackground(e);
        }

        internal void Init(DiagramElement element)
        {
            EditorHandler.Init(element);
        }

        bool relocating;
        internal void Relocate(DiagramElement element)
        {
            if (relocating) return;
            relocating = true;
            EditorHandler.Relocate(element);
            relocating = false;
        }

        public void ValidateData()
        {
            EditorHandler.ValidateData();
        }

        public override void Closing()
        {
            ValidateData();
        }
    }
}
