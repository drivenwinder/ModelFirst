using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EAP
{
    public class StatusBusy : DisposableBase
    {
        private string _oldStatus;
        private Cursor _oldCursor;
        private IStatusable _form;

        public StatusBusy(string statusText, IStatusable form)
        {
            _form = form;
            _oldStatus = form.StatusText;
            _form.StatusText = statusText;
            _oldCursor = form.Cursor;
            _form.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
        }

        protected override void Cleanup()
        {
            _form.StatusText = _oldStatus;
            _form.Cursor = _oldCursor;
        }
    }
}
