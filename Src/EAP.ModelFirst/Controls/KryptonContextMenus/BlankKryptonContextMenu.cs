using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Relationships;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Controls.Dialogs;
using EAP.ModelFirst.Controls.Explorers;

namespace EAP.ModelFirst.Controls.KryptonContextMenus
{
    public sealed class BlankKryptonContextMenu : DiagramKryptonContextMenu
    {
		static BlankKryptonContextMenu _default = new BlankKryptonContextMenu();

		#region MenuItem fields
        KryptonContextMenuItem mnuAddNewElement;
        KryptonContextMenuItem mnuNewClass;
        KryptonContextMenuItem mnuNewStructure;
        KryptonContextMenuItem mnuNewInterface;
        KryptonContextMenuItem mnuNewEnum;
        KryptonContextMenuItem mnuNewDelegate;
        KryptonContextMenuItem mnuNewComment;
        KryptonContextMenuItem mnuNewAssociation;
        KryptonContextMenuItem mnuNewComposition;
        KryptonContextMenuItem mnuNewAggregation;
        KryptonContextMenuItem mnuNewGeneralization;
        KryptonContextMenuItem mnuNewRealization;
        KryptonContextMenuItem mnuNewDependency;
        KryptonContextMenuItem mnuNewNesting;
        KryptonContextMenuItem mnuNewCommentRelationship;

        KryptonContextMenuItem mnuMembersFormat;
        KryptonContextMenuItem mnuShowType;
        KryptonContextMenuItem mnuShowParameters;
        KryptonContextMenuItem mnuShowParameterNames;
        KryptonContextMenuItem mnuShowInitialValue;

        KryptonContextMenuItem mnuPaste;
        KryptonContextMenuItem mnuSaveAsImage;

        KryptonContextMenuItem mnuEditDatabaseSchema;
        KryptonContextMenuItem mnuGenerateCode;

        KryptonContextMenuItem mnuFormat;

        KryptonContextMenuItem mnuCollapseAll;
        KryptonContextMenuItem mnuExpandAll;
        KryptonContextMenuItem mnuAutoSize;
        KryptonContextMenuItem mnuAutoWidth;
        KryptonContextMenuItem mnuAutoHeight;

        KryptonContextMenuItem mnuSelectAll;

		#endregion

        private BlankKryptonContextMenu()
		{
			InitMenuItems();
		}

        public static BlankKryptonContextMenu Default
		{
			get { return _default; }
		}

		public override void ValidateMenuItems(Diagram diagram)
		{
			base.ValidateMenuItems(diagram);
			mnuPaste.Enabled = diagram.CanPasteFromClipboard;

			mnuNewStructure.Visible = diagram.Language.SupportsStructures;
			mnuNewDelegate.Visible = diagram.Language.SupportsDelegates;

            mnuShowType.Checked = Properties.Settings.Default.ShowType;
            mnuShowParameters.Checked = Properties.Settings.Default.ShowParameters;
            mnuShowParameterNames.Checked = Properties.Settings.Default.ShowParameterNames;
            mnuShowInitialValue.Checked = Properties.Settings.Default.ShowInitialValue;

			mnuSaveAsImage.Enabled = !diagram.IsEmpty;
		}


        public void UpdateTexts()
        {
            // Diagram menu
            mnuAddNewElement.Text = Strings.MenuNew;
            mnuNewClass.Text = Strings.MenuClass;
            mnuNewStructure.Text = Strings.MenuStruct;
            mnuNewInterface.Text = Strings.MenuInterface;
            mnuNewEnum.Text = Strings.MenuEnum;
            mnuNewDelegate.Text = Strings.MenuDelegate;
            mnuNewComment.Text = Strings.MenuComment;
            mnuNewAssociation.Text = Strings.MenuAssociation;
            mnuNewComposition.Text = Strings.MenuComposition;
            mnuNewAggregation.Text = Strings.MenuAggregation;
            mnuNewGeneralization.Text = Strings.MenuGeneralization;
            mnuNewRealization.Text = Strings.MenuRealization;
            mnuNewDependency.Text = Strings.MenuDependency;
            mnuNewNesting.Text = Strings.MenuNesting;
            mnuNewCommentRelationship.Text = Strings.MenuCommentRelationship;
            mnuMembersFormat.Text = Strings.MenuMembersFormat;
            mnuShowType.Text = Strings.MenuType;
            mnuShowParameters.Text = Strings.MenuParameters;
            mnuShowParameterNames.Text = Strings.MenuParameterNames;
            mnuShowInitialValue.Text = Strings.MenuInitialValue;
            mnuGenerateCode.Text = Strings.MenuGenerateCode;
            mnuEditDatabaseSchema.Text = Strings.MenuEditDatabaseSchema;
            mnuSaveAsImage.Text = Strings.MenuSaveAsImage;

            mnuFormat.Text = Strings.MenuFormat;
            mnuCollapseAll.Text = Strings.MenuCollapseAll;
            mnuExpandAll.Text = Strings.MenuExpandAll;
            mnuAutoHeight.Text = Strings.MenuAutoHeight;
            mnuAutoSize.Text = Strings.MenuAutoSize;
            mnuAutoWidth.Text = Strings.MenuAutoWidth;
        }

