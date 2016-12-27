namespace Neverer
{
    partial class CrosswordSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrosswordSettings));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.tlpAllSettings = new System.Windows.Forms.TableLayoutPanel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.nudRows = new System.Windows.Forms.NumericUpDown();
            this.nudCols = new System.Windows.Forms.NumericUpDown();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblRows = new System.Windows.Forms.Label();
            this.comboRotationalSymmetry = new System.Windows.Forms.ComboBox();
            this.lblCols = new System.Windows.Forms.Label();
            this.lblRotationalSymmetry = new System.Windows.Forms.Label();
            this.tlpAllSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCols)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(12, 226);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(326, 226);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // tlpAllSettings
            // 
            this.tlpAllSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpAllSettings.AutoScroll = true;
            this.tlpAllSettings.ColumnCount = 2;
            this.tlpAllSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpAllSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpAllSettings.Controls.Add(this.txtTitle, 1, 3);
            this.tlpAllSettings.Controls.Add(this.nudRows, 1, 2);
            this.tlpAllSettings.Controls.Add(this.nudCols, 1, 1);
            this.tlpAllSettings.Controls.Add(this.lblTitle, 0, 3);
            this.tlpAllSettings.Controls.Add(this.lblRows, 0, 2);
            this.tlpAllSettings.Controls.Add(this.comboRotationalSymmetry, 1, 0);
            this.tlpAllSettings.Controls.Add(this.lblCols, 0, 1);
            this.tlpAllSettings.Controls.Add(this.lblRotationalSymmetry, 0, 0);
            this.tlpAllSettings.Location = new System.Drawing.Point(12, 12);
            this.tlpAllSettings.Name = "tlpAllSettings";
            this.tlpAllSettings.RowCount = 4;
            this.tlpAllSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpAllSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpAllSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpAllSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpAllSettings.Size = new System.Drawing.Size(389, 208);
            this.tlpAllSettings.TabIndex = 2;
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.Location = new System.Drawing.Point(112, 82);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(274, 20);
            this.txtTitle.TabIndex = 5;
            this.txtTitle.Text = "[Untitled Crossword]";
            // 
            // nudRows
            // 
            this.nudRows.Location = new System.Drawing.Point(112, 56);
            this.nudRows.Name = "nudRows";
            this.nudRows.Size = new System.Drawing.Size(40, 20);
            this.nudRows.TabIndex = 3;
            // 
            // nudCols
            // 
            this.nudCols.Location = new System.Drawing.Point(112, 30);
            this.nudCols.Name = "nudCols";
            this.nudCols.Size = new System.Drawing.Size(40, 20);
            this.nudCols.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(3, 79);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(27, 13);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Title";
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(3, 53);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(34, 13);
            this.lblRows.TabIndex = 2;
            this.lblRows.Text = "Rows";
            // 
            // comboRotationalSymmetry
            // 
            this.comboRotationalSymmetry.FormattingEnabled = true;
            this.comboRotationalSymmetry.Items.AddRange(new object[] {
            "1-fold",
            "2-fold",
            "4-fold"});
            this.comboRotationalSymmetry.Location = new System.Drawing.Point(112, 3);
            this.comboRotationalSymmetry.Name = "comboRotationalSymmetry";
            this.comboRotationalSymmetry.Size = new System.Drawing.Size(121, 21);
            this.comboRotationalSymmetry.TabIndex = 6;
            // 
            // lblCols
            // 
            this.lblCols.AutoSize = true;
            this.lblCols.Location = new System.Drawing.Point(3, 27);
            this.lblCols.Name = "lblCols";
            this.lblCols.Size = new System.Drawing.Size(47, 13);
            this.lblCols.TabIndex = 0;
            this.lblCols.Text = "Columns";
            // 
            // lblRotationalSymmetry
            // 
            this.lblRotationalSymmetry.AutoSize = true;
            this.lblRotationalSymmetry.Location = new System.Drawing.Point(3, 0);
            this.lblRotationalSymmetry.Name = "lblRotationalSymmetry";
            this.lblRotationalSymmetry.Size = new System.Drawing.Size(103, 13);
            this.lblRotationalSymmetry.TabIndex = 7;
            this.lblRotationalSymmetry.Text = "Rotational Symmetry";
            // 
            // CrosswordSettings
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(413, 261);
            this.Controls.Add(this.tlpAllSettings);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CrosswordSettings";
            this.Text = "Crossword Settings";
            this.tlpAllSettings.ResumeLayout(false);
            this.tlpAllSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCols)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.TableLayoutPanel tlpAllSettings;
        private System.Windows.Forms.Label lblCols;
        private System.Windows.Forms.NumericUpDown nudCols;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.NumericUpDown nudRows;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.ComboBox comboRotationalSymmetry;
        private System.Windows.Forms.Label lblRotationalSymmetry;
    }
}