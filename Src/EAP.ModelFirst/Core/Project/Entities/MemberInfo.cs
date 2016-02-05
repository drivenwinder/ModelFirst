using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.ModelFirst.Core.Project.Entities
{
    public class MemberInfo
    {
        public TypeBase Type { get; protected set; }

        public MemberInfo(TypeBase type)
        {
            Type = type;
        }

        public override string ToString()
        {
            if(Type is CompositeType)
                return ((CompositeType)Type).MemberCount + " Members";
            else if(Type is EnumType)
                return ((EnumType)Type).ValueCount + " Values";
            else if (Type is DelegateType)
                return ((DelegateType)Type).ArgumentCount + " Arguments";
            return "";
        }
    }
}
