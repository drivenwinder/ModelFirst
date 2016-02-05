using System;
using System.Xml;
using System.ComponentModel;

namespace EAP.ModelFirst.Core.Project
{
	public abstract class Element : IModifiable
	{
		bool isDirty = false;
		bool initializing = false;
		int dontRaiseRequestCount = 0;

        public event EventHandler Modified;

        [System.ComponentModel.Browsable(false)]
		public bool IsDirty
		{
			get { return isDirty; }
		}

		public virtual void Clean()
		{
			isDirty = false;
			//TODO: 清理成员
		}

		protected bool Initializing
		{
			get { return initializing; }
			set { initializing = value; }
		}

		protected bool RaiseChangedEvent
		{
			get
			{
				return (dontRaiseRequestCount == 0);
			}
			set
			{
				if (!value)
					dontRaiseRequestCount++;
				else if (dontRaiseRequestCount > 0)
					dontRaiseRequestCount--;

				if (RaiseChangedEvent && isDirty)
					OnModified(EventArgs.Empty);
			}
		}

		protected virtual void Changed()
		{
			if (!Initializing)
			{
                if (RaiseChangedEvent)
                    OnModified(EventArgs.Empty);
                else
                    isDirty = true;
			}
		}

		private void OnModified(EventArgs e)
		{
			isDirty = true;
			if (Modified != null)
				Modified(this, e);
		}
    }
}