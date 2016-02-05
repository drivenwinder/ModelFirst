using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using EAP.Utils;

namespace EAP.Entity
{
    [Serializable]
    public class DataObjectList<T> : IList<T>, IBindingList, IList, ISupportInitializeNotification, IEditableObject, ICloneable
        where T : DataObject<T>, new()
    {
        #region fileds

        List<T> items = new List<T>();
        [NonSerialized]
        List<T> deletedItems = new List<T>();
        [NonSerialized]
        List<T> modifiedItems = new List<T>();
        [NonSerialized]
        List<T> newItems = new List<T>();
        [NonSerialized]
        bool isInitialized = true;
        [NonSerialized]
        int editLevel = 0;
        [NonSerialized]
        List<EditedObject<T>> track = new List<EditedObject<T>>();
        [NonSerialized]
        bool editable = false;
        [NonSerialized]
        EventHandler<ListChangedEventArgs<T>> listChangedHandler;
        [NonSerialized]
        ListChangedEventHandler listChangedEventHandler;
        [NonSerialized]
        EventHandler initializedHandler;
        [NonSerialized]
        PropertyDescriptor sortProperty;
        [NonSerialized]
        ListSortDirection sortDirection;

        #endregion

        public DataObjectList()
        {
        }

        protected DataObjectList(IEnumerable<T> collection)
        {
            AddRange(collection);
        }

        protected DataObjectList(SerializationInfo info, StreamingContext context)
        {
        }

        public event EventHandler<ListChangedEventArgs<T>> ListChanged
        {
            add
            {
                listChangedHandler = (EventHandler<ListChangedEventArgs<T>>)
                    System.Delegate.Combine(listChangedHandler, value);
            }
            remove
            {
                listChangedHandler = (EventHandler<ListChangedEventArgs<T>>)
                    System.Delegate.Remove(listChangedHandler, value);
            }
        }

        #region ISupportInitializeNotification Members

        public event EventHandler Initialized
        {
            add
            {
                initializedHandler = (EventHandler)
                    System.Delegate.Combine(initializedHandler, value);
            }
            remove
            {
                initializedHandler = (EventHandler)
                    System.Delegate.Remove(initializedHandler, value);
            }
        }

        public bool IsInitialized { get { return isInitialized; } }

        public void BeginInit()
        {
            isInitialized = false;
        }

        public void EndInit()
        {
            isInitialized = true;
            if (initializedHandler != null)
                initializedHandler.Invoke(this, new EventArgs());
        }

        #endregion

        public bool Editable
        {
            get { return editable; }
        }

        public bool CanUndo { get { return editLevel > 0; } }

        public bool CanRedo { get { return editLevel < track.Count; } }

        public List<T> ModifiedItems { get { return modifiedItems; } }

        public List<T> DeletedItems { get { return deletedItems; } }

        public List<T> NewItems { get { return newItems; } }

        public List<T> GetChangedItems()
        {
            List<T> lst = new List<T>(newItems.Count + modifiedItems.Count + deletedItems.Count);
            AddToChangedList(lst, newItems);
            AddToChangedList(lst, modifiedItems);
            AddToChangedList(lst, deletedItems);
            return lst;
        }

        void AddToChangedList(List<T> lst, List<T> items)
        {
            foreach (var item in items)
            {
                T t = (T)item.Clone();
                t.DisplayIndex = IndexOf(item);
                lst.Add(t);
            }
        }

        public bool HasChanged { get { return modifiedItems.Count > 0 || newItems.Count > 0 || deletedItems.Count > 0; } }

        EditedObject<T> Undo(bool suppressEvent)
        {
            isInitialized = false;
            EditedObject<T> editTrack = track[--editLevel];
            switch (editTrack.NewState)
            {
                case DataState.Modified:
                    editTrack.DataObject.GetType().GetProperty(editTrack.PropertyName).SetValue(editTrack.DataObject, editTrack.OldValue, null);
                    if (editTrack.OldState == DataState.None)
                    {
                        editTrack.DataObject.ResetState();
                        modifiedItems.Remove(editTrack.DataObject);
                    }
                    editTrack.DataObject.OnPropertyChanged(editTrack.PropertyName);
                    editTrack.DataObject.OnValueChanged(editTrack.PropertyName, editTrack.OldValue, editTrack.NewValue);
                    break;
                case DataState.Deleted:
                    switch (editTrack.OldState)
                    {
                        case DataState.None:
                            editTrack.DataObject.ResetState();
                            deletedItems.Remove(editTrack.DataObject);
                            break;
                        case DataState.Created:
                            newItems.Add(editTrack.DataObject);
                            break;
                        case DataState.Modified:
                            modifiedItems.Add(editTrack.DataObject);
                            editTrack.DataObject.MarkModified();
                            deletedItems.Remove(editTrack.DataObject);
                            break;
                    }
                    items.Add(editTrack.DataObject);
                    editTrack.DataObject.ValueChanged += item_ValueChanged;
                    break;
                case DataState.Created:
                    newItems.Remove(editTrack.DataObject);
                    items.Remove(editTrack.DataObject);
                    editTrack.DataObject.ValueChanged -= item_ValueChanged;
                    break;
            }
            isInitialized = true;
            if (!suppressEvent)
            {
                OnListChanged(new ListChangedEventArgs<T>(ListChangedType.ItemUndo, editTrack));
                OnListChanged(new ListChangedEventArgs(System.ComponentModel.ListChangedType.Reset, -1));
            }
            return editTrack;
        }

        public EditedObject<T> Undo()
        {
            return Undo(false);
        }

        EditedObject<T> Redo(bool suppressEvent)
        {
            isInitialized = false;
            EditedObject<T> editTrack = track[editLevel++];
            switch (editTrack.NewState)
            {
                case DataState.Modified:
                    editTrack.DataObject.GetType().GetProperty(editTrack.PropertyName).SetValue(editTrack.DataObject, editTrack.NewValue, null);
                    if (editTrack.OldState == DataState.None)
                    {
                        modifiedItems.Add(editTrack.DataObject);
                        editTrack.DataObject.MarkModified();
                    }
                    editTrack.DataObject.OnPropertyChanged(editTrack.PropertyName);
                    editTrack.DataObject.OnValueChanged(editTrack.PropertyName, editTrack.NewValue, editTrack.OldValue);
                    break;
                case DataState.Deleted:
                    if (editTrack.DataObject.DataState == DataState.Created)
                        newItems.Remove(editTrack.DataObject);
                    else
                    {
                        if (editTrack.DataObject.DataState == DataState.Modified)
                            modifiedItems.Remove(editTrack.DataObject);
                        deletedItems.Add(editTrack.DataObject);
                    }
                    items.Remove(editTrack.DataObject);
                    editTrack.DataObject.MarkDeleted();
                    editTrack.DataObject.ValueChanged -= item_ValueChanged;
                    break;
                case DataState.Created:
                    newItems.Add(editTrack.DataObject);
                    items.Add(editTrack.DataObject);
                    editTrack.DataObject.MarkCreated();
                    editTrack.DataObject.ValueChanged += item_ValueChanged;
                    break;
            }
            isInitialized = true;
            if (!suppressEvent)
            {
                OnListChanged(new ListChangedEventArgs<T>(ListChangedType.ItemRedo, editTrack));
                OnListChanged(new ListChangedEventArgs(System.ComponentModel.ListChangedType.Reset, -1));
            }
            return editTrack;
        }

        public EditedObject<T> Redo()
        {
            return Redo(false);
        }

        protected virtual void OnListChanged(ListChangedEventArgs<T> e)
        {
            if (listChangedHandler != null)
                listChangedHandler.Invoke(this, e);
        }

        protected virtual void OnListChanged(ListChangedEventArgs e)
        {
            if (listChangedEventHandler != null)
                listChangedEventHandler.Invoke(this, e);
        }

        protected void AddTrack(EditedObject<T> editTrack)
        {
            if (editLevel < track.Count)
                track.RemoveRange(editLevel, track.Count - editLevel);
            track.Add(editTrack);
            editLevel++;
        }

        [OnDeserialized()]
        protected internal void OnDeserialized(StreamingContext context)
        {
            deletedItems = new List<T>();
            modifiedItems = new List<T>();
            newItems = new List<T>();
            track = new List<EditedObject<T>>();
            isInitialized = true;
        }

        protected void SetModified(T item, string propertyName, object oldValue, object newValue)
        {
            if (!IsInitialized || !Editable) return;
            EditedObject<T> editedObj = new EditedObject<T>()
            {
                DataObject = item,
                OldState = item.DataState,
                NewState = DataState.Modified,
                OldValue = oldValue,
                NewValue = newValue,
                PropertyName = propertyName
            };
            if (item.DataState == DataState.None)
            {
                modifiedItems.Add(item);
                item.MarkModified();
            }
            AddTrack(editedObj);
            OnListChanged(new ListChangedEventArgs<T>(ListChangedType.ItemModified, editedObj));
            OnListChanged(new ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemChanged, IndexOf(item)));
        }