		private void InitMenuItems()
		{
            mnuAddNewElement = new KryptonContextMenuItem(Strings.MenuAddNew, Resources.NewEntity, null);
            mnuNewClass = new KryptonContextMenuItem(Strings.MenuClass, Resources.Class, mnuNewClass_Click);
			mnuNewStructure = new KryptonContextMenuItem(Strings.MenuStruct, Resources.Structure, mnuNewStructure_Click);
			mnuNewInterface = new KryptonContextMenuItem(Strings.MenuInterface, Resources.Interface32, mnuNewInterface_Click);
			mnuNewEnum = new KryptonContextMenuItem(Strings.MenuEnum, Resources.Enum, mnuNewEnum_Click);
			mnuNewDelegate = new KryptonContextMenuItem(Strings.MenuDelegate, Resources.Delegate, mnuNewDelegate_Click);
			mnuNewComment = new KryptonContextMenuItem(Strings.MenuComment, Resources.Comment, mnuNewComment_Click);
			mnuNewAssociation = new KryptonContextMenuItem(Strings.MenuAssociation, Resources.Association, mnuNewAssociation_Click);
			mnuNewComposition = new KryptonContextMenuItem(Strings.MenuComposition, Resources.Composition, mnuNewComposition_Click);
			mnuNewAggregation = new KryptonContextMenuItem(Strings.MenuAggregation, Resources.Aggregation, mnuNewAggregation_Click);
			mnuNewGeneralization = new KryptonContextMenuItem(Strings.MenuGeneralization, Resources.Generalization, mnuNewGeneralization_Click);
			mnuNewRealization = new KryptonContextMenuItem(Strings.MenuRealization, Resources.Realization, mnuNewRealization_Click);
			mnuNewDependency = new KryptonContextMenuItem(Strings.MenuDependency, Resources.Dependency, mnuNewDependency_Click);
			mnuNewNesting = new KryptonContextMenuItem(Strings.MenuNesting, Resources.Nesting, mnuNewNesting_Click);
			mnuNewCommentRelationship = new KryptonContextMenuItem(Strings.MenuCommentRelationship, Resources.CommentRel, mnuNewCommentRelationship_Click);

			mnuMembersFormat = new KryptonContextMenuItem(Strings.MenuMembersFormat);
			mnuShowType = new KryptonContextMenuItem(Strings.MenuType);
			mnuShowType.CheckedChanged += mnuShowType_CheckedChanged;
			mnuShowType.CheckOnClick = true;
			mnuShowParameters = new KryptonContextMenuItem(Strings.MenuParameters);
			mnuShowParameters.CheckedChanged += mnuShowParameters_CheckedChanged;
			mnuShowParameters.CheckOnClick = true;
			mnuShowParameterNames = new KryptonContextMenuItem(Strings.MenuParameterNames);
			mnuShowParameterNames.CheckedChanged += mnuShowParameterNames_CheckedChanged;
			mnuShowParameterNames.CheckOnClick = true;
			mnuShowInitialValue = new KryptonContextMenuItem(Strings.MenuInitialValue);
			mnuShowInitialValue.CheckedChanged += mnuShowInitialValue_CheckedChanged;
			mnuShowInitialValue.CheckOnClick = true;

			mnuPaste = new KryptonContextMenuItem(Strings.MenuPaste, Resources.Paste, mnuPaste_Click);
            mnuSaveAsImage = new KryptonContextMenuItem(Strings.MenuSaveAsImage, Resources.Image, mnuSaveAsImage_Click);

            mnuGenerateCode = new KryptonContextMenuItem(Strings.MenuGenerateCode, Resources.CodeGenerator, mnuGenerateCode_Click);
            mnuEditDatabaseSchema = new KryptonContextMenuItem(Strings.MenuEditDatabaseSchema, Resources.DatabaseInfo, mnuEditDatabaseSchema_Click);

            mnuFormat = new KryptonContextMenuItem(Strings.MenuFormat);
            mnuAutoHeight = new KryptonContextMenuItem(Strings.MenuAutoHeight, mnuAutoHeight_Click);
            mnuAutoSize = new KryptonContextMenuItem(Strings.MenuAutoSize, mnuAutoSize_Click);
            mnuAutoWidth = new KryptonContextMenuItem(Strings.MenuAutoWidth, mnuAutoWidth_Click);
            mnuExpandAll = new KryptonContextMenuItem(Strings.MenuExpandAll, Resources.ExpandAll, mnuExpandAll_Click);
            mnuCollapseAll = new KryptonContextMenuItem(Strings.MenuCollapseAll, Resources.CollapseAll, mnuCollapseAll_Click);

            mnuSelectAll = new KryptonContextMenuItem(Strings.MenuSelectAll, mnuSelectAll_Click);


            mnuAddNewElement.Items.Add(new KryptonContextMenuItems(new KryptonContextMenuItemBase[] {
				mnuNewClass,
				mnuNewStructure,
				mnuNewInterface,
				mnuNewEnum,
				mnuNewDelegate,
				mnuNewComment,
				new KryptonContextMenuSeparator(),
				mnuNewAssociation,
				mnuNewComposition,
				mnuNewAggregation,
				mnuNewGeneralization,
				mnuNewRealization,
				mnuNewDependency,
				mnuNewNesting,
				mnuNewCommentRelationship
            }));

            mnuMembersFormat.Items.Add(new KryptonContextMenuItems(new KryptonContextMenuItemBase[] {
				mnuShowType,
				mnuShowParameters,
				mnuShowParameterNames,
				mnuShowInitialValue
                
            }));

            mnuFormat.Items.Add(new KryptonContextMenuItems(new KryptonContextMenuItemBase[] {
                mnuAutoSize,
                mnuAutoWidth,
				mnuAutoHeight,
				new KryptonContextMenuSeparator(),
                mnuExpandAll,
                mnuCollapseAll                
            }));

            MenuList.AddRange(new KryptonContextMenuItemBase[] {
				mnuAddNewElement,
				mnuMembersFormat,
				new KryptonContextMenuSeparator(),
				mnuPaste,
				mnuSaveAsImage,
				new KryptonContextMenuSeparator(),
                mnuEditDatabaseSchema,
                mnuGenerateCode,
				new KryptonContextMenuSeparator(),
                mnuFormat,
				mnuSelectAll
			});
		}

