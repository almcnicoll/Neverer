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
    public partial class CrosswordSettings : Form
    {
        public Crossword crossword;
        public DialogResult result = DialogResult.Cancel;

        public CrosswordSettings(Creator creator)
        {
            InitializeComponent();
            crossword = creator.crossword;
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Rotational Symmetry
            switch (crossword.rotationalSymmetryOrder)
            {
                case 1:
                    comboRotationalSymmetry.Text = "1-fold";
                    break;
                case 2:
                    comboRotationalSymmetry.Text = "2-fold";
                    break;
                default:
                    comboRotationalSymmetry.Text = "4-fold";
                    break;
            }

            // Rows & cols
            int minCols = 2;
            int minRows = 2;
            if (crossword.placedClues.Count > 0)
            {
                minCols = (
                                from PlacedClue pc in crossword.placedClues
                                select pc.x + ((pc.orientation == AD.Across) ? pc.clue.length : 0)
                               ).Max();
                minRows = (
                                from PlacedClue pc in crossword.placedClues
                                select pc.y + ((pc.orientation == AD.Down) ? pc.clue.length : 0)
                               ).Max();
            }
            if (minCols > crossword.cols) { crossword.cols = minCols; }
            if (minRows > crossword.rows) { crossword.rows = minRows; }

            nudCols.Maximum = Math.Max(320, minCols) + 1;
            nudRows.Maximum = Math.Max(320, minRows) + 1;
            nudCols.Minimum = minCols;
            nudRows.Minimum = minRows;
            nudCols.Value = crossword.cols;
            nudRows.Value = crossword.rows;

            // Title
            txtTitle.Text = ((crossword.title == null) ? "[Untitled crossword]" : crossword.title);
        }

        private void SaveSettings()
        {
            crossword.title = txtTitle.Text;
            crossword.rotationalSymmetryOrder = Convert.ToInt32(comboRotationalSymmetry.Text.ToString().Substring(0, 1));
            crossword.cols = Convert.ToInt32(nudCols.Value);
            crossword.rows = Convert.ToInt32(nudRows.Value);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            LoadSettings();
            this.Hide();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (comboRotationalSymmetry.Text.ToString().Substring(0, 1) == "4")
            {
                if (nudCols.Value != nudRows.Value)
                {
                    MessageBox.Show("If you choose 4-fold symmetry, you must have the same number of rows as columns!", "Symmetry error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            SaveSettings();
            result = DialogResult.OK;
            this.Hide();
        }
    }
}
