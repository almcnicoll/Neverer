using CrosswordControls;
using Neverer.UtilityClass;
using System;
using System.Collections.Generic;
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
                pcws.intersections = (from PlacedClueWithStats pcwsTmp in clues
                                      where pcwsTmp.UniqueID != pcws.UniqueID
                                      && pcwsTmp.orientation != pcws.orientation
                                      && pcwsTmp.y <= pcws.y && pcwsTmp.y >= pcws.y
                                      && pcwsTmp.x <= pcws.x && pcwsTmp.x >= pcws.x
                                      select pcwsTmp).Count();
                
            }

            // Calculate intersection-related stats
            stats[tpIntersections.Name].Add("Min intersections", (from PlacedClueWithStats pcws in clues
                                                                select pcws.intersections).Min());
            stats[tpIntersections.Name].Add("Max intersections", (from PlacedClueWithStats pcws in clues
                                                                select pcws.intersections).Max());
            stats[tpIntersections.Name].Add("Max intersects / letter", (from PlacedClueWithStats pcws in clues
                                                                        select pcws.intersections / pcws.clue.length).Max());
            stats[tpIntersections.Name].Add("Min intersects / letter", (from PlacedClueWithStats pcws in clues
                                                                        select pcws.intersections / pcws.clue.length).Min());

            // Populate stats into grid
            dgvIntersectionStats.SuspendLayout();
            dgvIntersectionStats.Columns.Clear();
            dgvIntersectionStats.Columns.Add("Key", "Statistic");
            dgvIntersectionStats.Columns.Add("Value", "Value");
            dgvIntersectionStats.ResumeLayout();
            dgvIntersectionStats.DataSource = null;
            dgvIntersectionStats.DataSource = stats[tpIntersections.Name];

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
                flpClues.Controls.Add(cd);
            }
        }
    }
}
