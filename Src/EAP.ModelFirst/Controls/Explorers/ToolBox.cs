using System.Windows.Forms;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Controls.Editors.DiagramEditor;
using EAP.Win.UI;

namespace EAP.ModelFirst.Controls.Explorers
{
    public partial class ToolBox : ExplorerBase
    {
        #region ListViewItem

        System.Windows.Forms.ListViewGroup typeGroup = new System.Windows.Forms.ListViewGroup("Type", System.Windows.Forms.HorizontalAlignment.Left);
        System.Windows.Forms.ListViewGroup relationshipGroup = new System.Windows.Forms.ListViewGroup("Relationship", System.Windows.Forms.HorizontalAlignment.Left);
        System.Windows.Forms.ListViewItem itemClass = new System.Windows.Forms.ListViewItem("Class", "class");
        System.Windows.Forms.ListViewItem itemStructure = new System.Windows.Forms.ListViewItem("Structure", "struct");
        System.Windows.Forms.ListViewItem itemInterface = new System.Windows.Forms.ListViewItem("Interface", "interface");
        System.Windows.Forms.ListViewItem itemEnum = new System.Windows.Forms.ListViewItem("Enum", "enum");
        System.Windows.Forms.ListViewItem itemDelegate = new System.Windows.Forms.ListViewItem("Delegate", "delegate");
        System.Windows.Forms.ListViewItem itemComment = new System.Windows.Forms.ListViewItem("Comment", "comment");
        System.Windows.Forms.ListViewItem itemAssociation = new System.Windows.Forms.ListViewItem("Association", "association");
        System.Windows.Forms.ListViewItem itemComposition = new System.Windows.Forms.ListViewItem("Composition", "composition");
        System.Windows.Forms.ListViewItem itemAggregation = new System.Windows.Forms.ListViewItem("Aggregation", "aggregation");
        System.Windows.Forms.ListViewItem itemGeneralization = new System.Windows.Forms.ListViewItem("Generalization", "generalization");
        System.Windows.Forms.ListViewItem itemRealization = new System.Windows.Forms.ListViewItem("Realization", "realization");
        System.Windows.Forms.ListViewItem itemDependency = new System.Windows.Forms.ListViewItem("Dependency", "dependency");
        System.Windows.Forms.ListViewItem itemNesting = new System.Windows.Forms.ListViewItem("Nesting", "nesting");
        System.Windows.Forms.ListViewItem itemCommentRel = new System.Windows.Forms.ListViewItem("Comment", "commentRel");

        #endregion

        static ToolBox instance;

        static ToolBox Instance
        {
            get
            {
                if (instance == null)
                    instance = new ToolBox();
                return instance;
            }
        }

        IDocumentItem document;

        public static void SetDocument(IDocumentItem doc)
        {
            if (instance == null) return;
            instance.document = doc;
            instance.InitListView();
        }

        public static void Show(IDockForm dockForm)
        {
            Instance.DockForm = dockForm;
            if (dockForm.DockPanel.ActiveDocument is IDocument)
                SetDocument(((IDocument)dockForm.DockPanel.ActiveDocument).DocumentItem);
            Instance.Show(dockForm.DockPanel);
        }

        protected ToolBox()
        {
            InitializeComponent();
            InitListView();
            UpdateTexts();
        }

        void InitListView()
        {
            IDiagram diagram = document as IDiagram;
            if (diagram == null)
            {
                listView.Groups.Clear();
                listView.Items.Clear();
                return;
            }
            else if (listView.Items.Count > 0)
                return;
            InitDiagramItems();
            listView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
                typeGroup,
                relationshipGroup});

