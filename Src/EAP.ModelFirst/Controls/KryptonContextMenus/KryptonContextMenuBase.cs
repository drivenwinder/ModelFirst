using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Controls.Editors.DiagramEditor;

namespace EAP.ModelFirst.Controls.KryptonContextMenus
{
    public abstract class KryptonContextMenuBase
    {
        internal static readonly KryptonContextMenu MenuStrip = new KryptonContextMenu();

        KryptonContextMenuItems menuItems = new KryptonContextMenuItems();

        public KryptonContextMenuItems GetMenuItems(IDiagram document)
        {
            ValidateMenuItems(document);
            return menuItems;
        }

        protected abstract IDiagram Document
        {
            get;
        }

        internal KryptonContextMenuItemBase[] MenuItems
        {
            get { return menuItems.Items.ToArray(); }
        }

        protected KryptonContextMenuItemCollection MenuList
        {
            get { return menuItems.Items;}
        }

        public abstract void ValidateMenuItems(IDiagram document);
    }
}
