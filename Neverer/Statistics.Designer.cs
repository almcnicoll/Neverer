namespace Neverer
{
    partial class Statistics
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
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tpIntersections = new System.Windows.Forms.TabPage();
            this.cmbPerLetter = new System.Windows.Forms.ComboBox();
            this.splitContainerIntersections = new System.Windows.Forms.SplitContainer();
            this.flpClues = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvIntersectionStats = new System.Windows.Forms.DataGridView();
            this.tpLetters = new System.Windows.Forms.TabPage();
            this.dgvLetterSpread = new System.Windows.Forms.DataGridView();
            this.tabCtrl.SuspendLayout();
            this.tpIntersections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerIntersections)).BeginInit();
            this.splitContainerIntersections.Panel1.SuspendLayout();
            this.splitContainerIntersections.Panel2.SuspendLayout();
            this.splitContainerIntersections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIntersectionStats)).BeginInit();
            this.tpLetters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLetterSpread)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCtrl
            // 
            this.tabCtrl.Controls.Add(this.tpIntersections);
            this.tabCtrl.Controls.Add(this.tpLetters);
            this.tabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrl.Location = new System.Drawing.Point(0, 0);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(800, 450);
            this.tabCtrl.TabIndex = 0;
            // 
            // tpIntersections
            // 
            this.tpIntersections.Controls.Add(this.cmbPerLetter);
            this.tpIntersections.Controls.Add(this.splitContainerIntersections);
            this.tpIntersections.Location = new System.Drawing.Point(4, 22);
            this.tpIntersections.Name = "tpIntersections";
            this.tpIntersections.Padding = new System.Windows.Forms.Padding(3);
            this.tpIntersections.Size = new System.Drawing.Size(792, 424);
            this.tpIntersections.TabIndex = 0;
            this.tpIntersections.Text = "Intersections";
            this.tpIntersections.UseVisualStyleBackColor = true;
            // 
            // cmbPerLetter
            // 
            this.cmbPerLetter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbPerLetter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPerLetter.FormattingEnabled = true;
            this.cmbPerLetter.Items.AddRange(new object[] {
            "Intersections (absolute)",
            "Intersections (per letter)"});
            this.cmbPerLetter.Location = new System.Drawing.Point(8, 6);
            this.cmbPerLetter.Name = "cmbPerLetter";
            this.cmbPerLetter.Size = new System.Drawing.Size(178, 21);
            this.cmbPerLetter.TabIndex = 2;
            this.cmbPerLetter.Text = "Intersections (absolute)";
            this.cmbPerLetter.TextChanged += new System.EventHandler(this.cmbPerLetter_TextChanged);
            // 
            // splitContainerIntersections
            // 
            this.splitContainerIntersections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerIntersections.Location = new System.Drawing.Point(3, 33);
            this.splitContainerIntersections.Name = "splitContainerIntersections";
            // 
            // splitContainerIntersections.Panel1
            // 
            this.splitContainerIntersections.Panel1.Controls.Add(this.flpClues);
            // 
            // splitContainerIntersections.Panel2
            // 
            this.splitContainerIntersections.Panel2.Controls.Add(this.dgvIntersectionStats);
            this.splitContainerIntersections.Size = new System.Drawing.Size(786, 388);
            this.splitContainerIntersections.SplitterDistance = 360;
            this.splitContainerIntersections.TabIndex = 0;
            // 
            // flpClues
            // 
            this.flpClues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpClues.AutoScroll = true;
            this.flpClues.Location = new System.Drawing.Point(0, 0);
            this.flpClues.Margin = new System.Windows.Forms.Padding(0);
            this.flpClues.Name = "flpClues";
            this.flpClues.Size = new System.Drawing.Size(361, 388);
            this.flpClues.TabIndex = 1;
            // 
            // dgvIntersectionStats
            // 
            this.dgvIntersectionStats.AllowUserToAddRows = false;
            this.dgvIntersectionStats.AllowUserToDeleteRows = false;
            this.dgvIntersectionStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIntersectionStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvIntersectionStats.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvIntersectionStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIntersectionStats.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvIntersectionStats.Location = new System.Drawing.Point(0, 0);
            this.dgvIntersectionStats.Name = "dgvIntersectionStats";
            this.dgvIntersectionStats.ReadOnly = true;
            this.dgvIntersectionStats.RowHeadersVisible = false;
            this.dgvIntersectionStats.Size = new System.Drawing.Size(422, 388);
            this.dgvIntersectionStats.TabIndex = 0;
            // 
            // tpLetters
            // 
            this.tpLetters.Controls.Add(this.dgvLetterSpread);
            this.tpLetters.Location = new System.Drawing.Point(4, 22);
            this.tpLetters.Name = "tpLetters";
            this.tpLetters.Padding = new System.Windows.Forms.Padding(3);
            this.tpLetters.Size = new System.Drawing.Size(792, 424);
            this.tpLetters.TabIndex = 1;
            this.tpLetters.Text = "Letters";
            this.tpLetters.UseVisualStyleBackColor = true;
            // 
            // dgvLetterSpread
            // 
            this.dgvLetterSpread.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLetterSpread.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLetterSpread.Location = new System.Drawing.Point(8, 6);
            this.dgvLetterSpread.Name = "dgvLetterSpread";
            this.dgvLetterSpread.Size = new System.Drawing.Size(776, 410);
            this.dgvLetterSpread.TabIndex = 0;
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabCtrl);
            this.Name = "Statistics";
            this.Text = "Statistics";
            this.tabCtrl.ResumeLayout(false);
            this.tpIntersections.ResumeLayout(false);
            this.splitContainerIntersections.Panel1.ResumeLayout(false);
            this.splitContainerIntersections.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerIntersections)).EndInit();
            this.splitContainerIntersections.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIntersectionStats)).EndInit();
            this.tpLetters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLetterSpread)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl tabCtrl;
        public System.Windows.Forms.TabPage tpIntersections;
        private System.Windows.Forms.SplitContainer splitContainerIntersections;
        private System.Windows.Forms.FlowLayoutPanel flpClues;
        private System.Windows.Forms.DataGridView dgvIntersectionStats;
        private System.Windows.Forms.ComboBox cmbPerLetter;
        private System.Windows.Forms.DataGridView dgvLetterSpread;
        public System.Windows.Forms.TabPage tpLetters;
    }
}