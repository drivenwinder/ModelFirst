using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;
using System.Xml;
using System.Reflection;

namespace EAP.ModelFirst.Core.Project
{
    public abstract class PackageBase : IModifiable, INamedObject
    {
        bool isDirty;
        protected string name;
        protected List<IProjectItem> items = new List<IProjectItem>();

        public event ProjectItemEventHandler ItemAdded;
        public event ProjectItemEventHandler ItemRemoved;
        public event EventHandler Renamed;
        public event EventHandler Modified;
        public event SerializeEventHandler Serializing;
        public event SerializeEventHandler Deserializing;

        protected PackageBase()
        {
            name = Strings.Untitled;
        }

        [System.ComponentModel.Browsable(false)]
        public Guid Id { get; protected set; }

        [System.ComponentModel.Browsable(false)]
        public bool IsUntitled { get; set; }

        [System.ComponentModel.Browsable(false)]
        public bool Collapsed { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                if (value.IsNullOrEmpty())
                    throw new BadSyntaxException(Strings.ErrorNameIsRequired);
                CheckName(value);
                if (name != value)
                {
                    name = value;
                    IsUntitled = false;
                    OnRenamed(EventArgs.Empty);
                    OnModified(EventArgs.Empty);
                }
            }
        }

        protected virtual void CheckName(string name)
        {

        }

        [System.ComponentModel.Browsable(false)]
        public abstract Project ProjectInfo { get; }

        [System.ComponentModel.Browsable(false)]
        public bool IsDirty
        {
            get { return isDirty; }
        }

        [System.ComponentModel.Browsable(false)]
        public IEnumerable<IProjectItem> Items { get { return items; } }

        public void Add(IProjectItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (items.Contains(item))
                throw new ArgumentException("The package already contains this item.");

            if (!Project.CheckName(this, item.ItemType, item.Name))
                throw new BadSyntaxException(Strings.ErrorNameExists.FormatArgs(item.Name));
            
            item.ProjectInfo = ProjectInfo;
            item.Package = this;
            item.Modified += new EventHandler(item_Modified);
            items.Add(item);
            if (item is IEntity)
                ProjectInfo.AddEntity((IEntity)item);
            if (item is Model)
                ProjectInfo.AddModel((Model)item);
            OnItemAdded(new ProjectItemEventArgs(item));
            OnModified(EventArgs.Empty);
        }

        void item_Modified(object sender, EventArgs e)
        {
            OnModified(EventArgs.Empty);
        }

        public void Remove(IProjectItem item)
        {
            if (item is IEntity)
                ProjectInfo.RemoveEntity((IEntity)item);
            if (item is Model)
                ProjectInfo.RemoveModel((Model)item);
            if (items.Remove(item))
            {
                item.Modified -= new EventHandler(item_Modified);
                OnItemRemoved(new ProjectItemEventArgs(item));
                OnModified(EventArgs.Empty);
            }
        }

        public virtual void Clean()
        {
            foreach (IProjectItem item in Items)
            {
                item.Clean();
            }
            isDirty = false;
        }

        #region OnChanged

        protected virtual void OnRenamed(EventArgs e)
        {
            if (Renamed != null)
                Renamed(this, e);
        }

        protected virtual void OnModified(EventArgs e)
        {
            isDirty = true;
            if (Modified != null)
                Modified(this, e);
        }

        protected virtual void OnItemAdded(ProjectItemEventArgs e)
        {
            if (ItemAdded != null)
                ItemAdded(this, e);
        }

        protected virtual void OnItemRemoved(ProjectItemEventArgs e)
        {
            if (ItemRemoved != null)
                ItemRemoved(this, e);
        }

        #endregion

        public void Move(IProjectItem item, PackageBase to)
        {
            if (!to.items.Contains(item))
            {
                if (!Project.CheckName(to, item.ItemType, item.Name))
                    throw new BadSyntaxException(Strings.ErrorNameExists.FormatArgs(item.Name));
                items.Remove(item);
                to.items.Add(item);
                item.Package = to;
                OnModified(EventArgs.Empty);
            }
        }

        public List<TypeBase> GetTypes()
        {
            List<TypeBase> lst = new List<TypeBase>();
            LoadTypes(lst, Items);
            return lst;
        }

