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
            this.splitContainerIntersections = new System.Windows.Forms.SplitContainer();
            this.flpClues = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvIntersectionStats = new System.Windows.Forms.DataGridView();
            this.tabCtrl.SuspendLayout();
            this.tpIntersections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerIntersections)).BeginInit();
            this.splitContainerIntersections.Panel1.SuspendLayout();
            this.splitContainerIntersections.Panel2.SuspendLayout();
            this.splitContainerIntersections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIntersectionStats)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCtrl
            // 
            this.tabCtrl.Controls.Add(this.tpIntersections);
            this.tabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrl.Location = new System.Drawing.Point(0, 0);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(800, 450);
            this.tabCtrl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tpIntersections.Controls.Add(this.splitContainerIntersections);
            this.tpIntersections.Location = new System.Drawing.Point(4, 22);
            this.tpIntersections.Name = "tabPage1";
            this.tpIntersections.Padding = new System.Windows.Forms.Padding(3);
            this.tpIntersections.Size = new System.Drawing.Size(792, 424);
            this.tpIntersections.TabIndex = 0;
            this.tpIntersections.Text = "Intersections";
            this.tpIntersections.UseVisualStyleBackColor = true;
            // 
            // splitContainerIntersections
            // 
            this.splitContainerIntersections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerIntersections.Location = new System.Drawing.Point(3, 3);
            this.splitContainerIntersections.Name = "splitContainerIntersections";
            // 
            // splitContainerIntersections.Panel1
            // 
            this.splitContainerIntersections.Panel1.Controls.Add(this.flpClues);
            // 
            // splitContainerIntersections.Panel2
            // 
            this.splitContainerIntersections.Panel2.Controls.Add(this.dgvIntersectionStats);
            this.splitContainerIntersections.Size = new System.Drawing.Size(786, 418);
            this.splitContainerIntersections.SplitterDistance = 262;
            this.splitContainerIntersections.TabIndex = 0;
            // 
            // flpClues
            // 
            this.flpClues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpClues.Location = new System.Drawing.Point(0, 0);
            this.flpClues.Name = "flpClues";
            this.flpClues.Size = new System.Drawing.Size(262, 418);
            this.flpClues.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dgvIntersectionStats.AllowUserToAddRows = false;
            this.dgvIntersectionStats.AllowUserToDeleteRows = false;
            this.dgvIntersectionStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIntersectionStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIntersectionStats.Location = new System.Drawing.Point(0, 0);
            this.dgvIntersectionStats.Name = "dataGridView1";
            this.dgvIntersectionStats.ReadOnly = true;
            this.dgvIntersectionStats.Size = new System.Drawing.Size(520, 418);
            this.dgvIntersectionStats.TabIndex = 0;
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
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl tabCtrl;
        public System.Windows.Forms.TabPage tpIntersections;
        private System.Windows.Forms.SplitContainer splitContainerIntersections;
        private System.Windows.Forms.FlowLayoutPanel flpClues;
        private System.Windows.Forms.DataGridView dgvIntersectionStats;
    }
}