namespace Neverer
{
    partial class Creator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Creator));
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepAutoBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridWithAnswersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyGridToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gridWithAnswersToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dictionaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dictionaryManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickImportDictionaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.anagramCreatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regularExpressionSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intersectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsClueClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToDictionaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvPuzzle = new System.Windows.Forms.DataGridView();
            this.cmsPuzzleGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newClueHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsClearCell = new System.Windows.Forms.ToolStripMenuItem();
            this.tsClearClue = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.regexSearchHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblClues = new System.Windows.Forms.Label();
            this.lblGrid = new System.Windows.Forms.Label();
            this.cmdAddClue = new System.Windows.Forms.Button();
            this.cmdRemoveClue = new System.Windows.Forms.Button();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.lblPuzzleTitle = new System.Windows.Forms.Label();
            this.dlgDictOpen = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbClueUpdate = new System.Windows.Forms.ToolStripProgressBar();
            this.timerMessageReset = new System.Windows.Forms.Timer(this.components);
            this.bwDictionaryChecker = new System.ComponentModel.BackgroundWorker();
            this.flpClues = new System.Windows.Forms.FlowLayoutPanel();
            this.timerBackup = new System.Windows.Forms.Timer(this.components);
            this.exportTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msMain.SuspendLayout();
            this.cmsClueClick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuzzle)).BeginInit();
            this.cmsPuzzleGrid.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.statsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(636, 24);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "Primary Menu Strip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.keepAutoBackupToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportExcelToolStripMenuItem,
            this.exportHTMLToolStripMenuItem,
            this.exportPDFToolStripMenuItem,
            this.exportTextToolStripMenuItem,
            this.toolStripSeparator2,
            this.recentFilesToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "&New...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // keepAutoBackupToolStripMenuItem
            // 
            this.keepAutoBackupToolStripMenuItem.CheckOnClick = true;
            this.keepAutoBackupToolStripMenuItem.Name = "keepAutoBackupToolStripMenuItem";
            this.keepAutoBackupToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.keepAutoBackupToolStripMenuItem.Text = "Keep AutoBackup";
            this.keepAutoBackupToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.keepAutoBackupToolStripMenuItem_CheckStateChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // exportExcelToolStripMenuItem
            // 
            this.exportExcelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyGridToolStripMenuItem,
            this.gridWithAnswersToolStripMenuItem});
            this.exportExcelToolStripMenuItem.Name = "exportExcelToolStripMenuItem";
            this.exportExcelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportExcelToolStripMenuItem.Text = "Export &Excel";
            // 
            // emptyGridToolStripMenuItem
            // 
            this.emptyGridToolStripMenuItem.Name = "emptyGridToolStripMenuItem";
            this.emptyGridToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.emptyGridToolStripMenuItem.Text = "&Empty Grid";
            this.emptyGridToolStripMenuItem.Click += new System.EventHandler(this.emptyGridToolStripMenuItem_Click);
            // 
            // gridWithAnswersToolStripMenuItem
            // 
            this.gridWithAnswersToolStripMenuItem.Name = "gridWithAnswersToolStripMenuItem";
            this.gridWithAnswersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gridWithAnswersToolStripMenuItem.Text = "Grid with &Answers";
            this.gridWithAnswersToolStripMenuItem.Click += new System.EventHandler(this.gridWithAnswersToolStripMenuItem_Click);
            // 
            // exportHTMLToolStripMenuItem
            // 
            this.exportHTMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyGridToolStripMenuItem1,
            this.gridWithAnswersToolStripMenuItem1});
            this.exportHTMLToolStripMenuItem.Name = "exportHTMLToolStripMenuItem";
            this.exportHTMLToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportHTMLToolStripMenuItem.Text = "Export &HTML";
            // 
            // emptyGridToolStripMenuItem1
            // 
            this.emptyGridToolStripMenuItem1.Name = "emptyGridToolStripMenuItem1";
            this.emptyGridToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.emptyGridToolStripMenuItem1.Text = "&Empty Grid";
            this.emptyGridToolStripMenuItem1.Click += new System.EventHandler(this.emptyGridToolStripMenuItem1_Click);
            // 
            // gridWithAnswersToolStripMenuItem1
            // 
            this.gridWithAnswersToolStripMenuItem1.Name = "gridWithAnswersToolStripMenuItem1";
            this.gridWithAnswersToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.gridWithAnswersToolStripMenuItem1.Text = "Grid with &Answers";
            this.gridWithAnswersToolStripMenuItem1.Click += new System.EventHandler(this.gridWithAnswersToolStripMenuItem1_Click);
            // 
            // exportPDFToolStripMenuItem
            // 
            this.exportPDFToolStripMenuItem.Name = "exportPDFToolStripMenuItem";
            this.exportPDFToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportPDFToolStripMenuItem.Text = "Export &PDF";
            this.exportPDFToolStripMenuItem.Visible = false;
            this.exportPDFToolStripMenuItem.Click += new System.EventHandler(this.exportPDFToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.recentFilesToolStripMenuItem.Text = "Recent &Files";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dictionaryToolStripMenuItem,
            this.dictionaryManagementToolStripMenuItem,
            this.quickImportDictionaryToolStripMenuItem,
            this.toolStripSeparator4,
            this.anagramCreatorToolStripMenuItem,
            this.regularExpressionSearchToolStripMenuItem,
            this.toolStripSeparator5,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // dictionaryToolStripMenuItem
            // 
            this.dictionaryToolStripMenuItem.Name = "dictionaryToolStripMenuItem";
            this.dictionaryToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.dictionaryToolStripMenuItem.Text = "&Dictionary...";
            // 
            // dictionaryManagementToolStripMenuItem
            // 
            this.dictionaryManagementToolStripMenuItem.Name = "dictionaryManagementToolStripMenuItem";
            this.dictionaryManagementToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.dictionaryManagementToolStripMenuItem.Text = "Dictionary &Management...";
            this.dictionaryManagementToolStripMenuItem.Click += new System.EventHandler(this.dictionaryManagementToolStripMenuItem_Click);
            // 
            // quickImportDictionaryToolStripMenuItem
            // 
            this.quickImportDictionaryToolStripMenuItem.Name = "quickImportDictionaryToolStripMenuItem";
            this.quickImportDictionaryToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.quickImportDictionaryToolStripMenuItem.Text = "&Quick Import Dictionary...";
            this.quickImportDictionaryToolStripMenuItem.Click += new System.EventHandler(this.quickImportDictionaryToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(290, 6);
            // 
            // anagramCreatorToolStripMenuItem
            // 
            this.anagramCreatorToolStripMenuItem.Name = "anagramCreatorToolStripMenuItem";
            this.anagramCreatorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.anagramCreatorToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.anagramCreatorToolStripMenuItem.Text = "&Anagram Creator...";
            this.anagramCreatorToolStripMenuItem.Click += new System.EventHandler(this.anagramCreatorToolStripMenuItem_Click);
            // 
            // regularExpressionSearchToolStripMenuItem
            // 
            this.regularExpressionSearchToolStripMenuItem.Name = "regularExpressionSearchToolStripMenuItem";
            this.regularExpressionSearchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.regularExpressionSearchToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.regularExpressionSearchToolStripMenuItem.Text = "Regular E&xpression Search...";
            this.regularExpressionSearchToolStripMenuItem.Click += new System.EventHandler(this.regularExpressionSearchToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(290, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(293, 22);
            this.optionsToolStripMenuItem.Text = "&Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // statsToolStripMenuItem
            // 
            this.statsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.intersectionsToolStripMenuItem});
            this.statsToolStripMenuItem.Name = "statsToolStripMenuItem";
            this.statsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.statsToolStripMenuItem.Text = "&Stats";
            // 
            // intersectionsToolStripMenuItem
            // 
            this.intersectionsToolStripMenuItem.Name = "intersectionsToolStripMenuItem";
            this.intersectionsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.intersectionsToolStripMenuItem.Text = "&Intersections";
            this.intersectionsToolStripMenuItem.Click += new System.EventHandler(this.intersectionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // cmsClueClick
            // 
            this.cmsClueClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToDictionaryToolStripMenuItem});
            this.cmsClueClick.Name = "cmsClueClick";
            this.cmsClueClick.Size = new System.Drawing.Size(168, 26);
            // 
            // addToDictionaryToolStripMenuItem
            // 
            this.addToDictionaryToolStripMenuItem.Name = "addToDictionaryToolStripMenuItem";
            this.addToDictionaryToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.addToDictionaryToolStripMenuItem.Text = "&Add to Dictionary";
            this.addToDictionaryToolStripMenuItem.Click += new System.EventHandler(this.addToDictionaryToolStripMenuItem_Click);
            // 
            // dgvPuzzle
            // 
            this.dgvPuzzle.AllowUserToAddRows = false;
            this.dgvPuzzle.AllowUserToDeleteRows = false;
            this.dgvPuzzle.AllowUserToResizeColumns = false;
            this.dgvPuzzle.AllowUserToResizeRows = false;
            this.dgvPuzzle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPuzzle.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPuzzle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPuzzle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPuzzle.ContextMenuStrip = this.cmsPuzzleGrid;
            this.dgvPuzzle.Location = new System.Drawing.Point(321, 43);
            this.dgvPuzzle.MultiSelect = false;
            this.dgvPuzzle.Name = "dgvPuzzle";
            this.dgvPuzzle.ReadOnly = true;
            this.dgvPuzzle.ShowCellErrors = false;
            this.dgvPuzzle.ShowEditingIcon = false;
            this.dgvPuzzle.ShowRowErrors = false;
            this.dgvPuzzle.Size = new System.Drawing.Size(303, 322);
            this.dgvPuzzle.TabIndex = 2;
            this.dgvPuzzle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPuzzle_CellClick);
            this.dgvPuzzle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPuzzle_CellDoubleClick);
            this.dgvPuzzle.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPuzzle_CellMouseEnter);
            this.dgvPuzzle.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPuzzle_CellMouseLeave);
            this.dgvPuzzle.Resize += new System.EventHandler(this.dgvPuzzle_Resize);
            // 
            // cmsPuzzleGrid
            // 
            this.cmsPuzzleGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newClueHereToolStripMenuItem,
            this.toolStripSeparator7,
            this.tsClearCell,
            this.tsClearClue,
            this.toolStripSeparator6,
            this.regexSearchHereToolStripMenuItem});
            this.cmsPuzzleGrid.Name = "cmsPuzzleGrid";
            this.cmsPuzzleGrid.Size = new System.Drawing.Size(213, 104);
            // 
            // newClueHereToolStripMenuItem
            // 
            this.newClueHereToolStripMenuItem.Name = "newClueHereToolStripMenuItem";
            this.newClueHereToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.newClueHereToolStripMenuItem.Text = "New Clue &Here...";
            this.newClueHereToolStripMenuItem.Click += new System.EventHandler(this.newClueHereToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(209, 6);
            // 
            // tsClearCell
            // 
            this.tsClearCell.Name = "tsClearCell";
            this.tsClearCell.Size = new System.Drawing.Size(212, 22);
            this.tsClearCell.Text = "C&lear Cell";
            this.tsClearCell.Click += new System.EventHandler(this.tsClearCell_Click);
            // 
            // tsClearClue
            // 
            this.tsClearClue.Name = "tsClearClue";
            this.tsClearClue.Size = new System.Drawing.Size(212, 22);
            this.tsClearClue.Text = "Cl&ear Clue";
            this.tsClearClue.Click += new System.EventHandler(this.tsClearClue_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(209, 6);
            // 
            // regexSearchHereToolStripMenuItem
            // 
            this.regexSearchHereToolStripMenuItem.Name = "regexSearchHereToolStripMenuItem";
            this.regexSearchHereToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.regexSearchHereToolStripMenuItem.Text = "Rege&x Search From Here...";
            this.regexSearchHereToolStripMenuItem.Click += new System.EventHandler(this.regexSearchHereToolStripMenuItem_Click);
            // 
            // lblClues
            // 
            this.lblClues.AutoSize = true;
            this.lblClues.Location = new System.Drawing.Point(12, 27);
            this.lblClues.Name = "lblClues";
            this.lblClues.Size = new System.Drawing.Size(47, 13);
            this.lblClues.TabIndex = 3;
            this.lblClues.Text = "Clue List";
            // 
            // lblGrid
            // 
            this.lblGrid.AutoSize = true;
            this.lblGrid.Location = new System.Drawing.Point(318, 27);
            this.lblGrid.Name = "lblGrid";
            this.lblGrid.Size = new System.Drawing.Size(64, 13);
            this.lblGrid.TabIndex = 4;
            this.lblGrid.Text = "Puzzle View";
            // 
            // cmdAddClue
            // 
            this.cmdAddClue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAddClue.Location = new System.Drawing.Point(12, 342);
            this.cmdAddClue.Name = "cmdAddClue";
            this.cmdAddClue.Size = new System.Drawing.Size(75, 23);
            this.cmdAddClue.TabIndex = 5;
            this.cmdAddClue.Text = "&Add";
            this.cmdAddClue.UseVisualStyleBackColor = true;
            this.cmdAddClue.Click += new System.EventHandler(this.cmdAddClue_Click);
            // 
            // cmdRemoveClue
            // 
            this.cmdRemoveClue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRemoveClue.Location = new System.Drawing.Point(240, 342);
            this.cmdRemoveClue.Name = "cmdRemoveClue";
            this.cmdRemoveClue.Size = new System.Drawing.Size(75, 23);
            this.cmdRemoveClue.TabIndex = 6;
            this.cmdRemoveClue.Text = "&Remove";
            this.cmdRemoveClue.UseVisualStyleBackColor = true;
            this.cmdRemoveClue.Click += new System.EventHandler(this.cmdRemoveClue_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.DefaultExt = "*.nev";
            this.dlgOpen.FileName = "openFileDialog1";
            this.dlgOpen.Filter = "Neverer Crosswords|*.nev|All files|*.*";
            this.dlgOpen.Title = "Open Crossword";
            // 
            // dlgSave
            // 
            this.dlgSave.CheckPathExists = false;
            this.dlgSave.DefaultExt = "*.nev";
            this.dlgSave.Filter = "Neverer Crosswords|*.nev|All files|*.*";
            this.dlgSave.Title = "Save Crossword";
            // 
            // cmdEdit
            // 
            this.cmdEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdEdit.Location = new System.Drawing.Point(128, 342);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(75, 23);
            this.cmdEdit.TabIndex = 7;
            this.cmdEdit.Text = "&Edit";
            this.cmdEdit.UseVisualStyleBackColor = true;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // lblPuzzleTitle
            // 
            this.lblPuzzleTitle.AutoSize = true;
            this.lblPuzzleTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuzzleTitle.Location = new System.Drawing.Point(388, 27);
            this.lblPuzzleTitle.Name = "lblPuzzleTitle";
            this.lblPuzzleTitle.Size = new System.Drawing.Size(36, 13);
            this.lblPuzzleTitle.TabIndex = 8;
            this.lblPuzzleTitle.Text = "[title]";
            // 
            // dlgDictOpen
            // 
            this.dlgDictOpen.DefaultExt = "*.nev.dic";
            this.dlgDictOpen.Filter = "Neverer Dictionaries|*.nev.dic|Plain text|*.*";
            this.dlgDictOpen.Title = "Open Crossword";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslMessage,
            this.tspbClueUpdate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 368);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(636, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslMessage
            // 
            this.tsslMessage.Name = "tsslMessage";
            this.tsslMessage.Size = new System.Drawing.Size(0, 17);
            // 
            // tspbClueUpdate
            // 
            this.tspbClueUpdate.Name = "tspbClueUpdate";
            this.tspbClueUpdate.Size = new System.Drawing.Size(100, 16);
            // 
            // timerMessageReset
            // 
            this.timerMessageReset.Interval = 5000;
            this.timerMessageReset.Tick += new System.EventHandler(this.timerMessageReset_Tick);
            // 
            // bwDictionaryChecker
            // 
            this.bwDictionaryChecker.WorkerReportsProgress = true;
            this.bwDictionaryChecker.WorkerSupportsCancellation = true;
            this.bwDictionaryChecker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDictionaryChecker_DoWork);
            // 
            // flpClues
            // 
            this.flpClues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flpClues.AutoScroll = true;
            this.flpClues.Location = new System.Drawing.Point(12, 43);
            this.flpClues.Margin = new System.Windows.Forms.Padding(0);
            this.flpClues.Name = "flpClues";
            this.flpClues.Size = new System.Drawing.Size(306, 293);
            this.flpClues.TabIndex = 11;
            // 
            // timerBackup
            // 
            this.timerBackup.Enabled = true;
            this.timerBackup.Interval = 10000;
            this.timerBackup.Tick += new System.EventHandler(this.timerBackup_Tick);
            // 
            // exportTextToolStripMenuItem
            // 
            this.exportTextToolStripMenuItem.Name = "exportTextToolStripMenuItem";
            this.exportTextToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportTextToolStripMenuItem.Text = "Export &Text...";
            this.exportTextToolStripMenuItem.Click += new System.EventHandler(this.exportTextToolStripMenuItem_Click);
            // 
            // Creator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 390);
            this.Controls.Add(this.flpClues);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblPuzzleTitle);
            this.Controls.Add(this.cmdEdit);
            this.Controls.Add(this.cmdRemoveClue);
            this.Controls.Add(this.cmdAddClue);
            this.Controls.Add(this.lblGrid);
            this.Controls.Add(this.lblClues);
            this.Controls.Add(this.dgvPuzzle);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "Creator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Neverer Creator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Creator_FormClosing);
            this.Load += new System.EventHandler(this.Creator_Load);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.cmsClueClick.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuzzle)).EndInit();
            this.cmsPuzzleGrid.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvPuzzle;
        private System.Windows.Forms.Label lblClues;
        private System.Windows.Forms.Label lblGrid;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.Button cmdAddClue;
        private System.Windows.Forms.Button cmdRemoveClue;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.Button cmdEdit;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportPDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportExcelToolStripMenuItem;
        private System.Windows.Forms.Label lblPuzzleTitle;
        private System.Windows.Forms.OpenFileDialog dlgDictOpen;
        private System.Windows.Forms.ToolStripMenuItem dictionaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dictionaryManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickImportDictionaryToolStripMenuItem;
        //private Community.Windows.Forms.MenuStripMRUManager menuStripManagerFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsClueClick;
        private System.Windows.Forms.ToolStripMenuItem addToDictionaryToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslMessage;
        private System.Windows.Forms.Timer timerMessageReset;
        private System.ComponentModel.BackgroundWorker bwDictionaryChecker;
        private System.Windows.Forms.ToolStripProgressBar tspbClueUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem regularExpressionSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem emptyGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridWithAnswersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyGridToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gridWithAnswersToolStripMenuItem1;
        private System.Windows.Forms.FlowLayoutPanel flpClues;
        private System.Windows.Forms.ContextMenuStrip cmsPuzzleGrid;
        private System.Windows.Forms.ToolStripMenuItem tsClearCell;
        private System.Windows.Forms.ToolStripMenuItem newClueHereToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem regexSearchHereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intersectionsToolStripMenuItem;
        private System.Windows.Forms.Timer timerBackup;
        private System.Windows.Forms.ToolStripMenuItem keepAutoBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anagramCreatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsClearClue;
        private System.Windows.Forms.ToolStripMenuItem exportTextToolStripMenuItem;
    }
}

