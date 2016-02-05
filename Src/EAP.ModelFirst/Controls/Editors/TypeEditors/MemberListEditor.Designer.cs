namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    partial class MemberListEditor
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonGroupBoxMemberList = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.lstMembers = new System.Windows.Forms.ListView();
            this.icon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.access = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.modifier = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dbGenColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dbName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dbType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dbLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dbNotNull = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dbPrimaryKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dbIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dbAutoIncrement = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dbDefaultValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dbComments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolNewField = new System.Windows.Forms.ToolStripButton();
            this.toolNewMethod = new System.Windows.Forms.ToolStripButton();
            this.toolNewConstructor = new System.Windows.Forms.ToolStripButton();
            this.toolNewDestructor = new System.Windows.Forms.ToolStripButton();
            this.toolNewProperty = new System.Windows.Forms.ToolStripButton();
            this.toolNewEvent = new System.Windows.Forms.ToolStripButton();
            this.toolSepAddNew = new System.Windows.Forms.ToolStripSeparator();
            this.toolOverrideList = new System.Windows.Forms.ToolStripButton();
            this.toolImplementList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolSortByKind = new System.Windows.Forms.ToolStripButton();
            this.toolSortByAccess = new System.Windows.Forms.ToolStripButton();
            this.toolSortByName = new System.Windows.Forms.ToolStripButton();
            this.toolSepSorting = new System.Windows.Forms.ToolStripSeparator();
            this.toolMoveUp = new System.Windows.Forms.ToolStripButton();
            this.toolMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolSepMoving = new System.Windows.Forms.ToolStripSeparator();
            this.toolDelete = new System.Windows.Forms.ToolStripButton();
            this.kryptonGroupBoxSchemaInfo = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.schemaEditor = new EAP.ModelFirst.Controls.Editors.TypeEditors.SchemaEditor();
            this.kryptonGroupBoxMember = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.memberEditor = new EAP.ModelFirst.Controls.Editors.TypeEditors.MemberEditor();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxMemberList)).BeginInit();
            this.kryptonGroupBoxMemberList.Panel.SuspendLayout();
            this.kryptonGroupBoxMemberList.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxSchemaInfo)).BeginInit();
            this.kryptonGroupBoxSchemaInfo.Panel.SuspendLayout();
            this.kryptonGroupBoxSchemaInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxMember)).BeginInit();
            this.kryptonGroupBoxMember.Panel.SuspendLayout();
            this.kryptonGroupBoxMember.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonGroupBoxMemberList
            // 
            this.kryptonGroupBoxMemberList.AutoSize = true;
            this.kryptonGroupBoxMemberList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupBoxMemberList.Location = new System.Drawing.Point(0, 216);
            this.kryptonGroupBoxMemberList.Name = "kryptonGroupBoxMemberList";
            // 
            // kryptonGroupBoxMemberList.Panel
            // 
            this.kryptonGroupBoxMemberList.Panel.Controls.Add(this.lstMembers);
            this.kryptonGroupBoxMemberList.Panel.Controls.Add(this.toolStrip);
            this.kryptonGroupBoxMemberList.Size = new System.Drawing.Size(890, 359);
            this.kryptonGroupBoxMemberList.TabIndex = 6;
            this.kryptonGroupBoxMemberList.Text = "Member List";
            this.kryptonGroupBoxMemberList.Values.Heading = "Member List";
            // 
            // lstMembers
            // 
            this.lstMembers.AutoArrange = false;
            this.lstMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.icon,
            this.name,
            this.type,
            this.access,
            this.modifier,
            this.dbGenColumn,
            this.dbName,
            this.dbType,
            this.dbLength,
            this.dbNotNull,
            this.dbPrimaryKey,
            this.dbIndex,
            this.dbAutoIncrement,
            this.dbDefaultValue,
            this.dbComments});
            this.lstMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMembers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lstMembers.FullRowSelect = true;
            this.lstMembers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstMembers.HideSelection = false;
            this.lstMembers.Location = new System.Drawing.Point(0, 25);
            this.lstMembers.MultiSelect = false;
            this.lstMembers.Name = "lstMembers";
            this.lstMembers.ShowGroups = false;
            this.lstMembers.Size = new System.Drawing.Size(886, 314);
            this.lstMembers.TabIndex = 0;
            this.lstMembers.UseCompatibleStateImageBehavior = false;
            this.lstMembers.View = System.Windows.Forms.View.Details;
            this.lstMembers.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstMembers_ItemSelectionChanged);
            this.lstMembers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstMembers_KeyDown);
            // 
            // icon
            // 
            this.icon.Text = global::EAP.ModelFirst.Properties.Strings.Translator;
            this.icon.Width = 20;
            // 
            // name
            // 
            this.name.Text = "Name";
            this.name.Width = 119;
            // 
            // type
            // 
            this.type.Text = "Type";
            this.type.Width = 101;
            // 
            // access
            // 
            this.access.Text = "Access";
            this.access.Width = 102;
            // 
            // modifier
            // 
            this.modifier.Text = "Modifier";
            this.modifier.Width = 99;
            // 
            // dbGenColumn
            // 
            this.dbGenColumn.Text = " DbColumn";
            this.dbGenColumn.Width = 100;
            // 
            // dbName
            // 
            this.dbName.Text = "Column Name";
            this.dbName.Width = 100;
            // 
            // dbType
            // 
            this.dbType.Text = "DbType";
            // 
            // dbLength
            // 
            this.dbLength.Text = "Length";
            // 
            // dbNotNull
            // 
            this.dbNotNull.Text = "NN";
            this.dbNotNull.Width = 50;
            // 
            // dbPrimaryKey
            // 
            this.dbPrimaryKey.Text = "PK";
            this.dbPrimaryKey.Width = 50;
            // 
            // dbIndex
            // 
            this.dbIndex.Text = "IDX";
            this.dbIndex.Width = 50;
            // 
            // dbAutoIncrement
            // 
            this.dbAutoIncrement.Text = "AI";
            this.dbAutoIncrement.Width = 50;
            // 
            // dbDefaultValue
            // 
            this.dbDefaultValue.Text = "Default";
            this.dbDefaultValue.Width = 100;
            // 
            // dbComments
            // 
            this.dbComments.Text = "Comments";
            this.dbComments.Width = 200;
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNewField,
            this.toolNewMethod,
            this.toolNewConstructor,
            this.toolNewDestructor,
            this.toolNewProperty,
            this.toolNewEvent,
            this.toolSepAddNew,
            this.toolOverrideList,
            this.toolImplementList,
            this.toolStripSeparator1,
            this.toolSortByKind,
            this.toolSortByAccess,
            this.toolSortByName,
            this.toolSepSorting,
            this.toolMoveUp,
            this.toolMoveDown,
            this.toolSepMoving,
            this.toolDelete});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(886, 25);
            this.toolStrip.TabIndex = 20;
            this.toolStrip.Text = "ToolStrip";
            // 
            // toolNewField
            // 
            this.toolNewField.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolNewField.Image = global::EAP.ModelFirst.Properties.Resources.Field;
            this.toolNewField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNewField.Name = "toolNewField";
            this.toolNewField.Size = new System.Drawing.Size(23, 22);
            this.toolNewField.Text = "New Field";
            this.toolNewField.Click += new System.EventHandler(this.toolNewField_Click);
            // 
            // toolNewMethod
            // 
            this.toolNewMethod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolNewMethod.Image = global::EAP.ModelFirst.Properties.Resources.Method;
            this.toolNewMethod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNewMethod.Name = "toolNewMethod";
            this.toolNewMethod.Size = new System.Drawing.Size(23, 22);
            this.toolNewMethod.Text = "New Method";
            this.toolNewMethod.Click += new System.EventHandler(this.toolNewMethod_Click);
            // 
            // toolNewConstructor
            // 
            this.toolNewConstructor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolNewConstructor.Image = global::EAP.ModelFirst.Properties.Resources.Constructor;
            this.toolNewConstructor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNewConstructor.Name = "toolNewConstructor";
            this.toolNewConstructor.Size = new System.Drawing.Size(23, 22);
            this.toolNewConstructor.Text = "New Constructor";
            this.toolNewConstructor.Click += new System.EventHandler(this.toolNewConstructor_Click);
            // 
            // toolNewDestructor
            // 
            this.toolNewDestructor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolNewDestructor.Image = global::EAP.ModelFirst.Properties.Resources.Destructor;
            this.toolNewDestructor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNewDestructor.Name = "toolNewDestructor";
            this.toolNewDestructor.Size = new System.Drawing.Size(23, 22);
            this.toolNewDestructor.Text = "New Destructor";
            this.toolNewDestructor.Click += new System.EventHandler(this.toolNewDestructor_Click);
            // 
            // toolNewProperty
            // 
            this.toolNewProperty.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolNewProperty.Image = global::EAP.ModelFirst.Properties.Resources.Property;
            this.toolNewProperty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNewProperty.Name = "toolNewProperty";
            this.toolNewProperty.Size = new System.Drawing.Size(23, 22);
            this.toolNewProperty.Text = "New Property";
            this.toolNewProperty.Click += new System.EventHandler(this.toolNewProperty_Click);
            // 
            // toolNewEvent
            // 
            this.toolNewEvent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolNewEvent.Image = global::EAP.ModelFirst.Properties.Resources.Event;
            this.toolNewEvent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNewEvent.Name = "toolNewEvent";
            this.toolNewEvent.Size = new System.Drawing.Size(23, 22);
            this.toolNewEvent.Text = "New Event";
            this.toolNewEvent.Click += new System.EventHandler(this.toolNewEvent_Click);
            // 
            // toolSepAddNew
            // 
            this.toolSepAddNew.Name = "toolSepAddNew";
            this.toolSepAddNew.Size = new System.Drawing.Size(6, 25);
            // 
            // toolOverrideList
            // 
            this.toolOverrideList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolOverrideList.Image = global::EAP.ModelFirst.Properties.Resources.Overrides;
            this.toolOverrideList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOverrideList.Name = "toolOverrideList";
            this.toolOverrideList.Size = new System.Drawing.Size(23, 22);
            this.toolOverrideList.Text = "Override List";
            this.toolOverrideList.Click += new System.EventHandler(this.toolOverrideList_Click);
            // 
            // toolImplementList
            // 
            this.toolImplementList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolImplementList.Image = global::EAP.ModelFirst.Properties.Resources.Implements;
            this.toolImplementList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolImplementList.Name = "toolImplementList";
            this.toolImplementList.Size = new System.Drawing.Size(23, 22);
            this.toolImplementList.Text = "Interface Implement List";
            this.toolImplementList.Click += new System.EventHandler(this.toolImplementList_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolSortByKind
            // 
            this.toolSortByKind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSortByKind.Image = global::EAP.ModelFirst.Properties.Resources.SortByKind;
            this.toolSortByKind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSortByKind.Name = "toolSortByKind";
            this.toolSortByKind.Size = new System.Drawing.Size(23, 22);
            this.toolSortByKind.Text = "Sort by Kind";
            this.toolSortByKind.Click += new System.EventHandler(this.toolSortByKind_Click);
            // 
            // toolSortByAccess
            // 
            this.toolSortByAccess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSortByAccess.Image = global::EAP.ModelFirst.Properties.Resources.SortByAccess;
            this.toolSortByAccess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSortByAccess.Name = "toolSortByAccess";
            this.toolSortByAccess.Size = new System.Drawing.Size(23, 22);
            this.toolSortByAccess.Text = "Sort by Access";
            this.toolSortByAccess.Click += new System.EventHandler(this.toolSortByAccess_Click);
            // 
            // toolSortByName
            // 
            this.toolSortByName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSortByName.Image = global::EAP.ModelFirst.Properties.Resources.SortByName;
            this.toolSortByName.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSortByName.Name = "toolSortByName";
            this.toolSortByName.Size = new System.Drawing.Size(23, 22);
            this.toolSortByName.Text = "Sort by Name";
            this.toolSortByName.Click += new System.EventHandler(this.toolSortByName_Click);
            // 
            // toolSepSorting
            // 
            this.toolSepSorting.Name = "toolSepSorting";
            this.toolSepSorting.Size = new System.Drawing.Size(6, 25);
            // 
            // toolMoveUp
            // 
            this.toolMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolMoveUp.Image = global::EAP.ModelFirst.Properties.Resources.MoveUp;
            this.toolMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMoveUp.Name = "toolMoveUp";
            this.toolMoveUp.Size = new System.Drawing.Size(23, 22);
            this.toolMoveUp.Text = "Move Up";
            this.toolMoveUp.Click += new System.EventHandler(this.toolMoveUp_Click);
            // 
            // toolMoveDown
            // 
            this.toolMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolMoveDown.Image = global::EAP.ModelFirst.Properties.Resources.MoveDown;
            this.toolMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMoveDown.Name = "toolMoveDown";
            this.toolMoveDown.Size = new System.Drawing.Size(23, 22);
            this.toolMoveDown.Text = "Move Down";
            this.toolMoveDown.Click += new System.EventHandler(this.toolMoveDown_Click);
            // 
            // toolSepMoving
            // 
            this.toolSepMoving.Name = "toolSepMoving";
            this.toolSepMoving.Size = new System.Drawing.Size(6, 25);
            // 
            // toolDelete
            // 
            this.toolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDelete.Image = global::EAP.ModelFirst.Properties.Resources.Delete;
            this.toolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(23, 22);
            this.toolDelete.Text = "Delete";
            this.toolDelete.Click += new System.EventHandler(this.toolDelete_Click);
            // 
            // kryptonGroupBoxSchemaInfo
            // 
            this.kryptonGroupBoxSchemaInfo.AutoSize = true;
            this.kryptonGroupBoxSchemaInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonGroupBoxSchemaInfo.Location = new System.Drawing.Point(0, 132);
            this.kryptonGroupBoxSchemaInfo.Name = "kryptonGroupBoxSchemaInfo";
            // 
            // kryptonGroupBoxSchemaInfo.Panel
            // 
            this.kryptonGroupBoxSchemaInfo.Panel.Controls.Add(this.schemaEditor);
            this.kryptonGroupBoxSchemaInfo.Size = new System.Drawing.Size(890, 84);
            this.kryptonGroupBoxSchemaInfo.TabIndex = 7;
            this.kryptonGroupBoxSchemaInfo.Text = "Schema Info";
            this.kryptonGroupBoxSchemaInfo.Values.Heading = "Schema Info";
            // 
            // schemaEditor
            // 
            this.schemaEditor.AutoSize = true;
            this.schemaEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.schemaEditor.Location = new System.Drawing.Point(0, 0);
            this.schemaEditor.Name = "schemaEditor";
            this.schemaEditor.Size = new System.Drawing.Size(747, 59);
            this.schemaEditor.TabIndex = 0;
            // 
            // kryptonGroupBoxMember
            // 
            this.kryptonGroupBoxMember.AutoSize = true;
            this.kryptonGroupBoxMember.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonGroupBoxMember.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroupBoxMember.Name = "kryptonGroupBoxMember";
            // 
            // kryptonGroupBoxMember.Panel
            // 
            this.kryptonGroupBoxMember.Panel.Controls.Add(this.memberEditor);
            this.kryptonGroupBoxMember.Size = new System.Drawing.Size(890, 132);
            this.kryptonGroupBoxMember.TabIndex = 5;
            this.kryptonGroupBoxMember.Text = "Member Info.";
            this.kryptonGroupBoxMember.Values.Heading = "Member Info.";
            // 
            // memberEditor
            // 
            this.memberEditor.AutoSize = true;
            this.memberEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.memberEditor.Location = new System.Drawing.Point(0, 0);
            this.memberEditor.Name = "memberEditor";
            this.memberEditor.Size = new System.Drawing.Size(734, 107);
            this.memberEditor.TabIndex = 0;
            // 
            // MemberListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonGroupBoxMemberList);
            this.Controls.Add(this.kryptonGroupBoxSchemaInfo);
            this.Controls.Add(this.kryptonGroupBoxMember);
            this.Name = "MemberListEditor";
            this.Size = new System.Drawing.Size(890, 575);
            this.kryptonGroupBoxMemberList.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxMemberList)).EndInit();
            this.kryptonGroupBoxMemberList.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.kryptonGroupBoxSchemaInfo.Panel.ResumeLayout(false);
            this.kryptonGroupBoxSchemaInfo.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxSchemaInfo)).EndInit();
            this.kryptonGroupBoxSchemaInfo.ResumeLayout(false);
            this.kryptonGroupBoxMember.Panel.ResumeLayout(false);
            this.kryptonGroupBoxMember.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxMember)).EndInit();
            this.kryptonGroupBoxMember.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxMemberList;
        private System.Windows.Forms.ListView lstMembers;
        private System.Windows.Forms.ColumnHeader icon;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader access;
        private System.Windows.Forms.ColumnHeader modifier;
        private System.Windows.Forms.ColumnHeader dbType;
        private System.Windows.Forms.ColumnHeader dbLength;
        private System.Windows.Forms.ColumnHeader dbNotNull;
        private System.Windows.Forms.ColumnHeader dbPrimaryKey;
        private System.Windows.Forms.ColumnHeader dbIndex;
        private System.Windows.Forms.ColumnHeader dbAutoIncrement;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolNewField;
        private System.Windows.Forms.ToolStripButton toolNewMethod;
        private System.Windows.Forms.ToolStripButton toolNewConstructor;
        private System.Windows.Forms.ToolStripButton toolNewDestructor;
        private System.Windows.Forms.ToolStripButton toolNewProperty;
        private System.Windows.Forms.ToolStripButton toolNewEvent;
        private System.Windows.Forms.ToolStripButton toolDelete;
        private System.Windows.Forms.ToolStripSeparator toolSepMoving;
        private System.Windows.Forms.ToolStripButton toolMoveDown;
        private System.Windows.Forms.ToolStripButton toolMoveUp;
        private System.Windows.Forms.ToolStripSeparator toolSepSorting;
        private System.Windows.Forms.ToolStripButton toolSortByName;
        private System.Windows.Forms.ToolStripButton toolSortByAccess;
        private System.Windows.Forms.ToolStripButton toolSortByKind;
        private System.Windows.Forms.ToolStripSeparator toolSepAddNew;
        private System.Windows.Forms.ToolStripButton toolOverrideList;
        private System.Windows.Forms.ToolStripButton toolImplementList;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxSchemaInfo;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBoxMember;
        private System.Windows.Forms.ColumnHeader dbGenColumn;
        private System.Windows.Forms.ColumnHeader dbName;
        private System.Windows.Forms.ColumnHeader dbDefaultValue;
        private System.Windows.Forms.ColumnHeader dbComments;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private SchemaEditor schemaEditor;
        private MemberEditor memberEditor;
    }
}
