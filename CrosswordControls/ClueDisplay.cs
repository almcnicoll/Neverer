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
        private String __clueText = "";
        private Object __statistic = null;
        public Object Statistic
        {
            get
            {
                return __statistic;
            }
            set
            {
                __statistic = value;
                if (__statistic == null)
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action<Boolean, String>(lblStat_Set), false, "");

                    }
                    else
                    {
                        lblStat.Visible = false;
                        lblStat.Text = "";
                    }
                }
                else
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action<Boolean, String>(lblStat_Set), true, __statistic.ToString());

                    }
                    else
                    {
                        lblStat.Text = __statistic.ToString();
                        lblStat.Visible = true;
                    }
                }
            }
        }

        public void lblClueNumber_SetText(String value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<String>(lblClueNumber_SetText), value);
                return;
            }
            lblClueNumber.Text = value;
        }
        public void lblClueText_SetText(String value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<String>(lblClueText_SetText), value);
                return;
            }
            lblClueText.Text = value;
        }
        public void lblClueNumber_SetFont(Font value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Font>(lblClueNumber_SetFont), value);
                return;
            }
            lblClueNumber.Font = value;
        }
        public void lblClueText_SetFont(Font value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Font>(lblClueText_SetFont), value);
                return;
            }
            lblClueText.Font = value;
        }
        public void lblClueNumber_SetColors(Color value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Color>(lblClueNumber_SetColors), value);
                return;
            }
            tlpMaster.BackColor = value;
            lblClueNumber.BackColor = value;
            lblClueText.BackColor = value;
            this.BackColor = value;
        }
        public void chkRowSelect_SetChecked(Boolean value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Boolean>(chkRowSelect_SetChecked), value);
                return;
            }
            chkRowSelect.Checked = value;
        }

        public void lblStat_Set(Boolean isVisible, String value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Boolean, String>(lblStat_Set), isVisible, value);
                return;
            }
            lblStat.Text = value;
            lblStat.Visible = isVisible;
        }
        public Color StatusColor
        {
            get
            {
                return __StatusColor;
            }
            set
            {
                __StatusColor = value;
                lblClueNumber_SetColors(value);
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
                //lblClueNumber.Text = value; // TODO - cross-thread error happening here
                lblClueNumber_SetText(value);
            }
        }

        public String ClueText
        {
            get
            {
                return __clueText;
            }
            set
            {
                __clueText = value;
                lblClueText_SetText(value);
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
                __Clue.ClueSpecificationChanged += __Clue_ClueDefinitionChanged;
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
                chkRowSelect_SetChecked(__Selected);
                if (__Selected)
                {
                    lblClueNumber_SetFont(new Font(lblClueNumber.Font, FontStyle.Bold));
                    lblClueText_SetFont(new Font(lblClueText.Font, FontStyle.Bold));
                }
                else
                {
                    lblClueNumber_SetFont(new Font(lblClueNumber.Font, FontStyle.Regular));
                    lblClueText_SetFont(new Font(lblClueText.Font, FontStyle.Regular));
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
