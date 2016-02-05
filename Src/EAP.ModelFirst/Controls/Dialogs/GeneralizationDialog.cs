using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Project.Relationships;
using EAP.ModelFirst.Properties;
using EAP.Collections;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Controls.Dialogs
{
    public partial class GeneralizationDialog : DialogForm
    {
        GeneralizationRelationship relationShip = null;

        public GeneralizationDialog()
        {
            InitializeComponent();
            cbxStrategy.Items.Add(new ValueTextPair(InheritanceStrategy.Subclass, Strings.InheritanceStrategySubclass));
            cbxStrategy.Items.Add(new ValueTextPair(InheritanceStrategy.Joined, Strings.InheritanceStrategyJoined));
            cbxStrategy.Items.Add(new ValueTextPair(InheritanceStrategy.Union, Strings.InheritanceStrategyUnion));
            cbxStrategy.SelectedIndex = 0;
            UpdateTexts();
        }

        public GeneralizationRelationship Generalization
        {
            get
            {
                return relationShip;
            }
            set
            {
                if (value != null)
                {
                    relationShip = value;
                    var s = (relationShip.Second as ClassType).InheritanceStrategy;
                    cbxStrategy.SelectedItem = cbxStrategy.Items.Cast<ValueTextPair>()
                        .FirstOrDefault(p => (InheritanceStrategy)p.Value == s);
                }
            }
        }

        private void UpdateTexts()
        {
            this.Text = Strings.EditGeneralization;
            lblStrategy.Text = Strings.InheritanceStrategy;
            btnOK.Text = Strings.ButtonOK;
            btnCancel.Text = Strings.ButtonCancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (relationShip != null)
                (relationShip.Second as ClassType).InheritanceStrategy = (InheritanceStrategy)((ValueTextPair)cbxStrategy.SelectedItem).Value;
        }
    }
}
