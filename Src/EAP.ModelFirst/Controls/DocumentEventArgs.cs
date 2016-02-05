using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.ModelFirst.Controls
{
    public delegate void DocumentEventHandler(object sender, DocumentEventArgs e);

    public class DocumentEventArgs
    {
        IDocument document;

        public DocumentEventArgs(IDocument document)
        {
            this.document = document;
        }

        public IDocument Document
        {
            get { return document; }
        }
    }
}
