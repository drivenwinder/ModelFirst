using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Core.Project.Entities
{
    public abstract class TypeBase : LanguageElement, IProjectDocument, IEntity
    {
        string name;
        string comments;
        Project project;
        AccessModifier access = AccessModifier.Public;
        CompositeType nestingParent = null;
        NestingRelationship nestingRelationship;
        List<AssociationRelationship> associationRelationships = new List<AssociationRelationship>();
        List<TypeBase> associations = new List<TypeBase>();

        public event SerializeEventHandler Serializing;
        public event SerializeEventHandler Deserializing;
        public event EventHandler Renamed;
        public event EventHandler Closing;
        public event EventHandler StateChanged;

        protected virtual void OnStateChanged(EventArgs e)
        {
            if (StateChanged != null)
                StateChanged(this, e);
        }

        public override void Clean()
        {
            var changed = IsDirty;
            base.Clean();
            if (changed)
                OnStateChanged(EventArgs.Empty);
        }

        protected override void Changed()
        {
            var changed = !IsDirty;
            base.Changed();
            if (changed)
                OnStateChanged(EventArgs.Empty);
        }

        /// <exception cref="BadSyntaxException">
        /// The <paramref name="name"/> does not fit to the syntax.
        /// </exception>
        protected TypeBase(string name)
        {
            Initializing = true;
            Id = Guid.NewGuid();
            Name = name;
            Initializing = false;
        }

        [System.ComponentModel.Browsable(false)]
        public Guid Id { get; protected set; }

        /// <exception cref="BadSyntaxException">
        /// The <paramref name="value"/> does not fit to the syntax.
        /// </exception>
        public virtual string Name
        {
            get { return name; }
            set
            {
                if (value.IsNullOrEmpty())
                    throw new BadSyntaxException(Strings.ErrorNameIsRequired);

                string newName = Language.GetValidName(value, true);

                if (newName != name)
                {
                    if (!Project.CheckName(Package, ItemType, value))
                        throw new BadSyntaxException(Strings.ErrorNameExists.FormatArgs(value));

                    name = newName;
                    IsUntitled = false;
                    OnRenamed(EventArgs.Empty);
                    Changed();
                }
            }
        }

        public virtual string Comments
        {
            get { return comments; }
            set
            {
                if (comments != value)
                {
                    comments = value;
                    Changed();
                }
            }
        }

        [System.ComponentModel.Browsable(false)]
        public abstract EntityType EntityType { get; }

        [System.ComponentModel.Browsable(false)]
        public ProjectItemType ItemType
        {
            get { return ProjectItemType.Type; }
        }

        /// <exception cref="BadSyntaxException">
        /// The type visibility is not valid in the current context.
        /// </exception>
        public virtual AccessModifier AccessModifier
        {
            get { return access; }
            set
            {
                if (!Language.IsValidModifier(value))
                    throw new BadSyntaxException(Strings.ErrorInvalidModifier);

                if (access != value)
                {
                    access = value;
                    Changed();
                }
            }
        }

        [System.ComponentModel.Browsable(false)]
        public abstract AccessModifier DefaultAccess { get; }

        [System.ComponentModel.Browsable(false)]
        public AccessModifier Access
        {
            get
            {
                if (AccessModifier == AccessModifier.Default)
                    return DefaultAccess;
                else
                    return AccessModifier;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public IEnumerable<AssociationRelationship> AssociationRelationships
        {
            get { return associationRelationships; }
        }

        [System.ComponentModel.Browsable(false)]
        public IEnumerable<TypeBase> Associations
        {
            get { return associations; }
        }

        public void AddAssociationRelationship(AssociationRelationship a)
        {
            associationRelationships.Add(a);
            ((TypeBase)a.Second).associations.Add(this);
        }

        public void RemoveAssociationRelationship(AssociationRelationship a)
        {
            ((TypeBase)a.Second).associations.Remove(this);
            associationRelationships.Remove(a);
        }

        [System.ComponentModel.Browsable(false)]
        public NestingRelationship NestingRelationship
        {
            get { return nestingRelationship; }
            set
            {
                NestingParent = (CompositeType)value.First;
                nestingRelationship = value;
            }
        }

        /// <exception cref="RelationshipException">
        /// Parent type does not support nesting.-or-
        /// The inner type is already nested.-or-
        /// The parent type is already a child member of the type.
        /// </exception>
        [System.ComponentModel.Browsable(false)]
        public virtual CompositeType NestingParent
        {
            get { return nestingParent; }
            protected internal set
            {
                if (nestingParent != value)
                {
                    if (value == this)
                    {
                        throw new RelationshipException(Strings.ErrorRecursiveNesting);
                    }
                    if (value != null && !value.SupportsNesting)
                    {
                        throw new RelationshipException(Strings.ErrorNestingNotSupported);
                    }
                    if (value != null && value.IsNestedAncestor(this))
                    {
                        throw new RelationshipException(Strings.ErrorCyclicNesting);
                    }

                    if (nestingParent != null)
                        nestingParent.RemoveNestedChild(this);
                    nestingParent = value;
                    if (nestingParent != null)
                        nestingParent.AddNestedChild(this);
                    Changed();
                }
            }
        }

        public CompositeType GetNestingRoot()
        {
            if (IsNested)
                return NestingParent.GetNestingRoot();
            if (this is CompositeType)
            {
                var c = (CompositeType)this;
                if (c.SupportsNesting)
                    return c;
            }
            return null;
        }

        [System.ComponentModel.Browsable(false)]
        public bool IsNested
        {
            get { return (NestingParent != null); }
        }

        [System.ComponentModel.Browsable(false)]
        public abstract string Stereotype { get; }

        [System.ComponentModel.Browsable(false)]
        public abstract string Signature { get; }

        [System.ComponentModel.Browsable(false)]
        public Project ProjectInfo
        {
            get { return project; }
            set
            {
                if (project != null && project != value)
                    throw new InvalidException("Project have been set value.");
                project = value;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public bool IsUntitled { get; set; }

        public PackageBase Package { get; set; }
        
        /// <summary>
        /// full name of package 
        /// </summary>
        /// <returns></returns>

        public string GetPackageName(string seperator = ".", bool trimEnd = true)
        {
            string package = "";
            PackageBase t = Package;
            while (t != ProjectInfo)
            {
                package = t.Name + seperator + package;
                t = ((IProjectItem)t).Package;
            }
            if (trimEnd && package.EndsWith(seperator))
                return package.Remove(package.Length - seperator.Length);
            return package;
        }

        private bool IsNestedAncestor(TypeBase type)
        {
            if (NestingParent != null && NestingParent.IsNestedAncestor(type))
                return true;
            else
                return (type == this);
        }

        public abstract bool MoveUpItem(object item);

        public abstract bool MoveDownItem(object item);

        protected static bool MoveUp(IList list, object item)
        {
            if (item == null)
                return false;

            int index = list.IndexOf(item);
            if (index > 0)
            {
                object temp = list[index - 1];
                list[index - 1] = list[index];
                list[index] = temp;
                return true;
            }
            else
            {
                return false;
            }
        }

        protected static bool MoveDown(IList list, object item)
        {
            if (item == null)
                return false;

            int index = list.IndexOf(item);
            if (index >= 0 && index < list.Count - 1)
            {
                object temp = list[index + 1];
                list[index + 1] = list[index];
                list[index] = temp;
                return true;
            }
            else
            {
                return false;
            }
        }

        protected virtual void CopyFrom(TypeBase type)
        {
            Id = type.Id;
            name = type.name;
            access = type.access;
            comments = type.comments;
        }

        void ISerializableElement.Serialize(XmlElement node)
        {
            Serialize(node);
        }

        void ISerializableElement.Deserialize(XmlElement node)
        {
            Deserialize(node);
        }

        /// <exception cref="ArgumentNullException">
        /// <paramref name="node"/> is null.
        /// </exception>
        protected internal virtual void Serialize(XmlElement node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            node.CreateElement("Id", Id.ToString());
            node.CreateElement("Name", Name);
            node.CreateElement("Access", AccessModifier.ToString());
            if(Comments.IsNotEmpty())
                node.CreateElement("Comments", Comments);
            OnSerializing(new SerializeEventArgs(node));
        }

        /// <exception cref="BadSyntaxException">
        /// An error occured whiledeserializing.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The XML document is corrupt.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="node"/> is null.
        /// </exception>
        protected internal virtual void Deserialize(XmlElement node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            RaiseChangedEvent = false;

            Id = node["Id"].GetValue(Guid.Empty);
            Name = node["Name"].GetValue(Strings.Untitled);
            AccessModifier = node["Access"].GetValue(AccessModifier.Default);
            Comments = node["Comments"].GetValue("");

            RaiseChangedEvent = true;
            OnDeserializing(new SerializeEventArgs(node));
        }

        public void Close()
        {
            Clean();
            OnClosing(EventArgs.Empty);
        }

        void IProjectItem.Serialize(XmlElement node)
        {
            Serialize(node);
        }

        void IProjectItem.Deserialize(DeserializeContext context, XmlElement node)
        {
            Deserialize(node);
        }

        public override string ToString()
        {
            return Name + ": " + Signature;
        }

        #region OnChanged

        private void OnSerializing(SerializeEventArgs e)
        {
            if (Serializing != null)
                Serializing(this, e);
        }

        private void OnDeserializing(SerializeEventArgs e)
        {
            if (Deserializing != null)
                Deserializing(this, e);
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

        #endregion
    }
}
