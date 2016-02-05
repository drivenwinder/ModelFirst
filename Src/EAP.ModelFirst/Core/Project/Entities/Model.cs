using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using EAP.ModelFirst.Core.Project.Relationships;
using EAP.ModelFirst.Properties;
using System.Reflection;

namespace EAP.ModelFirst.Core.Project.Entities
{
    public abstract class Model : IProjectDocument
	{
        string name;
        bool isDirty;
        bool loading;
        Project project;
		List<IEntity> entities = new List<IEntity>();
		List<Relationship> relationships = new List<Relationship>();

		public event EventHandler Modified;
		public event EventHandler Renamed;
		public event EventHandler Closing;
		public event EntityEventHandler EntityAdded;
		public event EntityEventHandler EntityRemoved;
		public event RelationshipEventHandler RelationAdded;
		public event RelationshipEventHandler RelationRemoved;
		public event SerializeEventHandler Serializing;
        public event SerializeEventHandler Deserializing;
        public event EventHandler StateChanged;

		protected Model()
        {
            name = Strings.Untitled;
		}

        #region Property

        [System.ComponentModel.Browsable(false)]
        public Guid Id { get; protected set; }

		public string Name
		{
			get
			{
				return name;
			}
			set
            {
                if (value.IsNullOrEmpty())
                    throw new BadSyntaxException(Strings.ErrorNameIsRequired);
                if (!Project.CheckName(Package, ItemType, value))
                    throw new BadSyntaxException(Strings.ErrorNameExists.FormatArgs(value));

				if (name != value && value != null)
				{
                    name = value;
                    IsUntitled = false;
					OnRenamed(EventArgs.Empty);
					OnModified(EventArgs.Empty);
				}
			}
		}

        [System.ComponentModel.Browsable(false)]
        public bool IsUntitled { get; set; }

        [System.ComponentModel.Browsable(false)]
		public Language Language
		{
			get { return ProjectInfo.Language; }
        }

        [System.ComponentModel.Browsable(false)]
        public ProjectItemType ItemType
        {
            get { return ProjectItemType.Model; }
        }

        [System.ComponentModel.Browsable(false)]
        public virtual Project ProjectInfo
        {
            get { return project; }
            set
            {
                if (project != null && project != value)
                    throw new InvalidException("Project have been set value.");
                project = value;
            }
        }

        public PackageBase Package { get; set; }

        [System.ComponentModel.Browsable(false)]
		public bool IsDirty
		{
			get { return isDirty; }
        }

        [System.ComponentModel.Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return (entities.Count == 0 && relationships.Count == 0);
            }
        }

        [System.ComponentModel.Browsable(false)]
        public IEnumerable<IEntity> Entities
        {
            get { return entities; }
        }

        [System.ComponentModel.Browsable(false)]
        public IEnumerable<Relationship> Relationships
        {
            get { return relationships; }
        }

        protected bool Loading
		{
			get { return loading; }
        }

        #endregion

		private void ElementChanged(object sender, EventArgs e)
		{
			OnModified(e);
		}
        
        protected void AddEntity(IEntity entity)
        {
            if (entities.Contains(entity))
                throw new InvalidOperationException(Strings.ErrorItemExists.FormatArgs(entity.Name));
			entities.Add(entity);
			entity.Modified += new EventHandler(ElementChanged);
			OnEntityAdded(new EntityEventArgs(entity));
		}

        protected void AddRelationship(Relationship relationship)
		{
			relationships.Add(relationship);
			relationship.Modified += new EventHandler(ElementChanged);
			OnRelationAdded(new RelationshipEventArgs(relationship));
		}
        
        public void RemoveEntity(IEntity entity)
		{
			if (entities.Remove(entity))
			{
				entity.Modified -= new EventHandler(ElementChanged);
				RemoveRelationships(entity);
				OnEntityRemoved(new EntityEventArgs(entity));
			}
		}

		void RemoveRelationships(IEntity entity)
		{
			for (int i = 0; i < relationships.Count; i++)
			{
				Relationship relationship = relationships[i];
				if (relationship.First == entity || relationship.Second == entity)
                {
                    Detach(relationship);
					relationship.Modified -= new EventHandler(ElementChanged);
					relationships.RemoveAt(i--);
					OnRelationRemoved(new RelationshipEventArgs(relationship));
				}
			}
		}

