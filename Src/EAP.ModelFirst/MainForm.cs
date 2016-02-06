using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Controls;
using EAP.ModelFirst.Controls.Dialogs;
using EAP.ModelFirst.Controls.Documents;
using EAP.ModelFirst.Controls.Explorers;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;
using EAP.Win.UI;
using System.Drawing;

namespace EAP.ModelFirst
{
    public partial class MainForm : KryptonForm, IDockForm
    {
        DocumentManager docManager;
        static Workspace workspace;
        IZoomable zoomHandler;
        IEditable editHandler;
        ILayoutable layoutHandler;
        IDynamicMenu dynamicMenu = null;

        public static Workspace Workspace { get { return workspace; } }

        public MainForm()
        {
            InitializeComponent();
            UpdateTexts();
            docManager = new DocumentManager(this);
            Generator.SetDockForm(this);
            workspace = new Workspace();
            workspace.ActiveProjectChanged += delegate { ProjectStateChanged(); };
            workspace.ActiveProjectStateChanged += delegate { ProjectStateChanged(); };
        }

        public DockPanel DockPanel
        {
            get { return dockPanel; }
        }

        Workspace IDockForm.Workspace
        {
            get { return workspace; }
        }

        public string StatusText
        {
            get { return lblStatusText.Text; }
            set { lblStatusText.Text = value; }
        }

        public void UpdateProgress(int value)
        {
            if (value >= 100)
                toolStripProgressBar.Visible = false;
            else
            {
                if (!toolStripProgressBar.Visible)
                    toolStripProgressBar.Visible = true;
                toolStripProgressBar.Value = value;
            }
        }

        public void OpenDocument(IDocumentItem item)
        {
            docManager.OpenDocument(item);
        }

        public void UpdateTexts()
        {
            Text = Strings.AppName;
            // File menu
            fileToolStripMenuItem.Text = Strings.MenuFile;
            newProjectToolStripMenuItem.Text = Strings.MenuNewProject;
            openToolStripMenuItem.Text = Strings.MenuOpen;
            openFileToolStripMenuItem.Text = Strings.MenuOpenFile;
            saveToolStripMenuItem.Text = Strings.MenuSave;
            saveAsToolStripMenuItem.Text = Strings.MenuSaveAs;
            saveAllToolStripMenuItem.Text = Strings.MenuSaveAllProjects;
            printToolStripMenuItem.Text = Strings.MenuPrint;
            closeProjectToolStripMenuItem.Text = Strings.MenuCloseProject;
            closeAllProjectToolStripMenuItem.Text = Strings.MenuCloseAllProjects;
            exitToolStripMenuItem.Text = Strings.MenuExit;

            // Edit menu
            editToolStripMenuItem.Text = Strings.MenuEdit;
            undoToolStripMenuItem.Text = Strings.MenuUndo;
            redoToolStripMenuItem.Text = Strings.MenuRedo;
            cutToolStripMenuItem.Text = Strings.MenuCut;
            copyToolStripMenuItem.Text = Strings.MenuCopy;
            pasteToolStripMenuItem.Text = Strings.MenuPaste;
            deleteToolStripMenuItem.Text = Strings.MenuDelete;
            selectAllToolStripMenuItem.Text = Strings.MenuSelectAll;

            // View menu
            viewToolStripMenuItem.Text = Strings.MenuView;
            zoomToolStripMenuItem.Text = Strings.MenuZoom;
            autoZoomToolStripMenuItem.Text = Strings.MenuAutoZoom;
            explorersToolStripMenuItem.Text = Strings.MenuExplorers;
            navigatorToolStripMenuItem.Text = Strings.MenuDiagramNavigator;
            projectExplorerToolStripMenuItem.Text = Strings.MenuProjectExplorer;
            properiesToolStripMenuItem.Text = Strings.MenuProperies;
            templateExplorerToolStripMenuItem.Text = Strings.MenuTemplateExplorer;
            generatedFilesToolStripMenuItem.Text = Strings.GeneratedFileList;
            errorsToolStripMenuItem.Text = Strings.ErrorList;
            toolBoxToolStripMenuItem.Text = Strings.MenuToolBox;
            toolBarToolStripMenuItem.Text = Strings.MenuToolBar;
            statusBarToolStripMenuItem.Text = Strings.MenuStatusBar;

            // Tools menu
            toolsToolStripMenuItem.Text = Strings.MenuTools;
            optionsToolStripMenuItem.Text = Strings.MenuOptions;
            pluginsToolStripMenuItem.Text = Strings.MenuPlugins;

            // Windows menu
            windowsToolStripMenuItem.Text = Strings.MenuWindows;
            closeAllDocumentsToolStripMenuItem.Text = Strings.MenuCloseAllDocuments;

            // Help menu
            helpToolStripMenuItem.Text = Strings.MenuHelp;
            aboutToolStripMenuItem.Text = Strings.MenuAbout;

            // Toolbar
            newProjectToolStripButton.Text = Strings.NewProject;
            openProjectToolStripButton.Text = Strings.OpenProject;
            saveToolStripButton.Text = Strings.Save;
            printToolStripButton.Text = Strings.Print;
            cutToolStripButton.Text = Strings.Cut;
            copyToolStripButton.Text = Strings.Copy;
            pasteToolStripButton.Text = Strings.Paste;
            undoToolStripButton.Text = Strings.Undo;
            redoToolStripButton.Text = Strings.Redo;
            zoomInToolStripButton.Text = Strings.ZoomIn;
            zoomOutToolStripButton.Text = Strings.ZoomOut;
            autoZoomToolStripButton.Text = Strings.AutoZoom;
            deleteToolStripButton.Text = Strings.Delete;

            StatusText = Strings.Ready;
        }

