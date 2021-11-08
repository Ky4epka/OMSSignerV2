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
    public partial class SignerHandlerProfileComponent : UserControl
    {
        public IARMProfile Profile
        {
            get
            {
                UIBinderAttribute.PushFromUI(iProfile.GetType(), iProfile, tlpRoot);
                return iProfile;
            }
            set
            {
                tlpRoot.RowCount = 0;
                iProfile = value;
                UIBinderAttribute.DropToUI(iProfile.GetType(), iProfile, tlpRoot);
            }
        }

        public event Action<SignerHandlerProfileComponent, bool> OnSelected;

        protected IARMProfile iProfile = null;
        protected bool iSelected = false;

        public void FromUIToData()
        {
            UIBinderAttribute.PushFromUI(iProfile.GetType(), iProfile, tlpRoot);
        }

        public bool Selected
        {
            get => iSelected;
            set
            {
                iSelected = value;

                if (iSelected)
                {
                    BackColor = Color.Gray;
                }
                else
                {
                    BackColor = Form.DefaultBackColor;
                }

                if (OnSelected != null)
                {
                    OnSelected.Invoke(this, iSelected);
                }
            }
        }

        public SignerHandlerProfileComponent()
        {
            InitializeComponent();
        }

        public SignerHandlerProfileComponent(IARMProfile profile)
        {
            InitializeComponent();
            Profile = profile;
        }

        private void SignerHandlerProfileComponent_Load(object sender, EventArgs e)
        {

        }

        private void tlpRoot_Enter(object sender, EventArgs e)
        {
            Selected = true;
        }

        private void tlpRoot_Leave(object sender, EventArgs e)
        {
        }
    }
}
