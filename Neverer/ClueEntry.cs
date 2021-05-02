using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neverer.UtilityClass;

namespace Neverer
{
    // FIXED METHINKS - when loading solutions from dictionaries, spaces are counted as a letter, resulting in wrong letter count
    public partial class ClueEntry : PopupForm
    {
        Creator __callingInstance;
        PlacedClue pc = new PlacedClue();
        DialogResult result = DialogResult.Retry;
        private bool __loaded = false;
        private List<KeyValuePair<String, String>> possibilities = new List<KeyValuePair<String, String>>();

        public ClueEntry(Creator callingInstance) : base(callingInstance)
        {
            InitializeComponent();
            __callingInstance = callingInstance;
            comboOrientation.Text = "Across";
            setMinMax();
            __loaded = true;
        }
        public ClueEntry(Creator callingInstance, PlacedClue template) : base(callingInstance)
        {
            InitializeComponent();
            __callingInstance = callingInstance;
            comboOrientation.Text = "Across";

            if (template != null)
            {
                txtAnswer.Text = template.clue.answer;
                txtQuestion.Text = template.clue.question;
                nudCol.Maximum = __callingInstance.crossword.cols;
                nudRow.Maximum = __callingInstance.crossword.rows;
                nudCol.Value = template.x + 1;
                nudRow.Value = template.y + 1;
                if (template.orientation == AD.Down)
                {
                    comboOrientation.Text = "Down";
                }
                getIntersectionPattern();
                possibilities.Clear();
                foreach (KeyValuePair<String, List<String>> kvp in template.Matches)
                {
                    foreach (String s in kvp.Value)
                    {
                        possibilities.Add(new KeyValuePair<String, String>(kvp.Key, s));
                    }
                }
                DataGridViewColumn dgvcClueSolution = new DataGridViewTextBoxColumn();
                dgvcClueSolution.DataPropertyName = "key";
                dgvcClueSolution.HeaderText = "Solution";
                dgvcClueSolution.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvPossibleClues.Columns.Add(dgvcClueSolution);
                DataGridViewColumn dgvcClueText = new DataGridViewTextBoxColumn();
                dgvcClueText.DataPropertyName = "value";
                dgvcClueText.HeaderText = "Clue";
                dgvcClueText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvPossibleClues.Columns.Add(dgvcClueText);
                dgvPossibleClues.DataSource = possibilities;
                UpdatePreview();
            }
            __loaded = true;
        }

