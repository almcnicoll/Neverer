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
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvClues = new System.Windows.Forms.DataGridView();
            this.dgvPuzzle = new System.Windows.Forms.DataGridView();
            this.lblClues = new System.Windows.Forms.Label();
            this.lblGrid = new System.Windows.Forms.Label();
            this.cmdAddClue = new System.Windows.Forms.Button();
            this.cmdRemoveClue = new System.Windows.Forms.Button();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.lblPuzzleTitle = new System.Windows.Forms.Label();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuzzle)).BeginInit();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(716, 24);
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
            this.exportExcelToolStripMenuItem,
            this.exportPDFToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.newToolStripMenuItem.Text = "&New...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exportExcelToolStripMenuItem
            // 
            this.exportExcelToolStripMenuItem.Name = "exportExcelToolStripMenuItem";
            this.exportExcelToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exportExcelToolStripMenuItem.Text = "Export &Excel";
            this.exportExcelToolStripMenuItem.Click += new System.EventHandler(this.exportExcelToolStripMenuItem_Click);
            // 
            // exportPDFToolStripMenuItem
            // 
            this.exportPDFToolStripMenuItem.Name = "exportPDFToolStripMenuItem";
            this.exportPDFToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exportPDFToolStripMenuItem.Text = "Export &PDF";
            this.exportPDFToolStripMenuItem.Visible = false;
            this.exportPDFToolStripMenuItem.Click += new System.EventHandler(this.exportPDFToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // dgvClues
            // 
            this.dgvClues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvClues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClues.Location = new System.Drawing.Point(12, 43);
            this.dgvClues.MultiSelect = false;
            this.dgvClues.Name = "dgvClues";
            this.dgvClues.ReadOnly = true;
            this.dgvClues.Size = new System.Drawing.Size(262, 295);
            this.dgvClues.TabIndex = 1;
            this.dgvClues.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvClues_CellMouseDoubleClick);
            this.dgvClues.SelectionChanged += new System.EventHandler(this.dgvClues_SelectionChanged);
            this.dgvClues.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvClues_KeyDown);
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
            this.dgvPuzzle.Location = new System.Drawing.Point(280, 43);
            this.dgvPuzzle.MultiSelect = false;
            this.dgvPuzzle.Name = "dgvPuzzle";
            this.dgvPuzzle.ReadOnly = true;
            this.dgvPuzzle.ShowCellErrors = false;
            this.dgvPuzzle.ShowEditingIcon = false;
            this.dgvPuzzle.ShowRowErrors = false;
            this.dgvPuzzle.Size = new System.Drawing.Size(424, 324);
            this.dgvPuzzle.TabIndex = 2;
            this.dgvPuzzle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPuzzle_CellClick);
            this.dgvPuzzle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPuzzle_CellDoubleClick);
            this.dgvPuzzle.Resize += new System.EventHandler(this.dgvPuzzle_Resize);
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
            this.lblGrid.Location = new System.Drawing.Point(277, 27);
            this.lblGrid.Name = "lblGrid";
            this.lblGrid.Size = new System.Drawing.Size(64, 13);
            this.lblGrid.TabIndex = 4;
            this.lblGrid.Text = "Puzzle View";
            // 
            // cmdAddClue
            // 
            this.cmdAddClue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAddClue.Location = new System.Drawing.Point(12, 344);
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
            this.cmdRemoveClue.Location = new System.Drawing.Point(199, 344);
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
            this.cmdEdit.Location = new System.Drawing.Point(106, 344);
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
            this.lblPuzzleTitle.Location = new System.Drawing.Point(347, 27);
            this.lblPuzzleTitle.Name = "lblPuzzleTitle";
            this.lblPuzzleTitle.Size = new System.Drawing.Size(36, 13);
            this.lblPuzzleTitle.TabIndex = 8;
            this.lblPuzzleTitle.Text = "[title]";
            // 
            // Creator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 379);
            this.Controls.Add(this.lblPuzzleTitle);
            this.Controls.Add(this.cmdEdit);
            this.Controls.Add(this.cmdRemoveClue);
            this.Controls.Add(this.cmdAddClue);
            this.Controls.Add(this.lblGrid);
            this.Controls.Add(this.lblClues);
            this.Controls.Add(this.dgvPuzzle);
            this.Controls.Add(this.dgvClues);
            this.Controls.Add(this.msMain);
            this.MainMenuStrip = this.msMain;
            this.Name = "Creator";
            this.Text = "Neverer Creator";
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuzzle)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvClues;
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
    }
}

