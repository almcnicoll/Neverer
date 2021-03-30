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
        }

        public void calculateStats_Intersections()
        {
            // Create stats dictionary
            stats.AddOrUpdate(tpIntersections.Name, new Dictionary<String, Decimal>());

            // Calculate all intersecting clues
            foreach (PlacedClueWithStats pcws in clues)
            {
                //TODO - some of these are calculating correctly, but most are not (most evaluate to zero)
                /*String desc = String.Format("All clues where:{0}UniqueID != {1}{0}Orientation != {2}{0}"
                    + "y+height >= {4}{0}y <= {3}{0}x+width >= {6}{0}x <= {5}{0}"
                    , Environment.NewLine, pcws.UniqueID, pcws.orientation
                    , pcws.y + pcws.height - 1, pcws.y, pcws.x + pcws.width - 1, pcws.x);
                MessageBox.Show(desc);*/
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
            Char a = 'a'; Char z = 'z';
            for (int c = (int)a; c <= (int)z; c++)
            {
                int count = (from Char cc in allLetters
                             where cc == (Char)c
                             select cc).Count();
                letterCounts.Add((Char)c, count);
            }

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
            dgvLetterSpread.ResumeLayout();
            dgvLetterSpread.DataSource = (from kvp in letterCounts
                                          select new CrosswordStatistic(kvp.Key.ToString(), kvp.Value)).ToArray();

        }
    }
}
