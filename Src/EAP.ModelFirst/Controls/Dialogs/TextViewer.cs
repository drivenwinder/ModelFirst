using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using ICSharpCode.TextEditor.Document;

namespace EAP.ModelFirst.Controls.Dialogs
{
    public partial class TextViewer : KryptonForm
    {
        public TextViewer(string text)
        {
            InitializeComponent();
            txtContent.Text = text;
        }
    }
}
