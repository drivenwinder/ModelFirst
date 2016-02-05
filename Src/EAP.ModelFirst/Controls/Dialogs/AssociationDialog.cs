using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Relationships;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;

namespace EAP.ModelFirst.Controls.Dialogs
{
    public partial class AssociationDialog : DialogForm
    {
        const int ArrowWidth = 18;
        const int ArrowHeight = 10;
        const int DiamondWidth = 20;
        const int DiamondHeight = 10;

        AssociationRelationship association = null;
        Direction modifiedDirection;
        AssociationType modifiedType;

        public AssociationDialog()
        {
            InitializeComponent();
            UpdateTexts();
        }

        public AssociationRelationship Association
        {
            get
            {
                return association;
            }
            set
            {
                if (value != null)
                {
                    association = value;
                    UpdateFields();
                }
            }
        }

        private void UpdateTexts()
        {
            this.Text = Strings.EditAssociation;
            btnOK.Text = Strings.ButtonOK;
            btnCancel.Text = Strings.ButtonCancel;
        }

        private void UpdateFields()
        {
            var first = (association.First as CompositeType);
            if (first != null)
            {
                foreach (var f in first.Fields)
                    cbxStartRole.Items.Add(f.Name);
                foreach (var p in first.Operations)
                    if (p is Property)
                        cbxStartRole.Items.Add(p.Name);
            }

            var second = (association.Second as CompositeType);
            if (second != null)
            {
                foreach (var f in second.Fields)
                    cbxEndRole.Items.Add(f.Name);
                foreach (var p in second.Operations)
                    if (p is Property)
                        cbxEndRole.Items.Add(p.Name);
            }


            cbxStartRole.Text = association.StartRole;
            cbxEndRole.Text = association.EndRole;

            modifiedDirection = association.Direction;
            modifiedType = association.AssociationType;

            txtName.Text = association.Label;

            cbxStartMultiplicity.SelectedItem = association.StartMultiplicity;
            cbxEndMultiplicity.SelectedItem = association.EndMultiplicity;
        }

        private void ModifyRelationship()
        {
            association.AssociationType = modifiedType;
            association.Direction = modifiedDirection;
            association.Label = txtName.Text;
            association.StartRole = cbxStartRole.Text;
            association.EndRole = cbxEndRole.Text;
            association.StartMultiplicity = cbxStartMultiplicity.Text;
            association.EndMultiplicity = cbxEndMultiplicity.Text;
        }

        private void picArrow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X <= DiamondWidth)
            {
                ChangeType();
                picArrow.Invalidate();
            }
            else if (e.X >= picArrow.Width - ArrowWidth)
            {
                ChangeHead();
                picArrow.Invalidate();
            }
        }

        private void ChangeType()
        {
            if (modifiedType == AssociationType.Association)
            {
                modifiedType = AssociationType.Aggregation;
            }
            else if (modifiedType == AssociationType.Aggregation)
            {
                modifiedType = AssociationType.Composition;
            }
            else
            {
                modifiedType = AssociationType.Association;
            }
        }

        private void ChangeHead()
        {
            if (modifiedDirection == Direction.Bidirectional)
            {
                modifiedDirection = Direction.Unidirectional;
            }
            else
            {
                modifiedDirection = Direction.Bidirectional;
            }
        }

        private void picArrow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int center = picArrow.Height / 2;
            int width = picArrow.Width;

            // Draw line
            g.DrawLine(Pens.Black, 0, center, width, center);

            // Draw arrow head
            if (modifiedDirection == Direction.Unidirectional)
            {
                g.DrawLine(Pens.Black, width - ArrowWidth, center - ArrowHeight / 2, width, center);
                g.DrawLine(Pens.Black, width - ArrowWidth, center + ArrowHeight / 2, width, center);
            }

            // Draw start symbol
            if (modifiedType != AssociationType.Association)
            {
                Point[] diamondPoints =  {
					new Point(0, center),
					new Point(DiamondWidth / 2, center - DiamondHeight / 2),
					new Point(DiamondWidth, center),
					new Point(DiamondWidth / 2, center + DiamondHeight / 2)
				};

                if (modifiedType == AssociationType.Aggregation)
                {
                    g.FillPolygon(Brushes.White, diamondPoints);
                    g.DrawPolygon(Pens.Black, diamondPoints);
                }
                else if (modifiedType == AssociationType.Composition)
                {
                    g.FillPolygon(Brushes.Black, diamondPoints);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (association != null)
            {
                ModifyRelationship();
            }
        }
    }
}
