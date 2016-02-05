using System;
using System.Collections;
using System.ComponentModel;
using EAP.Utils;

namespace EAP.Entity
{
    [Serializable]
    public partial class DataObject<T> : BindableObject, ICloneable
        where T : DataObject<T>
    {
        private int displayIndex = -1;

        protected DataState dataState = DataState.None;

        protected Hashtable @__table = new Hashtable();

        /// <summary>
        /// The state of the object.
        /// </summary>
        [Browsable(false)]
        public DataState DataState
        {
            get { return dataState; }
        }

        [Browsable(false)]
        public int DisplayIndex
        {
            get { return displayIndex; }
            set { displayIndex = value; }
        }

        protected V GetValue<V>(string propertyName)
        {
            if (@__table.ContainsKey(propertyName))
                return (V)@__table[propertyName];
            return default(V);
        }

        /// <summary>
        /// Set the value by of the property. And the events will be fired if the DataState is other than Initializing.
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        protected void SetValue<V>(string propertyName, V value)
        {
            V original = GetValue<V>(propertyName);
            if (!object.Equals(original, value))
            {
                object oldValue = ObjectHelper.Clone(original);
                @__table[propertyName] = value;
                if (DataState != DataState.Initializing)
                    OnValueChanged(propertyName, value, oldValue);
                OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Change the DataState to Created.
        /// </summary>
        public void MarkCreated()
        {
            dataState = DataState.Created;
        }

        /// <summary>
        /// Change the DataState to Modified.
        /// </summary>
        public void MarkModified()
        {
            dataState = DataState.Modified;
        }

        /// <summary>
        /// Change the DataState to Deleted.
        /// </summary>
        public void MarkDeleted()
        {
            dataState = DataState.Deleted;
        }

        /// <summary>
        /// Change the DataState to None.
        /// </summary>
        public void ResetState()
        {
            dataState = DataState.None;
        }

        public virtual object Clone()
        {
            return ObjectHelper.BinaryClone(this);
        }

        /// <summary>
        /// Change the DataState to Initializing.
        /// </summary>
        public void BeginInit()
        {
            dataState = DataState.Initializing;
        }

        /// <summary>
        /// Call ResetState().
        /// </summary>
        public void EndInit()
        {
            ResetState();
        }
    }
}
