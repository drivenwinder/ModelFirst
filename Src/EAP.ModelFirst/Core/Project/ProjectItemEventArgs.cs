﻿using System;

namespace EAP.ModelFirst.Core.Project
{
	public delegate void ProjectItemEventHandler(object sender, ProjectItemEventArgs e);

	public class ProjectItemEventArgs
	{
		IProjectItem projectItem;

		public ProjectItemEventArgs(IProjectItem projectItem)
		{
			this.projectItem = projectItem;
		}

		public IProjectItem ProjectItem
		{
			get { return projectItem; }
		}
	}
}