        #region ActiveDocumentChanged

        void docManager_ActiveDocumentChanged()
        {
        }

        private void UpdateDynamicMenus(IDocument doc)
        {
            IDynamicMenu newMenu = null;
            if (doc != null)
                newMenu = doc.GetDynamicMenu();

            if (newMenu != dynamicMenu)
            {
                if (dynamicMenu != null)
                {
                    foreach (ToolStripMenuItem menuItem in dynamicMenu.GetMenuItems())
                        MainMenuStrip.Items.Remove(menuItem);
                    dynamicMenu.Close();
                }
                if (newMenu != null)
                {
                    int preferredIndex = newMenu.PreferredIndex;
                    if (preferredIndex < 0)
                        preferredIndex = 3;
                    foreach (ToolStripMenuItem menuItem in newMenu.GetMenuItems())
                        MainMenuStrip.Items.Insert(preferredIndex++, menuItem);
                }
                dynamicMenu = newMenu;
            }
        }

        void UpdateEditHandler(DocumentHandler handler)
        {
            if (editHandler != null)
                editHandler.EditStateChanged -= new EventHandler(editHandler_EditStateChanged);
            editHandler = handler.EditHandler;
            if (handler.IsEditable)
                editHandler.EditStateChanged += new EventHandler(editHandler_EditStateChanged);

            UpdateEditButtons();
        }

        void UpdateEditButtons()
        {
            bool isEditable = editHandler != null;
            copyToolStripButton.Enabled = isEditable && editHandler.CanCopyToClipboard;
            cutToolStripButton.Enabled = isEditable && editHandler.CanCutToClipboard;
            pasteToolStripButton.Enabled = isEditable && editHandler.CanPasteFromClipboard;
            deleteToolStripButton.Enabled = isEditable && editHandler.CanDelete;
            undoToolStripButton.Enabled = isEditable && editHandler.CanUndo;
            redoToolStripButton.Enabled = isEditable && editHandler.CanRedo;
            selectAllToolStripMenuItem.Enabled = isEditable && editHandler.IsEmpty;
            saveToolStripButton.Enabled = docManager.HasActiveDocument && docManager.ActiveDocument.IsDirty;

            undoToolStripMenuItem.Enabled = isEditable && editHandler.CanUndo;
            redoToolStripMenuItem.Enabled = isEditable && editHandler.CanRedo;
            cutToolStripMenuItem.Enabled = isEditable && editHandler.CanCutToClipboard;
            copyToolStripMenuItem.Enabled = isEditable && editHandler.CanCopyToClipboard;
            pasteToolStripMenuItem.Enabled = isEditable && editHandler.CanPasteFromClipboard;
            deleteToolStripMenuItem.Enabled = isEditable && editHandler.CanDelete;
            selectAllToolStripMenuItem.Enabled = isEditable && !editHandler.IsEmpty;
        }

