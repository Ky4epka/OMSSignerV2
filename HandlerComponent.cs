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
    public partial class HandlerComponent : UserControl
    {
        public IFileHandler Handler 
        {
            get
            {
                UIBinderAttribute.PushFromUI(iHandler.GetType(), iHandler, tlpRoot);
                return iHandler;
            }
            set
            {
                tlpRoot.RowCount = 0;
                iHandler = value;
                UIBinderAttribute.DropToUI(iHandler.GetType(), iHandler, tlpRoot);
            }
        }

        protected IFileHandler iHandler = null;

        public HandlerComponent()
        {
            InitializeComponent();
        }

        public HandlerComponent(IFileHandler handler)
        {
            InitializeComponent();
            Handler = handler;
        }
    }
}
