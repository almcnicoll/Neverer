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
                pcws.intersections = (from PlacedClueWithStats pcwsTmp in clues
                                      where pcwsTmp.UniqueID != pcws.UniqueID
                                      && pcwsTmp.orientation != pcws.orientation
                                      && pcwsTmp.y <= pcws.y + pcws.height - 1 && pcwsTmp.y >= pcws.y
                                      && pcwsTmp.x <= pcws.x + pcws.width - 1 && pcwsTmp.x >= pcws.x
                                      select pcwsTmp).Count();
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
                                                                            select pcws.intersections / pcws.clue.length).Max());
                stats[tpIntersections.Name].Add("Min intersects / letter", (from PlacedClueWithStats pcws in clues
                                                                            select pcws.intersections / pcws.clue.length).Min());
            }

            // Populate stats into grid
            dgvIntersectionStats.SuspendLayout();
            dgvIntersectionStats.Columns.Clear();
            //dgvIntersectionStats.Columns.Add("Key", "Statistic");
            //dgvIntersectionStats.Columns.Add("Value", "Value");
            dgvIntersectionStats.DataSource = null;
            dgvIntersectionStats.ResumeLayout();
            dgvIntersectionStats.DataSource = (from kvp in stats[tpIntersections.Name]
                                              select new CrosswordStatistic(kvp.Key,kvp.Value)).ToArray();

            // Clear FlowLayoutPanel
            foreach (Control ctrl in flpClues.Controls)
            {
                ctrl.Dispose();
            }
            flpClues.Controls.Clear();
            // Populate FlowLayoutPanel with sorted clues
            foreach (PlacedClueWithStats pcws in
                (
                from PlacedClueWithStats pcwsTmp in clues
                orderby pcwsTmp.intersections
                select pcwsTmp
                )
                )
            {
                ClueDisplay cd = new ClueDisplay();
                cd.Clue = pcws;
                cd.Statistic = pcws.intersections;
                flpClues.Controls.Add(cd);
                Debug.WriteLine("pcwsTmp.intersections == " + pcws.intersections);
            }
        }
    }
}
