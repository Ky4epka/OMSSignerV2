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
    public partial class HandlersComponent : UserControl
    {
        public IFileHandler[] Handlers 
        {
            get 
            {
                IFileHandler[] result = new IFileHandler[iHandlersComponents.Count];

                for (int i = 0; i<iHandlersComponents.Count; i++)
                {
                    result[i] = iHandlersComponents[i].Handler;
                }

                return result;
            }
            set
            {
                tlpRoot.RowCount = 0;

                foreach (HandlerComponent handler_comp in iHandlersComponents)
                {
                    handler_comp.Dispose();
                }

                iHandlersComponents.Clear();

                foreach (IFileHandler handler in value)
                {
                    HandlerComponent comp = new HandlerComponent(handler);
                    comp.Parent = tlpRoot;
                    comp.Dock = DockStyle.Top;

                    iHandlersComponents.Add(comp);
                }

                if (iHandlersComponents.Count == 0)
                {
                    tlpRoot.Hide();
                    lbEmpty.Show();
                }
                else
                {
                    lbEmpty.Hide();
                    tlpRoot.Show();
                }
            }
        }

        protected List<HandlerComponent> iHandlersComponents = new List<HandlerComponent>();

        public HandlersComponent()
        {
            InitializeComponent();
        }
    }
}
