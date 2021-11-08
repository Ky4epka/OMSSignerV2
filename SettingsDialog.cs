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
    public partial class SettingsDialog : Form
    {
        protected ProgramConfig iConfig = new ProgramConfig();

        public SettingsDialog()
        {
            InitializeComponent();
            iConfig = ProgramConfig.Instance;
        }


        public void ApplyConfig(ProgramConfig source)
        {
            //iConfig.Assign(source);
            UI_Update();
        }

        public void DropConfig(ProgramConfig dest)
        {
            UI_DropToConfig();
            //dest.Assign(iConfig);
        }

        public void UI_Update()
        {
            //valInPath.Text = iConfig.InPath;
            //valOutPath.Text = iConfig.OutPath;
           // valHandledPacketsRepository.Text = iConfig.HandledRepositoryPath;
            UI_HandlerTypeListUpdate();
            valHandlerType.SelectedIndex = -1;
            UI_HandlerTypeUpdate();
        }

        public void UI_DropToConfig()
        {
            //iConfig.InPath = Environment.ExpandEnvironmentVariables(valInPath.Text);
           // iConfig.OutPath = Environment.ExpandEnvironmentVariables(valOutPath.Text);
            //iConfig.HandledRepositoryPath = Environment.ExpandEnvironmentVariables(valHandledPacketsRepository.Text);
            UI_HandlerTypeDropToConfig();
        }

        public void UI_HandlerTypeListUpdate()
        {
            valHandlerType.Items.Clear();

        }

        public void UI_HandlerTypeUpdate()
        {
           /* ProgramConfig.HandlerType handler = iConfig.GetHandlerType(valHandlerType.Text);
            
            if (handler == null)
            {
                UI_HandlerTypeClear();
             //   gbHandlerCont.Enabled = false;
                return;
            }

          //  gbHandlerCont.Enabled = true;
            valHandlerName.Text = handler.Id;
            valSignaturesProfiles.Items.Clear();
            valSignaturesProfiles.Items.AddRange(handler.ArmProfiles);
            valPacketFilesToSignExtensions.Items.Clear();
            valPacketFilesToSignExtensions.Items.AddRange(handler.FilesToSignExtensions);
            valDeleteFileAfterSign.Checked = handler.DeleteFileAfterSign;
            valJoinSignatures.Checked = handler.JoinProfiles;
           */
        }

        public void UI_HandlerTypeDropToConfig()
        {
            /*
            ProgramConfig.HandlerType handler = iConfig.GetHandlerType(valHandlerType.Text);

            if (handler != null)
            {
                handler.Id = valHandlerName.Text;
                string[] buffer = new string[valSignaturesProfiles.Items.Count];
                valSignaturesProfiles.Items.CopyTo(buffer, 0);
                handler.ArmProfiles = buffer;
                buffer = new string[valPacketFilesToSignExtensions.Items.Count];
                valPacketFilesToSignExtensions.Items.CopyTo(buffer, 0);
                handler.FilesToSignExtensions = buffer;
                handler.DeleteFileAfterSign = valDeleteFileAfterSign.Checked;
                handler.JoinProfiles = valJoinSignatures.Checked;
            }
            */
        }

        public void UI_HandlerTypeClear()
        {
            valHandlerName.Text = "";
            tbSignaturesProfilesEdit.Text = "";
            valSignaturesProfiles.Items.Clear();
            tbPacketFilesToSignExtensionsEdit.Text = "";
            valPacketFilesToSignExtensions.Items.Clear();
        }

        private void btInPathSelector_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                valInPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btOutPathSelector_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                valOutPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void valSignaturesProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = valSignaturesProfiles.SelectedIndex;

            if ((index >= 0) && (index < valSignaturesProfiles.Items.Count))
            {
                tbSignaturesProfilesEdit.Text = valSignaturesProfiles.Items[index].ToString();
            }
        }

        private void btSignaturesProfilesAdd_Click(object sender, EventArgs e)
        {
            valSignaturesProfiles.Items.Add(tbSignaturesProfilesEdit.Text);
            tbSignaturesProfilesEdit.Text = "";
            UI_HandlerTypeDropToConfig();
        }

        private void btSignaturesProfilesChange_Click(object sender, EventArgs e)
        {
            int index = valSignaturesProfiles.SelectedIndex;

            if ((index >= 0) && (index < valSignaturesProfiles.Items.Count))
            {
                valSignaturesProfiles.Items[index] = tbSignaturesProfilesEdit.Text;
            }
            UI_HandlerTypeDropToConfig();
        }

        private void btSignaturesProfilesDelete_Click(object sender, EventArgs e)
        {
            int index = valSignaturesProfiles.SelectedIndex;

            if ((index >= 0) && (index < valSignaturesProfiles.Items.Count))
            {
                valSignaturesProfiles.Items.RemoveAt(index);
                tbSignaturesProfilesEdit.Text = "";
            }
            UI_HandlerTypeDropToConfig();
        }

        private void valPacketFilesToSignExtensions_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = valPacketFilesToSignExtensions.SelectedIndex;

            if ((index >= 0) && (index < valPacketFilesToSignExtensions.Items.Count))
            {
                tbPacketFilesToSignExtensionsEdit.Text = valPacketFilesToSignExtensions.Items[index].ToString();
            }
            UI_HandlerTypeDropToConfig();
        }

        private void btPFTSEAdd_Click(object sender, EventArgs e)
        {
            valPacketFilesToSignExtensions.Items.Add(tbPacketFilesToSignExtensionsEdit.Text);
            tbPacketFilesToSignExtensionsEdit.Text = "";
            UI_HandlerTypeDropToConfig();
        }

        private void btPFTSEChange_Click(object sender, EventArgs e)
        {
            int index = valPacketFilesToSignExtensions.SelectedIndex;

            if ((index >= 0) && (index < valPacketFilesToSignExtensions.Items.Count))
            {
                valPacketFilesToSignExtensions.Items[index] = tbPacketFilesToSignExtensionsEdit.Text;
            }
            UI_HandlerTypeDropToConfig();
        }

        private void btPFTSEDelete_Click(object sender, EventArgs e)
        {
            int index = valPacketFilesToSignExtensions.SelectedIndex;

            if ((index >= 0) && (index < valPacketFilesToSignExtensions.Items.Count))
            {
                valPacketFilesToSignExtensions.Items.RemoveAt(index);
                tbPacketFilesToSignExtensionsEdit.Text = "";
            }
            UI_HandlerTypeDropToConfig();
        }

        private void dlgCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void dlgApply_Click(object sender, EventArgs e)
        {
            DropConfig(ProgramConfig.Instance);
            ProgramConfig.SaveConfigToFile(Globals.CONFIG_FILE, ProgramConfig.Instance);
        }

        private void dlgOK_Click(object sender, EventArgs e)
        {
            dlgApply_Click(sender, e);
            DialogResult = DialogResult.OK;
        }

        private void SettingsDialog_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void SettingsDialog_Shown(object sender, EventArgs e)
        {
            if (Visible) ApplyConfig(ProgramConfig.Instance);
        }

        private void btvalHandledPacketsRepositorySelector_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                valHandledPacketsRepository.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void valHandlerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_HandlerTypeUpdate();
        }

        private void btHandlerAdd_Click(object sender, EventArgs e)
        {
            /*ProgramConfig.HandlerType handler = iConfig.AddNewHandler();

            if (handler != null)
            {
                UI_HandlerTypeListUpdate();
                valHandlerType.Text = handler.Id;
                UI_HandlerTypeUpdate();
            }
            */
        }

        private void btHandlerRemove_Click(object sender, EventArgs e)
        {
            //iConfig.DeleteHandler(valHandlerType.Text);
            UI_HandlerTypeListUpdate();
            valHandlerType.SelectedItem = -1;
            UI_HandlerTypeUpdate();
        }
    }
}