        protected void SetAdded(T item)
        {
            item.ValueChanged += item_ValueChanged;
            if (!IsInitialized || !Editable) return;
            EditedObject<T> editedObj = new EditedObject<T>()
            {
                DataObject = item,
                OldState = item.DataState,
                NewState = DataState.Created
            };
            newItems.Add(item);
            item.MarkCreated();
            AddTrack(editedObj);
            OnListChanged(new ListChangedEventArgs<T>(ListChangedType.ItemAdded, editedObj));
            OnListChanged(new ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemAdded, this.Count - 1));
        }

        void item_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            SetModified((T)sender, e.PropertyName, e.OldValue, e.NewValue);
        }

        protected void SetDeleted(T item, int index)
        {
            item.ValueChanged -= item_ValueChanged;
            if (!IsInitialized || !Editable) return;
            EditedObject<T> editedObj = new EditedObject<T>()
            {
                DataObject = item,
                OldState = item.DataState,
                NewState = DataState.Deleted,
            };
            if (item.DataState == DataState.Created)
                newItems.Remove(item);
            else
            {
                if (item.DataState == DataState.Modified)
                    modifiedItems.Remove(item);
                deletedItems.Add(item);
                item.MarkDeleted();
            }
            AddTrack(editedObj);
            OnListChanged(new ListChangedEventArgs<T>(ListChangedType.ItemDeleted, editedObj));
            OnListChanged(new ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemDeleted, index));
        }

        private bool IsCompatibleObject(object value)
        {
            if (!(value is T) && ((value != null) || typeof(T).IsValueType))
                return false;
            return true;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                Add(item);
        }


        #region IList

        public int IndexOf(T item)
        {
            return items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            items.Insert(index, item);
            SetAdded(item);
        }

        public void RemoveAt(int index)
        {
            T item = items[index];
            items.RemoveAt(index);
            SetDeleted(item, index);
        }

        public T this[int index]
        {
            get { return items[index]; }
            set
            {
                T item = items[index];
                SetDeleted(item, index);
                items[index] = value;
                SetAdded(value);
            }
        }

        public void Add(T item)
        {
            items.Add(item);
            SetAdded(item);
        }

        public void Clear()
        {
            items.Clear();
            deletedItems.Clear();
            newItems.Clear();
            modifiedItems.Clear();
            editLevel = 0;
            track.Clear();
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return items.Count; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return ((ICollection<T>)items).IsReadOnly; }
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index > -1)
                RemoveAt(index);
            return index > -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion

        #region IList Members

        int IList.Add(object value)
        {
            Add((T)value);
            return (this.Count - 1);
        }

        bool IList.Contains(object value)
        {
            if (IsCompatibleObject(value))
                return Contains((T)value);
            return false;
        }

        int IList.IndexOf(object value)
        {
            if (IsCompatibleObject(value))
                return IndexOf((T)value);
            return -1;
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        bool IList.IsFixedSize
        {
            get { return ((IList)items).IsFixedSize; }
        }

        bool IList.IsReadOnly
        {
            get { return ((IList)items).IsReadOnly; }
        }

        void IList.Remove(object value)
        {
            if (IsCompatibleObject(value))
                Remove((T)value);
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = (T)value; }
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            Array.Copy(items.ToArray(), 0, array, index, this.items.Count);
        }

        int ICollection.Count
        {
            get { return Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return ((ICollection)items).IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return ((ICollection)items).SyncRoot; }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return ObjectHelper.Clone(this);
        }

        #endregion

        #region IEditableObject Members

        public void BeginEdit()
        {
            editable = true;
        }

        public void CancelEdit()
        {
            while (CanUndo)
            {
                Undo(true);
            }
            track.Clear();
            OnListChanged(new ListChangedEventArgs<T>(ListChangedType.Reset, null));
            OnListChanged(new ListChangedEventArgs(System.ComponentModel.ListChangedType.Reset, -1));
        }

        public void EndEdit()
        {
            foreach (T item in items)
                item.ResetState();
            modifiedItems.Clear();
            deletedItems.Clear();
            newItems.Clear();
            track.Clear();
            editLevel = 0;
            editable = false;
        }

        #endregion

        public void AddIndex(PropertyDescriptor property)
        {

        }

        public object AddNew()
        {
            T m = new T();
            Add(m);
            return m;
        }

        public bool AllowEdit
        {
            get { return true; }
        }

        public bool AllowNew
        {
            get { return true; }
        }

        public bool AllowRemove
        {
            get { return true; }
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            sortProperty = property;
            sortDirection = direction;
            items.Sort(SortComparer);
        }

        int SortComparer(T a, T b)
        {
            object obj1 = sortProperty.GetValue(a);
            object obj2 = sortProperty.GetValue(b);
            int result = 0;
            if (object.Equals(obj1, null))
            {
                if (object.Equals(obj2, null))
                    result = 0;
                else
                    result = -1;
            }
            else if (obj1 is IComparable)
                result = ((IComparable)obj1).CompareTo(obj2);
            if (sortDirection == ListSortDirection.Descending)
                result = -result;
            return result;
        }

        public int Find(PropertyDescriptor property, object key)
        {
            for (int i = 0; i < Count; i++)
                if (object.Equals(property.GetValue(items[i]), key))
                    return i;
            return -1;
        }

        public bool IsSorted
        {
            get { return sortProperty != null; }
        }

        event ListChangedEventHandler IBindingList.ListChanged
        {
            add { listChangedEventHandler += value; }
            remove { listChangedEventHandler -= value; }
        }

        public void RemoveIndex(PropertyDescriptor property)
        {

        }

        public void RemoveSort()
        {
            sortProperty = null;
            sortDirection = ListSortDirection.Ascending;
        }

        public ListSortDirection SortDirection
        {
            get { return sortDirection; }
        }

        public PropertyDescriptor SortProperty
        {
            get { return sortProperty; }
        }

        public bool SupportsChangeNotification
        {
            get { return true; }
        }

        public bool SupportsSearching
        {
            get { return true; }
        }

        public bool SupportsSorting
        {
            get { return true; }
        }
    }
}