        private void mnuGenerateCode_Click(object sender, EventArgs e)
        {
            if (Diagram == null) return;
            var types = Diagram.Entities.OfType<TypeBase>();
            Generator.Show(types);
        }

        private void mnuEditDatabaseSchema_Click(object sender, EventArgs e)
        {
            if (Diagram == null) return;
            using (DbSchemaDialog dialog = new DbSchemaDialog())
            {
                var types = Diagram.Entities.OfType<SingleInharitanceType>();
                dialog.ShowDialog(types);
            }
        }

        private void mnuAutoHeight_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.AutoHeightOfShapes();
        }

        private void mnuAutoSize_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.AutoSizeOfShapes();
        }

        private void mnuAutoWidth_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.AutoWidthOfShapes();
        }

        private void mnuCollapseAll_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.CollapseAll();
        }

        private void mnuExpandAll_Click(object sender, EventArgs e)
        {
            if (Diagram != null)
                Diagram.ExpandAll();
        }

		private void mnuNewClass_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.CreateShape(EntityType.Class);
		}

		private void mnuNewStructure_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
                Diagram.CreateShape(EntityType.Structure);
		}

		private void mnuNewInterface_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
                Diagram.CreateShape(EntityType.Interface);
		}

		private void mnuNewEnum_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
                Diagram.CreateShape(EntityType.Enum);
		}

		private void mnuNewDelegate_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
                Diagram.CreateShape(EntityType.Delegate);
		}

		private void mnuNewComment_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
                Diagram.CreateShape(EntityType.Comment);
		}

		private void mnuNewAssociation_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.CreateConnection(RelationshipType.Association);
		}

		private void mnuNewComposition_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.CreateConnection(RelationshipType.Composition);
		}

		private void mnuNewAggregation_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.CreateConnection(RelationshipType.Aggregation);
		}

		private void mnuNewGeneralization_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.CreateConnection(RelationshipType.Generalization);
		}

		private void mnuNewRealization_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.CreateConnection(RelationshipType.Realization);
		}

		private void mnuNewDependency_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.CreateConnection(RelationshipType.Dependency);
		}

		private void mnuNewNesting_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.CreateConnection(RelationshipType.Nesting);
		}

		private void mnuNewCommentRelationship_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.CreateConnection(RelationshipType.Comment);
		}

		private void mnuShowType_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.ShowType = ((KryptonContextMenuItem) sender).Checked;
			if (Diagram != null)
				Diagram.Redraw();
		}

		private void mnuShowParameters_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.ShowParameters = ((KryptonContextMenuItem) sender).Checked;
			if (Diagram != null)
				Diagram.Redraw();
		}

		private void mnuShowParameterNames_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.ShowParameterNames = ((KryptonContextMenuItem) sender).Checked;
			if (Diagram != null)
				Diagram.Redraw();
		}

		private void mnuShowInitialValue_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.ShowInitialValue = ((KryptonContextMenuItem) sender).Checked;
			if (Diagram != null)
				Diagram.Redraw();
		}

		private void mnuPaste_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.Paste();
		}

		private void mnuSaveAsImage_Click(object sender, EventArgs e)
		{
			if (Diagram != null && !Diagram.IsEmpty)
				Diagram.SaveAsImage();
		}

		private void mnuSelectAll_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.SelectAll();
		}
    }
}
