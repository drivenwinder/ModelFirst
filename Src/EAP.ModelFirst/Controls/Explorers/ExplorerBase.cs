
namespace EAP.ModelFirst.Controls.Explorers
{
    public partial class ExplorerBase : DockContent
    {
        protected ExplorerBase()
        {
            HideOnClose = true;
        }

        public virtual IDockForm DockForm
        {
            get;
            set;
        }

        protected override string GetPersistString()
        {
            return typeof(ExplorerBase) + "|" + base.GetPersistString();
        }

        public static ExplorerBase LoadForm(string type, DocumentManager manager)
        {
            var p = typeof(ExplorerBase).Assembly.GetType(type)
                .GetProperty("Instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            if (p != null)
            {
                var e = (ExplorerBase)p.GetValue(null, null);
                e.DockForm = manager.DockForm;
                return e;
            }
            return null;
        }

    }
}
