using CrosswordControls;
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
using System.Windows.Forms;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;
using Serial = System.Xml.Serialization;
using WebUI = System.Web.UI;

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
        private int __mouseRow = 0;
        private int __mouseCol = 0;

        private List<CrosswordControls.ClueDisplay> __clueDisplays = new List<CrosswordControls.ClueDisplay>();

        public Settings currentSettings = new Settings();
        public WordSources currentWordSources = new WordSources();

        public CrosswordDictionaryCollection AllDictionaries = new CrosswordDictionaryCollection();

        /*private CrosswordDictionary DefaultWords = new CrosswordDictionary();
        private CrosswordDictionary CustomWords = new CrosswordDictionary();*/

        public Creator()
        {
            InitializeComponent();

            // Optimize painting to avoid flicker
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
#if (DEBUG == false)
            dlgOpen.InitialDirectory = Application.StartupPath;
            dlgSave.InitialDirectory = Application.StartupPath;
#endif

            // Get settings
            currentSettings = Settings.Load();
            currentSettings.FileListChanged += RepopulateRecentFiles; // Redo menu if list of recent files changes
            currentSettings.Set("Version", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            currentSettings.Save();

            // Auto-saving etc.
            keepAutoBackupToolStripMenuItem.Checked = currentSettings.AutoSaveOnTimer;

            // Get word sources
            currentWordSources = WordSources.Load();
            currentWordSources.Save();

            // Move any dictionary entries from Settings to WordSources file
            if ((currentSettings.DictionaryFiles != null) && (currentSettings.DictionaryFiles.Keys.Count > 0))
            {
                foreach (DictType keyType in currentSettings.DictionaryFiles.Keys)
                {
                    foreach (String dictFile in currentSettings.DictionaryFiles[keyType])
                    {
                        if (!currentWordSources.Dictionaries.Contains(dictFile))
                        {
                            currentWordSources.Dictionaries.Add(dictFile);
                        }
                    }
                }
                currentWordSources.Save();
                currentSettings.DictionaryFiles.Clear();
                currentSettings.DictionaryFiles = new SerializableDictionary<DictType, List<String>>();
                currentSettings.Save();
            }

            // Repopulate Recent Files
            RepopulateRecentFiles();

            SetupGrid();
            LoadDictionaries();

            // Handle background worker updates
            bwDictionaryChecker.ProgressChanged += BwDictionaryChecker_ProgressChanged;
            this.ClueChanged += Creator_ClueChanged;
        }

        private void Creator_ClueChanged(object sender, PlacedClueChangedEventArgs e)
        {
            if ((e.clues != null) && (e.clues.Count > 0))
            {
                // Do any per-clue events here
                if (e.newlyCreated)
                {
                    foreach (PlacedClue pc in e.clues)
                    {
                        createClueDisplay(pc);
                    }
                }

                // Do any once-only events here
                crossword.refreshNumbers();
                reorderClueDisplays();
            }
            else
            {
                //UpdateClueGrid();
                populateClueDisplays();
            }
            UpdatePreviewGrid();
            runClueCheck();
        }

        private void BwDictionaryChecker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tspbClueUpdate.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100)
            {
                // Shouldn't now need to call anything - controls should update themselves as needed
                //UpdateClueGrid();
                //populateClueDisplays();
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
        public event EventHandler<PlacedClueChangedEventArgs> ClueChanged;

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
                                // Failsafe - shouldn't happen, but occasionally we get a clue trying to display out of bounds
                                if (xx >= crossword.cols) { continue; }
                                if (yy >= crossword.rows) { continue; }
                                // Now populate grid
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

        /// <summary>
        /// Sets up or resets the crossword preview grid
        /// </summary>
        public void SetupGrid()
        {
            // Set up columns and rows
            dgvPuzzle.ColumnCount = crossword.width;
            dgvPuzzle.RowCount = crossword.height;
            dgvPuzzle.ColumnHeadersVisible = false;
            dgvPuzzle.RowHeadersVisible = false;
            dgvPuzzle.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;

            SetBoxSize();

            // if (ClueChanged != null) { ClueChanged("", new EventArgs()); } // This just triggers a clue repaint, which should get handled elsewhere in code
            UpdatePreviewGrid();
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

        private void UpdatePreviewGrid()
        {
            this.Cursor = Cursors.WaitCursor;
            dgvPuzzle.SuspendLayout();
            ClueDisplay cd = GetSelectedClue();
            PlacedClue pc = null;
            if (cd == null)
            {
                pc = null;
            }
            else
            {
                pc = cd.Clue;
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
        /// <summary>
        /// Creates a new clue at the specified location
        /// </summary>
        /// <param name="x">The 1-based column from which the clue starts</param>
        /// <param name="y">The 1-based row from which the clue starts</param>
        /// <returns></returns>
        private PlacedClue getClue(int x, int y)
        {
            AD defaultOrientation = AD.Across;
            // Check if there's existing clues intersecting the coordinates
            List<PlacedClue> existing = FindCluesFromGridPos(x, y);
            if (existing.Count > 0)
            {
                if (existing.Count > 1)
                {
                    // Two intersecting clues - don't create another
                    MessageBox.Show("Cannot create another clue here - it already has two.");
                    return null;
                }
                else
                {
                    // One intersecting clue - default to opposite direction
                    if (existing[0].orientation == AD.Across) { defaultOrientation = AD.Down; }
                }
            }

            PlacedClue template = new PlacedClue();
            template.x = x; template.y = y;
            template.clue = new Clue(Clue.BlankQuestion, "?");
            template.orientation = defaultOrientation;
            ClueEntry ce = new ClueEntry(this, template);
            ce.ShowDialog();

            return ce.AcceptedClue;
        }

        private void reorderClueDisplays()
        {
            var allCDs = (from Control ctrl in flpClues.Controls
                          where ctrl is ClueDisplay
                          orderby (ctrl as ClueDisplay).Clue.orientation, (ctrl as ClueDisplay).Clue.order
                          select (ctrl as ClueDisplay)).ToArray();
            /*var allCDs = (from ClueDisplay cd in flpClues.Controls
                          orderby cd.Clue.orientation, cd.Clue.order
                          select cd).ToArray();*/
            /*var allPCs = (from PlacedClue pc in crossword.placedClues
                          orderby pc.orientation, pc.order
                          select pc).ToArray();*/
            for (var i = 0; i < allCDs.Count(); i++)
            {
                flpClues.Controls.SetChildIndex(allCDs[i], i);
            }
        }

        private ClueDisplay createClueDisplay(PlacedClue pcRef)
        {
            ClueDisplay cd = new ClueDisplay();
            cd.Height = 24;
            cd.BorderStyle = BorderStyle.FixedSingle;
            cd.Margin = new Padding(0);
            cd.Clue = pcRef;
            __clueDisplays.Add(cd);
            flpClues.Controls.Add(cd);
            cd.MouseClick += ClueDisplay_MouseClick;
            cd.DoubleClick += ClueDisplay_DoubleClick;
            return cd;
        }

        private void populateClueDisplays()
        {
            // Clear the panel
            flpClues.ClearAndDispose();

            //crossword.RefreshNumbers(); // Use this more wisely (sometimes instead of refreshing whole grid)

            // Populate new controls from the PlacedClues collection
            var clueList = (from PlacedClue pcLoop in crossword.placedClues
                            orderby pcLoop.orientation, pcLoop.order
                            select pcLoop);
            foreach (PlacedClue pcRef in clueList) // crossword.placedClues)
            {
                createClueDisplay(pcRef);
                /*ClueDisplay cd = new ClueDisplay();
                cd.Height = 24;
                cd.BorderStyle = BorderStyle.FixedSingle;
                cd.Margin = new Padding(0);
                cd.Clue = pcRef;
                __clueDisplays.Add(cd);
                flpClues.Controls.Add(cd);
                cd.MouseClick += ClueDisplay_MouseClick;
                cd.DoubleClick += ClueDisplay_DoubleClick;
                */
            }
        }

        /// <summary>
        /// Selects the requested clue from the clue list, and deselects all others, then scrolls it into view
        /// </summary>
        /// <param name="clueDisplay"></param>
        private void SelectOnly(ClueDisplay clueDisplay)
        {
            foreach (ClueDisplay cdLoop in flpClues.Controls)
            {
                cdLoop.Selected = false;
            }
            clueDisplay.Selected = true;
            flpClues.ScrollControlIntoView(clueDisplay);
            UpdatePreviewGrid();
        }

        private void ClueDisplay_DoubleClick(object sender, EventArgs e)
        {
            if (sender is ClueDisplay)
            {
                ClueDisplay cdClicked = (ClueDisplay)sender;
                SelectOnly(cdClicked);
                TryClueEdit();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void ClueDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is ClueDisplay)
            {
                ClueDisplay cdClicked = (ClueDisplay)sender;
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (cdClicked.Selected)
                        {
                            cdClicked.Selected = false;
                        }
                        else
                        {
                            SelectOnly(cdClicked);
                        }
                        break;
                    case MouseButtons.Right:
                        // Select right-clicked clue
                        SelectOnly(cdClicked);
                        cmsClueClick.Show(Cursor.Position);
                        break;
                    default:
                        // Ignore
                        break;
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

        /// <summary>
        /// Creates a new crossword, prompting first if the current crossword has unsaved changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            SetupGrid();
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
                runClueCheck(true);
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
                SetupGrid();
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
                SetupGrid();
                currentFilename = fileName;
            }
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            TryClueEdit();
        }

        /// <summary>
        /// Attempts to add a clue to the crossword
        /// </summary>
        /// <param name="x">The 1-based column from which the clue starts</param>
        /// <param name="y">The 1-based row from which the clue starts</param>
        private void TryClueAdd(int x = 1, int y = 1)
        {
            List<PlacedClue> added = new List<PlacedClue>();
            PlacedClue pc = getClue(x, y);
            // Check if we got a new clue or a cancellation
            if (pc != null)
            {
                crossword.placedClues.Add(pc);
                added.Add(pc);
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
                        added.Add(pc90);
                        crossword.placedClues.Add(pc180);
                        added.Add(pc180);
                        crossword.placedClues.Add(pc270);
                        added.Add(pc270);
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
                        added.Add(pcComplement);
                        break;
                    default:
                        // No need to reflect any clues
                        break;
                }
                UpdatePreviewGrid();
            }
            if (added.Count == 0) { added = null; }
            ClueChanged("", new PlacedClueChangedEventArgs(added, true));
        }

        private ClueDisplay GetSelectedClue()
        {
            // Get selected clue
            ClueDisplay cdSelected
                = (from Control ctrl in flpClues.Controls
                   where ((ctrl is ClueDisplay) && (((ClueDisplay)ctrl).Selected == true))
                   select (ClueDisplay)ctrl).FirstOrDefault();
            if (cdSelected == null)
            {
                return null;
            }
            else
            {
                return cdSelected;
            }
        }

        private void TryClueEdit()
        {
            // Get selected clue
            ClueDisplay cdSelected = GetSelectedClue();
            if (cdSelected == null)
            {
                MessageBox.Show("You must select a clue to edit", "Edit Clue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            PlacedClue pc = cdSelected.Clue;

            // pc is template, pcTmp is the edited clone
            PlacedClue pcTmp = getClue(pc);
            if (pcTmp != null)
            {
                // Copy to original PlacedClue entry, to update crossword object and clue list
                pcTmp.CopyTo(pc); // TODO - test this line for ClueDisplay usage (I'm not sure what this comment means any more)
                //var junk = crossword.sortedClueList; // TODO - test this line for ClueDisplay usage

                pc.status = ClueStatus.Unknown;
                pc.changesChecked(false);

                unsavedChanges = true;
                ClueChanged("", new PlacedClueChangedEventArgs(pc));
            }
            // Reorder ClueDisplay controls list - happens automatically on clue change
        }

        private void cmdRemoveClue_Click(object sender, EventArgs e)
        {
            ClueDisplay cdSelected = GetSelectedClue();
            if (cdSelected == null)
            {
                MessageBox.Show("You must select a clue to remove", "Remove Clue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            PlacedClue pc = cdSelected.Clue;

            DialogResult dr = MessageBox.Show(String.Format("Are you sure you want to remove this clue?{0}{0}{1}{0}{2}", Environment.NewLine, pc.clueText, pc.clue.answer), "Remove Clue", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }

            // Backwards loop, so removed clues don't affect indexing
            for (int i = crossword.placedClues.Count - 1; i >= 0; i--)
            {
                PlacedClue pcLoop = crossword.placedClues[i];
                if (pcLoop.UniqueID == pc.UniqueID)
                {
                    ClueChanged("", new PlacedClueChangedEventArgs(pcLoop));
                    crossword.placedClues.RemoveAt(i);
                }
            }
            UpdatePreviewGrid();
            unsavedChanges = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Selects the clue intersecting with the supplied co-ordinates. If there are two, the second is selected.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>True if an intersecting clue was found and selected, otherwise False</returns>
        private bool SelectClueFromGridPos(int x, int y)
        {
            foreach (PlacedClue pc in this.crossword.placedClues)
            {
                if (pc.Overlaps(x, y))
                {
                    foreach (Control ctrl in flpClues.Controls)
                    {
                        if (ctrl is ClueDisplay)
                        {
                            ClueDisplay cdLoop = (ClueDisplay)ctrl;
                            PlacedClue pc2 = cdLoop.Clue;
                            if (pc.UniqueID == pc2.UniqueID)
                            {
                                SelectOnly(cdLoop);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private List<PlacedClue> FindCluesFromGridPos(int x, int y)
        {
            List<PlacedClue> outList = new List<PlacedClue>();
            foreach (PlacedClue pc in this.crossword.placedClues)
            {
                if (pc.Overlaps(x, y))
                {
                    outList.Add(pc);
                }
            }
            return outList;
        }

        private void dgvPuzzle_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            __mouseCol = e.ColumnIndex;
            __mouseRow = e.RowIndex;
            dgvPuzzle[e.ColumnIndex, e.RowIndex].Style.Tag = dgvPuzzle[e.ColumnIndex, e.RowIndex].Style.BackColor;
            dgvPuzzle[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.FromKnownColor(KnownColor.Yellow);
        }

        private void dgvPuzzle_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            SelectClueFromGridPos(e.ColumnIndex, e.RowIndex);
        }

        private void dgvPuzzle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bool foundClue = SelectClueFromGridPos(e.ColumnIndex, e.RowIndex);
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
            Dictionary<WebUI.HtmlTextWriterAttribute, String> titleAttr = new Dictionary<WebUI.HtmlTextWriterAttribute, string>();
            titleAttr.Add(WebUI.HtmlTextWriterAttribute.Class, "titlestyle");
            Dictionary<WebUI.HtmlTextWriterStyle, String> titleStyle = new Dictionary<WebUI.HtmlTextWriterStyle, string>();
            titleStyle.Add(WebUI.HtmlTextWriterStyle.FontSize, "24px");
            titleStyle.Add(WebUI.HtmlTextWriterStyle.FontWeight, "bold");
            titleStyle.Add(WebUI.HtmlTextWriterStyle.TextAlign, "center");
            titleStyle.Add(WebUI.HtmlTextWriterStyle.VerticalAlign, "middle");
            titleStyle.Add(WebUI.HtmlTextWriterStyle.Margin, "auto");

            // Black squares
            Dictionary<WebUI.HtmlTextWriterAttribute, String> blackAttr = new Dictionary<WebUI.HtmlTextWriterAttribute, string>();
            blackAttr.Add(WebUI.HtmlTextWriterAttribute.Class, "blacksquare");
            Dictionary<WebUI.HtmlTextWriterStyle, String> blackStyle = new Dictionary<WebUI.HtmlTextWriterStyle, string>();
            blackStyle.Add(WebUI.HtmlTextWriterStyle.TextAlign, "center");
            blackStyle.Add(WebUI.HtmlTextWriterStyle.VerticalAlign, "middle");
            blackStyle.Add(WebUI.HtmlTextWriterStyle.BackgroundColor, "#000");
            blackStyle.Add(WebUI.HtmlTextWriterStyle.Color, "#000");
            blackStyle.Add(WebUI.HtmlTextWriterStyle.BorderColor, "#000");
            blackStyle.Add(WebUI.HtmlTextWriterStyle.BorderWidth, "1px");
            blackStyle.Add(WebUI.HtmlTextWriterStyle.BorderStyle, "solid");
            blackStyle.Add(WebUI.HtmlTextWriterStyle.Width, cellDimension.ToString() + "px");
            blackStyle.Add(WebUI.HtmlTextWriterStyle.Height, cellDimension.ToString() + "px");

            Dictionary<WebUI.HtmlTextWriterAttribute, String> letterAttr = new Dictionary<WebUI.HtmlTextWriterAttribute, string>();
            letterAttr.Add(WebUI.HtmlTextWriterAttribute.Class, "lettersquare");
            Dictionary<WebUI.HtmlTextWriterStyle, String> letterStyle = new Dictionary<WebUI.HtmlTextWriterStyle, string>();
            letterStyle.Add(WebUI.HtmlTextWriterStyle.TextAlign, "center");
            letterStyle.Add(WebUI.HtmlTextWriterStyle.VerticalAlign, "middle");
            letterStyle.Add(WebUI.HtmlTextWriterStyle.BackgroundColor, "#fff");
            letterStyle.Add(WebUI.HtmlTextWriterStyle.Color, "#000");
            letterStyle.Add(WebUI.HtmlTextWriterStyle.FontSize, "18px");
            letterStyle.Add(WebUI.HtmlTextWriterStyle.BorderColor, "#000");
            letterStyle.Add(WebUI.HtmlTextWriterStyle.BorderWidth, "1px");
            letterStyle.Add(WebUI.HtmlTextWriterStyle.BorderStyle, "solid");
            letterStyle.Add(WebUI.HtmlTextWriterStyle.Width, cellDimension.ToString() + "px");
            letterStyle.Add(WebUI.HtmlTextWriterStyle.Height, cellDimension.ToString() + "px");

            Dictionary<WebUI.HtmlTextWriterAttribute, String> numberAttr = new Dictionary<WebUI.HtmlTextWriterAttribute, string>();
            numberAttr.Add(WebUI.HtmlTextWriterAttribute.Class, "numbersquare");
            Dictionary<WebUI.HtmlTextWriterStyle, String> numberStyle = new Dictionary<WebUI.HtmlTextWriterStyle, string>();
            numberStyle.Add(WebUI.HtmlTextWriterStyle.TextAlign, "left");
            numberStyle.Add(WebUI.HtmlTextWriterStyle.VerticalAlign, "top");
            numberStyle.Add(WebUI.HtmlTextWriterStyle.BackgroundColor, "#fff");
            numberStyle.Add(WebUI.HtmlTextWriterStyle.Color, "#000");
            numberStyle.Add(WebUI.HtmlTextWriterStyle.FontSize, "10px");
            numberStyle.Add(WebUI.HtmlTextWriterStyle.BorderColor, "#000");
            numberStyle.Add(WebUI.HtmlTextWriterStyle.BorderWidth, "1px");
            numberStyle.Add(WebUI.HtmlTextWriterStyle.BorderStyle, "solid");
            numberStyle.Add(WebUI.HtmlTextWriterStyle.Width, cellDimension.ToString() + "px");
            numberStyle.Add(WebUI.HtmlTextWriterStyle.Height, cellDimension.ToString() + "px");


            // Create title
            HtmlTag title = new HtmlTag(WebUI.HtmlTextWriterTag.H1, crossword.title, titleAttr, titleStyle);

            // Puzzle setup (black fill, square cells, borders)
            for (int rr = firstPuzzleRow; rr < (firstPuzzleRow + crossword.rows); rr++)
            {
                for (int cc = firstPuzzleCol; cc < (firstPuzzleCol + crossword.cols); cc++)
                {
                    cells[cc, rr] = new HtmlTag(WebUI.HtmlTextWriterTag.Td, "", blackAttr, blackStyle);
                }
            }


            // Clue table setup
            Dictionary<AD, List<HtmlTag>> clues = new Dictionary<AD, List<HtmlTag>>();
            clues.Add(AD.Across, new List<HtmlTag>());
            clues[AD.Across].Add(new HtmlTag(WebUI.HtmlTextWriterTag.Td, "Clues across", titleAttr, titleStyle));
            clues.Add(AD.Down, new List<HtmlTag>());
            clues[AD.Down].Add(new HtmlTag(WebUI.HtmlTextWriterTag.Td, "Clues down", titleAttr, titleStyle));

            // Clues
            //r = firstClueRow;
            if (true)
            {
                foreach (PlacedClue pc in this.crossword.sortedClueList)
                {
                    clues[pc.orientation].Add(new HtmlTag(WebUI.HtmlTextWriterTag.Td, pc.placeNumber + " " + pc.clueText));
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
                                        cells[pc.x + firstPuzzleCol, pc.y + yy + firstPuzzleRow] = new HtmlTag(WebUI.HtmlTextWriterTag.Td, "", letterAttr, letterStyle);
                                    }
                                    y2 += pc.clue.length - 1;
                                    break;
                                case AD.Across:
                                    for (int xx = 0; xx < pc.clue.length; xx++)
                                    {
                                        cells[pc.x + xx + firstPuzzleCol, pc.y + firstPuzzleRow] = new HtmlTag(WebUI.HtmlTextWriterTag.Td, "", letterAttr, letterStyle);
                                    }
                                    x2 += pc.clue.length - 1;
                                    break;
                            }
                            // Number clue
                            cells[pc.x + firstPuzzleCol, pc.y + firstPuzzleRow] = new HtmlTag(WebUI.HtmlTextWriterTag.Td, pc.placeNumber.ToString(), numberAttr, numberStyle);
                            break;
                        case OutputStyle.GridWithAnswers:
                            // Enter answer into grid
                            switch (pc.orientation)
                            {
                                case AD.Down:
                                    for (int yy = 0; yy < pc.clue.length; yy++)
                                    {
                                        cells[pc.x + firstPuzzleCol, pc.y + yy + firstPuzzleRow] = new HtmlTag(WebUI.HtmlTextWriterTag.Td, pc.clue.letters[yy].ToString(), letterAttr, letterStyle);
                                    }
                                    break;
                                case AD.Across:
                                    for (int xx = 0; xx < pc.clue.length; xx++)
                                    {
                                        cells[pc.x + xx + firstPuzzleCol, pc.y + firstPuzzleRow] = new HtmlTag(WebUI.HtmlTextWriterTag.Td, pc.clue.letters[xx].ToString(), letterAttr, letterStyle);
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

            using (WebUI.HtmlTextWriter writer = new WebUI.HtmlTextWriter(stringWriter))
            {
                // Html
                writer.RenderBeginTag(WebUI.HtmlTextWriterTag.Html); // html
                writer.RenderBeginTag(WebUI.HtmlTextWriterTag.Head); // head
                writer.RenderEndTag(); // head
                writer.RenderBeginTag(WebUI.HtmlTextWriterTag.Body); // body

                // Title
                writer.RenderWholeTag(title); // h1

                // Grid table
                writer.AddStyleAttribute(WebUI.HtmlTextWriterStyle.BorderCollapse, "collapse");
                writer.AddStyleAttribute(WebUI.HtmlTextWriterStyle.Margin, "auto");
                writer.RenderBeginTag(WebUI.HtmlTextWriterTag.Table); // table
                for (int rr = 0; rr < cells.rows; rr++)
                {
                    writer.RenderBeginTag(WebUI.HtmlTextWriterTag.Tr); // tr
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
                writer.AddStyleAttribute(WebUI.HtmlTextWriterStyle.Margin, "auto");
                writer.RenderBeginTag(WebUI.HtmlTextWriterTag.Table); // table
                int clueTableRows = Math.Max(clues[AD.Across].Count - 1, clues[AD.Down].Count - 1);
                for (int rr = 0; rr < clueTableRows; rr++)
                {
                    // Across
                    writer.RenderBeginTag(WebUI.HtmlTextWriterTag.Tr); // tr
                    if (clues[AD.Across].Count > rr)
                    {
                        writer.RenderWholeTag(clues[AD.Across][rr]); // td
                    }
                    else
                    {
                        writer.RenderBeginTag(WebUI.HtmlTextWriterTag.Td); // td
                        writer.RenderEndTag(); // td
                    }
                    // Down
                    if (clues[AD.Down].Count > rr)
                    {
                        writer.RenderWholeTag(clues[AD.Down][rr]); // td
                    }
                    else
                    {
                        writer.RenderBeginTag(WebUI.HtmlTextWriterTag.Td); // td
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
                SetupGrid();
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private bool LoadDictionaries() //List<String> builtInFileNames = null, List<String> customFileNames = null)
        {
            // TODO - check conversion this code to WordSources (mostly done if not 100%)
            String dictionaryPath = WordSources.DefaultFolder;
            List<String> allDictFiles = currentWordSources.Dictionaries;
            // Default dictionary if not set
            if (allDictFiles.Count == 0)
            {
                String defaultDictPath = Path.Combine(dictionaryPath, "default.nev.dic");
                if (File.Exists(defaultDictPath))
                {
                    allDictFiles.Add(defaultDictPath);
                    currentWordSources.Dictionaries = allDictFiles;
                    currentWordSources.Save();
                }
            }
            /*
            if (allDictFiles.RetrieveIfKeyExists(DictType.Custom, new List<String>()).Count == 0)
            {
                allDictFiles.AddIfNotExists(DictType.Custom, new List<String>());
                String customDictPath = Path.Combine(dictionaryPath, "custom.nev.dic");
                if (File.Exists(customDictPath))
                {
                    allDictFiles[DictType.Custom].Add(customDictPath);
                    currentSettings.DictionaryFiles = allDictFiles;
                    currentSettings.Save();
                }
            }
            */
            // Built-in
            if (allDictFiles.Count == 0)
            {
                CrosswordDictionary cd = new CrosswordDictionary(Path.Combine(dictionaryPath, "default.nev.dic"));
                cd.Save();
                AllDictionaries.Add(DictType.Default, new List<IWordSource> { cd });
            }
            else
            {
                CrosswordDictionary cdTmp;
                WordList wlTmp;

                if (!AllDictionaries.ContainsKey(DictType.Default)) { AllDictionaries.Add(DictType.Default, new List<IWordSource>()); }

                foreach (String fileName in allDictFiles)
                {
                    String ext = Path.GetExtension(fileName);
                    switch (ext)
                    {
                        case ".dic":
                            cdTmp = null;
                            cdTmp = CrosswordDictionary.Load(fileName, DictFileType.XML);
                            AllDictionaries[DictType.Default].Add(cdTmp);
                            break;
                        case ".txt":
                            wlTmp = new WordList();
                            wlTmp.LoadFromFile(fileName);
                            AllDictionaries[DictType.Default].Add(wlTmp);
                            break;
                        default:
                            wlTmp = new WordList();
                            wlTmp.LoadFromFile(fileName);
                            AllDictionaries[DictType.Default].Add(wlTmp);
                            break;
                    }

                }
            }

            return true;
        }

        private void quickImportDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Convert this code to WordSources
            DialogResult dr = dlgDictOpen.ShowDialog();
            if (dr == DialogResult.OK)
            {
                switch (Path.GetExtension(dlgDictOpen.FileName))
                {
                    case ".dic":
                        AllDictionaries[DictType.Custom].Add(CrosswordDictionary.Load(dlgDictOpen.FileName, DictFileType.XML));

                        //currentSettings.AddDictionaryFile(DictType.Custom, dlgDictOpen.FileName);
                        currentSettings.DictionaryFiles[DictType.Custom].Add(dlgDictOpen.FileName);
                        currentSettings.Save();
                        break;
                    default:
                        AllDictionaries[DictType.Custom].Add(CrosswordDictionary.Load(dlgDictOpen.FileName, DictFileType.Plaintext));
                        dlgDictOpen.FileName += CrosswordDictionary.importSuffix;
                        //currentSettings.AddDictionaryFile(DictType.Custom, dlgDictOpen.FileName);
                        currentSettings.DictionaryFiles[DictType.Custom].Add(dlgDictOpen.FileName);
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
            if (currentWordSources != null) { currentWordSources.Save(); }
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
            // Get selected clue
            ClueDisplay cdSelected = GetSelectedClue();
            if (cdSelected == null)
            {
                MessageBox.Show("You must select a clue to add to the dictionary", "Add to Dictionary", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            PlacedClue pc = cdSelected.Clue;

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

            // Retrieve user dictionary (1st custom, then 1st default - think about a better way to do this)
            IWordSource ws;
            if (AllDictionaries.ContainsKey(DictType.Custom) && (AllDictionaries[DictType.Custom].Count > 0))
            {
                ws = AllDictionaries[DictType.Custom][0];
            }
            else
            {
                if (AllDictionaries.ContainsKey(DictType.Default) && (AllDictionaries[DictType.Default].Count > 0))
                {
                    ws = AllDictionaries[DictType.Default][0];
                }
                else
                {
                    MessageBox.Show("There are no dictionaries available to add to.");
                    return;
                }
            }

            // See if there is a matching entry already
            if (ws.entries.ContainsKey(word))
            {
                // See if there's a definition already
                if (ws.entries[word].Count == 0)
                {
                    ws.entries[word].Add(pc.clue.question);
                }
                else
                {
                    DictionaryClueList dclPrompt = new DictionaryClueList();
                    dclPrompt.ClueChoice = new List<Clue>();
                    foreach (String clueOption in ws.entries[word])
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
                            ws.entries[word].Add(pc.clue.question);
                        }
                        else
                        {
                            // Update the selected clue
                            for (int i = 0; i < ws.entries[word].Count; i++)
                            {
                                if (ws.entries[word][i] == dclPrompt.SelectedClue.question) { ws.entries[word][i] = pc.clue.question; }
                            }
                        }
                        ws.Save();
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
                ws.entries.Add(word, new List<String>() { pc.clue.question });
            }
            ws.Save();
            tsslMessage.Text = String.Format("Saved clue for {0}", word);

            timerMessageReset.Enabled = false;
            timerMessageReset.Interval = 5000;
            timerMessageReset.Enabled = true;
        }

        private void timerMessageReset_Tick(object sender, EventArgs e)
        {
            tsslMessage.Text = "";
        }

        private void runClueCheck(bool forceCheckAll = false)
        {
            // Force-check-all means we re-run the background worker on all clues (e.g. on crossword load)
            if (forceCheckAll)
            {
                foreach (PlacedClue pc in crossword.placedClues) { pc.changesChecked(false); }
            }
            __cluesToCheck = true;
            if (!bwDictionaryChecker.IsBusy)
            {
                bwDictionaryChecker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Run checks on clues in the background, looking for possible solutions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                            ClueChanged("", new PlacedClueChangedEventArgs(pc));
                        }
                        );
                    }

                    // Create regular expression
                    Regex reWord = pc.clue.regExp; /*new Regex(
                        "^"
                        + Regex.Replace(
                            pc.clue.answer.Replace("?", ".")
                            , "."
                            , "$0[" + Clue.NonCountingChars_Regex + "]*"
                        )
                        + "$"
                    , RegexOptions.IgnoreCase);*/

                    // Clear any existing matches
                    pc.clearMatches();

                    // Loop through dictionaries, looking for matches
                    for (DictType dt = DictType.Default; dt < DictType.Remote; dt++)
                    {
                        if (!AllDictionaries.ContainsKey(dt)) { continue; } // Mayhaps we have no dicts of this type
                        List<IWordSource> dicts = AllDictionaries[dt];
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
                    pc.recalculateStatus();
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

        private void newClueHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TryClueAdd(__mouseCol, __mouseRow);
        }

        private void dgvPuzzle_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPuzzle[e.ColumnIndex, e.RowIndex].Style.Tag == null)
            {
                UpdatePreviewGrid();
            }
            else
            {
                try
                {
                    dgvPuzzle[e.ColumnIndex, e.RowIndex].Style.BackColor = (System.Drawing.Color)dgvPuzzle[e.ColumnIndex, e.RowIndex].Style.Tag;
                }
                catch (Exception)
                {
                    // Was thrown if Style.Tag == null and therefore cannot be cast to a System.Drawing.Color, although we now trap that in the if() above
                    // We don't know what colour to revert to, so don't
                    UpdatePreviewGrid();
                }
            }
        }

        /// <summary>
        /// Loads a new regex searcher based on each clue intersecting with the selected cell, or a blank searcher if black cell chosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void regexSearchHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<RegexSearcher> allRE = new List<RegexSearcher>();

            List<PlacedClue> matchingClues = FindCluesFromGridPos(__mouseCol, __mouseRow);
            if ((matchingClues == null) || (matchingClues.Count == 0))
            {
                allRE.Add(new RegexSearcher(this, null));
                allRE[0].Show();
            }
            else
            {
                foreach (PlacedClue pc in matchingClues)
                {
                    RegexSearcher re = new RegexSearcher(this, pc.clue);
                    allRE.Add(re);
                    re.Show();
                }
            }
        }

        private void intersectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Statistics frmStats = new Statistics(this);
            frmStats.tabCtrl.SelectTab(frmStats.tpIntersections);
            frmStats.ShowDialog();
        }

        private void timerBackup_Tick(object sender, EventArgs e)
        {
            if (currentSettings.AutoSaveOnTimer && (crossword != null))
            {
                currentSettings.PerformAutosave(crossword);
            }
        }

        private void keepAutoBackupToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            currentSettings.AutoSaveOnTimer = keepAutoBackupToolStripMenuItem.Checked;
            currentSettings.Save();
        }

        private void anagramCreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnagramCreator ac = new AnagramCreator(this);
            ac.Show();
        }
        private void tsClearCell_Click(object sender, EventArgs e)
        {
            if ((__mouseRow > 0) && (__mouseCol > 0))
            {
                ClearCell(__mouseRow, __mouseCol);
            }
            UpdatePreviewGrid();
        }

        private void tsClearClue_Click(object sender, EventArgs e)
        {
            if ((__mouseCol > 0) && (__mouseRow > 0))
            {
                ClearClues(__mouseCol, __mouseRow);
            }
            UpdatePreviewGrid();
            //populateClueDisplays();
        }

        /// <summary>
        /// Clears the given cell (by x and y) in all intersecting clues
        /// </summary>
        /// <param name="x">The x position at which to clear</param>
        /// <param name="y">The y position at which to clear</param>
        private void ClearCell(int x, int y)
        {
            List<PlacedClue> matches = FindCluesFromGridPos(x, y);
            int pos = 0;
            foreach (PlacedClue pc in matches)
            {
                switch (pc.orientation)
                {
                    case AD.Across:
                        pos = x - pc.x;
                        break;
                    case AD.Down:
                        pos = y - pc.y;
                        break;
                }
                // Now allow for non-counting characters:
                //  every one of these occurring before the relevant part of the string should increase pos by 1
                int cumulative = 0; int nonCountings = 0;
                foreach (int part in pc.clue.pattern)
                {
                    cumulative += part;
                    if (cumulative <= pos) { nonCountings += 1; }
                }
                pos += nonCountings;
                StringBuilder sb = new StringBuilder(pc.clue.answer);
                sb[pos] = '?';
                pc.clue.answer = sb.ToString();
            }
        }

        /// <summary>
        /// Clears all cells in the specified clue, including any intersections with other clues, as well as the clue question
        /// </summary>
        /// <param name="clue">The clue to clear</param>
        private void ClearClue(PlacedClue clue)
        {
            int xLoop = clue.x;
            int yLoop = clue.y;
            switch (clue.orientation)
            {
                case AD.Across:
                    for (xLoop = clue.x; xLoop < (clue.x + clue.clue.length); xLoop++)
                    {
                        ClearCell(xLoop, yLoop);
                    }
                    break;
                case AD.Down:
                    for (yLoop = clue.y; yLoop < (clue.y + clue.clue.length); yLoop++)
                    {
                        ClearCell(xLoop, yLoop);
                    }
                    break;
                default:
                    // Shouldn't really happen!
                    break;
            }
            clue.clue.question = Clue.BlankQuestion;
            clue.Changed();
        }

        /// <summary>
        /// Clears all cells in any clue crossing the specified cell
        /// </summary>
        /// <param name="x">The x position at which to select clues to clear</param>
        /// <param name="y">The y position at which to select clues to clear</param>
        private void ClearClues(int x, int y)
        {
            //int xLoop;
            //int yLoop;
            List<PlacedClue> intersections = FindCluesFromGridPos(x, y);
            foreach (PlacedClue pc in intersections)
            {
                ClearClue(pc);
            }
        }

        //TODO - abstract contents of BackgroundWorker DoWork into a "CheckClues" function
        //TODO - then make the folowing function run automatically instead of on demand
        /// <summary>
        /// Look through all the clues, seeing where their possible matches narrow down choices in other clues
        /// </summary>
        private void CheckCluesRefined()
        {
            // Push all incomplete clues with at least one match onto the stack
            // Push all incomplete clues with at least one match onto the stack
            Stack<PlacedClue> clueStack = new Stack<PlacedClue>();
            foreach (PlacedClue pc in crossword.placedClues)
            {
                if (
                    (!pc.status.HasFlag(ClueStatus.AnswerComplete)) &&
                    (pc.status.HasFlag(ClueStatus.HasMatches))
                    )
                {
                    clueStack.Push(pc);
                }
            }

            while(clueStack.Count>0)
            {
                PlacedClue target = clueStack.Pop();
                // Analyse this clue, including adding back to the stack any other clues that have been affected
            }
        }

        private void exportTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextExporter te = new TextExporter(ref crossword);
            te.Show();
        }

        private void letterFrequenciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Statistics frmStats = new Statistics(this);
            frmStats.tabCtrl.SelectTab(frmStats.tpLetters);
            frmStats.ShowDialog();
        }
    }
}
