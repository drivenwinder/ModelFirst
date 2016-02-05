using EAP.ModelFirst.Core;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Controls.Explorers
{
    public partial class Navigator : ExplorerBase
    {
        static Navigator instance;

        static Navigator Instance
        {
            get
            {
                if (instance == null)
                    instance = new Navigator();
                return instance;
            }
        }

        public static void Show(IDockForm dockForm)
        {
            Instance.DockForm = dockForm;
            if (dockForm.DockPanel.ActiveDocument is IDocument)
                SetDocumentVisualizer(((IDocument)dockForm.DockPanel.ActiveDocument).Handler.VisualizerHandler);
            Instance.Show(dockForm.DockPanel);
        }

        public static void SetDocumentVisualizer(IDocumentVisualizer documentVisualizer)
        {
            if (instance == null) return;
            instance.diagramNavigator.DocumentVisualizer = documentVisualizer;
        }

        protected Navigator()
        {
            InitializeComponent();
            UpdateTexts();
        }

        public override void UpdateTexts()
        {
            Text = Strings.DiagramNavigator;
            TabText = Strings.DiagramNavigator;
            base.UpdateTexts();
        }
    }
}
