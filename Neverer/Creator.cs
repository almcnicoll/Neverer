﻿using CrosswordControls;
using Neverer.DataGridViewClasses;
using Neverer.UtilityClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;
using Serial = System.Xml.Serialization;

/*using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;*/

namespace Neverer
{
    public partial class Creator : Form
    {
        public Crossword crossword = new Crossword();
        private bool __unsavedChanges = false;
        private String __currentFilename = null;
        private bool __cluesToCheck = false;

        private List<CrosswordControls.ClueDisplay> __clueDisplays = new List<CrosswordControls.ClueDisplay>();

        public Settings currentSettings = new Settings();

        public CrosswordDictionaryCollection AllDictionaries = new CrosswordDictionaryCollection();

        /*private CrosswordDictionary DefaultWords = new CrosswordDictionary();
        private CrosswordDictionary CustomWords = new CrosswordDictionary();*/

        public Creator()
        {
            InitializeComponent();

#if (DEBUG == false)
            dlgOpen.InitialDirectory = Application.StartupPath;
            dlgSave.InitialDirectory = Application.StartupPath;
#endif

            // Get settings
            currentSettings = Settings.Load();
            currentSettings.FileListChanged += RepopulateRecentFiles; // Redo menu if list of recent files changes
            currentSettings.Set("Version", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            currentSettings.Save();

            // Repopulate Recent Files
            RepopulateRecentFiles();

            PopulateForm();
            LoadDictionaries();

            // Handle background worker updates
            bwDictionaryChecker.ProgressChanged += BwDictionaryChecker_ProgressChanged;
            this.ClueChanged += Creator_ClueChanged;
        }

        private void Creator_ClueChanged(Object sender, EventArgs e)
        {
            if (sender is PlacedClue)
            {
                UpdateClueGrid(sender as PlacedClue);
                //populateClueDisplays(sender as PlacedClue);
            }
            else
            {
                UpdateClueGrid();
                //populateClueDisplays();
            }
            UpdatePreview();
            runClueCheck();
        }

        private void BwDictionaryChecker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tspbClueUpdate.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100)
            {
                UpdateClueGrid();
            }
        }

        // Enums
        public enum OutputStyle
        {
            EmptyGrid = 0,
            GridWithAnswers = 1
        }

        // Variables
        public List<String> previewErrors = new List<String>();
        private int __minDimension = 10000;

        // Events
        public event EventHandler ClueChanged;

        // Functions & Properties
        private void UpdateTitleText()
        {
            String unsavedMarker = "";
            if (unsavedChanges)
            {
                unsavedMarker = "*";
            }
            String filenameMarker = "[unsaved crossword]";
            if (currentFilename != null)
            {
                filenameMarker = Path.GetFileName(currentFilename);
            }
            this.Text = String.Format("Neverer Creator - {0}{1}", filenameMarker, unsavedMarker);
        }

        public bool unsavedChanges
        {
            get
            {
                return __unsavedChanges;
            }
            set
            {
                __unsavedChanges = value;
                UpdateTitleText();
            }
        }

        public String currentFilename
        {
            get
            {
                return __currentFilename;
            }
            set
            {
                __currentFilename = value;
                UpdateTitleText();
            }
        }

        public String[,] GridArray
        {
            get
            {
                previewErrors.Clear();

                String[,] allLetters = new String[crossword.width, crossword.height];

                // Populate with blanks
                for (var c = 0; c < crossword.width; c++)
                {
                    for (var r = 0; r < crossword.height; r++)
                    {
                        allLetters[c, r] = "";
                    }
                }

                // Fill out letters from clues
                foreach (PlacedClue pc in crossword.placedClues)
                {
                    switch (pc.orientation)
                    {
                        case AD.Across:
                            for (var i = 0; i < pc.clue.length; i++)
                            {
                                int xx = pc.x + i; int yy = pc.y;
                                if (allLetters[xx, yy] == "")
                                {
                                    allLetters[xx, yy] += pc.clue.letters[i].ToString();
                                }
                                else if (allLetters[xx, yy] == pc.clue.letters[i].ToString())
                                {
                                    // All good - intersecting clues with same letter
                                }
                                else if (allLetters[xx, yy] == "?")
                                {
                                    // All good - question-mark can be overwritten
                                    allLetters[xx, yy] = pc.clue.letters[i].ToString();
                                }
                                else if (pc.clue.letters[i].ToString() == "?")
                                {
                                    // All good - question-mark can be ignored
                                }
                                else
                                {
                                    previewErrors.Add(String.Format("There is a conflict between two clues at ({0},{1})", xx + 1, yy + 1));
                                    allLetters[xx, yy] += pc.clue.letters[i].ToString();
                                }
                            }
                            break;
                        case AD.Down:
                            for (var i = 0; i < pc.clue.length; i++)
                            {
                                int xx = pc.x; int yy = pc.y + i;
                                if (allLetters[xx, yy] == "")
                                {
                                    allLetters[xx, yy] += pc.clue.letters[i].ToString();
                                }
                                else if (allLetters[xx, yy] == pc.clue.letters[i].ToString())
                                {
                                    // All good - intersecting clues with same letter
                                }
                                else if (allLetters[xx, yy] == "?")
                                {
                                    // All good - question-mark can be overwritten
                                    allLetters[xx, yy] = pc.clue.letters[i].ToString();
                                }
                                else if (pc.clue.letters[i].ToString() == "?")
                                {
                                    // All good - question-mark can be ignored
                                }
                                else
                                {
                                    previewErrors.Add(String.Format("There is a conflict between two clues at ({0},{1})", xx + 1, yy + 1));
                                    allLetters[xx, yy] += pc.clue.letters[i].ToString();
                                }
                            }
                            break;
                        default:
                            previewErrors.Add(String.Format("A clue has not been set as either across or down: {0}", pc.clue.answer));
                            break;
                    }
                }

                return allLetters;
            }
        }
        public void PopulateForm()
        {
            // Set up columns and rows
            dgvPuzzle.ColumnCount = crossword.width;
            dgvPuzzle.RowCount = crossword.height;
            dgvPuzzle.ColumnHeadersVisible = false;
            dgvPuzzle.RowHeadersVisible = false;
            dgvPuzzle.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;

            SetBoxSize();

            if (ClueChanged != null) { ClueChanged("", new EventArgs()); }
            DisplayCrosswordTitle();

            cmdAddClue.Focus();
        }

        private void DisplayCrosswordTitle()
        {
            if (this.crossword.title == null)
            {
                lblPuzzleTitle.Text = "";
            }
            else
            {
                lblPuzzleTitle.Text = " : " + this.crossword.title;
            }
        }

        private void SetBoxSize()
        {
            // Set up dimensions
            __minDimension = Math.Min(
                Convert.ToInt32(Math.Floor((double)dgvPuzzle.Width / (double)crossword.cols)),
                Convert.ToInt32(Math.Floor((double)dgvPuzzle.Height / (double)crossword.rows))
            );
            __minDimension -= 3;
            foreach (DataGridViewColumn dgvc in dgvPuzzle.Columns) { dgvc.Width = __minDimension; }
            foreach (DataGridViewRow dgvr in dgvPuzzle.Rows) { dgvr.Height = __minDimension; }
        }

