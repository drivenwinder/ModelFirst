using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.ModelFirst.Core.Project
{
    public class ProjectEventArgs
    {
        Project project;

        public ProjectEventArgs(Project project)
        {
            this.project = project;
        }

        public Project Project
        {
            get { return project; }
        }
    }

    public delegate void ProjectEventHandler(object sender, ProjectEventArgs e);
}
