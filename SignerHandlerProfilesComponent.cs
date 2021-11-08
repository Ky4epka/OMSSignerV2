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
    public partial class SignerHandlerProfilesComponent : UserControl
    {

        public IARMProfile[] Profiles
        {
            get
            {
                IARMProfile[] result = new IARMProfile[iProfiles.Count];

                for (int i = 0; i < iProfiles.Count; i++)
                {
                    result[i] = iProfiles[i].Profile;
                }

                return result;
            }
            set
            {
                tlpRoot.RowCount = 0;

                foreach (SignerHandlerProfileComponent profile in iProfiles)
                {
                    profile.Dispose();
                }

                iSelectedProfile = null;
                iProfiles.Clear();

                foreach (IARMProfile profile in value)
                {
                    AddProfile(profile);
                }
            }
        }

        protected SignerHandlerProfileComponent iSelectedProfile = null;
        protected List<SignerHandlerProfileComponent> iProfiles = new List<SignerHandlerProfileComponent>();

        public SignerHandlerProfilesComponent()
        {
            InitializeComponent();
        }

        public void AddProfile(IARMProfile profile)
        {
            if (profile == null)
                profile = new ARMProfile();

            SignerHandlerProfileComponent comp = new SignerHandlerProfileComponent(profile);
            comp.Parent = tlpRoot;
            comp.Dock = DockStyle.Top;
            comp.OnSelected += Event_OnProfileSelected;
            iProfiles.Add(comp);
            UpdateUI();
        }

        public void ApplyChangesToSelectedProfile()
        {
            if (iSelectedProfile == null) return;

            iSelectedProfile.FromUIToData();
            UpdateUI();
        }

        public void DeleteSelectedProfile()
        {
            if (iSelectedProfile == null) return;

            iSelectedProfile.Dispose();
            iProfiles.Remove(iSelectedProfile);
            iSelectedProfile = null;
            UpdateUI();
        }

        public void SelectProfile(SignerHandlerProfileComponent profile)
        {
            iSelectedProfile = profile;
            UpdateUI();
        }

        public void Event_OnProfileSelected(SignerHandlerProfileComponent sender, bool selected)
        {
            if (!iProfiles.Contains(sender)) return;

            if (selected)
            {
                if ((iSelectedProfile != null) && (iSelectedProfile != sender))
                    iSelectedProfile.Selected = false;

                SelectProfile(sender);
            }
            else if (iSelectedProfile.Equals(sender))
            {
                SelectProfile(null);
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            AddProfile(null);
        }

        private void btChange_Click(object sender, EventArgs e)
        {
            ApplyChangesToSelectedProfile();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedProfile();
        }

        protected void UpdateUI()
        {
            bool has_profiles = Profiles.Length > 0;
            bool has_selected = iSelectedProfile != null;
            btDelete.Enabled = has_profiles && has_selected;
            btChange.Enabled = has_profiles && has_selected;
        }
    }
}