        void editHandler_EditStateChanged(object sender, EventArgs e)
        {
            UpdateEditButtons();
            if (docManager.HasActiveDocument)
                StatusText = docManager.ActiveDocument.GetStatus();
            else
                StatusText = Strings.Ready;
        }

        void UpdateLayoutHandler(DocumentHandler handler)
        {
            if (layoutHandler != null)
                layoutHandler.SelectionChanged -= new EventHandler(layoutHandler_SelectionChanged);
            layoutHandler = handler.LayoutHandler;
            if (handler.IsLayoutable)
                layoutHandler.SelectionChanged += new EventHandler(layoutHandler_SelectionChanged);
            UpdateLayoutButtons();
        }

        void UpdateLayoutButtons()
        {
            bool enabled = layoutHandler != null && layoutHandler.SelectedShapeCount >= 2;
            btnAlignTop.Enabled = enabled;
            btnAlignLeft.Enabled = enabled;
            btnAlignBottom.Enabled = enabled;
            btnAlignRight.Enabled = enabled;
            btnAlignHorizontal.Enabled = enabled;
            btnAlignVertical.Enabled = enabled;
            btnSameWidth.Enabled = enabled;
            btnSameHeight.Enabled = enabled;
            btnSameSize.Enabled = enabled;
        }

        void layoutHandler_SelectionChanged(object sender, EventArgs e)
        {
            UpdateLayoutButtons();
        }

        void UpdateZoomHandler(DocumentHandler handler)
        {
            if (zoomHandler != null)
                zoomHandler.ZoomChanged -= new EventHandler(zoomHandler_ZoomChanged);
            zoomHandler = handler.ZoomHandler;
            if (handler.IsZoomable)
                zoomHandler.ZoomChanged += new EventHandler(zoomHandler_ZoomChanged);
            zoomingToolStrip.Enabled = handler.IsZoomable;
            zoomToolStripLabel.Enabled = handler.IsZoomable;
            autoZoomToolStripButton.Enabled = handler.IsZoomable;
            UpdateZoomValue();
        }

        void UpdateZoomValue()
        {
            zoomingToolStrip.ZoomValue = zoomHandler != null ? zoomHandler.Zoom : 1;
            zoomInToolStripButton.Enabled = zoomHandler != null && zoomHandler.Zoom < 4;
            zoomOutToolStripButton.Enabled = zoomHandler != null && zoomHandler.Zoom > 0.1f;
        }

        void zoomHandler_ZoomChanged(object sender, EventArgs e)
        {
            UpdateZoomValue();
        }

        #endregion

        void ProjectStateChanged()
        {
            if (Workspace.HasActiveProject)
            {
                string projectName = Workspace.ActiveProject.Name;

                if (Workspace.ActiveProject.IsDirty)
                    Text = projectName + "* - " + Strings.AppName;
                else
                    Text = projectName + " - " + Strings.AppName;
            }
            else
            {
                Text = Strings.AppName;
            }
        }

        private void OpenRecentFile_Click(object sender, EventArgs e)
        {
            int index = ((ToolStripItem)sender).Tag.ConvertTo<int>();
            if (index >= 0 && index < Settings.Default.RecentFiles.Count)
            {
                string fileName = Settings.Default.RecentFiles[index];
                Workspace.OpenProject(fileName);
            }
        }

        #region Load & Save

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
                LoadWindowSettings();
            Show();
            using (StatusBusy status = new StatusBusy(Strings.Loading, this))
            {
                if (Settings.Default.RememberOpenProjects)
                {
                    Workspace.Load();
                }
                LoadDocking();
                ProjectExplorer.LoadProjects(this);
            }
        }

