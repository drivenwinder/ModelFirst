using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.ModelFirst.Core
{
    public interface ITextFormatable
    {
        void Indent();

        void Outdent();

        bool WordWrap { get; set; }
    }
}
