﻿using System.Collections.Generic;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram
{
	public class ElementList<T> : OrderedList<T> where T : DiagramElement
	{
		public IEnumerable<T> GetSelectedElements()
		{
			foreach (T element in GetModifiableList())
			{
				if (element.IsSelected)
					yield return element;
			}
		}

		public IEnumerable<T> GetUnselectedElements()
		{
			foreach (T element in GetModifiableList())
			{
				if (!element.IsSelected)
					yield return element;
			}
		}

		public IEnumerable<T> GetSelectedElementsReversed()
		{
			foreach (T element in GetReversedList())
			{
				if (element.IsSelected)
					yield return element;
			}
		}

		public IEnumerable<T> GetUnselectedElementsReversed()
		{
			foreach (T element in GetReversedList())
			{
				if (!element.IsSelected)
					yield return element;
			}
		}
	}
}
