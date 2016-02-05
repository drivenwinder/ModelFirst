﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.Editors.FloatingEditors
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class EnumEditor : EditorWindow, ILocalizable
	{
		static readonly string newValueText = "« " + Strings.NewValue + " »";

		EnumShape shape = null;
		bool needValidation = false;
		bool noNewValue = true;

        class EnumEditorHandler : TypeEditorHandler
        {
            EnumEditor editor { get { return Window as EnumEditor; } }

            internal override void Init(DiagramElement element)
            {
                editor.shape = (EnumShape)element;
                editor.txtNewValue.Text = newValueText;
                editor.noNewValue = true;
                editor.RefreshValues();
            }

            public override void ValidateData()
            {
                editor.ValidateName();
                editor.SetError(null);
            }
        }

		public EnumEditor()
            : base(new EnumEditorHandler())
		{
			InitializeComponent();
			toolStrip.Renderer = ToolStripSimplifiedRenderer.Default;
			UpdateTexts();
		}

		public void UpdateTexts()
		{
			toolNewValue.ToolTipText = Strings.NewValue;
		}

		private void RefreshValues()
		{
			EnumType type = shape.EnumType;
            Language language = type.Language;
			SuspendLayout();

			int cursorPosition = txtName.SelectionStart;
			txtName.Text = type.Name;
			txtName.SelectionStart = cursorPosition;

			SetError(null);
			needValidation = false;

			RefreshVisibility();
			ResumeLayout();
		}

		private void RefreshVisibility()
		{
            Language language = shape.EnumType.Language;
			EnumType type = shape.EnumType;

			toolVisibility.Image = Icons.GetImage(type);
			toolVisibility.Text = language.ValidAccessModifiers[type.AccessModifier];

			// Public
			if (language.ValidAccessModifiers.ContainsKey(AccessModifier.Public))
			{
				toolPublic.Visible = true;
				toolPublic.Text = language.ValidAccessModifiers[AccessModifier.Public];
			}
			else
			{
				toolPublic.Visible = false;
			}
			// Protected Internal
			if (type.IsNested && language.ValidAccessModifiers.ContainsKey(AccessModifier.ProtectedInternal))
			{
				toolProtint.Visible = true;
				toolProtint.Text = language.ValidAccessModifiers[AccessModifier.ProtectedInternal];
			}
			else
			{
				toolProtint.Visible = false;
			}
			// Internal
			if (language.ValidAccessModifiers.ContainsKey(AccessModifier.Internal))
			{
				toolInternal.Visible = true;
				toolInternal.Text = language.ValidAccessModifiers[AccessModifier.Internal];
			}
			else
			{
				toolInternal.Visible = false;
			}
			// Protected
			if (type.IsNested && language.ValidAccessModifiers.ContainsKey(AccessModifier.Protected))
			{
				toolProtected.Visible = true;
				toolProtected.Text = language.ValidAccessModifiers[AccessModifier.Protected];
			}
			else
			{
				toolProtected.Visible = false;
			}
			// Private
			if (type.IsNested && language.ValidAccessModifiers.ContainsKey(AccessModifier.Private))
			{
				toolPrivate.Visible = true;
				toolPrivate.Text = language.ValidAccessModifiers[AccessModifier.Private];
			}
			else
			{
				toolPrivate.Visible = false;
			}
			// Default
			if (language.ValidAccessModifiers.ContainsKey(AccessModifier.Default))
			{
				toolDefault.Visible = true;
				toolDefault.Text = language.ValidAccessModifiers[AccessModifier.Default];
			}
			else
			{
				toolDefault.Visible = false;
			}
		}

		private bool ValidateName()
		{
			if (needValidation)
			{
				try
				{
					shape.EnumType.Name = txtName.Text;
					RefreshValues();
				}
				catch (BadSyntaxException ex)
				{
					SetError(ex.Message);
					return false;
				}
			}
			return true;
		}

		private void SetError(string message)
		{
			if (MonoHelper.IsRunningOnMono && MonoHelper.IsOlderVersionThan("2.4"))
				return;

			errorProvider.SetError(this, message);
		}

		private void AddNewValue()
		{
			if (!noNewValue && ValidateName())
			{
				try
				{
					shape.EnumType.AddValue(txtNewValue.Text);
					ClearNewValueField();
				}
				catch (BadSyntaxException ex)
				{
					SetError(ex.Message);
				}
			}
		}

		private void ClearNewValueField()
		{
			SetError(null);
			txtNewValue.Text = string.Empty;
		}

		private void ChangeAccess(AccessModifier access)
		{
			if (ValidateName())
			{
				try
				{
					shape.EnumType.AccessModifier = access;
					RefreshValues();
				}
				catch (BadSyntaxException ex)
				{
					RefreshValues();
					SetError(ex.Message);
				}
			}
		}

		private void toolPublic_Click(object sender, EventArgs e)
		{
			ChangeAccess(AccessModifier.Public);
		}

		private void toolProtint_Click(object sender, EventArgs e)
		{
			ChangeAccess(AccessModifier.ProtectedInternal);
		}

		private void toolInternal_Click(object sender, EventArgs e)
		{
			ChangeAccess(AccessModifier.Internal);
		}

		private void toolProtected_Click(object sender, EventArgs e)
		{
			ChangeAccess(AccessModifier.Protected);
		}

		private void toolPrivate_Click(object sender, EventArgs e)
		{
			ChangeAccess(AccessModifier.Private);
		}

		private void toolDefault_Click(object sender, EventArgs e)
		{
			ChangeAccess(AccessModifier.Default);
		}

		private void txtNewValue_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					AddNewValue();
					e.Handled = true;
					break;

				case Keys.Escape:
					needValidation = false;
					shape.HideEditor();
					e.Handled = true;
					break;

				case Keys.Tab:
					txtName.Focus();
					e.Handled = true;
					break;
			}
		}

		private void txtNewValue_TextChanged(object sender, EventArgs e)
		{
			noNewValue = (txtNewValue.Text.Length == 0);
		}

		private void txtNewValue_GotFocus(object sender, EventArgs e)
		{
			if (noNewValue)
			{
				txtNewValue.Text = string.Empty;
			}
		}

		private void txtNewValue_LostFocus(object sender, System.EventArgs e)
		{
			if (noNewValue)
			{
				txtNewValue.Text = newValueText;
				noNewValue = true;
			}
		}

		private void toolNewValue_Click(object sender, EventArgs e)
		{
			AddNewValue();
		}

		private void txtName_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					ValidateName();
					e.Handled = true;
					break;

				case Keys.Escape:
					needValidation = false;
					shape.HideEditor();
					e.Handled = true;
					break;

				case Keys.Down:
					shape.ActiveMemberIndex = 0;
					e.Handled = true;
					break;
			}
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			needValidation = true;
		}

		private void txtName_Validating(object sender, CancelEventArgs e)
		{
			ValidateName();
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			txtName.SelectionStart = 0;
		}
	}
}
