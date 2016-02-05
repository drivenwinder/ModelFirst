using System;

namespace EAP.ModelFirst.Core
{
	public interface IEditable
	{
        bool IsEditing { get; }

        void BeginEdit();

        void EndEdit();

        void CancelEdit();

		bool IsEmpty { get; }

        bool CanUndo { get; }

        bool CanRedo { get; }

        bool CanDelete { get; }

		bool CanCutToClipboard { get; }

		bool CanCopyToClipboard { get; }

		bool CanPasteFromClipboard { get; }
		
		event EventHandler EditStateChanged;

        void Undo();

        void Redo();
		
		void Cut();

		void Copy();

		void Paste();

		void SelectAll();

        void Delete();
	}
}
