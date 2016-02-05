using System;

namespace EAP
{
    public class PropertyInfoAttribute : Attribute
    {
        bool _visible = true;

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public string DbColumn { get; set; }

        public bool Queryable { get; set; }

        public bool Sortable { get; set; }
    }
}
