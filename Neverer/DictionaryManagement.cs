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
    public partial class DictionaryManagement : Form
    {
        Creator caller = null;

        public DictionaryManagement(Creator caller)
        {
            this.caller = caller;
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void DictionaryManagement_Load(object sender, EventArgs e)
        {
            clbDictionaries.DisplayMember = "ToString";
            //clbDictionaries.ValueMember = "enabled";

            foreach (DictType dt in Enum.GetValues(typeof(DictType)))
            {
                foreach (CrosswordDictionary cd in caller.AllDictionaries[dt])
                {
                    int i = clbDictionaries.Items.Add(cd);
                    clbDictionaries.SetItemChecked(i, cd.enabled);
                }
            }
        }

        private void cmdAddLocal_Click(object sender, EventArgs e)
        {
            DialogResult dr = ofdOpenDictionary.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    foreach (String fileName in ofdOpenDictionary.FileNames)
                    {
                        CrosswordDictionary cd = CrosswordDictionary.Load(fileName, DictFileType.XML);
                        cd.enabled = true;
                        clbDictionaries.Items.Add(cd);
                        caller.currentSettings.DictionaryFiles[DictType.Custom].Add(fileName);
                        caller.currentSettings.Save();
                        caller.AllDictionaries[DictType.Custom].Add(cd);
                    }
                    break;
            }
        }
    }
}