        private void setMinMax()
        {
            int currentLength = txtAnswer.Text.Replace(Clue.NonCountingChars, "").Length;
            int maxX; int maxY;
            switch (comboOrientation.Text)
            {
                case "Down":
                    maxX = __callingInstance.crossword.cols;
                    maxY = __callingInstance.crossword.rows - currentLength + 1;
                    break;
                default:
                    maxX = __callingInstance.crossword.cols - currentLength + 1;
                    maxY = __callingInstance.crossword.rows;
                    break;
            }
            nudCol.Minimum = 1;
            nudRow.Minimum = 1;
            if (maxX < nudCol.Value)
            {
                MessageBox.Show("The x value was too large for the clue to fit on the crossword. Please check the clue length and orientation.", "X position corrected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (maxY < nudRow.Value)
            {
                MessageBox.Show("The y value was too large for the clue to fit on the crossword. Please check the clue length and orientation.", "Y position corrected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if ((maxX >= 1) && (maxY >= 1))
            {
                nudCol.Maximum = maxX;
                nudRow.Maximum = maxY;
                cmdOK.Enabled = true;
            }
            else
            {
                nudCol.Maximum = 1;
                nudRow.Maximum = 1;
                cmdOK.Enabled = false;
            }
        }

        private void getIntersectionPattern()
        {
            UpdatePreview();

            //String[] letters = new String[txtAnswer.Text.Length];

            // Get definite and proposed substitutions
            LetterSubstitutionSet ssChanges = LetterSubstitutionSet.getIntersectionChanges(pc, __callingInstance.crossword.placedClues, txtAnswer.Text.Length);

            if (ssChanges.proposedChanges.Count > 0)
            {
                DialogResult dr = MessageBox.Show(String.Format("This clue intersects with {0} other clue(s) which have letters in place. Do you want to update this clue's answer to match?", ssChanges.proposedChanges.Count),
                    "Update Clue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    foreach (LetterSubstitution ls in ssChanges.proposedChanges)
                    {
                        int j = 0;
                        for (int i = 0; i < ls.position; i++)
                        {
                            while (txtAnswer.Text[i + j] == ' ') { j++; }
                        }
                        StringBuilder tmp = new StringBuilder(txtAnswer.Text);
                        tmp[ls.position + j] = Convert.ToChar(ls.newValue);
                        txtAnswer.Text = tmp.ToString();
                    }
                }
            }
            if (ssChanges.definiteChanges.Count > 0)
            {
                foreach (LetterSubstitution ls in ssChanges.definiteChanges)
                {
                    int j = 0;
                    for (int i = 0; i < ls.position; i++)
                    {
                        while (txtAnswer.Text[i + j] == ' ') { j++; }
                    }
                    StringBuilder tmp = new StringBuilder(txtAnswer.Text);
                    tmp[ls.position + j] = Convert.ToChar(ls.newValue);
                    txtAnswer.Text = tmp.ToString();
                }
            }
            UpdatePreview();
        }

        public PlacedClue AcceptedClue
        {
            get
            {
                if (result == DialogResult.OK)
                {
                    return pc;
                }
                else
                {
                    return null;
                }
            }
        }

        private void UpdatePreview()
        {
            pc.clue.answer = txtAnswer.Text;
            pc.clue.question = txtQuestion.Text;
            pc.x = Convert.ToInt32(nudCol.Value) - 1;
            pc.y = Convert.ToInt32(nudRow.Value) - 1;
            switch (comboOrientation.Text)
            {
                case "Across":
                    pc.orientation = AD.Across;
                    break;
                case "Down":
                    pc.orientation = AD.Down;
                    break;
                default:
                    pc.orientation = AD.Unset;
                    break;
            }
            txtPreview.Text = pc.clue.ToString();
            setMinMax();
        }

        private void txtAnswer_TextChanged(object sender, EventArgs e)
        {
            if (__loaded) { UpdatePreview(); }
        }

        private void txtQuestion_TextChanged(object sender, EventArgs e)
        {
            if (__loaded) { UpdatePreview(); }
        }

        private void txtAnswer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (__loaded) { UpdatePreview(); }
        }

        private void txtQuestion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (__loaded) { UpdatePreview(); }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            UpdatePreview();
            result = DialogResult.OK;
            this.Hide();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            result = DialogResult.Cancel;
            this.Hide();
        }

        private void nudCol_ValueChanged(object sender, EventArgs e)
        {
            if (__loaded) { UpdatePreview(); }
        }

        private void nudRow_ValueChanged(object sender, EventArgs e)
        {
            if (__loaded) { UpdatePreview(); }
        }

        private void comboOrientation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (__loaded) { UpdatePreview(); }
        }

        private void comboOrientation_TextUpdate(object sender, EventArgs e)
        {
            if (__loaded) { UpdatePreview(); }
        }

        private void nudCol_Enter(object sender, EventArgs e)
        {
            nudCol.Select(0, nudCol.Value.ToString().Length);
        }

        private void nudRow_Enter(object sender, EventArgs e)
        {
            nudRow.Select(0, nudRow.Value.ToString().Length);
        }

        private void dgvPossibleClues_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell dgvc = dgvPossibleClues[0, e.RowIndex];
            txtAnswer.Text = dgvc.Value.ToString().ToUpper();
            dgvc = dgvPossibleClues[1, e.RowIndex];
            if (dgvc.Value.ToString() != "") { txtQuestion.Text = dgvc.Value.ToString(); }
        }

        /// <summary>
        /// Sets focus to question or answer, depending on the state of completion of the clue
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">The EventArgs relating to the triggering event</param>
        private void ClueEntry_Shown(object sender, EventArgs e)
        {
            // Set focus on question if answer has no blanks and is longer than 1 character            
            if (txtAnswer.Text.Contains("?") || (txtAnswer.Text.Length <= 1))
            {
                txtAnswer.Focus();
            }
            else
            {
                txtQuestion.Focus();
            }
        }

        private void ClueEntry_VisibleChanged(object sender, EventArgs e)
        {
            if (txtAnswer.Text.Contains("?"))
            {
                txtAnswer.Focus();
            }
            else
            {
                txtQuestion.Focus();
            }
        }

        /*
        private void ClueEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            // TODO - abstract this to a class method in a new class PopupForm, and make ClueEntry etc. inherit from that form
            if (__callingInstance != null)
            {
                __callingInstance.SetLastPosition("ClueEntry", this.Location);
            }
        }
        */
    }
}
