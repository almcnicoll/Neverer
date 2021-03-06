﻿namespace CrosswordControls
{
    partial class ClueDisplay
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMaster = new System.Windows.Forms.TableLayoutPanel();
            this.chkRowSelect = new System.Windows.Forms.CheckBox();
            this.lblClueNumber = new System.Windows.Forms.Label();
            this.lblClueText = new System.Windows.Forms.Label();
            this.lblStat = new System.Windows.Forms.Label();
            this.tlpMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMaster
            // 
            this.tlpMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMaster.ColumnCount = 3;
            this.tlpMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMaster.Controls.Add(this.chkRowSelect, 0, 0);
            this.tlpMaster.Controls.Add(this.lblClueNumber, 1, 0);
            this.tlpMaster.Controls.Add(this.lblClueText, 2, 0);
            this.tlpMaster.Location = new System.Drawing.Point(0, 0);
            this.tlpMaster.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMaster.Name = "tlpMaster";
            this.tlpMaster.RowCount = 1;
            this.tlpMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMaster.Size = new System.Drawing.Size(345, 24);
            this.tlpMaster.TabIndex = 3;
            // 
            // chkRowSelect
            // 
            this.chkRowSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkRowSelect.AutoSize = true;
            this.chkRowSelect.Location = new System.Drawing.Point(3, 3);
            this.chkRowSelect.Name = "chkRowSelect";
            this.chkRowSelect.Size = new System.Drawing.Size(14, 18);
            this.chkRowSelect.TabIndex = 6;
            this.chkRowSelect.UseVisualStyleBackColor = true;
            this.chkRowSelect.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkRowSelect_MouseClick);
            // 
            // lblClueNumber
            // 
            this.lblClueNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClueNumber.AutoSize = true;
            this.lblClueNumber.Location = new System.Drawing.Point(23, 0);
            this.lblClueNumber.Name = "lblClueNumber";
            this.lblClueNumber.Size = new System.Drawing.Size(64, 24);
            this.lblClueNumber.TabIndex = 4;
            this.lblClueNumber.Text = "xx across";
            this.lblClueNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblClueNumber.DoubleClick += new System.EventHandler(this.lblClueNumber_DoubleClick);
            this.lblClueNumber.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblClueNumber_MouseClick);
            // 
            // lblClueText
            // 
            this.lblClueText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClueText.AutoSize = true;
            this.lblClueText.Location = new System.Drawing.Point(93, 0);
            this.lblClueText.Name = "lblClueText";
            this.lblClueText.Size = new System.Drawing.Size(249, 24);
            this.lblClueText.TabIndex = 5;
            this.lblClueText.Text = "clue text here";
            this.lblClueText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblClueText.UseMnemonic = false;
            this.lblClueText.DoubleClick += new System.EventHandler(this.lblClueText_DoubleClick);
            this.lblClueText.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblClueText_MouseClick);
            // 
            // lblStat
            // 
            this.lblStat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStat.BackColor = System.Drawing.SystemColors.ControlText;
            this.lblStat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStat.ForeColor = System.Drawing.SystemColors.Control;
            this.lblStat.Location = new System.Drawing.Point(309, 0);
            this.lblStat.Name = "lblStat";
            this.lblStat.Size = new System.Drawing.Size(33, 24);
            this.lblStat.TabIndex = 6;
            this.lblStat.Text = "...";
            this.lblStat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStat.UseMnemonic = false;
            this.lblStat.Visible = false;
            // 
            // ClueDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblStat);
            this.Controls.Add(this.tlpMaster);
            this.Name = "ClueDisplay";
            this.Size = new System.Drawing.Size(345, 24);
            this.tlpMaster.ResumeLayout(false);
            this.tlpMaster.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMaster;
        private System.Windows.Forms.Label lblClueText;
        private System.Windows.Forms.Label lblClueNumber;
        private System.Windows.Forms.CheckBox chkRowSelect;
        private System.Windows.Forms.Label lblStat;
    }
}