        void LoadDocking()
        {
            try
            {
                var bytes = Convert.FromBase64String(WindowSettings.Default.Docking);
                using (var stream = new System.IO.MemoryStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Position = 0;
                    dockPanel.LoadFromXml(stream, delegate(string persistString)
                    {
                        return docManager.LoadForm(persistString);
                    });
                }
            }
            catch (Exception)
            {
                //加载失败时创建默认布局
                ToolBox.Show(this);
                TemplateExplorer.Show(this);
                Navigator.Show(this);
                ProjectExplorer.Show(this);
            }
        }

        void LoadWindowSettings()
        {
            statusStrip.Visible = WindowSettings.Default.StatusBarVisible;
            toolStripStandard.Visible = WindowSettings.Default.StandardToolBarVisible;
            toolStripVisualizer.Visible = WindowSettings.Default.VisualizerToolBarVisible;
            toolStripTextEditor.Visible = WindowSettings.Default.TextEditorBarVisible;
            toolStripLayout.Visible = WindowSettings.Default.LayoutToolBarVisible;

            // Mono hack because of a .NET/Mono serialization difference of Point and Size classes
            if (MonoHelper.IsRunningOnMono)
                return;

            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.toolStripStandard.SuspendLayout();
            this.toolStripLayout.SuspendLayout();
            this.toolStripTextEditor.SuspendLayout();
            this.toolStripVisualizer.SuspendLayout();
            this.SuspendLayout();
            Location = WindowSettings.Default.WindowPosition;
            Size = WindowSettings.Default.WindowSize;
            if (WindowSettings.Default.IsWindowMaximized)
                WindowState = FormWindowState.Maximized;
            this.toolStripContainer.TopToolStripPanel.Controls.Remove(toolStripStandard);
            this.toolStripContainer.TopToolStripPanel.Controls.Remove(toolStripVisualizer);
            this.toolStripContainer.TopToolStripPanel.Controls.Remove(toolStripTextEditor);
            this.toolStripContainer.TopToolStripPanel.Controls.Remove(toolStripLayout);

            var lst = new Dictionary<ToolStrip, Point>(4);
            lst.Add(toolStripStandard, WindowSettings.Default.StandardToolBarPosition);
            lst.Add(toolStripVisualizer, WindowSettings.Default.VisualizerToolBarPosition);
            lst.Add(toolStripTextEditor, WindowSettings.Default.TextEditorBarPosition);
            lst.Add(toolStripLayout, WindowSettings.Default.LayoutToolBarPosition);
            lst.OrderBy(p => p.Value.X).ToList().ForEach(p =>
            {
                this.toolStripContainer.TopToolStripPanel.Controls.Add(p.Key);
                if (p.Value.X > -1) p.Key.Location = p.Value;
            });

            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.toolStripStandard.ResumeLayout(false);
            this.toolStripStandard.PerformLayout();
            this.toolStripLayout.ResumeLayout(false);
            this.toolStripLayout.PerformLayout();
            this.toolStripTextEditor.ResumeLayout(false);
            this.toolStripTextEditor.PerformLayout();
            this.toolStripVisualizer.ResumeLayout(false);
            this.toolStripVisualizer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        void SaveWindowSettings()
        {
            using (var stream = new System.IO.MemoryStream())
            {
                //dockPanel.SaveAsXml("d:\\dock.txt");
                dockPanel.SaveAsXml(stream, Encoding.UTF8);
                WindowSettings.Default.Docking = Convert.ToBase64String(stream.ToArray());
            }

            WindowSettings.Default.StandardToolBarVisible = toolStripStandard.Visible;
            WindowSettings.Default.VisualizerToolBarVisible = toolStripVisualizer.Visible;
            WindowSettings.Default.TextEditorBarVisible = toolStripTextEditor.Visible;
            WindowSettings.Default.LayoutToolBarVisible = toolStripLayout.Visible;
            WindowSettings.Default.StatusBarVisible = statusStrip.Visible;

            // Mono hack because of a .NET/Mono serialization difference of Point and Size classes
            if (MonoHelper.IsRunningOnMono)
                return;

            if (WindowState == FormWindowState.Maximized)
            {
                WindowSettings.Default.IsWindowMaximized = true;
            }
            else
            {
                WindowSettings.Default.IsWindowMaximized = false;
                if (WindowState == FormWindowState.Normal)
                    WindowSettings.Default.WindowSize = Size;
                if (WindowState == FormWindowState.Normal)
                    WindowSettings.Default.WindowPosition = Location;
            }

            WindowSettings.Default.StandardToolBarPosition = toolStripStandard.Location;
            WindowSettings.Default.VisualizerToolBarPosition = toolStripVisualizer.Location;
            WindowSettings.Default.TextEditorBarPosition = toolStripTextEditor.Location;
            WindowSettings.Default.LayoutToolBarPosition = toolStripLayout.Location;
            WindowSettings.Default.Save();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Workspace.SaveAndClose())
            {
                e.Cancel = true;
                return;
            }
            if (!DesignMode)
                SaveWindowSettings();
        }

