using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Core.Project.Entities
{
    public sealed class Package : PackageBase, IProjectItem
    {
        Project project;

        public event EventHandler Closing;

        public static Package Create(string name)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentException("Name cannot empty string.");
            return new Package() { Id = Guid.NewGuid(), name = name };
        }

        [System.ComponentModel.Browsable(false)]
        public ProjectItemType ItemType
        {
            get { return ProjectItemType.Package; }
        }

        public override Project ProjectInfo
        {
            get { return project; }
        }

        Project IProjectItem.ProjectInfo
        {
            get { return project; }
            set
            {
                if (project != null && project != value)
                    throw new InvalidException("Project have been set value.");
                project = value;
            }
        }

        PackageBase IProjectItem.Package { get; set; }

        public void Close()
        {
            OnClosing(EventArgs.Empty);
        }

        protected override void CheckName(string name)
        {
            base.CheckName(name);
            if (!Project.CheckName(((IProjectItem)this).Package, ItemType, name))
                throw new BadSyntaxException(Strings.ErrorNameExists.FormatArgs(name));
        }

        #region On Changed

        void OnClosing(EventArgs e)
        {
            if (Closing != null)
                Closing(this, e);
        }

        #endregion

        public override string ToString()
        {
            string package = "";
            PackageBase t = ((IProjectItem)this).Package;
            while (t != ProjectInfo)
            {
                package = t.Name + "::" + package;
                t = ((IProjectItem)t).Package;
            }
            return package + Name;
        }
    }
}