        protected internal void RemoveRelationship(Relationship relationship)
		{
			if (relationships.Contains(relationship))
			{
                Detach(relationship);
				relationship.Modified -= new EventHandler(ElementChanged);
				relationships.Remove(relationship);
				OnRelationRemoved(new RelationshipEventArgs(relationship));
			}
        }

        void Detach(Relationship relationship)
        {
            foreach(var m in ProjectInfo.Models)
                if(m != this && m.relationships.Contains(relationship))
                    return ;
            relationship.Detach();
        }

        void IModifiable.Clean()
        {
            var changed = isDirty;
            isDirty = false;
            if (changed)
                OnStateChanged(EventArgs.Empty);
        }

        void IDocumentItem.Close()
        {
            OnClosing(EventArgs.Empty);
        }

		void IProjectItem.Serialize(XmlElement node)
		{
			Serialize(node);
		}

        void IProjectItem.Deserialize(DeserializeContext context, XmlElement node)
        {
            context.Add(this, node);
            Id = node["Id"].GetValue(Guid.Empty);
            name = node["Name"].GetValue(Strings.Untitled);
        }

        public void Serialize(XmlElement root)
		{
			if (root == null)
				throw new ArgumentNullException("root");
            
            root.CreateElement("Id", Id.ToString());
            root.CreateElement("Name", Name);

			SaveEntitites(root);
			SaveRelationships(root);

			OnSerializing(new SerializeEventArgs(root));
		}

        public void Deserialize(XmlElement root)
		{
			if (root == null)
				throw new ArgumentNullException("root");
			loading = true;

            entities.Clear();
            relationships.Clear();

            LoadEntitites(root);
			LoadRelationships(root);

			OnDeserializing(new SerializeEventArgs(root));
			loading = false;
		}

        protected abstract void LoadEntitites(XmlNode root);

        protected abstract void LoadRelationships(XmlNode root);

        protected abstract void SaveEntitites(XmlElement node);

        protected abstract void SaveRelationships(XmlNode root);

        #region OnChange

        protected virtual void OnEntityAdded(EntityEventArgs e)
		{
			if (EntityAdded != null)
				EntityAdded(this, e);
			OnModified(EventArgs.Empty);
		}

		protected virtual void OnEntityRemoved(EntityEventArgs e)
		{
			if (EntityRemoved != null)
				EntityRemoved(this, e);
			OnModified(EventArgs.Empty);
		}

		protected virtual void OnRelationAdded(RelationshipEventArgs e)
		{
			if (RelationAdded != null)
				RelationAdded(this, e);
			OnModified(EventArgs.Empty);
		}

		protected virtual void OnRelationRemoved(RelationshipEventArgs e)
		{
			if (RelationRemoved != null)
				RelationRemoved(this, e);
			OnModified(EventArgs.Empty);
		}

		protected virtual void OnSerializing(SerializeEventArgs e)
		{
			if (Serializing != null)
				Serializing(this, e);
		}

		protected virtual void OnDeserializing(SerializeEventArgs e)
		{
			if (Deserializing != null)
				Deserializing(this, e);
			OnModified(EventArgs.Empty);
		}

		protected virtual void OnModified(EventArgs e)
		{
            var changed = !isDirty;
			isDirty = true;
			if (Modified != null)
				Modified(this, e);
            if (changed)
                OnStateChanged(EventArgs.Empty);
		}

		protected virtual void OnRenamed(EventArgs e)
		{
			if (Renamed != null)
				Renamed(this, e);
		}

		protected virtual void OnClosing(EventArgs e)
		{
			if (Closing != null)
				Closing(this, e);
		}

        protected virtual void OnStateChanged(EventArgs e)
        {
            if (StateChanged != null)
                StateChanged(this, e);
        }

        #endregion

        public override string ToString()
		{
			if (IsDirty)
				return Name + "*";
			else
				return Name;
		}

        public static IProjectItem CreateProjectItem()
        {
            return (IProjectItem)typeof(Model).Assembly.CreateInstance("EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Diagram",
                false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null, null);
        }
    }
}
