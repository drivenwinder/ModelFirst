using System;
using System.Xml;

namespace EAP.ModelFirst.Core.Project.Relationships
{
	public abstract class Relationship : Element, ISerializableElement
	{
		string label = string.Empty;
		bool attached = false;

		public event EventHandler Attaching;
		public event EventHandler Detaching;
		public event SerializeEventHandler Serializing;
		public event SerializeEventHandler Deserializing;

        [System.ComponentModel.Browsable(false)]
		public abstract IEntity First
		{
			get;
			protected set;
		}

        [System.ComponentModel.Browsable(false)]
		public abstract IEntity Second
		{
			get;
			protected set;
		}

        [System.ComponentModel.Browsable(false)]
		public abstract RelationshipType RelationshipType
		{
			get;
		}

		public virtual string Label
		{
			get
			{
				return label;
			}
			set
			{
				if (value == "")
					value = null;
				
				if (label != value && SupportsLabel)
				{
					label = value;
					Changed();
				}
			}
		}

        public string Connection
        {
            get { return ToString(); }
        }

        [System.ComponentModel.Browsable(false)]
		public virtual bool SupportsLabel
		{
			get { return false; }
		}

		/// <exception cref="RelationshipException">
		/// Cannot finalize relationship.
		/// </exception>
		internal void Attach()
		{
			if (!attached)
				OnAttaching(EventArgs.Empty);
			attached = true;
		}

		internal void Detach()
		{
			if (attached)
				OnDetaching(EventArgs.Empty);
			attached = false;
		}

		protected virtual void CopyFrom(Relationship relationship)
		{
			label = relationship.label;
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

            if (SupportsLabel && Label != null)
                node.CreateElement("Label", Label);

			OnSerializing(new SerializeEventArgs(node));
		}

		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
		protected internal virtual void Deserialize(XmlElement node)
		{
			if (node == null)
				throw new ArgumentNullException("node");

			if (SupportsLabel)
                Label = node["Label"].GetValue("");

			OnDeserializing(new SerializeEventArgs(node));
		}

		protected virtual void OnAttaching(EventArgs e)
		{
			if (Attaching != null)
				Attaching(this, e);
		}

		protected virtual void OnDetaching(EventArgs e)
		{
			if (Detaching != null)
				Detaching(this, e);
		}

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

		public abstract override string ToString();
	}
}