        #endregion

        #region Menu File

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (docManager.HasActiveDocument)
            {
                saveToolStripMenuItem.Text = string.Format(Strings.MenuSaveProject, docManager.ActiveDocument.DockHandler.Form.Name);
            }
            else
            {
                saveToolStripMenuItem.Text = Strings.MenuSave;
            }
            if (Workspace.HasActiveProject)
            {
                string projectName = Workspace.ActiveProject.Name;
                saveAsToolStripMenuItem.Text = string.Format(Strings.MenuSaveProjectAs, projectName);
                closeProjectToolStripMenuItem.Text = string.Format(Strings.MenuClose, projectName);
            }
            else
            {
                saveAsToolStripMenuItem.Text = Strings.MenuSaveAs;
                closeProjectToolStripMenuItem.Text = Strings.MenuCloseProject;
            }
            //TODO Template file is also need to save.
            saveToolStripMenuItem.Enabled = Workspace.HasActiveProject && Workspace.ActiveProject.IsDirty;
            saveAsToolStripMenuItem.Enabled = Workspace.HasActiveProject;
            closeProjectToolStripMenuItem.Enabled = Workspace.HasActiveProject;
            saveAllToolStripMenuItem.Enabled = Workspace.HasProject;
            closeAllProjectToolStripMenuItem.Enabled = Workspace.HasProject;