        void LoadTypes(List<TypeBase> lst, IEnumerable<IProjectItem> items)
        {
            foreach (var i in items)
            {
                if (i is TypeBase)
                    lst.Add((TypeBase)i);
                if (i is PackageBase)
                    LoadTypes(lst, ((PackageBase)i).Items);
            }
        }

        public virtual void Serialize(XmlElement root)
        {
            if (root == null)
                throw new ArgumentNullException("root");

            root.CreateElement("Id", Id.ToString());
            root.CreateElement("Name", Name);
            root.CreateElement("Collapsed", Collapsed.ToString());

            XmlElement child = root.OwnerDocument.CreateElement("ProjectItems");

            foreach (IProjectItem item in items)
            {
                XmlElement itemElement = root.OwnerDocument.CreateElement("ProjectItem");
                item.Serialize(itemElement);
                if (item.ItemType == ProjectItemType.Type)
                    itemElement.SetAttribute("type", (item as TypeBase).EntityType.ToString());
                else
                    itemElement.SetAttribute("type", item.ItemType.ToString());

                child.AppendChild(itemElement);
            }
            root.AppendChild(child);

            OnSerializing(new SerializeEventArgs(root));
        }

        public virtual void Deserialize(DeserializeContext context, XmlElement root)
        {
            if (root == null)
                throw new ArgumentNullException("root");

            Id = root["Id"].GetValue(Guid.Empty);
            name = root["Name"].GetValue(Strings.Untitled);
            Collapsed = root["Collapsed"].GetValue(false);

            XmlNodeList nodeList = root.SelectNodes("ProjectItems/ProjectItem");

            foreach (XmlElement itemElement in nodeList)
            {
                XmlAttribute typeAttribute = itemElement.Attributes["type"];
                XmlAttribute assemblyAttribute = itemElement.Attributes["assembly"];


                if (assemblyAttribute == null)
                {
                    string typeName = typeAttribute.InnerText;
                    IProjectItem projectItem = null;
                    if (typeName == ProjectItemType.Package.ToString())
                        projectItem = new Package();
                    else if (typeName == ProjectItemType.Model.ToString())
                        projectItem = Model.CreateProjectItem();
                    else if(typeName == EntityType.Class.ToString())
                        projectItem = ProjectInfo.Language.CreateClass("Class");
                    else if (typeName == EntityType.Delegate.ToString())
                        projectItem = ProjectInfo.Language.CreateDelegate("Delegate");
                    else if (typeName == EntityType.Enum.ToString())
                        projectItem = ProjectInfo.Language.CreateEnum("Enum");
                    else if (typeName == EntityType.Interface.ToString())
                        projectItem = ProjectInfo.Language.CreateInterface("Interface");
                    else if (typeName == EntityType.Structure.ToString())
                        projectItem = ProjectInfo.Language.CreateStructure("Structure");

                    if(projectItem == null)
                        throw new InvalidException("Invalid type [{0}] of ProjectItem.".FormatArgs(typeName));
                    projectItem.ProjectInfo = ProjectInfo;
                    projectItem.Deserialize(context, itemElement);
                    Add(projectItem);
                }
                else
                {

                    string typeName = typeAttribute.InnerText;
                    string assemblyName = assemblyAttribute.InnerText;

                    try
                    {
                        Assembly assembly = Assembly.Load(assemblyName);
                        IProjectItem projectItem = (IProjectItem)assembly.CreateInstance(
                            typeName, false,
                            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                            null, null, null, null);
                        projectItem.ProjectInfo = ProjectInfo;
                        projectItem.Deserialize(context, itemElement);
                        Add(projectItem);
                    }
                    catch (InvalidException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidException("Invalid type or assembly of ProjectItem.", ex);
                    }
                }
            }
            OnDeserializing(new SerializeEventArgs(root));
        }



        void OnSerializing(SerializeEventArgs e)
        {
            if (Serializing != null)
                Serializing(this, e);
        }

        void OnDeserializing(SerializeEventArgs e)
        {
            if (Deserializing != null)
                Deserializing(this, e);
            OnModified(EventArgs.Empty);
        }
    }
}