            listView.Items.Add(itemClass);
            if(diagram.ProjectInfo.Language.SupportsStructures)
                listView.Items.Add(itemStructure);
            if (diagram.ProjectInfo.Language.SupportsInterfaces)
                listView.Items.Add(itemInterface);
            if (diagram.ProjectInfo.Language.SupportsEnums)
                listView.Items.Add(itemEnum);
            if (diagram.ProjectInfo.Language.SupportsDelegates)
                listView.Items.Add(itemDelegate);
            listView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
                itemComment,
                itemAssociation,
                itemComposition,
                itemAggregation,
                itemGeneralization,
                itemRealization,
                itemDependency,
                itemNesting,
                itemCommentRel});
        }

        void InitDiagramItems()
        {
            typeGroup.Name = "TypeGroup";
            relationshipGroup.Name = "RelationshipGroup";
            itemClass.Group = typeGroup;
            itemClass.StateImageIndex = 0;
            itemClass.Tag = "Class";
            itemStructure.Group = typeGroup;
            itemStructure.StateImageIndex = 0;
            itemStructure.Tag = "Structure";
            itemInterface.Group = typeGroup;
            itemInterface.StateImageIndex = 0;
            itemInterface.Tag = "Interface";
            itemEnum.Group = typeGroup;
            itemEnum.StateImageIndex = 0;
            itemEnum.Tag = "Enum";
            itemDelegate.Group = typeGroup;
            itemDelegate.StateImageIndex = 0;
            itemDelegate.Tag = "Delegate";
            itemComment.Group = typeGroup;
            itemComment.StateImageIndex = 0;
            itemComment.Tag = "Comment";
            itemAssociation.Group = relationshipGroup;
            itemAssociation.StateImageIndex = 0;
            itemAssociation.Tag = "Association";
            itemComposition.Group = relationshipGroup;
            itemComposition.StateImageIndex = 0;
            itemComposition.Tag = "Composition";
            itemAggregation.Group = relationshipGroup;
            itemAggregation.StateImageIndex = 0;
            itemAggregation.Tag = "Aggregation";
            itemGeneralization.Group = relationshipGroup;
            itemGeneralization.StateImageIndex = 0;
            itemGeneralization.Tag = "Generalization";
            itemRealization.Group = relationshipGroup;
            itemRealization.StateImageIndex = 0;
            itemRealization.Tag = "Realization";
            itemDependency.Group = relationshipGroup;
            itemDependency.StateImageIndex = 0;
            itemDependency.Tag = "Dependency";
            itemNesting.Group = relationshipGroup;
            itemNesting.StateImageIndex = 0;
            itemNesting.Tag = "Nesting";
            itemCommentRel.Group = relationshipGroup;
            itemCommentRel.StateImageIndex = 0;
            itemCommentRel.Tag = "Comment";
        }

        private void listView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem item = e.Item as ListViewItem;
            if ((e.Button == MouseButtons.Left) && (item != null))
            {
                DoDragDrop(item, DragDropEffects.Link);
            }
        }

        public override void UpdateTexts()
        {
            Text = Strings.ToolBox;
            TabText = Strings.ToolBox;

            typeGroup.Header = Strings.Type;
            relationshipGroup.Header = Strings.Relationship;

            itemClass.Text = Strings.Class;
            itemStructure.Text = Strings.Structure;
            itemInterface.Text = Strings.Interface;
            itemEnum.Text = Strings.Enum;
            itemDelegate.Text = Strings.Delegate;
            itemComment.Text = Strings.Comment;
            itemAssociation.Text = Strings.Association;
            itemComposition.Text = Strings.Composition;
            itemAggregation.Text = Strings.Aggregation;
            itemGeneralization.Text = Strings.Generalization;
            itemRealization.Text = Strings.Realization;
            itemDependency.Text = Strings.Dependency;
            itemNesting.Text = Strings.Nesting;
            itemCommentRel.Text = Strings.CommentRelationship;

            itemClass.ToolTipText = Strings.AddNewClass;
            itemStructure.ToolTipText = Strings.AddNewStructure;
            itemInterface.ToolTipText = Strings.AddNewInterface;
            itemEnum.ToolTipText = Strings.AddNewEnum;
            itemDelegate.ToolTipText = Strings.AddNewDelegate;
            itemComment.ToolTipText = Strings.AddNewComment;
            itemAssociation.ToolTipText = Strings.AddNewAssociation;
            itemComposition.ToolTipText = Strings.AddNewComposition;
            itemAggregation.ToolTipText = Strings.AddNewAggregation;
            itemGeneralization.ToolTipText = Strings.AddNewGeneralization;
            itemRealization.ToolTipText = Strings.AddNewRealization;
            itemDependency.ToolTipText = Strings.AddNewDependency;
            itemNesting.ToolTipText = Strings.AddNewNesting;
            itemCommentRel.ToolTipText = Strings.AddNewCommentRelationship;
            base.UpdateTexts();
        }
    }
}
