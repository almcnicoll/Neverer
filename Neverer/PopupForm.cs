using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Neverer
{
    public class PopupForm : Form
    {
        private Creator __callingInstance = null;

        public PopupForm(Creator callingInstance)
        {
            //this.OnFormClosing += PopupForm_FormClosing;
            __callingInstance = callingInstance;
        }

        protected override void OnShown(EventArgs e)
        {
            if (__callingInstance != null)
            {
                Point? location = __callingInstance.GetLastPosition(this.GetType().Name);
                if (location.HasValue)
                {
                    this.Location = location.Value;
                }
            }
            base.OnShown(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (__callingInstance != null)
            {
                __callingInstance.SetLastPosition(this.GetType().Name, this.Location);
            }
            base.OnFormClosing(e);
        }
    }
}
