using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Core.Project
{
    public sealed class Project : PackageBase, IModifiable
    {
        public const string FileExtension = ".mprj";

        FileInfo projectFile = null;
        bool isReadOnly = false;
        bool loading = false;
        Language language;

        List<IEntity> entities = new List<IEntity>();
        List<Model> models = new List<Model>();

        [System.ComponentModel.Browsable(false)]
        public IEnumerable<IEntity> Entities { get { return entities; } }

        [System.ComponentModel.Browsable(false)]
        public IEnumerable<Model> Models { get { return models; } }

        public event EventHandler FileStateChanged;

        public event EventHandler Saved;
        
        public static Project Create(string name, Language language)
        {
            if (language == null)
                throw new ArgumentNullException("language");
            if (name == null || name.Length == 0)
                throw new ArgumentException("Name cannot empty string.");
            return new Project() { Id = Guid.NewGuid(), name = name, language = language };
        }

        #region Property

        [System.ComponentModel.Browsable(false)]
        public override Project ProjectInfo
        {
            get { return this; }
        }

        public Language Language
        {
            get { return language; }
        }

        [System.ComponentModel.Browsable(false)]
        public bool IsReadOnly
        {
            get { return isReadOnly; }
        }

        public string FilePath
        {
            get
            {
                if (projectFile != null)
                    return projectFile.FullName;
                else
                    return null;
            }
            private set
            {
                if (value != null)
                {
                    try
                    {
                        FileInfo file = new FileInfo(value);

                        if (projectFile == null || projectFile.FullName != file.FullName)
                        {
                            projectFile = file;
                            OnFileStateChanged(EventArgs.Empty);
                        }
                    }
                    catch
                    {
                        if (projectFile != null)
                        {
                            projectFile = null;
                            OnFileStateChanged(EventArgs.Empty);
                        }
                    }
                }
                else if (projectFile != null) // value == null
                {
                    projectFile = null;
                    OnFileStateChanged(EventArgs.Empty);
                }
            }
        }

        public string FileName
        {
            get
            {
                if (projectFile != null)
                    return projectFile.Name;
                else
                    return Name + FileExtension;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public int ItemCount
        {
            get { return items.Count; }
        }

        [System.ComponentModel.Browsable(false)]
        public bool IsEmpty
        {
            get { return ItemCount == 0; }
        }

        #endregion

        public override void Clean()
        {
            bool isDirty = IsDirty;
            base.Clean();
            if (isDirty)
                OnFileStateChanged(EventArgs.Empty);
        }

        public void CloseItems()
        {
            CloseItems(this);
        }

        void CloseItems(PackageBase package)
        {
            foreach (IProjectItem item in package.Items)
            {
                if (item is PackageBase)
                    CloseItems((PackageBase)item);
                if (item is IDocumentItem)
                    ((IDocumentItem)item).Close();
            }
        }

        public string GetProjectDirectory()
        {
            if (projectFile != null)
                return projectFile.DirectoryName;
            else
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        /// <exception cref="IOException">
        /// Could not load the project.
        /// </exception>
        /// <exception cref="InvalidDataException">
        /// The save file is corrupt and could not be loaded.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="fileName"/> is empty string.
        /// </exception>
        public static Project Load(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException(Strings.ErrorBlankFilename, "fileName");

            if (!File.Exists(fileName))
                throw new FileNotFoundException(Strings.ErrorFileNotFound);

            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(fileName);
            }
            catch (Exception ex)
            {
                throw new IOException(Strings.ErrorCouldNotLoadFile, ex);
            }

            XmlElement root = document["Project"];
            if (root == null)
                throw new InvalidDataException(Strings.ErrorCorruptSaveFile);

            Project project = new Project();
            project.loading = true;
            try
            {
                DeserializeContext context = new DeserializeContext();
                project.Deserialize(context, root);
                foreach (var p in context.Models)
                {
                    p.Key.Deserialize(p.Value);
                    ((IModifiable)p.Key).Clean();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidException(Strings.ErrorCorruptSaveFile + "\r\n" + ex.ToString(), ex);
            }
            project.loading = false;
            project.FilePath = fileName;
            project.isReadOnly = project.projectFile.IsReadOnly;
            project.Clean();
            return project;
        }

        /// <exception cref="IOException">
        /// Could not save the project.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The project was not saved before by the <see cref="Save(string)"/> method.
        /// </exception>
        public void Save()
        {
            if (projectFile == null)
                throw new InvalidOperationException(Strings.ErrorCannotSaveFile);

            Save(FilePath);
        }

        /// <exception cref="IOException">
        /// Could not save the project.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="fileName"/> is null or empty string.
        /// </exception>
        public void Save(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException(Strings.ErrorBlankFilename, "fileName");

            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("Project");
            document.AppendChild(root);

            Serialize(root);
            try
            {
                document.Save(fileName);
            }
            catch (Exception ex)
            {
                throw new IOException(Strings.ErrorCouldNotSaveFile, ex);
            }

            isReadOnly = false;
            FilePath = fileName;
            Clean();
            OnSaved(EventArgs.Empty);        }

        public override void Serialize(XmlElement root)
        {
            base.Serialize(root);
            root.SetAttribute("Language", Language.AssemblyName);
        }

        /// <exception cref="InvalidException">
        /// The save format is corrupt and could not be loaded.
        /// </exception>
        public override void Deserialize(DeserializeContext context, XmlElement root)
        {
            try
            {
                Language language = Language.GetLanguage(root.GetAttributeValue("Language", "UML"));
                if (language == null)
                    throw new InvalidDataException("Invalid project language.");

                this.language = language;
            }
            catch (Exception ex)
            {
                throw new InvalidException("Invalid project language.", ex);
            }
            base.Deserialize(context, root);
        }
        
        private void OnFileStateChanged(EventArgs e)
        {
            if (FileStateChanged != null)
                FileStateChanged(this, e);
        }

        private void OnSaved(EventArgs e)
        {
            if (Saved != null)
                Saved(this, e);
        }

        protected override void OnModified(EventArgs e)
        {
            if (!loading)
                base.OnModified(e);
        }
        
        public IProjectDocument FindItem(Guid id)
        {
            return FindItem(id, Items);
        }

        IProjectDocument FindItem(Guid id, IEnumerable<IProjectItem> items)
        {
            foreach (var i in items)
            {
                if (i is IProjectDocument && ((IProjectDocument)i).Id == id)
                    return (IProjectDocument)i;
                if (i is PackageBase)
                {
                    var found = FindItem(id, ((PackageBase)i).Items);
                    if (found != null)
                        return found;
                }
            }
            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            Project project = (Project)obj;

            if (this.projectFile == null && project.projectFile == null)
                return object.ReferenceEquals(this, obj);

            return (
                this.projectFile != null && project.projectFile != null &&
                this.projectFile.FullName == project.projectFile.FullName
            );
        }

        public override int GetHashCode()
        {
            if (projectFile != null)
                return projectFile.GetHashCode();
            else
                return Id.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", Name, FilePath);
        }

        public static string GetName(PackageBase package, ProjectItemType type, string initName)
        {
            string n = initName;
            int i = 1;
            do
            {
                if (CheckName(package, type, n))
                    return n;
                n = initName + i++;
            }
            while (true);
        }

        public static bool CheckName(PackageBase package, ProjectItemType type, string name)
        {
            if (package == null) return true;
            foreach (var i in package.Items)
            {
                if (i.ItemType == type && i.Name == name)
                    return false;
            }
            return true;
        }

        public void AddEntity(IEntity item)
        {
            entities.Add(item);
        }

        public void RemoveEntity(IEntity item)
        {
            foreach (var m in models)
                m.RemoveEntity(item);
            entities.Remove(item);
        }

        public void AddModel(Model item)
        {
            models.Add(item);
        }

        public void RemoveModel(Model item)
        {
            models.Remove(item);
        }



        //internal void RemoveEntity(TypeBase entity)
        //{
        //    RemoveEntity(this, entity);
        //}

        //void RemoveEntity(PackageBase package, IEntity entity)
        //{
        //    foreach (var item in package.Items)
        //    {
        //        if (item is Model)
        //            ((Model)item).RemoveEntity(entity);
        //        else if (item is Package)
        //            RemoveEntity((Package)item, entity);
        //    }
        //}
    }
}