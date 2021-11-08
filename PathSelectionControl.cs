using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMSSigner
{
    public partial class PathSelectionControl : UserControl
    {
        public string SelectedPath { get => tbValue.Text; set => tbValue.Text = value; }
        public bool TextFieldReadonly 
        { 
            get => tbValue.ReadOnly;
            set
            {
                tbValue.ReadOnly = value;

                if (value)
                {
                    tbValue.BackColor = Color.Gray;
                }
                else
                {
                    tbValue.BackColor = Color.White;
                }
            }
        }

        public PathSelectionControl()
        {
            InitializeComponent();            
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if (!SelectedPath.Equals(String.Empty))
                fbDialog.SelectedPath = SelectedPath;

            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedPath = fbDialog.SelectedPath;
            }
        }
    }
}