        private void UpdateClueGrid(PlacedClue LastClue = null)
        {
            // If we don't specify a last clue, check if anything selected
            if (LastClue == null)
            {
                DataGridViewSelectedCellCollection selCells = dgvClues.SelectedCells;
                if ((selCells != null) && (selCells.Count > 0))
                {
                    DataGridViewRow dgvrLast = selCells[0].OwningRow;
                    LastClue = (PlacedClue)dgvrLast.DataBoundItem;
                }
            }
            dgvClues.AutoGenerateColumns = false;
            dgvClues.Columns.Clear();
            dgvClues.DataSource = null;
            DataGridViewColumn dgvcPlaceDescriptor = new DataGridViewColouredColumn();
            dgvcPlaceDescriptor.DataPropertyName = "placeDescriptor";
            dgvcPlaceDescriptor.HeaderText = "#";
            dgvcPlaceDescriptor.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridViewColumn dgvcClueText = new DataGridViewColouredColumn();
            dgvcClueText.DataPropertyName = "clueText";
            dgvcClueText.HeaderText = "Clue";
            dgvcClueText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvClues.Columns.Add(dgvcPlaceDescriptor);
            dgvClues.Columns.Add(dgvcClueText);
            dgvClues.RowHeadersWidth = 20;
            dgvClues.DataSource = crossword.sortedClueList;

            if (LastClue != null)
            {
                foreach (DataGridViewRow dgvrLoop in dgvClues.Rows)
                {
                    PlacedClue pcLoop = (PlacedClue)dgvrLoop.DataBoundItem;
                    if (pcLoop.UniqueID == LastClue.UniqueID)
                    {
                        dgvrLoop.Selected = true;
                    }

                    DataGridViewCellStyle cs = new DataGridViewCellStyle(dgvrLoop.DefaultCellStyle);
                    cs.BackColor = pcLoop.statusColor;

                    ((DataGridViewColouredCell)(dgvrLoop.Cells[0])).Status = pcLoop.status;
                    //dgvrLoop.Cells[0].Style = cs;
                }
            }

            dgvClues.Invalidate();
        }
        private void UpdatePreview()
        {
            this.Cursor = Cursors.WaitCursor;
            dgvPuzzle.SuspendLayout();
            PlacedClue pc;
            if (dgvClues.SelectedCells == null || dgvClues.SelectedCells.Count == 0)
            {
                pc = null;
            }
            else
            {
                int row = dgvClues.SelectedCells[0].RowIndex;
                pc = ((PlacedClue)dgvClues.Rows[row].DataBoundItem);
            }

            String[,] ga = GridArray;

            // Clear and refresh
            dgvPuzzle.Rows.Clear();

            for (int r = 0; r < crossword.height; r++)
            {
                DataGridViewRow row = new DataGridViewRow() { Height = __minDimension };
                DataGridViewTextBoxCell cell;
                for (int c = 0; c < crossword.width; c++)
                {
                    bool hasConflict = false;
                    switch (ga[c, r].Length)
                    {
                        case 0:
                            // Black square
                            cell = new DataGridViewTextBoxCell() { Value = "" };
                            cell.Style.BackColor = System.Drawing.Color.Black; cell.Style.ForeColor = System.Drawing.Color.Black;
                            row.Cells.Add(cell);
                            break;
                        case 1:
                            cell = new DataGridViewTextBoxCell() { Value = ga[c, r] };
                            cell.Style.BackColor = System.Drawing.Color.White; cell.Style.ForeColor = System.Drawing.Color.Black;
                            cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            cell.Style.WrapMode = DataGridViewTriState.True;
                            row.Cells.Add(cell);
                            break;
                        default:
                            hasConflict = true;
                            cell = new DataGridViewTextBoxCell() { Value = "*", ToolTipText = "Competing letters: " + ga[c, r] };
                            cell.Style.BackColor = System.Drawing.Color.Red; cell.Style.ForeColor = System.Drawing.Color.Red;
                            row.Cells.Add(cell);
                            break;
                    }
                    // Format current clue
                    if ((!hasConflict) && (pc != null) && (pc.Overlaps(c, r)))
                    {
                        cell.Style.BackColor = System.Drawing.Color.CornflowerBlue; cell.Style.ForeColor = System.Drawing.Color.Black;
                    }
                }
                dgvPuzzle.Rows.Add(row);
            }
            // No selection please
            dgvPuzzle.ClearSelection();
            dgvPuzzle.Rows[0].Cells[0].Selected = false;
            this.Cursor = Cursors.Default;
            dgvPuzzle.ResumeLayout();
        }

        private PlacedClue getClue()
        {
            ClueEntry ce = new ClueEntry(this);
            ce.ShowDialog();

            return ce.AcceptedClue;
        }
        private PlacedClue getClue(PlacedClue template)
        {
            ClueEntry ce = new ClueEntry(this, template);
            ce.ShowDialog();

            return ce.AcceptedClue;
        }
        private PlacedClue getClue(int x, int y)
        {
            PlacedClue template = new PlacedClue();
            template.x = x; template.y = y;
            template.clue = new Clue(Clue.BlankQuestion, "?");
            ClueEntry ce = new ClueEntry(this, template);
            ce.ShowDialog();

            return ce.AcceptedClue;
        }

        private void populateClueDisplays(PlacedClue pc = null)
        {
            // TODO - if pc != null, only update that one clue
            // (NB might not be needed - in theory, should update itself
            //  with the right event subscribed, similar to the statuschanged one)

            // Clear the panel
            flpClues.Controls.Clear();

            // Populate new controls from the PlacedClues collection
            foreach (PlacedClue pcRef in crossword.placedClues)
            {
                ClueDisplay cd = new ClueDisplay();
                cd.Height = 24;
                cd.BorderStyle = BorderStyle.FixedSingle;
                cd.Margin = new Padding(0);
                cd.Clue = pcRef;
                __clueDisplays.Add(cd);
                flpClues.Controls.Add(cd);
                cd.Click += ClueDisplay_Click;
            }
        }

        private void ClueDisplay_Click(object sender, EventArgs e)
        {
            if (sender is ClueDisplay)
            {
                ClueDisplay cdClicked = (ClueDisplay)sender;
                if (cdClicked.Selected)
                {
                    cdClicked.Selected = false;
                }
                else
                {
                    foreach (ClueDisplay cdLoop in flpClues.Controls)
                    {
                        cdLoop.Selected = false;
                    }
                    cdClicked.Selected = true;
                }
            }
        }

        private void cmdAddClue_Click(object sender, EventArgs e)
        {
            TryClueAdd();
        }

