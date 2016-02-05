using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;
using System.IO;

namespace EAP.ModelFirst
{
    public class Workspace
    {
        List<Project> projects = new List<Project>();
        Project activeProject = null;
        
        public event EventHandler ActiveProjectChanged;
        public event EventHandler ActiveProjectStateChanged;
        public event ProjectEventHandler ProjectAdded;
        public event ProjectEventHandler ProjectRemoved;

        public Workspace()
        {
        }

        public IEnumerable<Project> Projects
        {
            get { return projects; }
        }

        public int ProjectCount
        {
            get { return projects.Count; }
        }

        public bool HasProject
        {
            get { return (ProjectCount > 0); }
        }

        public Project ActiveProject
        {
            get
            {
                return activeProject;
            }
            set
            {
                if (value == null)
                {
                    if (activeProject != null)
                    {
                        activeProject = null;
                        OnActiveProjectChanged(EventArgs.Empty);
                    }
                }
                else if (activeProject != value && projects.Contains(value))
                {
                    activeProject = value;
                    OnActiveProjectChanged(EventArgs.Empty);
                }
            }
        }

        public bool HasActiveProject
        {
            get { return (activeProject != null); }
        }

        public Project AddEmptyProject()
        {
            return AddEmptyProject(Settings.Default.GetDefaultLanguage());
        }

        public Project AddEmptyProject(Language language)
        {
            Project project = Project.Create(GetProjectName(), language);
            projects.Add(project);
            project.Modified += new EventHandler(project_StateChanged);
            project.FileStateChanged += new EventHandler(project_StateChanged);
            OnProjectAdded(new ProjectEventArgs(project));
            return project;
        }

        string GetProjectName()
        {
            string name = "Project";
            int i = 1;
            do
            {
                if (CheckProjectName(name))
                    return name;
                name = "Project" + i++;
            }
            while (true);
        }

        bool CheckProjectName(string name)
        {
            foreach (var p in projects)
                if (p.Name == name)
                    return false;
            return true;
        }

        /// <exception cref="ArgumentNullException">
        /// <paramref name="project"/> is null.
        /// </exception>
        public void AddProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            if (!projects.Contains(project))
            {
                projects.Add(project);
                project.Modified += new EventHandler(project_StateChanged);
                project.FileStateChanged += new EventHandler(project_StateChanged);
                if (project.FilePath != null)
                    Settings.Default.AddRecentFile(project.FilePath);
                OnProjectAdded(new ProjectEventArgs(project));
            }
        }

        public bool RemoveProject(Project project)
        {
            return RemoveProject(project, true);
        }

