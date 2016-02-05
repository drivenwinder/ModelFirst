using System;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor
{
	public class EventArgs<T> : EventArgs
	{
		T value;

		public T Value
		{
			get { return this.value; }
			set { this.value = value; }
		}

		public EventArgs(T value)
		{
			this.value = value;
		}
	}
}
