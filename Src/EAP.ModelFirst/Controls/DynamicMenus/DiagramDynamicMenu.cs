using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Relationships;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Controls.Dialogs;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Controls.Explorers;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Editors.DiagramEditor;

namespace EAP.ModelFirst.Controls.DynamicMenu
{
    public sealed partial class DiagramDynamicMenu : MenuStrip, IDynamicMenu
	{
		static DiagramDynamicMenu _default = new DiagramDynamicMenu();

		ToolStripMenuItem[] menuItems;
		Diagram diagram = null;

		private DiagramDynamicMenu()
		{
			InitializeComponent();
			UpdateTexts();
			
			menuItems = new ToolStripMenuItem[2] { mnuDiagram, mnuFormat };
		}

		public static DiagramDynamicMenu Default
		{
			get { return _default; }
		}

        public int PreferredIndex
        {
            get { return 3; }
        }

		public IEnumerable<ToolStripMenuItem> GetMenuItems()
		{
			return menuItems;
		}

		public void SetReference(IDiagram _diagram)
        {
            if (_diagram == null)
				diagram = null;
			else
			{
                diagram = _diagram as Diagram;
				mnuNewStructure.Visible = diagram.Language.SupportsStructures;
				mnuNewDelegate.Visible = diagram.Language.SupportsDelegates;
			}
		}

        public void Close()
        {
            SetReference(null);
        }

		private void UpdateTexts()
		{
			// Diagram menu
			mnuDiagram.Text = Strings.MenuDiagram;
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
			mnuSaveAsImage.Text = Strings.MenuSaveAsImage;

			// Format menu
			mnuFormat.Text = Strings.MenuFormat;
			mnuAlign.Text = Strings.MenuAlign;
			mnuAlignTop.Text = Strings.MenuAlignTop;
			mnuAlignLeft.Text = Strings.MenuAlignLeft;
			mnuAlignBottom.Text = Strings.MenuAlignBottom;
			mnuAlignRight.Text = Strings.MenuAlignRight;
			mnuAlignHorizontal.Text = Strings.MenuAlignHorizontal;
			mnuAlignVertical.Text = Strings.MenuAlignVertical;
			mnuMakeSameSize.Text = Strings.MenuMakeSameSize;
			mnuSameWidth.Text = Strings.MenuSameWidth;
			mnuSameHeight.Text = Strings.MenuSameHeight;
			mnuSameSize.Text = Strings.MenuSameSize;
			mnuAutoSize.Text = Strings.MenuAutoSize;
			mnuAutoWidth.Text = Strings.MenuAutoWidth;
			mnuAutoHeight.Text = Strings.MenuAutoHeight;
			mnuCollapseAll.Text = Strings.MenuCollapseAll;
			mnuExpandAll.Text = Strings.MenuExpandAll;
		}

		#region Event handlers

		private void mnuDiagram_DropDownOpening(object sender, EventArgs e)
		{
			bool hasContent = (diagram != null && !diagram.IsEmpty);
			mnuGenerateCode.Enabled = hasContent;
			mnuSaveAsImage.Enabled = hasContent;
		}

		private void mnuNewClass_Click(object sender, EventArgs e)
		{
			if (diagram != null)
                diagram.CreateShape(EntityType.Class);
		}

		private void mnuNewStructure_Click(object sender, EventArgs e)
		{
			if (diagram != null)
                diagram.CreateShape(EntityType.Structure);
		}

		private void mnuNewInterface_Click(object sender, EventArgs e)
		{
			if (diagram != null)
                diagram.CreateShape(EntityType.Interface);
		}

		private void mnuNewEnum_Click(object sender, EventArgs e)
		{
			if (diagram != null)
                diagram.CreateShape(EntityType.Enum);
		}

		private void mnuNewDelegate_Click(object sender, EventArgs e)
		{
			if (diagram != null)
                diagram.CreateShape(EntityType.Delegate);
		}

		private void mnuNewComment_Click(object sender, EventArgs e)
		{
			if (diagram != null)
                diagram.CreateShape(EntityType.Comment);
		}

		private void mnuNewAssociation_Click(object sender, EventArgs e)
		{
			diagram.CreateConnection(RelationshipType.Association);
		}

		private void mnuNewComposition_Click(object sender, EventArgs e)
		{
			diagram.CreateConnection(RelationshipType.Composition);
		}

