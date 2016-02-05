using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.Win.UI.Design
{
    public class FilterColumnDesignTimeVisibleAttribute: Attribute
    {
        public static readonly FilterColumnDesignTimeVisibleAttribute Default = Yes;
        public static readonly FilterColumnDesignTimeVisibleAttribute No = new FilterColumnDesignTimeVisibleAttribute(false);
        private bool visible;
        public static readonly FilterColumnDesignTimeVisibleAttribute Yes = new FilterColumnDesignTimeVisibleAttribute(true);

        public FilterColumnDesignTimeVisibleAttribute()
        {
        }

        public FilterColumnDesignTimeVisibleAttribute(bool visible)
        {
            this.visible = visible;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            FilterColumnDesignTimeVisibleAttribute attribute = obj as FilterColumnDesignTimeVisibleAttribute;
            return ((attribute != null) && (attribute.Visible == this.visible));
        }

        public override int GetHashCode()
        {
            return (typeof(FilterColumnDesignTimeVisibleAttribute).GetHashCode() ^ (this.visible ? -1 : 0));
        }

        public override bool IsDefaultAttribute()
        {
            return (this.Visible == Default.Visible);
        }

        public bool Visible
        {
            get
            {
                return this.visible;
            }
        }
    }
}
