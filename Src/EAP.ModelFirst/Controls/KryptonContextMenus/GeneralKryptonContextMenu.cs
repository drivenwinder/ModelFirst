using System;
using System.Linq;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Controls.Dialogs;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Controls.Explorers;

namespace EAP.ModelFirst.Controls.KryptonContextMenus
{
    public sealed class GeneralKryptonContextMenu : DiagramKryptonContextMenu
    {
		static GeneralKryptonContextMenu _default = new GeneralKryptonContextMenu();

		KryptonContextMenuItem mnuCut;
		KryptonContextMenuItem mnuCopy;
		KryptonContextMenuItem mnuDelete;
		KryptonContextMenuItem mnuCopyAsImage;
        KryptonContextMenuItem mnuSaveAsImage;

        KryptonContextMenuItem mnuEditDatabaseSchema;
        KryptonContextMenuItem mnuGenerateCode;

        private GeneralKryptonContextMenu()
		{
			InitMenuItems();
		}

		public static GeneralKryptonContextMenu Default
		{
			get { return _default; }
		}

		private void UpdateTexts()
		{
			mnuCut.Text = Strings.MenuCut;
			mnuCopy.Text = Strings.MenuCopy;
			mnuDelete.Text = Strings.MenuDelete;
			mnuCopyAsImage.Text = Strings.MenuCopyImageToClipboard;
            mnuSaveAsImage.Text = Strings.MenuSaveSelectionAsImage;
            mnuGenerateCode.Text = Strings.MenuGenerateCode;
            mnuEditDatabaseSchema.Text = Strings.MenuEditDatabaseSchema;
		}

		public override void ValidateMenuItems(Diagram diagram)
		{
			base.ValidateMenuItems(diagram);
			mnuCut.Enabled = diagram.CanCutToClipboard;
            mnuCopy.Enabled = diagram.CanCopyToClipboard;
            mnuGenerateCode.Enabled = diagram.GetSelectedShapes().Any(p => p.Entity is TypeBase);
            mnuEditDatabaseSchema.Enabled = mnuGenerateCode.Enabled;
            UpdateTexts();
		}

        private void InitMenuItems()
        {
            mnuCut = new KryptonContextMenuItem(Strings.MenuCut, Resources.Cut, mnuCut_Click);
            mnuCopy = new KryptonContextMenuItem(Strings.MenuCopy, Resources.Copy, mnuCopy_Click);
            mnuDelete = new KryptonContextMenuItem(Strings.MenuDelete, Resources.Delete, mnuDelete_Click);
            mnuCopyAsImage = new KryptonContextMenuItem(Strings.MenuCopyImageToClipboard, Resources.CopyAsImage, mnuCopyAsImage_Click);
            mnuSaveAsImage = new KryptonContextMenuItem(Strings.MenuSaveSelectionAsImage, Resources.Image, mnuSaveAsImage_Click);

            mnuGenerateCode = new KryptonContextMenuItem(Strings.MenuGenerateCode, Resources.CodeGenerator, mnuGenerateCode_Click);
            mnuEditDatabaseSchema = new KryptonContextMenuItem(Strings.MenuEditDatabaseSchema, Resources.DatabaseInfo, mnuEditDatabaseSchema_Click);

            MenuList.AddRange(new KryptonContextMenuItemBase[] {
				mnuCut,
				mnuCopy,
				mnuDelete,
				new KryptonContextMenuSeparator(),
				mnuCopyAsImage,
				mnuSaveAsImage,
				new KryptonContextMenuSeparator(),
                mnuEditDatabaseSchema,
                mnuGenerateCode
			});
        }

        private void mnuGenerateCode_Click(object sender, EventArgs e)
        {
            if (Diagram == null) return;
            var types = Diagram.GetSelectedShapes().Select(p => p.Entity).OfType<TypeBase>();
            Generator.Show(types);
        }

        private void mnuEditDatabaseSchema_Click(object sender, EventArgs e)
        {
            if (Diagram == null) return;
            using (DbSchemaDialog dialog = new DbSchemaDialog())
            {
                var types = Diagram.GetSelectedShapes().Select(p => p.Entity)
                    .OfType<SingleInharitanceType>();
                dialog.ShowDialog(types);
            }
        }

		private void mnuCut_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.Cut();
		}

		private void mnuCopy_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.Copy();
		}

		private void mnuDelete_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.DeleteSelectedElements();
		}

		private void mnuCopyAsImage_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.CopyAsImage();
		}

		private void mnuSaveAsImage_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.SaveAsImage(true);
		}
	}
}