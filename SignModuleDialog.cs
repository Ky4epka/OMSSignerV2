using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMSSigner
{
    public partial class SignModuleDialog : Form
    {
        protected ISignModule iModule = null;

        public string AAA { get; }

        public SignModuleDialog()
        {
            InitializeComponent();
        }

        public SignModuleDialog(ISignModule module) : base()
        {
            InitializeComponent();
            iModule = module;
            UpdateUI();
        }

        public void UpdateUI()
        {
            UIBinderAttribute.DropToUI(iModule.GetType(), iModule, tlpRoot);
        }

        private void dlgOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void dlgApply_Click(object sender, EventArgs e)
        {
            UIBinderAttribute.PushFromUI(iModule.GetType(), iModule, tlpRoot);
            ProgramConfig.SaveConfigToFile(Globals.CONFIG_FILE, ProgramConfig.Instance);
        }

        private void dlgCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }

}
