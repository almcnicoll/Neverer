using Neverer.UtilityClass;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CrosswordControls
{
    public partial class ClueDisplay : UserControl
    {
        private Color __StatusColor = Color.Transparent;
        private PlacedClue __Clue = null;
        private bool __Selected = false;

        public Color StatusColor
        {
            get
            {
                return __StatusColor;
            }
            set
            {
                __StatusColor = value;
                tlpMaster.BackColor = __StatusColor;
                lblClueNumber.BackColor = __StatusColor;
                lblClueText.BackColor = __StatusColor;
                this.BackColor = __StatusColor;
            }
        }

        public String ClueNumber
        {
            get
            {
                return lblClueNumber.Text;
            }
            set
            {
                lblClueNumber.Text = value;
            }
        }

        public String ClueText
        {
            get
            {
                return lblClueText.Text;
            }
            set
            {
                lblClueText.Text = value;
            }
        }

        public PlacedClue Clue
        {
            get
            {
                return __Clue;
            }
            set
            {
                __Clue = value;
                this.ClueNumber = __Clue.placeDescriptor;
                this.ClueText = __Clue.clueText;
                this.BackColor = __Clue.statusColor;
                __Clue.ClueStatusChanged += __Clue_ClueStatusChanged;
                __Clue.ClueDefinitionChanged += __Clue_ClueDefinitionChanged;
            }
        }

        public bool Selected
        {
            get
            {
                return __Selected;
            }
            set
            {
                __Selected = value;
                chkRowSelect.Checked = __Selected;
                if (__Selected)
                {
                    lblClueNumber.Font = new Font(lblClueNumber.Font, FontStyle.Bold);
                    lblClueText.Font = new Font(lblClueText.Font, FontStyle.Bold);
                } else
                {
                    lblClueNumber.Font = new Font(lblClueNumber.Font, FontStyle.Regular);
                    lblClueText.Font = new Font(lblClueText.Font, FontStyle.Regular);
                }
            }
        }

        private void __Clue_ClueDefinitionChanged(object sender, EventArgs e)
        {
            this.ClueNumber = __Clue.placeDescriptor;
            this.ClueText = __Clue.clueText;
            this.BackColor = __Clue.statusColor;
        }

        private void __Clue_ClueStatusChanged(object sender, ClueStatusChangedEventArgs e)
        {
            this.StatusColor = PlacedClue.StatusColor(e.NewStatus);
        }

        public ClueDisplay()
        {
            InitializeComponent();
        }


        /*private void lblClueNumber_Click(object sender, EventArgs e)
        {
            InvokeOnClick(this, e);
        }

        private void lblClueText_Click(object sender, EventArgs e)
        {
            InvokeOnClick(this, e);
        }

        private void chkRowSelect_Click(object sender, EventArgs e)
        {
            InvokeOnClick(this, e);
        }*/

        private void lblClueText_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
        }

        private void lblClueNumber_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
        }

        private void lblClueNumber_MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClick(e);
        }

        private void lblClueText_MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClick(e);
        }

        private void chkRowSelect_MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClick(e);
        }

        // Prevent using Click - we want them to use MouseClick
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private new EventHandler Click(object sender, EventArgs e) { return null; }
    }
}
