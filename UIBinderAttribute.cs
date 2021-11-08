using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using NLog;

namespace OMSSigner
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UIBinderAttribute : Attribute
    {
        public Type UIClass;
        public string bindedPropertyName;
        public Type bindedPropertyType;
        public bool useLabel;
        public string LabelText;
        public int Control_Width;
        public int Control_Height;
        public string Control_Text = String.Empty;
        public bool IgnoreInheritedAttr;
        /// <summary>
        /// Индекс сортировки генерируемого UI-элемента в рамках текущего класса и его предков
        /// </summary>
        public int Control_SortIndex = 999;

        public static string BuildPropertyControlName(Type source, PropertyInfo property)
        {
            return source.Name + "." + property.Name;
        }

        public virtual void FillData(Control control, Label label, Control parent)
        {
            if (label != null)
            {
                label.Location = new Point(0, 0);
                label.AutoSize = true;
                label.Dock = DockStyle.Top;
                label.Parent = parent;
                label.Text = LabelText;
                
                label.Visible = true;
            }

            control.Parent = parent;
            control.Dock = DockStyle.Top;
            control.Visible = true;

            if (!Control_Text.Equals(string.Empty))
                control.Text = Control_Text;

            if (!(new Size(Control_Width, Control_Height)).IsEmpty)
                control.Size = new Size(Control_Width, Control_Height);
        }

        protected class UIProperty
        {
            public PropertyInfo prop;
            public UIBinderAttribute ui_attr;

            public UIProperty(PropertyInfo property, UIBinderAttribute attr)
            {
                prop = property;
                ui_attr = attr;
            }
        }

        public static void DropToUI(Type source, object source_instance, Control parent)
        {
            LinkedListEx<UIProperty> sortedProps = new LinkedListEx<UIProperty>();

            foreach (PropertyInfo prop in source.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                UIBinderAttribute ui_binded_attr = prop.GetCustomAttribute<UIBinderAttribute>(true);

                if (ui_binded_attr != null)
                {
                    if (ui_binded_attr.IgnoreInheritedAttr) continue;

                    if (prop.CanRead)
                    {
                        sortedProps.AddSorted(new UIProperty(prop, ui_binded_attr), (UIProperty cmp1, UIProperty cmp2) => { return cmp1.ui_attr.Control_SortIndex - cmp2.ui_attr.Control_SortIndex; });
                    }
                    else
                        Log._("Не удалось прочитать свойство '" + prop.Name + "': свойство помечено 'только запись'", LogLevel.Error);
                }
            }

            foreach (UIProperty prop_data in sortedProps)
            {
                UIBinderAttribute ui_binded_attr = prop_data.ui_attr;

                if (ui_binded_attr != null)
                {
                    Control ui_elem = Activator.CreateInstance(ui_binded_attr.UIClass) as Control;

                    if (ui_elem == null)
                    {
                        Log._("Не удалось создать образец класса: " + ui_binded_attr.UIClass.Name, LogLevel.Error);
                    }
                    else
                    {
                        ui_elem.Name = BuildPropertyControlName(source, prop_data.prop);
                        ui_binded_attr.FillData(
                            ui_elem,
                            (ui_binded_attr.useLabel) ? new Label() : null,
                            parent);

                        ui_elem.Enabled = prop_data.prop.CanWrite;

                        if (prop_data.prop.CanRead)
                        {
                            PropertyInfo ui_elem_prop = ui_binded_attr.UIClass.GetProperty(ui_binded_attr.bindedPropertyName, BindingFlags.Instance | BindingFlags.Public);

                            if (ui_elem_prop == null)
                            {
                                Log._("Не удалось получить свойство '" + ui_binded_attr.bindedPropertyName + "': свойство не найдено", LogLevel.Error);
                            }
                            else
                            {
                                if (ui_elem_prop.CanWrite)
                                {
                                    ui_elem_prop.SetValue(ui_elem, prop_data.prop.GetValue(source_instance));
                                }
                                else
                                {
                                    Log._("Запись в свойство '" + ui_elem_prop.Name + "' невозможно: свойство только для чтения", LogLevel.Error);
                                }
                            }
                        }
                        else
                            Log._("Не удалось прочитать свойство '" + prop_data.prop.Name + "': свойство помечено 'только запись'", LogLevel.Error);
                    }
                }
            }

            sortedProps.Clear();
        }

        public static void PushFromUI(Type source, object source_instance, Control parent)
        {
            foreach (PropertyInfo prop in source.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                UIBinderAttribute ui_binded_attr = prop.GetCustomAttribute<UIBinderAttribute>(true);

                if (ui_binded_attr != null)
                {
                    if (ui_binded_attr.IgnoreInheritedAttr) continue;

                    string match_name = BuildPropertyControlName(source, prop);
                    Control ui_elem = null;

                    foreach (Control control in parent.Controls)
                    {
                        if (match_name.Equals(control.Name))
                            ui_elem = control;
                    }
                    
                    if (ui_elem != null)
                    {
                        if (prop.CanWrite)
                        {
                            PropertyInfo ui_elem_prop = ui_binded_attr.UIClass.GetProperty(ui_binded_attr.bindedPropertyName, BindingFlags.Instance | BindingFlags.Public);

                            if (ui_elem_prop == null)
                            {
                                Log._("Не удалось получить свойство '" + ui_binded_attr.bindedPropertyName + "': свойство не найдено", LogLevel.Error);
                            }
                            else
                            {
                                if (ui_elem_prop.CanRead)
                                {
                                    prop.SetValue(source_instance, ui_elem_prop.GetValue(ui_elem));
                                }
                                else
                                {
                                    Log._("Не удалось прочитать свойство '" + ui_elem_prop.Name + "': свойство помечено 'только запись'", LogLevel.Error);
                                }
                            }
                        }
                        //else
                            //Log._("Запись в свойство '" + prop.Name + "' невозможно: свойство только для чтения", LogLevel.Error);
                    }
                }
            }
        }
    }

}
