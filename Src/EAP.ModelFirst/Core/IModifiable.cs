using System;

namespace EAP.ModelFirst.Core
{
	public interface IModifiable
	{
		event EventHandler Modified;

		bool IsDirty { get; }

		void Clean();
	}
}
