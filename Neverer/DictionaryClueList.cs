using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neverer
{
    public partial class DictionaryClueList : Form
    {
        public List<UtilityClass.Clue> ClueChoice { get; set; }
        public bool? CreateNew { get; set; }
        public UtilityClass.Clue SelectedClue { get; set; }

        public DictionaryClueList()
        {
            InitializeComponent();
        }

        public void Populate()
        {
            listAllClues.DisplayMember = "clueText";
            listAllClues.ValueMember = "id";
            listAllClues.DataSource = ClueChoice;
            CreateNew = null;
        }

        private void cmdAddNew_Click(object sender, EventArgs e)
        {
            CreateNew = true;
            this.Hide();
        }

        private void cmdReplace_Click(object sender, EventArgs e)
        {
            CreateNew = false;
            SelectedClue = (UtilityClass.Clue)listAllClues.SelectedItem;
            this.Hide();
        }
    }
}
