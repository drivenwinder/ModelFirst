using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace EAP.ModelFirst.Controls.Dialogs
{
    public partial class DialogForm : KryptonForm
    {
        public DialogForm()
        {
            InitializeComponent();
            KryptonManager.GlobalPaletteChanged += new EventHandler(KryptonManager_GlobalPaletteChanged);
            SetBackColor();
        }

        void SetBackColor()
        {
            BackColor = KryptonManager.CurrentGlobalPalette
                .GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);
        }

        void KryptonManager_GlobalPaletteChanged(object sender, EventArgs e)
        {
            SetBackColor();
        }
    }
}
