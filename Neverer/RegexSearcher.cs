﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Neverer.UtilityClass;
using System.Diagnostics;

namespace Neverer
{
    public partial class RegexSearcher : Form
    {
        private Creator __callingForm;
        private List<String> __matchingWords;

        public RegexSearcher(Creator callingForm)
        {
            this.__callingForm = callingForm;
            InitializeComponent();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            String pattern = txtSearchPattern.Text;
            String patternError = "Unknown error in search pattern";
            Regex re = null;

            try
            {
                re = new Regex(pattern, RegexOptions.IgnoreCase);
            }
            catch (Exception ex)
            {
                patternError = "Error in search pattern:"
                                + Environment.NewLine
                                + Environment.NewLine
                                + ex.Message;
            }
            if (re == null)
            {
                // Show error
                MessageBox.Show(patternError);
            }
            else
            {
                // Do search
                __matchingWords = new List<String>();

                for (DictType dt = DictType.Default; dt < DictType.Remote; dt++)
                {
                    List<CrosswordDictionary> dicts = __callingForm.AllDictionaries[dt];
                    foreach (CrosswordDictionary dict in dicts)
                    {
                        List<KeyValuePair<String, List<String>>> possibles = (from KeyValuePair<String, List<String>> kvp in dict.entries
                                                                              where re.IsMatch(kvp.Key)
                                                                              select kvp).ToList();
                        foreach (KeyValuePair<String, List<String>> kvp in possibles)
                        {
                            __matchingWords.Add(kvp.Key);
                            //if (kvp.Value == null || kvp.Value.Count == 0)
                            //{
                            //    // Word without definition
                            //    pc.addMatch(kvp.Key);
                            //}
                            //else
                            //{
                            //    // Add word with multiple definitions
                            //    foreach (String question in kvp.Value)
                            //    {
                            //        pc.addMatch(kvp.Key, question);
                            //    }
                            //}
                        }
                    }
                }

                lbResults.DataSource = __matchingWords;
            }

            txtSearchPattern.Focus();
        }

        private void llRegexSyntax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference");
        }
    }
}