        private bool RemoveProject(Project project, bool saveConfirmation)
        {
            if (saveConfirmation && project.IsDirty)
            {
                DialogResult result = Client.Show(
                    Strings.AskSaveChanges, Strings.Confirmation,
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (!SaveProject(project))
                        return false;
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }

            if (projects.Remove(project))
            {
                project.Modified -= new EventHandler(project_StateChanged);
                project.FileStateChanged -= new EventHandler(project_StateChanged);
                OnProjectRemoved(new ProjectEventArgs(project));
                if (ActiveProject == project)
                    ActiveProject = null;
                return true;
            }
            return false;
        }

        public void RemoveActiveProject()
        {
            RemoveActiveProject(true);
        }

        private void RemoveActiveProject(bool saveConfirmation)
        {
            if (HasActiveProject)
                RemoveProject(ActiveProject, saveConfirmation);
        }

        public bool RemoveAll()
        {
            if (!CloseAll())
                return false;

            while (HasProject)
            {
                int lastIndex = projects.Count - 1;
                Project project = projects[lastIndex];
                projects.RemoveAt(lastIndex);
                OnProjectRemoved(new ProjectEventArgs(project));
            }
            ActiveProject = null;
            return true;
        }

        bool CloseAll()
        {
            ICollection<Project> unsavedProjects = projects.FindAll(
                delegate(Project project) { return project.IsDirty; }
            );

            if (unsavedProjects.Count > 0)
            {
                string message = Strings.AskSaveChanges + "\n";
                foreach (Project project in unsavedProjects)
                    message += "\n" + project.Name;

                DialogResult result = Client.Show(message, Strings.Confirmation,
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (!SaveAllUnsavedProjects())
                        return false;
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        public Project OpenProject()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = string.Format(
                    "{0} (*{2})|*{2}|" +
                    "{1} (*.csd; *.jd)|*.csd;*.jd",
                    Strings.ModelFirstProjectFiles,
                    Strings.PreviousFileFormats,
                    Project.FileExtension);

                if (dialog.ShowDialog() == DialogResult.OK)
                    return OpenProject(dialog.FileName);
                else
                    return null;
            }
        }

        public Project OpenProject(string fileName)
        {
            try
            {
                Project project = Project.Load(fileName);
                AddProject(project);
                return project;
            }
            catch (Exception ex)
            {
                Client.ShowError(Strings.Error + ": " + ex);
                return null;
            }
        }

        /// <exception cref="ArgumentNullException">
        /// <paramref name="project"/> is null.
        /// </exception>
        public bool SaveProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            if (project.FilePath == null || project.IsReadOnly)
            {
                return SaveProjectAs(project);
            }
            else
            {
                try
                {
                    project.Save();
                    return true;
                }
                catch (Exception ex)
                {
                    Client.ShowError(Strings.Error + ": " + ex);
                    return false;
                }
            }
        }

        /// <exception cref="ArgumentNullException">
        /// <paramref name="project"/> is null.
        /// </exception>
        public bool SaveProjectAs(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.FileName = project.Name;
                dialog.InitialDirectory = project.GetProjectDirectory();
                dialog.Filter = Strings.ModelFirstProjectFiles + " (*{0})|*{0}".FormatArgs(Project.FileExtension);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        project.Save(dialog.FileName);
                        Settings.Default.AddRecentFile(project.FilePath);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Client.ShowError(Strings.Error + ": " + ex);
                    }
                }
                return false;
            }
        }

        public bool SaveActiveProject()
        {
            if (HasActiveProject)
                return SaveProject(ActiveProject);
            else
                return false;
        }

        public bool SaveActiveProjectAs()
        {
            if (HasActiveProject)
                return SaveProjectAs(ActiveProject);
            else
                return false;
        }

        public bool SaveAllProjects()
        {
            bool allSaved = true;

            foreach (Project project in projects)
            {
                allSaved &= SaveProject(project);
            }
            return allSaved;
        }

        public bool SaveAllUnsavedProjects()
        {
            bool allSaved = true;

            foreach (Project project in projects)
            {
                if (project.IsDirty)
                    allSaved &= SaveProject(project);
            }
            return allSaved;
        }

        public void Load()
        {
            if (HasProject)
                RemoveAll();

            var args = Environment.GetCommandLineArgs();
            bool loaded = false;
            foreach (var prj in args)
            {
                if (prj.EndsWith(Project.FileExtension) && File.Exists(prj))
                {
                    OpenProject(prj);
                    loaded = true;
                }
            }
            if (!loaded)
            {
                foreach (string projectFile in Settings.Default.OpenedProjects)
                {
                    if (!string.IsNullOrEmpty(projectFile))
                    {
                        OpenProject(projectFile);
                    }
                }
            }
        }

        public void Save()
        {
            Settings.Default.OpenedProjects.Clear();

            foreach (Project project in projects)
            {
                if (project.FilePath != null)
                    Settings.Default.OpenedProjects.Add(project.FilePath);
            }
        }

        public bool SaveAndClose()
        {
            Save();
            return CloseAll();
        }

        private void project_StateChanged(object sender, EventArgs e)
        {
            Project project = (Project)sender;
            if (project == ActiveProject)
                OnActiveProjectStateChanged(EventArgs.Empty);
        }

        protected virtual void OnActiveProjectChanged(EventArgs e)
        {
            if (ActiveProjectChanged != null)
                ActiveProjectChanged(this, EventArgs.Empty);
        }

        protected virtual void OnActiveProjectStateChanged(EventArgs e)
        {
            if (ActiveProjectStateChanged != null)
                ActiveProjectStateChanged(this, EventArgs.Empty);
        }

        protected virtual void OnProjectAdded(ProjectEventArgs e)
        {
            if (ProjectAdded != null)
                ProjectAdded(this, e);
        }

        protected virtual void OnProjectRemoved(ProjectEventArgs e)
        {
            if (ProjectRemoved != null)
                ProjectRemoved(this, e);
        }
    }
}