        private void dgvPuzzle_Resize(object sender, EventArgs e)
        {
            SetBoxSize();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (unsavedChanges)
            {
                DialogResult dr = MessageBox.Show("You have unsaved changes. Are you sure that you want to create a new crossword?", "New Crossword", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }
            crossword = new Crossword();
            currentFilename = null;
            ShowSettings();
            // In case they hit cancel, we still need to show new blank, default crossword
            PopulateForm();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String strSelectedFile = chooseSaveFilename();
            if (strSelectedFile == null)
            {
                MessageBox.Show("Crossword not saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                currentFilename = strSelectedFile;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFilename == null)
            {
                currentFilename = chooseSaveFilename();
                if (currentFilename == null)
                {
                    MessageBox.Show("Crossword not saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            save();
        }

        private String chooseSaveFilename()
        {
            if (currentFilename != null)
            {
                dlgSave.InitialDirectory = Path.GetDirectoryName(currentFilename);
            }
            DialogResult dr = dlgSave.ShowDialog();
            if (dr == DialogResult.OK)
            {
                return dlgSave.FileName;
            }
            else
            {
                return null;
            }
        }
        private String chooseOpenFilename()
        {
            if (currentFilename != null)
            {
                dlgOpen.InitialDirectory = Path.GetDirectoryName(currentFilename);
            }
            DialogResult dr = dlgOpen.ShowDialog();
            if (dr == DialogResult.OK)
            {
                return dlgOpen.FileName;
            }
            else
            {
                return null;
            }
        }

        private bool save()
        {
            try
            {
                Serial.XmlSerializer serial = new System.Xml.Serialization.XmlSerializer(crossword.GetType());
                XmlDocument xmlDoc = new XmlDocument();
                using (MemoryStream stream = new MemoryStream())
                {
                    serial.Serialize(stream, crossword);
                    stream.Position = 0;
                    xmlDoc.Load(stream);
                    xmlDoc.Save(currentFilename);
                    stream.Close();
                }
                unsavedChanges = false;
                currentSettings.AddToFileList(currentFilename);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error saving crossword: {0}", ex.Message));
                return false;
            }
        }

        private bool open(String fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show(String.Format("Error opening crossword: file {0} empty or missing.", fileName));
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                string xmlString = xmlDoc.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(Crossword);

                    Serial.XmlSerializer serializer = new Serial.XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        crossword = (Crossword)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
                unsavedChanges = false;
                currentSettings.AddToFileList(fileName);
                runClueCheck();
                populateClueDisplays();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error opening crossword: {0}", ex.Message));
                return false;
            }
        }

        private void RecentFileClickHandler(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            String fileName = tsi.Tag.ToString();

            if (unsavedChanges)
            {
                DialogResult dr = MessageBox.Show("You have unsaved changes. Are you sure that you want to open another crossword?", "New Crossword", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }

            if (open(fileName))
            {
                PopulateForm();
                currentFilename = fileName;
                currentSettings.AddToFileList(fileName);
            }
            return;
        }

        private void RepopulateRecentFiles(object sender = null, EventArgs e = null)
        {
            recentFilesToolStripMenuItem.DropDownItems.Clear();
            int counter = 0;
            foreach (String filePath in currentSettings.RecentFiles)
            {
                counter++;
                ToolStripItem tsi = new ToolStripMenuItem();
                tsi.Text = String.Format("&{0}: {1}", counter.ToString(),
                            Path.GetFileNameWithoutExtension(filePath));
                tsi.Tag = filePath;
                tsi.ToolTipText = filePath;
                tsi.Click += RecentFileClickHandler;
                recentFilesToolStripMenuItem.DropDownItems.Add(tsi);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (unsavedChanges)
            {
                DialogResult dr = MessageBox.Show("You have unsaved changes. Are you sure that you want to open another crossword?", "New Crossword", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }

            String fileName = chooseOpenFilename();
            if (fileName == null)
            {
                return;
            }

            if (open(fileName))
            {
                PopulateForm();
                currentFilename = fileName;
            }
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            TryClueEdit();
        }

        private void dgvClues_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TryClueEdit();
        }

        private void TryClueAdd(int x = 1, int y = 1)
        {
            PlacedClue pc = getClue(x, y);
            if (pc != null)
            {
                crossword.placedClues.Add(pc);
                unsavedChanges = true;
                switch (crossword.rotationalSymmetryOrder)
                {
                    case 4:
                        // Reflect 3x 90-degree clues
                        // 180
                        PlacedClue pc180 = new PlacedClue();
                        pc180.clue = pc.clue.blankClone();
                        pc180.orientation = pc.orientation;
                        if (pc.orientation == AD.Across)
                        {
                            pc180.x = Math.Min(crossword.cols - 1 - pc.x, crossword.cols - (pc.x + pc.clue.length));
                            pc180.y = crossword.rows - 1 - pc.y;
                        }
                        else if (pc.orientation == AD.Down)
                        {
                            pc180.y = Math.Min(crossword.rows - 1 - pc.y, crossword.rows - (pc.y + pc.clue.length));
                            pc180.x = crossword.cols - 1 - pc.x;
                        }
                        // 90
                        PlacedClue pc90 = new PlacedClue();
                        pc90.clue = pc.clue.blankClone();
                        pc90.orientation = ((pc.orientation == AD.Across) ? AD.Down : AD.Across);
                        pc90.x = pc180.y;
                        pc90.y = pc.x;
                        // 270
                        PlacedClue pc270 = new PlacedClue();
                        pc270.clue = pc.clue.blankClone();
                        pc270.orientation = ((pc.orientation == AD.Across) ? AD.Down : AD.Across);
                        pc270.x = pc.y;
                        pc270.y = pc180.x;

                        crossword.placedClues.Add(pc90);
                        crossword.placedClues.Add(pc180);
                        crossword.placedClues.Add(pc270);
                        break;
                    case 2:
                        // Reflect 1x Complement-degree clue
                        // 180
                        PlacedClue pcComplement = new PlacedClue();
                        pcComplement.clue = pc.clue.blankClone();
                        pcComplement.orientation = pc.orientation;
                        if (pc.orientation == AD.Across)
                        {
                            pcComplement.x = Math.Min(crossword.cols - 1 - pc.x, crossword.cols - (pc.x + pc.clue.length));
                            pcComplement.y = crossword.rows - 1 - pc.y;
                        }
                        else if (pc.orientation == AD.Down)
                        {
                            pcComplement.y = Math.Min(crossword.rows - 1 - pc.y, crossword.rows - (pc.y + pc.clue.length));
                            pcComplement.x = crossword.cols - 1 - pc.x;
                        }
                        crossword.placedClues.Add(pcComplement);
                        break;
                    default:
                        // No need to reflect any clues
                        break;
                }
                UpdatePreview();
            }
            ClueChanged("", new EventArgs());
        }

        private void TryClueEdit()
        {
            if (dgvClues.SelectedCells == null || dgvClues.SelectedCells.Count == 0)
            {
                MessageBox.Show("You must select a clue to edit", "Edit Clue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int row = dgvClues.SelectedCells[0].RowIndex;
            PlacedClue pc = ((PlacedClue)dgvClues.Rows[row].DataBoundItem);
            PlacedClue pcTmp = getClue(pc);
            if (pcTmp != null)
            {
                // Copy to original PlacedClue entry, to update crossword object and clue list
                pcTmp.CopyTo(pc); // TODO - test this line for ClueDisplay usage
                var junk = crossword.sortedClueList; // TODO - test this line for ClueDisplay usage

                foreach (PlacedClue pcLoop in crossword.placedClues)
                {
                    if (pcLoop.UniqueID == pc.UniqueID)
                    {
                        // Copy values across, but set status to "Unknown" so we reevaluate it
                        pcTmp.status = ClueStatus.Unknown;
                        pcTmp.CopyTo(pcLoop);
                        pcLoop.status = ClueStatus.Unknown;
                    }
                }
                unsavedChanges = true;
                ClueChanged(pcTmp, new EventArgs());
            }
            // TODO - need routine we can call to reorder ClueDisplay controls list
        }

        private void cmdRemoveClue_Click(object sender, EventArgs e)
        {
            if (dgvClues.SelectedCells == null || dgvClues.SelectedCells.Count == 0)
            {
                MessageBox.Show("You must select a clue to remove", "Remove Clue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int row = dgvClues.SelectedCells[0].RowIndex;
            PlacedClue pc = ((PlacedClue)dgvClues.Rows[row].DataBoundItem);

            DialogResult dr = MessageBox.Show(String.Format("Are you sure you want to remove this clue?{0}{0}{1}{0}{2}", Environment.NewLine, pc.clueText, pc.clue.answer), "Remove Clue", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }

            for (int i = crossword.placedClues.Count - 1; i >= 0; i--)
            {
                PlacedClue pcLoop = crossword.placedClues[i];
                if (pcLoop.UniqueID == pc.UniqueID)
                {
                    crossword.placedClues.RemoveAt(i);
                }
            }
            unsavedChanges = true;
            ClueChanged("", new EventArgs());
        }

        private void dgvClues_SelectionChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dgvClues_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    TryClueEdit();
                    break;
            }
        }

        private bool FindClueFromGridPos(int x, int y)
        {
            foreach (PlacedClue pc in this.crossword.placedClues)
            {
                if (pc.Overlaps(x, y))
                {
                    foreach (DataGridViewRow dgvr in dgvClues.Rows)
                    {
                        PlacedClue pc2 = (PlacedClue)dgvr.DataBoundItem;
                        if (pc.UniqueID == pc2.UniqueID)
                        {
                            dgvClues.ClearSelection();
                            dgvr.Selected = true;
                            dgvClues.CurrentCell = dgvr.Cells[0];
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private void dgvPuzzle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FindClueFromGridPos(e.ColumnIndex, e.RowIndex);
        }

        private void dgvPuzzle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bool foundClue = FindClueFromGridPos(e.ColumnIndex, e.RowIndex);
            if (foundClue)
            {
                // Edit double-clicked clue
                TryClueEdit();
            }
            else
            {
                // Create blank clue here
                TryClueAdd(e.ColumnIndex, e.RowIndex);
            }
        }


        /*private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeExcel();
        }*/
        private void exportPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakePDF();
        }

        private void MakePDF()
        {
            MessageBox.Show("Not implemented", "Make PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //// Create doc
            //Document doc = new Document();
            //doc.Info.Title = Path.GetFileNameWithoutExtension(currentFilename);
            //doc.Info.Subject = currentFilename;
            //doc.Info.Author = "Neverer Creator";

            //// Styles
            //Style style = doc.Styles["Normal"];
            //style.Font.Name = "Lora";
            //style = doc.Styles["Heading1"];
            //style.Font.Name = "Montserrat";
            //style.Font.Size = 18;
            //style = doc.Styles["Heading2"];
            //style.Font.Name = "Montserrat";
            //style.Font.Size = 14;

            //// Add title
            //doc.LastSection.AddParagraph(doc.Info.Title, "Heading1");

            //// Split into columns
            //Section section = doc.AddSection();
            //Table tabLayout = new Table();
            //Column colClues = tabLayout.AddColumn(Unit.FromCentimeter(10));
            //Column colPuzzle = tabLayout.AddColumn(Unit.FromCentimeter(8));
            //Row rowLayout = tabLayout.AddRow();
            //Cell cellClues = rowLayout.Cells[0];
            //Cell cellPuzzle = rowLayout.Cells[1];

            //// Clues column
            //Table tabClues = new Table();
            ////cellClues.Add

            //// Puzzle column
            //Table tabPuzzle = new Table();

            //// Add tables in
            //cellClues.AddTextFrame().Add(tabClues);
            //cellPuzzle.AddTextFrame().Add(tabPuzzle);
            //doc.LastSection.Add(tabLayout);
        }

        private void MakeExcel(OutputStyle style = OutputStyle.EmptyGrid)
        {
            if (crossword.title == null)
            {
                ShowSettings();
            }

            int firstClueCol = 1;
            int firstClueRow = 4;
            int firstPuzzleCol = 4;
            int firstPuzzleRow = 4;

            Excel.Application eApp = new Excel.Application();
            eApp.Visible = true;
            Excel.Workbook eWbk = eApp.Workbooks.Add();
            Excel.Worksheet eWks = eWbk.Sheets[1];

            int r = 0;

            // Title
            ((Excel.Range)eWks.Range[eWks.Cells[1, firstClueCol], eWks.Cells[firstPuzzleRow - 1, firstPuzzleCol + this.crossword.cols - 1]]).Merge();
            ((Excel.Range)eWks.Range[eWks.Cells[1, firstClueCol], eWks.Cells[firstPuzzleRow - 1, firstPuzzleCol + this.crossword.cols - 1]]).Font.Bold = true;
            ((Excel.Range)eWks.Range[eWks.Cells[1, firstClueCol], eWks.Cells[firstPuzzleRow - 1, firstPuzzleCol + this.crossword.cols - 1]]).Font.Size = 24;
            ((Excel.Range)eWks.Range[eWks.Cells[1, firstClueCol], eWks.Cells[firstPuzzleRow - 1, firstPuzzleCol + this.crossword.cols - 1]]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ((Excel.Range)eWks.Range[eWks.Cells[1, firstClueCol], eWks.Cells[firstPuzzleRow - 1, firstPuzzleCol + this.crossword.cols - 1]]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            ((Excel.Range)eWks.Range[eWks.Cells[1, firstClueCol], eWks.Cells[firstPuzzleRow - 1, firstPuzzleCol + this.crossword.cols - 1]]).Value = crossword.title;

            // Puzzle setup (black fill, square cells, borders
            ((Excel.Range)eWks.Range[eWks.Cells[firstPuzzleRow, firstPuzzleCol], eWks.Cells[firstPuzzleRow + this.crossword.rows - 1, firstPuzzleCol + this.crossword.cols - 1]]).Interior.Color = Color.Black.ToOle();
            switch (style)
            {
                case OutputStyle.EmptyGrid:
                    ((Excel.Range)eWks.Range[eWks.Cells[firstPuzzleRow, firstPuzzleCol], eWks.Cells[firstPuzzleRow + this.crossword.rows - 1, firstPuzzleCol + this.crossword.cols - 1]]).Font.Size = 6;
                    ((Excel.Range)eWks.Range[eWks.Cells[firstPuzzleRow, firstPuzzleCol], eWks.Cells[firstPuzzleRow + this.crossword.rows - 1, firstPuzzleCol + this.crossword.cols - 1]]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    ((Excel.Range)eWks.Range[eWks.Cells[firstPuzzleRow, firstPuzzleCol], eWks.Cells[firstPuzzleRow + this.crossword.rows - 1, firstPuzzleCol + this.crossword.cols - 1]]).VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                    break;

                case OutputStyle.GridWithAnswers:
                    ((Excel.Range)eWks.Range[eWks.Cells[firstPuzzleRow, firstPuzzleCol], eWks.Cells[firstPuzzleRow + this.crossword.rows - 1, firstPuzzleCol + this.crossword.cols - 1]]).Font.Size = 10;
                    ((Excel.Range)eWks.Range[eWks.Cells[firstPuzzleRow, firstPuzzleCol], eWks.Cells[firstPuzzleRow + this.crossword.rows - 1, firstPuzzleCol + this.crossword.cols - 1]]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ((Excel.Range)eWks.Range[eWks.Cells[firstPuzzleRow, firstPuzzleCol], eWks.Cells[firstPuzzleRow + this.crossword.rows - 1, firstPuzzleCol + this.crossword.cols - 1]]).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    break;
            }

            ((Excel.Range)eWks.Range[eWks.Cells[firstPuzzleRow, firstPuzzleCol], eWks.Cells[firstPuzzleRow + this.crossword.rows - 1, firstPuzzleCol + this.crossword.cols - 1]]).Columns.ColumnWidth = 3;
            ((Excel.Range)eWks.Range[eWks.Cells[firstPuzzleRow, firstPuzzleCol], eWks.Cells[firstPuzzleRow + this.crossword.rows - 1, firstPuzzleCol + this.crossword.cols - 1]]).Rows.RowHeight = 20;
            ((Excel.Range)eWks.Range[eWks.Cells[firstPuzzleRow, firstPuzzleCol], eWks.Cells[firstPuzzleRow + this.crossword.rows - 1, firstPuzzleCol + this.crossword.cols - 1]]).AllBorders();

            // Clues
            r = firstClueRow;
            AD lastOrient = AD.Unset;
            foreach (PlacedClue pc in this.crossword.sortedClueList)
            {
                // Add heading if it's first clue of this orientation
                if (pc.orientation != lastOrient)
                {
                    // Need clue-type title
                    switch (pc.orientation)
                    {
                        case AD.Down:
                            ((Excel.Range)eWks.Cells[r, firstClueCol]).Value = "Clues down";
                            ((Excel.Range)eWks.Cells[r, firstClueCol]).Font.Bold = true;
                            r++;
                            break;
                        case AD.Across:
                            ((Excel.Range)eWks.Cells[r, firstClueCol]).Value = "Clues across";
                            ((Excel.Range)eWks.Cells[r, firstClueCol]).Font.Bold = true;
                            r++;
                            break;
                    }
                    lastOrient = pc.orientation;
                }
                // Add clue text to clue list
                ((Excel.Range)eWks.Cells[r, firstClueCol]).Value = pc.placeNumber;
                ((Excel.Range)eWks.Cells[r, firstClueCol + 1]).Value = pc.clueText;
                ((Excel.Range)eWks.Cells[r, firstClueCol]).Rows.RowHeight = 20;
                r++;

                // Alter grid
                switch (style)
                {
                    case OutputStyle.EmptyGrid:
                        // White-out clue area
                        int x2 = pc.x; int y2 = pc.y;
                        switch (pc.orientation)
                        {
                            case AD.Down:
                                y2 += pc.clue.length - 1;
                                break;
                            case AD.Across:
                                x2 += pc.clue.length - 1;
                                break;
                        }
                        ((Excel.Range)eWks.Range[eWks.Cells[pc.y + firstPuzzleRow, pc.x + firstPuzzleCol], eWks.Cells[y2 + firstPuzzleRow, x2 + firstPuzzleCol]]).Interior.Color = Color.White.ToOle();
                        // Number clue
                        ((Excel.Range)eWks.Cells[pc.y + firstPuzzleRow, pc.x + firstPuzzleCol]).Value = pc.placeNumber;
                        break;
                    case OutputStyle.GridWithAnswers:
                        // Enter answer into grid
                        switch (pc.orientation)
                        {
                            case AD.Down:
                                for (int yy = 0; yy < pc.clue.length; yy++)
                                {
                                    ((Excel.Range)eWks.Range[eWks.Cells[pc.y + yy + firstPuzzleRow, pc.x + firstPuzzleCol], eWks.Cells[pc.y + yy + firstPuzzleRow, pc.x + firstPuzzleCol]]).Value = pc.clue.letters[yy].ToString();
                                    ((Excel.Range)eWks.Range[eWks.Cells[pc.y + yy + firstPuzzleRow, pc.x + firstPuzzleCol], eWks.Cells[pc.y + yy + firstPuzzleRow, pc.x + firstPuzzleCol]]).Interior.Color = Color.White.ToOle();
                                }
                                break;
                            case AD.Across:
                                for (int xx = 0; xx < pc.clue.length; xx++)
                                {

                                    ((Excel.Range)eWks.Range[eWks.Cells[pc.y + firstPuzzleRow, pc.x + xx + firstPuzzleCol], eWks.Cells[pc.y + firstPuzzleRow, pc.x + xx + firstPuzzleCol]]).Value = pc.clue.letters[xx].ToString();
                                    ((Excel.Range)eWks.Range[eWks.Cells[pc.y + firstPuzzleRow, pc.x + xx + firstPuzzleCol], eWks.Cells[pc.y + firstPuzzleRow, pc.x + xx + firstPuzzleCol]]).Interior.Color = Color.White.ToOle();
                                }
                                break;
                        }
                        break;
                }

            }
            ((Excel.Range)eWks.Range[eWks.Cells[1, 1], eWks.Cells[r, 2]]).Columns.AutoFit();
            Excel.PageSetup ps = eWks.PageSetup;
            ps.PrintArea = "A1:" + (firstPuzzleCol + this.crossword.cols - 1).ToExcelCol() + r.ToString();
            ps.Orientation = Excel.XlPageOrientation.xlLandscape;
            ps.LeftMargin = eApp.InchesToPoints(0.25);
            ps.RightMargin = eApp.InchesToPoints(0.25);
            ps.TopMargin = eApp.InchesToPoints(0.75);
            ps.BottomMargin = eApp.InchesToPoints(0.75);
            ps.HeaderMargin = eApp.InchesToPoints(0.3);
            ps.FooterMargin = eApp.InchesToPoints(0.3);
            ps.FitToPagesTall = 1;
            ps.FitToPagesWide = 1;
        }

        private void MakeHtml(OutputStyle style = OutputStyle.EmptyGrid)
        {
            if (crossword.title == null)
            {
                ShowSettings();
            }

            //int firstClueCol = 1;
            //int firstClueRow = 4;
            int firstPuzzleCol = 0;
            int firstPuzzleRow = 0;

            int cellDimension = 24;

            Matrix<HtmlTag> cells = new Matrix<HtmlTag>();

            //int r = 0;

            /**
             * Styles
             */
            // Title
            Dictionary<HtmlTextWriterAttribute, String> titleAttr = new Dictionary<HtmlTextWriterAttribute, string>();
            titleAttr.Add(HtmlTextWriterAttribute.Class, "titlestyle");
            Dictionary<HtmlTextWriterStyle, String> titleStyle = new Dictionary<HtmlTextWriterStyle, string>();
            titleStyle.Add(HtmlTextWriterStyle.FontSize, "24px");
            titleStyle.Add(HtmlTextWriterStyle.FontWeight, "bold");
            titleStyle.Add(HtmlTextWriterStyle.TextAlign, "center");
            titleStyle.Add(HtmlTextWriterStyle.VerticalAlign, "middle");
            titleStyle.Add(HtmlTextWriterStyle.Margin, "auto");

            // Black squares
            Dictionary<HtmlTextWriterAttribute, String> blackAttr = new Dictionary<HtmlTextWriterAttribute, string>();
            blackAttr.Add(HtmlTextWriterAttribute.Class, "blacksquare");
            Dictionary<HtmlTextWriterStyle, String> blackStyle = new Dictionary<HtmlTextWriterStyle, string>();
            blackStyle.Add(HtmlTextWriterStyle.TextAlign, "center");
            blackStyle.Add(HtmlTextWriterStyle.VerticalAlign, "middle");
            blackStyle.Add(HtmlTextWriterStyle.BackgroundColor, "#000");
            blackStyle.Add(HtmlTextWriterStyle.Color, "#000");
            blackStyle.Add(HtmlTextWriterStyle.BorderColor, "#000");
            blackStyle.Add(HtmlTextWriterStyle.BorderWidth, "1px");
            blackStyle.Add(HtmlTextWriterStyle.BorderStyle, "solid");
            blackStyle.Add(HtmlTextWriterStyle.Width, cellDimension.ToString() + "px");
            blackStyle.Add(HtmlTextWriterStyle.Height, cellDimension.ToString() + "px");

            Dictionary<HtmlTextWriterAttribute, String> letterAttr = new Dictionary<HtmlTextWriterAttribute, string>();
            letterAttr.Add(HtmlTextWriterAttribute.Class, "lettersquare");
            Dictionary<HtmlTextWriterStyle, String> letterStyle = new Dictionary<HtmlTextWriterStyle, string>();
            letterStyle.Add(HtmlTextWriterStyle.TextAlign, "center");
            letterStyle.Add(HtmlTextWriterStyle.VerticalAlign, "middle");
            letterStyle.Add(HtmlTextWriterStyle.BackgroundColor, "#fff");
            letterStyle.Add(HtmlTextWriterStyle.Color, "#000");
            letterStyle.Add(HtmlTextWriterStyle.FontSize, "18px");
            letterStyle.Add(HtmlTextWriterStyle.BorderColor, "#000");
            letterStyle.Add(HtmlTextWriterStyle.BorderWidth, "1px");
            letterStyle.Add(HtmlTextWriterStyle.BorderStyle, "solid");
            letterStyle.Add(HtmlTextWriterStyle.Width, cellDimension.ToString() + "px");
            letterStyle.Add(HtmlTextWriterStyle.Height, cellDimension.ToString() + "px");

            Dictionary<HtmlTextWriterAttribute, String> numberAttr = new Dictionary<HtmlTextWriterAttribute, string>();
            numberAttr.Add(HtmlTextWriterAttribute.Class, "numbersquare");
            Dictionary<HtmlTextWriterStyle, String> numberStyle = new Dictionary<HtmlTextWriterStyle, string>();
            numberStyle.Add(HtmlTextWriterStyle.TextAlign, "left");
            numberStyle.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            numberStyle.Add(HtmlTextWriterStyle.BackgroundColor, "#fff");
            numberStyle.Add(HtmlTextWriterStyle.Color, "#000");
            numberStyle.Add(HtmlTextWriterStyle.FontSize, "10px");
            numberStyle.Add(HtmlTextWriterStyle.BorderColor, "#000");
            numberStyle.Add(HtmlTextWriterStyle.BorderWidth, "1px");
            numberStyle.Add(HtmlTextWriterStyle.BorderStyle, "solid");
            numberStyle.Add(HtmlTextWriterStyle.Width, cellDimension.ToString() + "px");
            numberStyle.Add(HtmlTextWriterStyle.Height, cellDimension.ToString() + "px");


            // Create title
            HtmlTag title = new HtmlTag(HtmlTextWriterTag.H1, crossword.title, titleAttr, titleStyle);

            // Puzzle setup (black fill, square cells, borders)
            for (int rr = firstPuzzleRow; rr < (firstPuzzleRow + crossword.rows); rr++)
            {
                for (int cc = firstPuzzleCol; cc < (firstPuzzleCol + crossword.cols); cc++)
                {
                    cells[cc, rr] = new HtmlTag(HtmlTextWriterTag.Td, "", blackAttr, blackStyle);
                }
            }


            // Clue table setup
            Dictionary<AD, List<HtmlTag>> clues = new Dictionary<AD, List<HtmlTag>>();
            clues.Add(AD.Across, new List<HtmlTag>());
            clues[AD.Across].Add(new HtmlTag(HtmlTextWriterTag.Td, "Clues across", titleAttr, titleStyle));
            clues.Add(AD.Down, new List<HtmlTag>());
            clues[AD.Down].Add(new HtmlTag(HtmlTextWriterTag.Td, "Clues down", titleAttr, titleStyle));

            // Clues
            //r = firstClueRow;
            if (true)
            {
                foreach (PlacedClue pc in this.crossword.sortedClueList)
                {
                    clues[pc.orientation].Add(new HtmlTag(HtmlTextWriterTag.Td, pc.placeNumber + " " + pc.clueText));
                    // Add clue text to clue list
                    //r++;

                    // Alter grid
                    switch (style)
                    {
                        case OutputStyle.EmptyGrid:
                            // White-out clue area
                            int x2 = pc.x; int y2 = pc.y;
                            switch (pc.orientation)
                            {
                                case AD.Down:
                                    for (int yy = 0; yy < pc.clue.length; yy++)
                                    {
                                        cells[pc.x + firstPuzzleCol, pc.y + yy + firstPuzzleRow] = new HtmlTag(HtmlTextWriterTag.Td, "", letterAttr, letterStyle);
                                    }
                                    y2 += pc.clue.length - 1;
                                    break;
                                case AD.Across:
                                    for (int xx = 0; xx < pc.clue.length; xx++)
                                    {
                                        cells[pc.x + xx + firstPuzzleCol, pc.y + firstPuzzleRow] = new HtmlTag(HtmlTextWriterTag.Td, "", letterAttr, letterStyle);
                                    }
                                    x2 += pc.clue.length - 1;
                                    break;
                            }
                            // Number clue
                            cells[pc.x + firstPuzzleCol, pc.y + firstPuzzleRow] = new HtmlTag(HtmlTextWriterTag.Td, pc.placeNumber.ToString(), numberAttr, numberStyle);
                            break;
                        case OutputStyle.GridWithAnswers:
                            // Enter answer into grid
                            switch (pc.orientation)
                            {
                                case AD.Down:
                                    for (int yy = 0; yy < pc.clue.length; yy++)
                                    {
                                        cells[pc.x + firstPuzzleCol, pc.y + yy + firstPuzzleRow] = new HtmlTag(HtmlTextWriterTag.Td, pc.clue.letters[yy].ToString(), letterAttr, letterStyle);
                                    }
                                    break;
                                case AD.Across:
                                    for (int xx = 0; xx < pc.clue.length; xx++)
                                    {
                                        cells[pc.x + xx + firstPuzzleCol, pc.y + firstPuzzleRow] = new HtmlTag(HtmlTextWriterTag.Td, pc.clue.letters[xx].ToString(), letterAttr, letterStyle);
                                    }
                                    break;
                            }
                            break;
                    }
                }
            }
            /* Start new code */
            #region "Tag writing"
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                // Html
                writer.RenderBeginTag(HtmlTextWriterTag.Html); // html
                writer.RenderBeginTag(HtmlTextWriterTag.Head); // head
                writer.RenderEndTag(); // head
                writer.RenderBeginTag(HtmlTextWriterTag.Body); // body

                // Title
                writer.RenderWholeTag(title); // h1

                // Grid table
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderCollapse, "collapse");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "auto");
                writer.RenderBeginTag(HtmlTextWriterTag.Table); // table
                for (int rr = 0; rr < cells.rows; rr++)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr); // tr
                    for (int cc = 0; cc < cells.cols; cc++)
                    {
                        try
                        {
                            writer.RenderWholeTag(cells[cc, rr]);
                        }
                        catch
                        {
                            // Ignore
                        }
                    }
                    writer.RenderEndTag(); // tr
                }
                writer.RenderEndTag(); // table

                // Clues table
                writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "auto");
                writer.RenderBeginTag(HtmlTextWriterTag.Table); // table
                int clueTableRows = Math.Max(clues[AD.Across].Count - 1, clues[AD.Down].Count - 1);
                for (int rr = 0; rr < clueTableRows; rr++)
                {
                    // Across
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr); // tr
                    if (clues[AD.Across].Count > rr)
                    {
                        writer.RenderWholeTag(clues[AD.Across][rr]); // td
                    }
                    else
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Td); // td
                        writer.RenderEndTag(); // td
                    }
                    // Down
                    if (clues[AD.Down].Count > rr)
                    {
                        writer.RenderWholeTag(clues[AD.Down][rr]); // td
                    }
                    else
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Td); // td
                        writer.RenderEndTag(); // td
                    }
                    writer.RenderEndTag(); // tr
                }
                writer.RenderEndTag(); // table

                writer.RenderEndTag(); // body
                writer.RenderEndTag(); // html
            }

            // Write the result.
            String fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".html";
            System.IO.File.WriteAllText(fileName, stringWriter.ToString());
            System.Diagnostics.Process.Start(fileName);
            #endregion "Tag writing"
            /* End new code */

        }

        private void ShowSettings()
        {
            CrosswordSettings cs = new CrosswordSettings(this);
            cs.ShowDialog();
            DialogResult dr = cs.result;
            if (dr == DialogResult.OK)
            {
                unsavedChanges = true;
                PopulateForm();
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private bool LoadDictionaries() //List<String> builtInFileNames = null, List<String> customFileNames = null)
        {
            String dictionaryPath = currentSettings.DefaultFolder;
            SerializableDictionary<DictType, List<String>> dictFiles = currentSettings.DictionaryFiles;
            if (dictFiles.RetrieveIfKeyExists(DictType.Default, new List<String>()).Count == 0)
            {
                dictFiles.AddIfNotExists(DictType.Default, new List<String>());
                String defaultDictPath = Path.Combine(dictionaryPath, "default.nev.dic");
                if (File.Exists(defaultDictPath))
                {
                    dictFiles[DictType.Default].Add(defaultDictPath);
                    currentSettings.DictionaryFiles = dictFiles;
                    currentSettings.Save();
                }
            }
            if (dictFiles.RetrieveIfKeyExists(DictType.Custom, new List<String>()).Count == 0)
            {
                dictFiles.AddIfNotExists(DictType.Custom, new List<String>());
                String customDictPath = Path.Combine(dictionaryPath, "custom.nev.dic");
                if (File.Exists(customDictPath))
                {
                    dictFiles[DictType.Custom].Add(customDictPath);
                    currentSettings.DictionaryFiles = dictFiles;
                    currentSettings.Save();
                }
            }
            // Built-in
            if (dictFiles[DictType.Default].Count == 0)
            {
                CrosswordDictionary cd = new CrosswordDictionary(Path.Combine(dictionaryPath, "default.nev.dic"));
                cd.Save();
                AllDictionaries.Add(DictType.Default, new List<CrosswordDictionary> { cd });
            }
            else
            {
                AllDictionaries.Add(DictType.Default, new List<CrosswordDictionary>());
                CrosswordDictionary cdTmp;
                foreach (String fileName in dictFiles[DictType.Default])
                {
                    String ext = Path.GetExtension(fileName);
                    switch (ext)
                    {
                        case ".dic":
                            cdTmp = null;
                            cdTmp = CrosswordDictionary.Load(fileName, DictFileType.XML);
                            AllDictionaries[DictType.Default].Add(cdTmp);
                            break;
                        default:
                            cdTmp = null;
                            cdTmp = CrosswordDictionary.Load(fileName, DictFileType.Plaintext);
                            AllDictionaries[DictType.Default].Add(cdTmp);
                            break;
                    }

                }
            }
            // Custom
            if (dictFiles[DictType.Custom].Count == 0)
            {
                CrosswordDictionary cd = new CrosswordDictionary(Path.Combine(dictionaryPath, "custom.nev.dic"));
                cd.Save();
                AllDictionaries.Add(DictType.Custom, new List<CrosswordDictionary> { cd });
            }
            else
            {
                AllDictionaries.Add(DictType.Custom, new List<CrosswordDictionary>());
                foreach (String fileName in dictFiles[DictType.Custom])
                {
                    String ext = Path.GetExtension(fileName);
                    switch (ext)
                    {
                        case ".dic":
                            AllDictionaries[DictType.Custom].Add(CrosswordDictionary.Load(fileName, DictFileType.XML));
                            break;
                        default:
                            AllDictionaries[DictType.Custom].Add(CrosswordDictionary.Load(fileName, DictFileType.Plaintext));
                            break;
                    }

                }
            }

            return true;
        }

        private void quickImportDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = dlgDictOpen.ShowDialog();
            if (dr == DialogResult.OK)
            {
                switch (Path.GetExtension(dlgDictOpen.FileName))
                {
                    case ".dic":
                        AllDictionaries[DictType.Custom].Add(CrosswordDictionary.Load(dlgDictOpen.FileName, DictFileType.XML));

                        currentSettings.AddDictionaryFile(DictType.Custom, dlgDictOpen.FileName);
                        currentSettings.Save();
                        break;
                    default:
                        AllDictionaries[DictType.Custom].Add(CrosswordDictionary.Load(dlgDictOpen.FileName, DictFileType.Plaintext));
                        dlgDictOpen.FileName += CrosswordDictionary.importSuffix;
                        currentSettings.AddDictionaryFile(DictType.Custom, dlgDictOpen.FileName);
                        currentSettings.Save();
                        break;
                }
            }
        }

        private void mruManagerFiles_RecentFileMenuItemClick(string file)
        {
            open(file);
        }

        private void Creator_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentSettings != null) { currentSettings.Save(); }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
            about.Dispose();
        }

        private void dictionaryManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DictionaryManagement dm = new DictionaryManagement(this);
            dm.ShowDialog();
            dm.Dispose();
        }

        private void addToDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if the clue is valid to be added
            int row = dgvClues.SelectedCells[0].RowIndex;
            PlacedClue pc = ((PlacedClue)dgvClues.Rows[row].DataBoundItem);
            String word = pc.clue.answer;
            if ((word == null) | (word.Length == 0))
            {
                MessageBox.Show("This clue is blank, and cannot be added.");
                return;
            }
            if (word.IndexOf("?") > -1)
            {
                MessageBox.Show("This clue still contains blanks, so cannot be added.");
                return;
            }

            // Retrieve user dictionary (1st custom)
            CrosswordDictionary cd = AllDictionaries[DictType.Custom][0];

            // See if there is a matching entry already
            if (cd.entries.ContainsKey(word))
            {
                // See if there's a definition already
                if (cd.entries[word].Count == 0)
                {
                    cd.entries[word].Add(pc.clue.question);
                }
                else
                {
                    DictionaryClueList dclPrompt = new DictionaryClueList();
                    dclPrompt.ClueChoice = new List<Clue>();
                    foreach (String clueOption in cd.entries[word])
                    {
                        dclPrompt.ClueChoice.Add(new Clue(clueOption, word));
                    }
                    dclPrompt.Populate();
                    dclPrompt.ShowDialog();
                    if (dclPrompt.CreateNew.HasValue)
                    {
                        if (dclPrompt.CreateNew.Value == true)
                        {
                            // Add new clue entry to dictionary
                            cd.entries[word].Add(pc.clue.question);
                        }
                        else
                        {
                            // Update the selected clue
                            for (int i = 0; i < cd.entries[word].Count; i++)
                            {
                                if (cd.entries[word][i] == dclPrompt.SelectedClue.question) { cd.entries[word][i] = pc.clue.question; }
                            }
                        }
                        cd.Save();
                    }
                    else
                    {
                        // They cancelled out
                        return;
                    }
                }
            }
            else
            {
                // Just add it!
                cd.entries.Add(word, new List<String>() { pc.clue.question });
            }
            cd.Save();
            tsslMessage.Text = String.Format("Saved clue for {0}", word);

            timerMessageReset.Enabled = false;
            timerMessageReset.Interval = 5000;
            timerMessageReset.Enabled = true;
        }

        private void timerMessageReset_Tick(object sender, EventArgs e)
        {
            tsslMessage.Text = "";
        }

        private void runClueCheck()
        {
            __cluesToCheck = true;
            if (!bwDictionaryChecker.IsBusy)
            {
                bwDictionaryChecker.RunWorkerAsync();
            }
        }
        private void bwDictionaryChecker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (__cluesToCheck) // Stays in loop so long as we keep changing stuff in the UI
            {
                __cluesToCheck = false;
                bwDictionaryChecker.ReportProgress(0);
                int cluesChecked = 0;
                // Loop through each clue, seeing if it's solvable
                for (var ii = 0; ii < crossword.placedClues.Count; ii++)
                //foreach (PlacedClue pc in crossword.placedClues)
                {
                    PlacedClue pc = null;
                    if (ii < crossword.placedClues.Count)
                    {
                        pc = crossword.placedClues[ii];
                    }
                    // Ignore unchanged clues
                    if (!pc.uncheckedChanges) { continue; }

                    // Ignore blank answers
                    String word = pc.clue.answer;
                    if ((word == null) | (word.Length == 0)) { continue; }

                    //if (word.IndexOf("?") == -1) { pc.status = ClueStatus.MatchingWordNoQuestion; continue; }

                    // Change pattern as needed, depending on intersection with other clues
                    LetterSubstitutionSet lss = LetterSubstitutionSet.getIntersectionChanges(pc, crossword.placedClues);
                    if (lss.definiteChanges.Count > 0)
                    {
                        foreach (LetterSubstitution ls in lss.definiteChanges)
                        {
                            int j = 0;
                            for (int i = 0; i < ls.position; i++)
                            {
                                while (pc.clue.answer[i + j] == ' ') { j++; }
                            }
                            StringBuilder tmp = new StringBuilder(pc.clue.answer);
                            tmp[ls.position + j] = Convert.ToChar(ls.newValue);
                            pc.clue.answer = tmp.ToString();
                        }
                        //if (ClueChanged != null) { ClueChanged("", new EventArgs()); }
                        this.Invoke(delegate
                        {
                            ClueChanged("", new EventArgs());
                        }
                        );
                    }

                    // Create regular expression
                    Regex reWord = new Regex(
                        "^"
                        + Regex.Replace(
                            pc.clue.answer.Replace("?", ".")
                            , "."
                            , "$0[" + Clue.NonCountingChars_Regex + "]*"
                        )
                        + "$"
                    , RegexOptions.IgnoreCase);

                    // Clear any existing matches
                    pc.clearMatches();

                    // Loop through dictionaries, looking for matches
                    for (DictType dt = DictType.Default; dt < DictType.Remote; dt++)
                    {
                        List<CrosswordDictionary> dicts = AllDictionaries[dt];
                        foreach (CrosswordDictionary dict in dicts)
                        {
                            List<KeyValuePair<String, List<String>>> possibles = (from KeyValuePair<String, List<String>> kvp in dict.entries
                                                                                  where reWord.IsMatch(kvp.Key)
                                                                                  select kvp).ToList();
                            foreach (KeyValuePair<String, List<String>> kvp in possibles)
                            {
                                if (kvp.Value == null || kvp.Value.Count == 0)
                                {
                                    // Word without definition
                                    pc.addMatch(kvp.Key);
                                }
                                else
                                {
                                    // Add word with multiple definitions
                                    foreach (String question in kvp.Value)
                                    {
                                        pc.addMatch(kvp.Key, question);
                                    }
                                }
                            }
                        }
                    }
                    pc.changesChecked(); // Notify clue that all changes checked
                    bwDictionaryChecker.ReportProgress((int)Math.Round((Decimal)cluesChecked / (Decimal)crossword.placedClues.Count));
                }
                bwDictionaryChecker.ReportProgress(100);
            }
        }

        private void regularExpressionSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegexSearcher re = new RegexSearcher(this);
            re.Show();
        }

        private void emptyGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeExcel(OutputStyle.EmptyGrid);
        }

        private void gridWithAnswersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeExcel(OutputStyle.GridWithAnswers);
        }

        private void emptyGridToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MakeHtml(OutputStyle.EmptyGrid);
        }

        private void gridWithAnswersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MakeHtml(OutputStyle.GridWithAnswers);
        }

        private void Creator_Load(object sender, EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                this.Text += " " + Creator.VersionText();
            }
            else
            {
                this.Text += " " + Creator.VersionText();
            }
        }

        public static String VersionText()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                return String.Format("v{0}", ApplicationDeployment.CurrentDeployment.CurrentVersion);
            }
            else
            {
                return String.Format("static version {0}", Assembly.GetExecutingAssembly().GetName().Version);
            }
        }
    }
}
