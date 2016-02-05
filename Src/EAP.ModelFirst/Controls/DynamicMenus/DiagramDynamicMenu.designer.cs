namespace EAP.ModelFirst.Controls.DynamicMenu
{
	partial class DiagramDynamicMenu
	{
		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.mnuDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddNewElement = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewClass = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewStructure = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewInterface = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewEnum = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewDelegate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewComment = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuNewAssociation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewComposition = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewAggregation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewGeneralization = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewRealization = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewDependency = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewNesting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewCommentRelationship = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMembersFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowType = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowParameters = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowParameterNames = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowInitialValue = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGenerateCode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAsImage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAlign = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAlignTop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAlignLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAlignBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAlignRight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAlignHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAlignVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMakeSameSize = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSameWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSameHeight = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSameSize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAutoSize = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAutoWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAutoHeight = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAutoLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDiagram,
            this.mnuFormat});
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "menuStrip";
            this.Size = new System.Drawing.Size(530, 24);
            this.TabIndex = 0;
            this.Text = "menuStrip";
            // 
            // mnuDiagram
            // 
            this.mnuDiagram.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddNewElement,
            this.mnuMembersFormat,
            this.toolStripSeparator1,
            this.mnuGenerateCode,
            this.mnuSaveAsImage});
            this.mnuDiagram.Name = "mnuDiagram";
            this.mnuDiagram.Size = new System.Drawing.Size(69, 20);
            this.mnuDiagram.Text = "&Diagram";
            this.mnuDiagram.DropDownOpening += new System.EventHandler(this.mnuDiagram_DropDownOpening);
            // 
            // mnuAddNewElement
            // 
            this.mnuAddNewElement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewClass,
            this.mnuNewStructure,
            this.mnuNewInterface,
            this.mnuNewEnum,
            this.mnuNewDelegate,
            this.mnuNewComment,
            this.toolStripSeparator2,
            this.mnuNewAssociation,
            this.mnuNewComposition,
            this.mnuNewAggregation,
            this.mnuNewGeneralization,
            this.mnuNewRealization,
            this.mnuNewDependency,
            this.mnuNewNesting,
            this.mnuNewCommentRelationship});
            this.mnuAddNewElement.Image = global::EAP.ModelFirst.Properties.Resources.NewEntity;
            this.mnuAddNewElement.Name = "mnuAddNewElement";
            this.mnuAddNewElement.Size = new System.Drawing.Size(178, 22);
            this.mnuAddNewElement.Text = "&Add New";
            // 
            // mnuNewClass
            // 
            this.mnuNewClass.Image = global::EAP.ModelFirst.Properties.Resources.Class;
            this.mnuNewClass.Name = "mnuNewClass";
            this.mnuNewClass.Size = new System.Drawing.Size(205, 22);
            this.mnuNewClass.Text = "&Class";
            this.mnuNewClass.Click += new System.EventHandler(this.mnuNewClass_Click);
            // 
            // mnuNewStructure
            // 
            this.mnuNewStructure.Image = global::EAP.ModelFirst.Properties.Resources.Structure;
            this.mnuNewStructure.Name = "mnuNewStructure";
            this.mnuNewStructure.Size = new System.Drawing.Size(205, 22);
            this.mnuNewStructure.Text = "&Structure";
            this.mnuNewStructure.Click += new System.EventHandler(this.mnuNewStructure_Click);
            // 
            // mnuNewInterface
            // 
            this.mnuNewInterface.Image = global::EAP.ModelFirst.Properties.Resources.Interface32;
            this.mnuNewInterface.Name = "mnuNewInterface";
            this.mnuNewInterface.Size = new System.Drawing.Size(205, 22);
            this.mnuNewInterface.Text = "&Interface";
            this.mnuNewInterface.Click += new System.EventHandler(this.mnuNewInterface_Click);
            // 
            // mnuNewEnum
            // 
            this.mnuNewEnum.Image = global::EAP.ModelFirst.Properties.Resources.Enum;
            this.mnuNewEnum.Name = "mnuNewEnum";
            this.mnuNewEnum.Size = new System.Drawing.Size(205, 22);
            this.mnuNewEnum.Text = "&Enum";
            this.mnuNewEnum.Click += new System.EventHandler(this.mnuNewEnum_Click);
            // 
            // mnuNewDelegate
            // 
            this.mnuNewDelegate.Image = global::EAP.ModelFirst.Properties.Resources.Delegate;
            this.mnuNewDelegate.Name = "mnuNewDelegate";
            this.mnuNewDelegate.Size = new System.Drawing.Size(205, 22);
            this.mnuNewDelegate.Text = "&Delegate";
            this.mnuNewDelegate.Click += new System.EventHandler(this.mnuNewDelegate_Click);
            // 
            // mnuNewComment
            // 
            this.mnuNewComment.Image = global::EAP.ModelFirst.Properties.Resources.Comment;
            this.mnuNewComment.Name = "mnuNewComment";
            this.mnuNewComment.Size = new System.Drawing.Size(205, 22);
            this.mnuNewComment.Text = "Commen&t";
            this.mnuNewComment.Click += new System.EventHandler(this.mnuNewComment_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(202, 6);
            // 
            // mnuNewAssociation
            // 
            this.mnuNewAssociation.Image = global::EAP.ModelFirst.Properties.Resources.Association;
            this.mnuNewAssociation.Name = "mnuNewAssociation";
            this.mnuNewAssociation.Size = new System.Drawing.Size(205, 22);
            this.mnuNewAssociation.Text = "&Association";
            this.mnuNewAssociation.Click += new System.EventHandler(this.mnuNewAssociation_Click);
            // 
            // mnuNewComposition
            // 
            this.mnuNewComposition.Image = global::EAP.ModelFirst.Properties.Resources.Composition;
            this.mnuNewComposition.Name = "mnuNewComposition";
            this.mnuNewComposition.Size = new System.Drawing.Size(205, 22);
            this.mnuNewComposition.Text = "C&omposition";
            this.mnuNewComposition.Click += new System.EventHandler(this.mnuNewComposition_Click);
            // 
            // mnuNewAggregation
            // 
            this.mnuNewAggregation.Image = global::EAP.ModelFirst.Properties.Resources.Aggregation;
            this.mnuNewAggregation.Name = "mnuNewAggregation";
            this.mnuNewAggregation.Size = new System.Drawing.Size(205, 22);
            this.mnuNewAggregation.Text = "A&ggregation";
            this.mnuNewAggregation.Click += new System.EventHandler(this.mnuNewAggregation_Click);
            // 
            // mnuNewGeneralization
            // 
            this.mnuNewGeneralization.Image = global::EAP.ModelFirst.Properties.Resources.Generalization;
            this.mnuNewGeneralization.Name = "mnuNewGeneralization";
            this.mnuNewGeneralization.Size = new System.Drawing.Size(205, 22);
            this.mnuNewGeneralization.Text = "Genera&lization";
            this.mnuNewGeneralization.Click += new System.EventHandler(this.mnuNewGeneralization_Click);
            // 
            // mnuNewRealization
            // 
            this.mnuNewRealization.Image = global::EAP.ModelFirst.Properties.Resources.Realization;
            this.mnuNewRealization.Name = "mnuNewRealization";
            this.mnuNewRealization.Size = new System.Drawing.Size(205, 22);
            this.mnuNewRealization.Text = "&Realization";
            this.mnuNewRealization.Click += new System.EventHandler(this.mnuNewRealization_Click);
            // 
            // mnuNewDependency
            // 
            this.mnuNewDependency.Image = global::EAP.ModelFirst.Properties.Resources.Dependency;
            this.mnuNewDependency.Name = "mnuNewDependency";
            this.mnuNewDependency.Size = new System.Drawing.Size(205, 22);
            this.mnuNewDependency.Text = "&Dependency";
            this.mnuNewDependency.Click += new System.EventHandler(this.mnuNewDependency_Click);
            // 
            // mnuNewNesting
            // 
            this.mnuNewNesting.Image = global::EAP.ModelFirst.Properties.Resources.Nesting;
            this.mnuNewNesting.Name = "mnuNewNesting";
            this.mnuNewNesting.Size = new System.Drawing.Size(205, 22);
            this.mnuNewNesting.Text = "&Nesting";
            this.mnuNewNesting.Click += new System.EventHandler(this.mnuNewNesting_Click);
            // 
            // mnuNewCommentRelationship
            // 
            this.mnuNewCommentRelationship.Image = global::EAP.ModelFirst.Properties.Resources.CommentRel;
            this.mnuNewCommentRelationship.Name = "mnuNewCommentRelationship";
            this.mnuNewCommentRelationship.Size = new System.Drawing.Size(205, 22);
            this.mnuNewCommentRelationship.Text = "Co&mment Relationship";
            this.mnuNewCommentRelationship.Click += new System.EventHandler(this.mnuNewCommentRelationship_Click);
            // 
            // mnuMembersFormat
            // 
            this.mnuMembersFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowType,
            this.mnuShowParameters,
            this.mnuShowParameterNames,
            this.mnuShowInitialValue});
            this.mnuMembersFormat.Image = global::EAP.ModelFirst.Properties.Resources.Format;
            this.mnuMembersFormat.Name = "mnuMembersFormat";
            this.mnuMembersFormat.Size = new System.Drawing.Size(178, 22);
            this.mnuMembersFormat.Text = "&Member\'s Format";
            this.mnuMembersFormat.DropDownOpening += new System.EventHandler(this.mnuMembersFormat_DropDownOpening);
            // 
            // mnuShowType
            // 
            this.mnuShowType.CheckOnClick = true;
            this.mnuShowType.Name = "mnuShowType";
            this.mnuShowType.Size = new System.Drawing.Size(179, 22);
            this.mnuShowType.Text = "&Type";
            this.mnuShowType.Click += new System.EventHandler(this.mnuShowType_Click);
            // 
            // mnuShowParameters
            // 
            this.mnuShowParameters.CheckOnClick = true;
            this.mnuShowParameters.Name = "mnuShowParameters";
            this.mnuShowParameters.Size = new System.Drawing.Size(179, 22);
            this.mnuShowParameters.Text = "&Parameters";
            this.mnuShowParameters.Click += new System.EventHandler(this.mnuShowParameters_Click);
            // 
            // mnuShowParameterNames
            // 
            this.mnuShowParameterNames.CheckOnClick = true;
            this.mnuShowParameterNames.Name = "mnuShowParameterNames";
            this.mnuShowParameterNames.Size = new System.Drawing.Size(179, 22);
            this.mnuShowParameterNames.Text = "Parameter &Names";
            this.mnuShowParameterNames.Click += new System.EventHandler(this.mnuShowParameterNames_Click);
            // 
            // mnuShowInitialValue
            // 
            this.mnuShowInitialValue.CheckOnClick = true;
            this.mnuShowInitialValue.Name = "mnuShowInitialValue";
            this.mnuShowInitialValue.Size = new System.Drawing.Size(179, 22);
            this.mnuShowInitialValue.Text = "&Initial Value";
            this.mnuShowInitialValue.Click += new System.EventHandler(this.mnuShowInitialValue_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // mnuGenerateCode
            // 
            this.mnuGenerateCode.Image = global::EAP.ModelFirst.Properties.Resources.CodeGenerator;
            this.mnuGenerateCode.Name = "mnuGenerateCode";
            this.mnuGenerateCode.Size = new System.Drawing.Size(178, 22);
            this.mnuGenerateCode.Text = "&Generate Code...";
            this.mnuGenerateCode.Click += new System.EventHandler(this.mnuGenerateCode_Click);
            // 
            // mnuSaveAsImage
            // 
            this.mnuSaveAsImage.Image = global::EAP.ModelFirst.Properties.Resources.Image;
            this.mnuSaveAsImage.Name = "mnuSaveAsImage";
            this.mnuSaveAsImage.Size = new System.Drawing.Size(178, 22);
            this.mnuSaveAsImage.Text = "&Save As Image...";
            this.mnuSaveAsImage.Click += new System.EventHandler(this.mnuSaveAsImage_Click);
            // 
            // mnuFormat
            // 
            this.mnuFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAlign,
            this.mnuMakeSameSize,
            this.toolStripSeparator4,
            this.mnuAutoSize,
            this.mnuAutoWidth,
            this.mnuAutoHeight,
            this.mnuAutoLayout,
            this.toolStripSeparator5,
            this.mnuCollapseAll,
            this.mnuExpandAll});
            this.mnuFormat.Name = "mnuFormat";
            this.mnuFormat.Size = new System.Drawing.Size(60, 20);
            this.mnuFormat.Text = "F&ormat";
            this.mnuFormat.DropDownOpening += new System.EventHandler(this.mnuFormat_DropDownOpening);
            // 
            // mnuAlign
            // 
            this.mnuAlign.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAlignTop,
            this.mnuAlignLeft,
            this.mnuAlignBottom,
            this.mnuAlignRight,
            this.toolStripSeparator3,
            this.mnuAlignHorizontal,
            this.mnuAlignVertical});
            this.mnuAlign.Name = "mnuAlign";
            this.mnuAlign.Size = new System.Drawing.Size(170, 22);
            this.mnuAlign.Text = "&Align";
            // 
            // mnuAlignTop
            // 
            this.mnuAlignTop.Image = global::EAP.ModelFirst.Properties.Resources.AlignTop;
            this.mnuAlignTop.Name = "mnuAlignTop";
            this.mnuAlignTop.Size = new System.Drawing.Size(209, 22);
            this.mnuAlignTop.Text = "Align &Top";
            this.mnuAlignTop.Click += new System.EventHandler(this.mnuAlignTop_Click);
            // 
            // mnuAlignLeft
            // 
            this.mnuAlignLeft.Image = global::EAP.ModelFirst.Properties.Resources.AlignLeft;
            this.mnuAlignLeft.Name = "mnuAlignLeft";
            this.mnuAlignLeft.Size = new System.Drawing.Size(209, 22);
            this.mnuAlignLeft.Text = "Align &Left";
            this.mnuAlignLeft.Click += new System.EventHandler(this.mnuAlignLeft_Click);
            // 
            // mnuAlignBottom
            // 
            this.mnuAlignBottom.Image = global::EAP.ModelFirst.Properties.Resources.AlignBottom;
            this.mnuAlignBottom.Name = "mnuAlignBottom";
            this.mnuAlignBottom.Size = new System.Drawing.Size(209, 22);
            this.mnuAlignBottom.Text = "Align &Bottom";
            this.mnuAlignBottom.Click += new System.EventHandler(this.mnuAlignBottom_Click);
            // 
            // mnuAlignRight
            // 
            this.mnuAlignRight.Image = global::EAP.ModelFirst.Properties.Resources.AlignRight;
            this.mnuAlignRight.Name = "mnuAlignRight";
            this.mnuAlignRight.Size = new System.Drawing.Size(209, 22);
            this.mnuAlignRight.Text = "Align &Right";
            this.mnuAlignRight.Click += new System.EventHandler(this.mnuAlignRight_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(206, 6);
            // 
            // mnuAlignHorizontal
            // 
            this.mnuAlignHorizontal.Image = global::EAP.ModelFirst.Properties.Resources.AlignHorizontal;
            this.mnuAlignHorizontal.Name = "mnuAlignHorizontal";
            this.mnuAlignHorizontal.Size = new System.Drawing.Size(209, 22);
            this.mnuAlignHorizontal.Text = "Align &Horizontal Center";
            this.mnuAlignHorizontal.Click += new System.EventHandler(this.mnuAlignHorizontal_Click);
            // 
            // mnuAlignVertical
            // 
            this.mnuAlignVertical.Image = global::EAP.ModelFirst.Properties.Resources.AlignVertical;
            this.mnuAlignVertical.Name = "mnuAlignVertical";
            this.mnuAlignVertical.Size = new System.Drawing.Size(209, 22);
            this.mnuAlignVertical.Text = "Align &Vertical Center";
            this.mnuAlignVertical.Click += new System.EventHandler(this.mnuAlignVertical_Click);
            // 
            // mnuMakeSameSize
            // 
            this.mnuMakeSameSize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSameWidth,
            this.mnuSameHeight,
            this.mnuSameSize});
            this.mnuMakeSameSize.Name = "mnuMakeSameSize";
            this.mnuMakeSameSize.Size = new System.Drawing.Size(170, 22);
            this.mnuMakeSameSize.Text = "&Make Same Size";
            // 
            // mnuSameWidth
            // 
            this.mnuSameWidth.Name = "mnuSameWidth";
            this.mnuSameWidth.Size = new System.Drawing.Size(212, 22);
            this.mnuSameWidth.Text = "Same &Width";
            this.mnuSameWidth.Click += new System.EventHandler(this.mnuSameWidth_Click);
            // 
            // mnuSameHeight
            // 
            this.mnuSameHeight.Name = "mnuSameHeight";
            this.mnuSameHeight.Size = new System.Drawing.Size(212, 22);
            this.mnuSameHeight.Text = "Same &Height";
            this.mnuSameHeight.Click += new System.EventHandler(this.mnuSameHeight_Click);
            // 
            // mnuSameSize
            // 
            this.mnuSameSize.Name = "mnuSameSize";
            this.mnuSameSize.Size = new System.Drawing.Size(212, 22);
            this.mnuSameSize.Text = "&Same Width and Height";
            this.mnuSameSize.Click += new System.EventHandler(this.mnuSameSize_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(167, 6);
            // 
            // mnuAutoSize
            // 
            this.mnuAutoSize.Name = "mnuAutoSize";
            this.mnuAutoSize.Size = new System.Drawing.Size(170, 22);
            this.mnuAutoSize.Text = "Auto &Size";
            this.mnuAutoSize.Click += new System.EventHandler(this.mnuAutoSize_Click);
            // 
            // mnuAutoWidth
            // 
            this.mnuAutoWidth.Name = "mnuAutoWidth";
            this.mnuAutoWidth.Size = new System.Drawing.Size(170, 22);
            this.mnuAutoWidth.Text = "Auto &Width";
            this.mnuAutoWidth.Click += new System.EventHandler(this.mnuAutoWidth_Click);
            // 
            // mnuAutoHeight
            // 
            this.mnuAutoHeight.Name = "mnuAutoHeight";
            this.mnuAutoHeight.Size = new System.Drawing.Size(170, 22);
            this.mnuAutoHeight.Text = "Auto &Height";
            this.mnuAutoHeight.Click += new System.EventHandler(this.mnuAutoHeight_Click);
            // 
            // mnuAutoLayout
            // 
            this.mnuAutoLayout.Image = global::EAP.ModelFirst.Properties.Resources.AutoLayout;
            this.mnuAutoLayout.Name = "mnuAutoLayout";
            this.mnuAutoLayout.Size = new System.Drawing.Size(170, 22);
            this.mnuAutoLayout.Text = "Auto &Layout";
            this.mnuAutoLayout.Visible = false;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(167, 6);
            // 
            // mnuCollapseAll
            // 
            this.mnuCollapseAll.Image = global::EAP.ModelFirst.Properties.Resources.CollapseAll;
            this.mnuCollapseAll.Name = "mnuCollapseAll";
            this.mnuCollapseAll.Size = new System.Drawing.Size(170, 22);
            this.mnuCollapseAll.Text = "&Collapse All";
            this.mnuCollapseAll.Click += new System.EventHandler(this.mnuCollapseAll_Click);
            // 
            // mnuExpandAll
            // 
            this.mnuExpandAll.Image = global::EAP.ModelFirst.Properties.Resources.ExpandAll;
            this.mnuExpandAll.Name = "mnuExpandAll";
            this.mnuExpandAll.Size = new System.Drawing.Size(170, 22);
            this.mnuExpandAll.Text = "&Expand All";
            this.mnuExpandAll.Click += new System.EventHandler(this.mnuExpandAll_Click);
            
            this.ResumeLayout(false);
            this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.ToolStripMenuItem mnuAddNewElement;
		private System.Windows.Forms.ToolStripMenuItem mnuNewClass;
		private System.Windows.Forms.ToolStripMenuItem mnuNewStructure;
		private System.Windows.Forms.ToolStripMenuItem mnuNewInterface;
		private System.Windows.Forms.ToolStripMenuItem mnuNewEnum;
		private System.Windows.Forms.ToolStripMenuItem mnuNewDelegate;
		private System.Windows.Forms.ToolStripMenuItem mnuNewComment;
		private System.Windows.Forms.ToolStripMenuItem mnuNewAssociation;
		private System.Windows.Forms.ToolStripMenuItem mnuNewComposition;
		private System.Windows.Forms.ToolStripMenuItem mnuNewAggregation;
		private System.Windows.Forms.ToolStripMenuItem mnuNewGeneralization;
		private System.Windows.Forms.ToolStripMenuItem mnuNewRealization;
		private System.Windows.Forms.ToolStripMenuItem mnuNewDependency;
		private System.Windows.Forms.ToolStripMenuItem mnuNewNesting;
		private System.Windows.Forms.ToolStripMenuItem mnuNewCommentRelationship;
		private System.Windows.Forms.ToolStripMenuItem mnuMembersFormat;
		private System.Windows.Forms.ToolStripMenuItem mnuShowType;
		private System.Windows.Forms.ToolStripMenuItem mnuShowParameters;
		private System.Windows.Forms.ToolStripMenuItem mnuShowParameterNames;
		private System.Windows.Forms.ToolStripMenuItem mnuShowInitialValue;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveAsImage;
		private System.Windows.Forms.ToolStripMenuItem mnuAlign;
		private System.Windows.Forms.ToolStripMenuItem mnuAlignTop;
		private System.Windows.Forms.ToolStripMenuItem mnuAlignLeft;
		private System.Windows.Forms.ToolStripMenuItem mnuAlignBottom;
		private System.Windows.Forms.ToolStripMenuItem mnuAlignRight;
		private System.Windows.Forms.ToolStripMenuItem mnuAlignHorizontal;
		private System.Windows.Forms.ToolStripMenuItem mnuAlignVertical;
		private System.Windows.Forms.ToolStripMenuItem mnuMakeSameSize;
		private System.Windows.Forms.ToolStripMenuItem mnuSameWidth;
		private System.Windows.Forms.ToolStripMenuItem mnuSameHeight;
		private System.Windows.Forms.ToolStripMenuItem mnuSameSize;
		private System.Windows.Forms.ToolStripMenuItem mnuAutoWidth;
		private System.Windows.Forms.ToolStripMenuItem mnuAutoHeight;
		private System.Windows.Forms.ToolStripMenuItem mnuAutoLayout;
		private System.Windows.Forms.ToolStripMenuItem mnuCollapseAll;
		private System.Windows.Forms.ToolStripMenuItem mnuExpandAll;
		private System.Windows.Forms.ToolStripMenuItem mnuDiagram;
		private System.Windows.Forms.ToolStripMenuItem mnuFormat;
		private System.Windows.Forms.ToolStripMenuItem mnuGenerateCode;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem mnuAutoSize;
	}
}