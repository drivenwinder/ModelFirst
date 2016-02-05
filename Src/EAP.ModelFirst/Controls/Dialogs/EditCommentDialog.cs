using System;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Controls.Dialogs
{
    public partial class EditCommentDialog : DialogForm, ILocalizable
	{
		private EditCommentDialog()
		{
			InitializeComponent();
		}

		public EditCommentDialog(string initText)
            : this()
		{
			txtInput.Text = initText;
		}

		public string InputText
		{
			get { return txtInput.Text; }
		}

		public void UpdateTexts()
		{
			Text = Strings.EditComment;
			lblEdit.Text = Strings.EditText;
			btnOK.Text = Strings.ButtonOK;
			btnCancel.Text = Strings.ButtonCancel;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			UpdateTexts();
		}
	}
}