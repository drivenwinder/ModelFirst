using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EAP.ModelFirst.Core;
using EAP.Collections;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Controls.Explorers
{
    public partial class PropertyWindow : ExplorerBase
    {
        bool locked;
        static PropertyWindow instance;

        static PropertyWindow Instance
        {
            get
            {
                if (instance == null)
                    instance = new PropertyWindow();
                return instance;
            }
        }

        object propertyObject;

        public static void Show(IDockForm dockForm)
        {
            Instance.DockForm = dockForm;
            var cfg = dockForm.DockPanel.ActiveContent as IPropertyConfigurable;
            if (cfg != null)
                SetPropertyObject(cfg.PropertyObject);
            Instance.Show(dockForm.DockPanel);
        }

        protected PropertyWindow()
        {
            InitializeComponent();
            this.grid.BackColor = BackColor;
            this.grid.HelpBackColor = BackColor;
            this.grid.LineColor = BackColor;
            UpdateTexts();
        }
        public override void UpdateTexts()
        {
            Text = Strings.PropertyExplorer;
            TabText = Strings.PropertyExplorer;
            base.UpdateTexts();
        }

        public static void SetPropertyObject(object obj)
        {
            if (instance == null) return;
            instance.SetObject(obj);
        }

        void SetObject(object obj)
        {
            if(propertyObject is IModifiable)
                ((IModifiable)propertyObject).Modified -= new EventHandler(PropertyWindow_Modified);
            if (propertyObject is IPropertyGroup)
                ((IPropertyGroup)propertyObject).SelectionChanged -= new EventHandler(Properties_SelectionChanged);

            propertyObject = obj;

            propertyObjectBindingSource.Clear();
            if (obj is IPropertyGroup)
            {
                var group = (IPropertyGroup)obj;
                group.SelectionChanged += new EventHandler(Properties_SelectionChanged);
               propertyObjectBindingSource.DataSource = group.ObjectList.Cast<object>()
                   .Select(p => new PropertyObject(p)).OrderBy(p => p.DisplayName);
                SetSelectedObjects(group.SelectedObjects);
            }
            else
            {
                grid.SelectedObject = obj;
                propertyObjectBindingSource.Add(new PropertyObject(obj));
                cbxObjectList.SelectedIndex = 0;
            }
            if (propertyObject is IModifiable)
                ((IModifiable)propertyObject).Modified += new EventHandler(PropertyWindow_Modified);
        }

        void PropertyWindow_Modified(object sender, EventArgs e)
        {
            grid.Refresh();
        }

        void Properties_SelectionChanged(object sender, EventArgs e)
        {
            if (locked) return;
            if (propertyObject is IPropertyGroup)
            {
                var group = (IPropertyGroup)propertyObject;
                locked = true;
                SetSelectedObjects(group.SelectedObjects);
                locked = false;
            }
        }

        void SetSelectedObjects(object[] objs)
        {
            grid.SelectedObjects = objs;
            if (objs.Length == 1)
                cbxObjectList.SelectedItem = cbxObjectList.Items.OfType<PropertyObject>().FirstOrDefault(p => p.Object == objs[0]);
            else
            {
                cbxObjectList.SelectedItem = null;
            }
        }

        private void grid_SelectedObjectsChanged(object sender, EventArgs e)
        {

        }

        private void cbxObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked) return;
            if (cbxObjectList.SelectedItem != null && propertyObject is IPropertyGroup)
            {
                locked = true;
                var obj = (cbxObjectList.SelectedItem as PropertyObject).Object;
                ((IPropertyGroup)propertyObject).Select(obj);
                grid.SelectedObject = obj;
                locked = false;
            }
        }
    }

    public class PropertyObject : INotifyPropertyChanged
    {
        object obj;

        public PropertyObject(object o)
        {
            obj = o;
            if (obj is INamedObject)
                ((INamedObject)obj).Renamed += new EventHandler(PropertyObject_Renamed);
        }

        void PropertyObject_Renamed(object sender, EventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("DisplayName"));
        }

        ~PropertyObject()
        {
            if (obj is INamedObject)
                ((INamedObject)obj).Renamed -= new EventHandler(PropertyObject_Renamed);
        }

        public object Object
        {
            get { return obj; }
        }

        public string DisplayName
        {
            get
            {
                if (obj is INamedObject)
                    return ((INamedObject)obj).Name;
                else if (obj is IEntity)
                    return ((IEntity)obj).Name;
                else
                    return obj.ToSafeString();
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
