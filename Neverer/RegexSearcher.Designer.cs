namespace Neverer
{
    partial class RegexSearcher
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
            this.lblSearchPattern = new System.Windows.Forms.Label();
            this.txtSearchPattern = new System.Windows.Forms.TextBox();
            this.lbResults = new System.Windows.Forms.ListBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.llRegexSyntax = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblSearchPattern
            // 
            this.lblSearchPattern.AutoSize = true;
            this.lblSearchPattern.Location = new System.Drawing.Point(12, 9);
            this.lblSearchPattern.Name = "lblSearchPattern";
            this.lblSearchPattern.Size = new System.Drawing.Size(78, 13);
            this.lblSearchPattern.TabIndex = 0;
            this.lblSearchPattern.Text = "Search Pattern";
            // 
            // txtSearchPattern
            // 
            this.txtSearchPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchPattern.Location = new System.Drawing.Point(96, 6);
            this.txtSearchPattern.Name = "txtSearchPattern";
            this.txtSearchPattern.Size = new System.Drawing.Size(222, 20);
            this.txtSearchPattern.TabIndex = 1;
            // 
            // lbResults
            // 
            this.lbResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbResults.FormattingEnabled = true;
            this.lbResults.Location = new System.Drawing.Point(12, 45);
            this.lbResults.Name = "lbResults";
            this.lbResults.Size = new System.Drawing.Size(361, 134);
            this.lbResults.TabIndex = 2;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Location = new System.Drawing.Point(324, 4);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(49, 23);
            this.cmdSearch.TabIndex = 3;
            this.cmdSearch.Text = "→";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // llRegexSyntax
            // 
            this.llRegexSyntax.AutoSize = true;
            this.llRegexSyntax.Location = new System.Drawing.Point(12, 29);
            this.llRegexSyntax.Name = "llRegexSyntax";
            this.llRegexSyntax.Size = new System.Drawing.Size(181, 13);
            this.llRegexSyntax.TabIndex = 4;
            this.llRegexSyntax.TabStop = true;
            this.llRegexSyntax.Text = "Help with regular expression syntax...";
            this.llRegexSyntax.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRegexSyntax_LinkClicked);
            // 
            // RegexSearcher
            // 
            this.AcceptButton = this.cmdSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 186);
            this.Controls.Add(this.llRegexSyntax);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.lbResults);
            this.Controls.Add(this.txtSearchPattern);
            this.Controls.Add(this.lblSearchPattern);
            this.Name = "RegexSearcher";
            this.Text = "Regular Expression Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSearchPattern;
        private System.Windows.Forms.TextBox txtSearchPattern;
        private System.Windows.Forms.ListBox lbResults;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.LinkLabel llRegexSyntax;
    }
}