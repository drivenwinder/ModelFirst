using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.Dialogs
{
    public partial class OptionsDialog : DialogForm, ILocalizable
    {
        Style savedStyle = null;

        public OptionsDialog()
        {
            InitializeComponent();
        }

        public void UpdateTexts()
        {
            this.Text = Strings.Options;
            tabGeneral.Text = Strings.General;
            tabStyle.Text = Strings.Style;
            grpGeneral.Text = Strings.General;
            tabDiagram.Text = Strings.Diagram;
            lblLanguage.Text = Strings.Language + ":";
            chkRememberOpenProjects.Text = Strings.RememberOpenProjects;
            btnClearRecents.Text = Strings.ClearRecentList;
            chkUsePrecisionSnapping.Text = Strings.UseSnapping;
            grpShowChevron.Text = Strings.ShowChevron;
            radChevronAlways.Text = Strings.Always;
            radChevronAsNeeded.Text = Strings.WhenMouseHovers;
            radChevronNever.Text = Strings.Never;
            grpUseClearType.Text = Strings.UseClearType;
            radClearTypeAlways.Text = Strings.Always;
            radClearTypeWhenZoomed.Text = Strings.WhenZoomed;
            radClearTypeNever.Text = Strings.Never;
            chkClearTypeForImages.Text = Strings.UseClearTypeForImages;
            btnClear.Text = Strings.ButtonClear;
            btnLoad.Text = Strings.ButtonLoad;
            btnSave.Text = Strings.ButtonSave;
            btnOK.Text = Strings.ButtonOK;
            btnCancel.Text = Strings.ButtonCancel;

            cboLanguage.Left = lblLanguage.Left + lblLanguage.Width + 3;
        }

        private void LoadStyles()
        {
            cboStyles.Items.Clear();
            foreach (Style style in Style.AvaiableStyles)
            {
                cboStyles.Items.Add(style);
                if (style.Equals(Style.CurrentStyle))
                    cboStyles.SelectedItem = style;
            }
        }

        private void FillLanguages()
        {
            cboLanguage.Items.Clear();
            foreach (UILanguage language in UILanguage.AvalilableCultures)
            {
                cboLanguage.Items.Add(language);
            }
        }

        private void LoadSettings()
        {
            // General settings
            cboLanguage.SelectedItem = UILanguage.CreateUILanguage(Settings.Default.UILanguage);
            chkRememberOpenProjects.Checked = Settings.Default.RememberOpenProjects;

            // Diagram settings
            chkUsePrecisionSnapping.Checked = Settings.Default.UsePrecisionSnapping;
            if (Settings.Default.ShowChevron == ChevronMode.Always)
                radChevronAlways.Checked = true;
            else if (Settings.Default.ShowChevron == ChevronMode.AsNeeded)
                radChevronAsNeeded.Checked = true;
            else
                radChevronNever.Checked = true;

            if (Settings.Default.UseClearType == ClearTypeMode.Always)
                radClearTypeAlways.Checked = true;
            else if (Settings.Default.UseClearType == ClearTypeMode.WhenZoomed)
                radClearTypeWhenZoomed.Checked = true;
            else
                radClearTypeNever.Checked = true;
            chkClearTypeForImages.Checked = Settings.Default.UseClearTypeForImages;

            // Style settings
            savedStyle = (Style)Style.CurrentStyle.Clone();
            stylePropertyGrid.SelectedObject = Style.CurrentStyle;

            txtSavePath.Text = Settings.Default.TemplateFolder;
        }

        private void SaveSettings()
        {
            // General settings
            var uiLanguage = ((UILanguage)cboLanguage.SelectedItem).ShortName;
            if (Settings.Default.UILanguage != uiLanguage)
                Settings.Default.UILanguage = uiLanguage;
            Settings.Default.RememberOpenProjects = chkRememberOpenProjects.Checked;

            // Diagram settings
            Settings.Default.UsePrecisionSnapping = chkUsePrecisionSnapping.Checked;
            if (radChevronAlways.Checked)
                Settings.Default.ShowChevron = ChevronMode.Always;
            else if (radChevronAsNeeded.Checked)
                Settings.Default.ShowChevron = ChevronMode.AsNeeded;
            else
                Settings.Default.ShowChevron = ChevronMode.Never;

            if (radClearTypeAlways.Checked)
                Settings.Default.UseClearType = ClearTypeMode.Always;
            else if (radClearTypeWhenZoomed.Checked)
                Settings.Default.UseClearType = ClearTypeMode.WhenZoomed;
            else
                Settings.Default.UseClearType = ClearTypeMode.Never;
            Settings.Default.UseClearTypeForImages = chkClearTypeForImages.Checked;

            // Style settings
            Style.SaveCurrentStyle();
            if (savedStyle != null)
                savedStyle.Dispose();

            if (Settings.Default.TemplateFolder != txtSavePath.Text)
                Settings.Default.TemplateFolder = txtSavePath.Text;

            Settings.Default.Save();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            UpdateTexts();
            FillLanguages();
            LoadSettings();
            LoadStyles();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (this.DialogResult == DialogResult.Cancel)
                Style.CurrentStyle = savedStyle;
            savedStyle.Dispose();
        }

        private void stylePropertyGrid_PropertyValueChanged(object s,
            PropertyValueChangedEventArgs e)
        {
            cboStyles.SelectedIndex = -1;
        }

        private void btnClearRecents_Click(object sender, EventArgs e)
        {
            Settings.Default.RecentFiles.Clear();
        }

        private void cboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Style style = cboStyles.SelectedItem as Style;
            if (style != null)
                ChangeCurrentStyle(style);
        }

        private void ChangeCurrentStyle(Style style)
        {
            Style.CurrentStyle = style;
            stylePropertyGrid.SelectedObject = Style.CurrentStyle;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ChangeCurrentStyle(new Style());
            cboStyles.SelectedIndex = -1;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = Strings.DiagramStyle + " (*.dst)|*.dst";
                dialog.InitialDirectory = Style.StylesDirectory;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Style style = Style.Load(dialog.FileName);

                    if (style == null)
                    {
                        Client.ShowInfo(Strings.ErrorCouldNotLoadFile);
                    }
                    else
                    {
                        var s = cboStyles.Items.Cast<Style>().FirstOrDefault(p => p.FilePath == style.FilePath);
                        if (s != null)
                            cboStyles.Items.Remove(s);
                        cboStyles.Items.Add(style);
                        cboStyles.SelectedItem = style;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.FileName = Style.CurrentStyle.Name;
                dialog.InitialDirectory = Style.StylesDirectory;
                dialog.Filter = Strings.DiagramStyle + " (*.dst)|*.dst";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (Style.CurrentStyle.IsUntitled)
                    {
                        Style.CurrentStyle.Name = Path.GetFileNameWithoutExtension(dialog.FileName);
                    }

                    if (!Style.CurrentStyle.Save(dialog.FileName))
                    {
                        Client.ShowInfo(Strings.ErrorCouldNotSaveFile);
                    }
                    else
                    {
                        LoadStyles();
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select Template Folder";
                dialog.SelectedPath = txtSavePath.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtSavePath.Text = dialog.SelectedPath;
                }
            }
        }
    }
}