            printToolStripMenuItem.Enabled = docManager.HasActiveDocument && docManager.ActiveDocument.Handler.IsPrintable;
        }

        private void openToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in openToolStripMenuItem.DropDownItems)
            {
                if (item.Tag != null)
                {
                    int index = item.Tag.ConvertTo(-1);
                    if (index > -1)
                    {
                        if (index < Settings.Default.RecentFiles.Count)
                        {
                            item.Text = Settings.Default.RecentFiles[index];
                            item.Visible = true;
                        }
                        else
                        {
                            item.Visible = false;
                        }
                    }
                }
            }

            openFileToolStripSeparator.Visible = (Settings.Default.RecentFiles.Count > 0);
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project project = Workspace.AddEmptyProject();
            Workspace.ActiveProject = project;
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (StatusBusy s = new StatusBusy(Strings.LoadingProject, this))
            {
                Workspace.OpenProject();
            }
            StatusText = Strings.ProjectLoaded;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (docManager.HasActiveDocument)
            {
                using (StatusBusy s = new StatusBusy(Strings.Saveing, this))
                {
                    docManager.ActiveDocument.Save();
                }
                StatusText = Strings.Saved;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (StatusBusy s = new StatusBusy(Strings.Saveing, this))
            {
                Workspace.SaveActiveProjectAs();
            }
            StatusText = Strings.Saved;
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (StatusBusy s = new StatusBusy(Strings.Saveing, this))
            {
                Workspace.SaveAllProjects();
                foreach (var d in docManager.Documents)
                    if (d.IsDirty)
                        d.Save();
            }
            StatusText = Strings.Saved;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (docManager.HasActiveDocument && docManager.ActiveDocument.Handler.IsPrintable)
            {
                using (DiagramPrintDialog dialog = new DiagramPrintDialog())
                {
                    dialog.Document = docManager.ActiveDocument.Handler.PrintHandler;
                    dialog.ShowDialog();
                }
            }
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workspace.RemoveActiveProject();
        }

        private void closeAllProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workspace.RemoveAll();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Menu Edit

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editHandler != null && editHandler.CanUndo)
                editHandler.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editHandler != null && editHandler.CanRedo)
                editHandler.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editHandler != null && editHandler.CanCutToClipboard)
                editHandler.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editHandler != null && editHandler.CanCopyToClipboard)
                editHandler.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editHandler != null && editHandler.CanPasteFromClipboard)
                editHandler.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editHandler != null && editHandler.CanDelete)
                editHandler.Delete();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editHandler != null && !editHandler.IsEmpty)
                editHandler.SelectAll();
        }

        #endregion

        #region Menu View

        private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            autoZoomToolStripMenuItem.Enabled = zoomHandler != null;
            zoomToolStripMenuItem.Enabled = zoomHandler != null;
            statusBarToolStripMenuItem.Checked = statusStrip.Visible;
        }

        private void zoom10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoomHandler != null)
                zoomHandler.ChangeZoom(0.1f);
        }

        private void zoom25ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoomHandler != null)
                zoomHandler.ChangeZoom(0.25f);
        }

        private void zoom50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoomHandler != null)
                zoomHandler.ChangeZoom(0.5f);
        }

        private void zoom100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoomHandler != null)
                zoomHandler.ChangeZoom(1.0f);
        }

        private void zoom150ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoomHandler != null)
                zoomHandler.ChangeZoom(1.5f);
        }

        private void zoom200ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoomHandler != null)
                zoomHandler.ChangeZoom(2.0f);
        }

        private void zoom400ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoomHandler != null)
                zoomHandler.ChangeZoom(4.0f);
        }

        private void autoZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoomHandler != null)
                zoomHandler.AutoZoom();
        }

        private void projectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectExplorer.Show(this);
        }

        private void templateExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateExplorer.Show(this);
        }

        private void navigatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navigator.Show(this);
        }

        private void toolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolBox.Show(this);
        }

        private void errorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ErrorList.Show(this);
        }

        private void generatedFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneratedFileList.Show(this);
        }

        private void properiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertyWindow.Show(this);
        }

        private void toolBarToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            standardToolStripMenuItem.Checked = toolStripStandard.Visible;
            visualizerToolStripMenuItem.Checked = toolStripVisualizer.Visible;
            layoutToolStripMenuItem.Checked = toolStripLayout.Visible;
            textEditorToolStripMenuItem.Checked = toolStripTextEditor.Visible;
        }

        private void standardToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
                toolStripStandard.Show();
            else
                toolStripStandard.Hide();
        }

        private void visualizerToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
                toolStripVisualizer.Show();
            else
                toolStripVisualizer.Hide();
        }

        private void layoutToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
                toolStripLayout.Show();
            else
                toolStripLayout.Hide();
        }

        private void textEditorToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
                toolStripTextEditor.Show();
            else
                toolStripTextEditor.Hide();
        }

        private void statusBarToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
                statusStrip.Show();
            else
                statusStrip.Hide();
        }

        #endregion

        #region ToolStrip

        private void openProjectToolStripButton_DropDownOpening(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in openProjectToolStripButton.DropDownItems)
            {
                if (item.Tag != null)
                {
                    int index = item.Tag.ConvertTo(-1);
                    if (index > -1)
                    {
                        if (index < Settings.Default.RecentFiles.Count)
                        {
                            item.Text = Settings.Default.RecentFiles[index];
                            item.Visible = true;
                        }
                        else
                        {
                            item.Visible = false;
                        }
                    }
                }
            }

            openFileSeparator.Visible = (Settings.Default.RecentFiles.Count > 0);
        }

        private void zoomingToolStrip_ZoomValueChanged(object sender, EventArgs e)
        {
            if (zoomHandler != null)
                zoomHandler.ChangeZoom(zoomingToolStrip.ZoomValue);
            zoomToolStripLabel.Text = (int)Math.Round(zoomingToolStrip.ZoomValue * 100) + "%";
        }

        private void zoomOutToolStripButton_Click(object sender, EventArgs e)
        {
            if (zoomHandler != null)
                zoomHandler.ZoomOut();
        }

        private void zoomInToolStripButton_Click(object sender, EventArgs e)
        {
            if (zoomHandler != null)
                zoomHandler.ZoomIn();
        }

        private void btnAlignLeft_Click(object sender, EventArgs e)
        {
            if (layoutHandler != null)
                layoutHandler.AlignLeft();
        }

        private void btnAlignVertical_Click(object sender, EventArgs e)
        {
            if (layoutHandler != null)
                layoutHandler.AlignVertical();
        }

        private void btnAlignRight_Click(object sender, EventArgs e)
        {
            if (layoutHandler != null)
                layoutHandler.AlignRight();
        }

        private void btnAlignTop_Click(object sender, EventArgs e)
        {
            if (layoutHandler != null)
                layoutHandler.AlignTop();
        }

        private void btnAlignHorizontal_Click(object sender, EventArgs e)
        {
            if (layoutHandler != null)
                layoutHandler.AlignHorizontal();
        }

        private void btnAlignBottom_Click(object sender, EventArgs e)
        {
            if (layoutHandler != null)
                layoutHandler.AlignBottom();
        }

        private void btnSameWidth_Click(object sender, EventArgs e)
        {
            if (layoutHandler != null)
                layoutHandler.AdjustToSameWidth();
        }

        private void btnSameHeight_Click(object sender, EventArgs e)
        {
            if (layoutHandler != null)
                layoutHandler.AdjustToSameHeight();
        }

        private void btnSameSize_Click(object sender, EventArgs e)
        {
            if (layoutHandler != null)
                layoutHandler.AdjustToSameSize();
        }

        private void btnOutdent_Click(object sender, EventArgs e)
        {
            if (docManager.HasActiveDocument && docManager.ActiveDocument.Handler.IsTextFormatable)
                docManager.ActiveDocument.Handler.TextFormatHandler.Outdent();
        }

        private void btnIndent_Click(object sender, EventArgs e)
        {
            if (docManager.HasActiveDocument && docManager.ActiveDocument.Handler.IsTextFormatable)
                docManager.ActiveDocument.Handler.TextFormatHandler.Indent();
        }

        private void btnGeneratorCode_Click(object sender, EventArgs e)
        {
            if (docManager.HasActiveDocument)
                docManager.ActiveDocument.Handler.PerformGenerateCode();
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            if (docManager.HasActiveDocument)
                docManager.ActiveDocument.Handler.PerformCompile();
        }

        private void btnWordWrap_Click(object sender, EventArgs e)
        {
            if (docManager.HasActiveDocument && docManager.ActiveDocument.Handler.IsTextFormatable)
                docManager.ActiveDocument.Handler.TextFormatHandler.WordWrap = btnWordWrap.Checked;
        }

        #endregion

        #region Menu Windows

        private void closeAllDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var a = dockPanel.Documents.ToArray();
            foreach (var d in a)
            {
                if (d.DockHandler.HideOnClose)
                    d.DockHandler.Hide();
                else
                    d.DockHandler.Close();
            }
        }

        private void autoHideAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockPanel.Panes.ToList().ForEach(p =>
            {
                if (p.DockState == DockState.DockBottom)
                    p.DockState = DockState.DockBottomAutoHide;
                else if (p.DockState == DockState.DockLeft)
                    p.DockState = DockState.DockLeftAutoHide;
                else if (p.DockState == DockState.DockRight)
                    p.DockState = DockState.DockRightAutoHide;
                else if (p.DockState == DockState.DockTop)
                    p.DockState = DockState.DockTopAutoHide;
            });
        }

        private void windowsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            closeAllDocumentsToolStripMenuItem.Enabled = DockPanel.Documents.Any();
            autoHideAllToolStripMenuItem.Enabled = DockPanel.Panes.Any(p => p.Visible && p.DockState != DockState.Document && !p.IsAutoHide);

            floatToolStripMenuItem.Enabled = DockPanel.ActiveContent != null;
            bool hide = DockPanel.ActivePane != null && DockPanel.ActivePane.DockState != DockState.Document && !DockPanel.ActivePane.IsAutoHide;
            hideToolStripMenuItem.Enabled = hide;
            autoHideToolStripMenuItem.Enabled = hide;
            dockToolStripMenuItem.Enabled = DockPanel.FloatWindows.Any(p => p.Visible);
            var titled = DockPanel.ActiveDocument != null && DockPanel.ActiveDocument.DockHandler.PanelPane.Contents.Count > 1;
            addHorizontalToolStripMenuItem.Enabled = titled;
            addVerticalToolStripMenuItem.Enabled = titled;
        }

        private void floatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockPanel.ActiveContent.DockHandler.DockState = DockState.Float;
        }

        private void dockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var pane in DockPanel.FloatWindows.First(p => p.Visible).NestedPanes)
            {
                if (pane.DockState != DockState.Float)
                    continue;
                pane.RestoreToPanel();
            }
        }

        private void autoHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = DockPanel.ActivePane;
            if (p.DockState == DockState.DockBottom)
                p.DockState = DockState.DockBottomAutoHide;
            else if (p.DockState == DockState.DockLeft)
                p.DockState = DockState.DockLeftAutoHide;
            else if (p.DockState == DockState.DockRight)
                p.DockState = DockState.DockRightAutoHide;
            else if (p.DockState == DockState.DockTop)
                p.DockState = DockState.DockTopAutoHide;
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockPanel.ActiveContent.DockHandler.Hide();
        }

        private void addHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockPanel.ActiveDocument.DockHandler.DockTo(DockPanel.ActiveDocument.DockHandler.PanelPane, DockStyle.Bottom, 0);
        }

        private void addVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockPanel.ActiveDocument.DockHandler.DockTo(DockPanel.ActiveDocument.DockHandler.PanelPane, DockStyle.Right, 0);
        }

        #endregion

        private void pluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OptionsDialog dialog = new OptionsDialog())
            {
                dialog.ShowDialog();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox box = new AboutBox())
            {
                box.ShowDialog();
            }
        }

        private void contextMenuStripToolStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cmnStandard.Checked = toolStripStandard.Visible;
            cmnVisualizer.Checked = toolStripVisualizer.Visible;
            cmnLayout.Checked = toolStripLayout.Visible;
            cmnTextEditor.Checked = toolStripTextEditor.Visible;
        }

        private void dockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (Disposing) return;
            try
            {

                var handler = docManager.HasActiveDocument ? docManager.ActiveDocument.Handler : new DocumentHandler();
                UpdateZoomHandler(handler);
                UpdateEditHandler(handler);
                UpdateLayoutHandler(handler);

                printToolStripButton.Enabled = handler.IsPrintable;
                btnGeneratorCode.Enabled = handler.CanGenerateCode;
                btnCompile.Enabled = handler.CanCompile;
                btnIndent.Enabled = handler.IsTextFormatable;
                btnOutdent.Enabled = handler.IsTextFormatable;
                btnWordWrap.Enabled = handler.IsTextFormatable;
                btnWordWrap.Checked = btnWordWrap.Enabled && handler.TextFormatHandler.WordWrap;

                Navigator.SetDocumentVisualizer(handler.VisualizerHandler);
                if (docManager.HasActiveDocument)
                    ToolBox.SetDocument(docManager.ActiveDocument.DocumentItem);
                else
                    ToolBox.SetDocument(null);

                if (docManager.HasActiveDocument && docManager.ActiveDocument.DocumentItem is IProjectDocument)
                {
                    var p = (IProjectDocument)docManager.ActiveDocument.DocumentItem;
                    Workspace.ActiveProject = p.ProjectInfo;
                }
                else
                    Workspace.ActiveProject = null;

                UpdateDynamicMenus(docManager.ActiveDocument);

            }
            catch (Exception exc)
            {
                Client.ShowError(exc.ToString());
            }
        }

        private void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            if (Disposing) return;
            if (DockPanel.ActiveContent is Generator)
                PropertyWindow.SetPropertyObject(null);
            else if (DockPanel.ActiveContent is IPropertyConfigurable)
                PropertyWindow.SetPropertyObject(((IPropertyConfigurable)DockPanel.ActiveContent).PropertyObject);
        }
    }
}
