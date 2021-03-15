using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Neverer.UtilityClass;

namespace Neverer
{
    public partial class TextExporter : Form
    {
        public static bool ShowQuestions = false;
        public static bool ShowQuestionNumbers = false;
        public static bool ShowSolutions = false;

        private Crossword __source;

        public TextExporter(ref Crossword source)
        {
            InitializeComponent();
            __source = source;
            chkQuestionNumbers.Checked = TextExporter.ShowQuestionNumbers;
            chkQuestions.Checked = TextExporter.ShowQuestions;
            chkSolutions.Checked = TextExporter.ShowSolutions;
            GenerateText();
        }

        /// <summary>
        /// Produce the export text
        /// </summary>
        private void GenerateText(String separator = " ")
        {
            List<String> output = new List<String>();
            foreach (PlacedClue pc in __source.placedClues)
            {
                List<String> parts = new List<String>();
                if (ShowQuestionNumbers) { parts.Add(pc.placeDescriptor); }
                if (ShowQuestions) { parts.Add(pc.clueText); }
                if (ShowSolutions) { parts.Add(pc.clue.answer); }
                output.Add(String.Join(separator, parts));
            }
            txtSource.Text = String.Join(Environment.NewLine, output);
        }

        /// <summary>
        ///  Dismiss the form
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">The arguments relating to the event</param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Copy what is in the textbox to the clipboard
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">The arguments relating to the event</param>
        private void cmdCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtSource.Text);
        }

        /// <summary>
        /// Saves the contents of the textbox to a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlgFileChoice = new SaveFileDialog();
            dlgFileChoice.DefaultExt = "txt";
            DialogResult dr = dlgFileChoice.ShowDialog();
            if (dr == DialogResult.Cancel) { return; }
            File.WriteAllText(dlgFileChoice.FileName, txtSource.Text);
        }

        private void chkQuestions_CheckedChanged(object sender, EventArgs e)
        {
            TextExporter.ShowQuestions = chkQuestions.Checked;
            GenerateText();
        }

        private void chkQuestionNumbers_CheckedChanged(object sender, EventArgs e)
        {
            TextExporter.ShowQuestionNumbers = chkQuestionNumbers.Checked;
            GenerateText();
        }

        private void chkSolutions_CheckedChanged(object sender, EventArgs e)
        {
            TextExporter.ShowSolutions = chkSolutions.Checked;
            GenerateText();
        }
    }
}
