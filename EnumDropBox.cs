using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace OMSSigner
{
    public partial class EnumDropBox : UserControl
    {
        public Type EnumCollection 
        { 
            get => iEnumCollection; 
            set
            {
                iEnumCollection = value;
                ApplyCollection(value);
            }
        }

        public Enum Selected 
        { 
            get => iSelected;
            set
            {
                iSelected = value;
                SelectEnum(value);
            }
        }

        protected Type iEnumCollection = null;
        protected Enum iSelected = default;


        public EnumDropBox()
        {
            InitializeComponent();
        }

        protected void ApplyCollection(Type collection)
        {
            cbValue.Items.Clear();
            cbValue.Items.AddRange(Enum.GetNames(collection));
            SelectEnum(Selected);
        }

        protected void SelectEnum(Enum value)
        {
            if (value == default) return;

          //  cbValue.SelectedIndex = cbValue.Items.IndexOf(value.ToString());
        }
    }

    public class UIEnumDropBoxAttribute: UIBinderAttribute
    {
        public Type EnumCollection;

        public override void FillData(Control control, Label label, Control parent)
        {
            base.FillData(control, label, parent);

            Log._("EnumCollectionType: " + EnumCollection.FullName, LogLevel.Info);
            (control as EnumDropBox).EnumCollection = EnumCollection;
        }
    }
}
