using CrosswordControls;
using Neverer.UtilityClass;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Neverer
{
    public partial class Statistics : Form
    {
        public Creator callingForm = null;
        private List<PlacedClueWithStats> clues = new List<PlacedClueWithStats>();
        private Dictionary<String, Dictionary<String, Decimal>> stats = new Dictionary<String, Dictionary<String, Decimal>>();
        private Dictionary<Char, int> letterCounts = new Dictionary<Char, int>();
        private Dictionary<char, Decimal> freqText = new Dictionary<char, Decimal>();
        private Dictionary<char, Decimal> freqDict = new Dictionary<char, Decimal>();
        private int totalLetterCount = 1;

        public Statistics(Creator caller)
        {
            this.callingForm = caller;
            InitializeComponent();

            // Initialise / calculate
            Init();
        }


        public void Init()
        {
            foreach (PlacedClue pc in callingForm.crossword.placedClues)
            {
                PlacedClueWithStats pcws = new PlacedClueWithStats();
                pc.CopyTo(pcws);
                this.clues.Add(pcws);
            }
            calculateStats_Intersections();
            InitFreq();
            calculateStats_Letters();
        }

        private void InitFreq()
        {
            freqText.Add('a', 0.082m); freqDict.Add('a', 0.078m); freqText.Add('b', 0.015m); freqDict.Add('b', 0.02m); freqText.Add('c', 0.028m); freqDict.Add('c', 0.04m); freqText.Add('d', 0.043m); freqDict.Add('d', 0.038m); freqText.Add('e', 0.13m); freqDict.Add('e', 0.11m); freqText.Add('f', 0.022m); freqDict.Add('f', 0.014m); freqText.Add('g', 0.02m); freqDict.Add('g', 0.03m); freqText.Add('h', 0.061m); freqDict.Add('h', 0.023m); freqText.Add('i', 0.07m); freqDict.Add('i', 0.086m); freqText.Add('j', 0.0015m); freqDict.Add('j', 0.0021m); freqText.Add('k', 0.0077m); freqDict.Add('k', 0.0097m); freqText.Add('l', 0.04m); freqDict.Add('l', 0.053m); freqText.Add('m', 0.024m); freqDict.Add('m', 0.027m); freqText.Add('n', 0.067m); freqDict.Add('n', 0.072m); freqText.Add('o', 0.075m); freqDict.Add('o', 0.061m); freqText.Add('p', 0.019m); freqDict.Add('p', 0.028m); freqText.Add('q', 0.00095m); freqDict.Add('q', 0.0019m); freqText.Add('r', 0.06m); freqDict.Add('r', 0.073m); freqText.Add('s', 0.063m); freqDict.Add('s', 0.087m); freqText.Add('t', 0.091m); freqDict.Add('t', 0.067m); freqText.Add('u', 0.028m); freqDict.Add('u', 0.033m); freqText.Add('v', 0.0098m); freqDict.Add('v', 0.01m); freqText.Add('w', 0.024m); freqDict.Add('w', 0.0091m); freqText.Add('x', 0.0015m); freqDict.Add('x', 0.0027m); freqText.Add('y', 0.02m); freqDict.Add('y', 0.016m); freqText.Add('z', 0.00074m); freqDict.Add('z', 0.0044m);
        }

        public void calculateStats_Intersections()
        {
            // Create stats dictionary
            stats.AddOrUpdate(tpIntersections.Name, new Dictionary<String, Decimal>());

            // Calculate all intersecting clues
            foreach (PlacedClueWithStats pcws in clues)
            {
                pcws.intersections = (from PlacedClueWithStats pcwsTmp in clues
                                      where pcwsTmp.UniqueID != pcws.UniqueID
                                      && pcwsTmp.orientation != pcws.orientation
                                      && pcwsTmp.y <= pcws.y + pcws.height - 1 && pcwsTmp.y + pcwsTmp.height - 1 >= pcws.y
                                      && pcwsTmp.x <= pcws.x + pcws.width - 1 && pcwsTmp.x + pcwsTmp.width - 1 >= pcws.x
                                      select pcwsTmp).Count();
                if (pcws.clue.length == 0)
                {
                    pcws.intersectionsPerLetter = 0;
                }
                else
                {
                    pcws.intersectionsPerLetter = Decimal.Round((decimal)(pcws.intersections) / (decimal)(pcws.clue.length), 2, MidpointRounding.AwayFromZero);
                }
            }

            if (clues.Count == 0)
            {
                stats[tpIntersections.Name].Add("No clues populated", 0);
            }
            else
            {
                // Calculate intersection-related stats
                stats[tpIntersections.Name].Add("Min intersections", (from PlacedClueWithStats pcws in clues
                                                                      select pcws.intersections).Min());
                stats[tpIntersections.Name].Add("Max intersections", (from PlacedClueWithStats pcws in clues
                                                                      select pcws.intersections).Max());
                stats[tpIntersections.Name].Add("Max intersects / letter", (from PlacedClueWithStats pcws in clues
                                                                            select (decimal)(pcws.intersections) / (decimal)pcws.clue.length).Max());
                stats[tpIntersections.Name].Add("Min intersects / letter", (from PlacedClueWithStats pcws in clues
                                                                            select (decimal)pcws.intersections / (decimal)pcws.clue.length).Min());
            }

            populateStats_Intersections();
        }
        public void populateStats_Intersections()
        {
            // Populate stats into grid
            dgvIntersectionStats.SuspendLayout();
            dgvIntersectionStats.Columns.Clear();
            //dgvIntersectionStats.Columns.Add("Key", "Statistic");
            //dgvIntersectionStats.Columns.Add("Value", "Value");
            dgvIntersectionStats.DataSource = null;
            dgvIntersectionStats.ResumeLayout();
            dgvIntersectionStats.DataSource = (from kvp in stats[tpIntersections.Name]
                                               select new CrosswordStatistic(kvp.Key, Decimal.Round(kvp.Value, 2, MidpointRounding.AwayFromZero))).ToArray();

            // Clear FlowLayoutPanel
            bool perLetter = !(cmbPerLetter.Text.Contains("absolute"));
            foreach (Control ctrl in flpClues.Controls)
            {
                ctrl.Dispose();
            }
            flpClues.Controls.Clear();
            // Populate FlowLayoutPanel with sorted clues
            IEnumerable<PlacedClueWithStats> sorted;
            if (perLetter)
            {
                sorted = (
                from PlacedClueWithStats pcwsTmp in clues
                orderby pcwsTmp.intersectionsPerLetter
                select pcwsTmp
                );
            }
            else
            {
                sorted = (
                from PlacedClueWithStats pcwsTmp in clues
                orderby pcwsTmp.intersections
                select pcwsTmp
                );
            }

            foreach (PlacedClueWithStats pcws in sorted)
            {
                ClueDisplay cd = new ClueDisplay();
                cd.Clue = pcws;
                if (perLetter)
                {
                    cd.Statistic = pcws.intersectionsPerLetter;
                }
                else
                {
                    cd.Statistic = pcws.intersections;
                }
                flpClues.Controls.Add(cd);
                //Debug.WriteLine("pcwsTmp.intersections == " + pcws.intersections);
            }
        }

        private void cmbPerLetter_TextChanged(object sender, EventArgs e)
        {
            populateStats_Intersections();
        }

        public void calculateStats_Letters()
        {
            List<Char> allLetters = new List<Char>();
            foreach (PlacedClueWithStats pcws in clues)
            {
                allLetters.AddRange(pcws.clue.answer.ToLowerInvariant().ToCharArray());
            }

            letterCounts.Clear();
            totalLetterCount = 0;
            Char a = 'a'; Char z = 'z';
            for (int c = (int)a; c <= (int)z; c++)
            {
                int count = (from Char cc in allLetters
                             where cc == (Char)c
                             select cc).Count();
                letterCounts.Add((Char)c, count);
                totalLetterCount += count;
            }
            if (totalLetterCount == 0) { totalLetterCount = 1; }

            populateStats_Letters();
        }

        public void populateStats_Letters()
        {
            // Populate stats into grid
            dgvLetterSpread.SuspendLayout();
            dgvLetterSpread.Columns.Clear();
            //dgvLetterSpread.Columns.Add("Key", "Statistic");
            //dgvLetterSpread.Columns.Add("Value", "Value");
            dgvLetterSpread.DataSource = null;
            dgvLetterSpread.DataSource = (from kvp in letterCounts
                                          select new Tuple<String, Decimal, Decimal, Decimal>(
                                              kvp.Key.ToString(), kvp.Value, Math.Round(freqText[kvp.Key] * totalLetterCount, 2), Math.Round(freqDict[kvp.Key] * totalLetterCount, 2))
                                          ).ToArray();
            dgvLetterSpread.Columns[0].HeaderText = "Letter";
            dgvLetterSpread.Columns[1].HeaderText = "Usage";
            dgvLetterSpread.Columns[2].HeaderText = "Expected (text)";
            dgvLetterSpread.Columns[3].HeaderText = "Expected (dict)";
            dgvLetterSpread.ResumeLayout();
        }
    }
}
