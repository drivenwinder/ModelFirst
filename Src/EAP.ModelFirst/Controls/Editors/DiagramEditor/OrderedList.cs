using System;
using System.Collections.Generic;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor
{
	public class OrderedList<T> : LinkedList<T>
	{
		public T FirstValue
		{
			get
			{
				if (First == null)
					return default(T);
				else
					return First.Value;
			}
		}

		public T SecondValue
		{
			get
			{
				if (First == null || First.Next == null)
					return default(T);
				else
					return First.Next.Value;
			}
		}

		public T SecondLastValue
		{
			get
			{
				if (Last == null || Last.Previous == null)
					return default(T);
				else
					return Last.Previous.Value;
			}
		}

		public T LastValue
		{
			get
			{
				if (Last == null)
					return default(T);
				else
					return Last.Value;
			}
		}

		public void Add(T value)
		{
			this.AddLast(value);
		}

		public IEnumerable<T> GetModifiableList()
		{
			LinkedListNode<T> current = this.First;
			while (current != null)
			{
				LinkedListNode<T> next = current.Next;
				yield return current.Value;
				current = next;
			}
		}

		public IEnumerable<T> GetReversedList()
		{
			LinkedListNode<T> current = this.Last;
			while (current != null)
			{
				yield return current.Value;
				current = current.Previous;
			}
		}

		public void ShiftToFirstPlace(T value)
		{
			this.Remove(value);
			AddFirst(value);
		}

		public bool RemoveOn(Predicate<T> match)
		{
			LinkedListNode<T> current = First;
			while (current != null)
			{
				if (match(current.Value))
				{
					this.Remove(current);
					return true;
				}
				current = current.Next;
			}
			return false;
		}

		public void Reverse()
		{
			LinkedListNode<T> node = First;
			while (Last != node)
			{
				LinkedListNode<T> last = Last;
				Remove(last);
				AddBefore(node, last);
			}
		}
	}
}