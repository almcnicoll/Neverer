using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neverer
{
    public static class ExtensionMethods
    {
        public static void ClearAndDispose(this FlowLayoutPanel flp)
        {
            foreach(Control ctrl in flp.Controls)
            {
                if (ctrl is IDisposable)
                {
                    ctrl.Dispose();
                }
            }
            flp.Controls.Clear();
        }
    }
}