		private void mnuNewAggregation_Click(object sender, EventArgs e)
		{
			diagram.CreateConnection(RelationshipType.Aggregation);
		}

		private void mnuNewGeneralization_Click(object sender, EventArgs e)
		{
			diagram.CreateConnection(RelationshipType.Generalization);
		}

		private void mnuNewRealization_Click(object sender, EventArgs e)
		{
			diagram.CreateConnection(RelationshipType.Realization);
		}

		private void mnuNewDependency_Click(object sender, EventArgs e)
		{
			diagram.CreateConnection(RelationshipType.Dependency);
		}

		private void mnuNewNesting_Click(object sender, EventArgs e)
		{
			diagram.CreateConnection(RelationshipType.Nesting);
		}

		private void mnuNewCommentRelationship_Click(object sender, EventArgs e)
		{
			diagram.CreateConnection(RelationshipType.Comment);
		}

		private void mnuMembersFormat_DropDownOpening(object sender, EventArgs e)
		{
            mnuShowType.Checked = Properties.Settings.Default.ShowType;
            mnuShowParameters.Checked = Properties.Settings.Default.ShowParameters;
            mnuShowParameterNames.Checked = Properties.Settings.Default.ShowParameterNames;
            mnuShowInitialValue.Checked = Properties.Settings.Default.ShowInitialValue;
		}

		private void mnuShowType_Click(object sender, EventArgs e)
		{
            Properties.Settings.Default.ShowType = ((ToolStripMenuItem)sender).Checked;
			if (diagram != null)
				diagram.Redraw();
		}

		private void mnuShowParameters_Click(object sender, EventArgs e)
		{
            Properties.Settings.Default.ShowParameters = ((ToolStripMenuItem)sender).Checked;
			if (diagram != null)
				diagram.Redraw();
		}

		private void mnuShowParameterNames_Click(object sender, EventArgs e)
		{
            Properties.Settings.Default.ShowParameterNames = ((ToolStripMenuItem)sender).Checked;
			if (diagram != null)
				diagram.Redraw();
		}

		private void mnuShowInitialValue_Click(object sender, EventArgs e)
		{
            Properties.Settings.Default.ShowInitialValue = ((ToolStripMenuItem)sender).Checked;
			if (diagram != null)
				diagram.Redraw();
		}

		private void mnuGenerateCode_Click(object sender, EventArgs e)
		{
			if (diagram != null)
            {
                var types = diagram.GetSelectedShapes().Select(p => p.Entity).OfType<TypeBase>();
                if (!types.Any())
                    types = diagram.Entities.OfType<TypeBase>();
                Generator.Show(types);
			}
		}

		private void mnuSaveAsImage_Click(object sender, EventArgs e)
		{
			if (diagram != null && !diagram.IsEmpty)
				diagram.SaveAsImage();
		}

		private void mnuFormat_DropDownOpening(object sender, EventArgs e)		
		{
			bool shapeSelected = (diagram != null && diagram.SelectedShapeCount >= 1);
			bool multiselection = (diagram != null && diagram.SelectedShapeCount >= 2);

            //mnuAutoWidth.Enabled = shapeSelected;
            //mnuAutoHeight.Enabled = shapeSelected;
            //mnuAutoSize.Enabled = shapeSelected;
			mnuAlign.Enabled = multiselection;
			mnuMakeSameSize.Enabled = multiselection;
		}

		private void mnuAlignTop_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.AlignTop();
		}

		private void mnuAlignLeft_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.AlignLeft();
		}

		private void mnuAlignBottom_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.AlignBottom();
		}

		private void mnuAlignRight_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.AlignRight();
		}

		private void mnuAlignHorizontal_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.AlignHorizontal();
		}

		private void mnuAlignVertical_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.AlignVertical();
		}

		private void mnuSameWidth_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.AdjustToSameWidth();
		}

		private void mnuSameHeight_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.AdjustToSameHeight();
		}

		private void mnuSameSize_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.AdjustToSameSize();
		}

		private void mnuAutoSize_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.AutoSizeOfShapes();
		}

		private void mnuAutoWidth_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.AutoWidthOfShapes();
		}

		private void mnuAutoHeight_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.AutoHeightOfShapes();
		}

		private void mnuCollapseAll_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.CollapseAll();
		}

		private void mnuExpandAll_Click(object sender, EventArgs e)
		{
			if (diagram != null)
				diagram.ExpandAll();
		}

		#endregion
    }
}