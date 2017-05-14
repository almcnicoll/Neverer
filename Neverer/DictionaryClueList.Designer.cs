namespace Neverer
{
    partial class DictionaryClueList
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
            this.cmdReplace = new System.Windows.Forms.Button();
            this.listAllClues = new System.Windows.Forms.ListBox();
            this.cmdAddNew = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdReplace
            // 
            this.cmdReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReplace.Location = new System.Drawing.Point(182, 226);
            this.cmdReplace.Name = "cmdReplace";
            this.cmdReplace.Size = new System.Drawing.Size(75, 23);
            this.cmdReplace.TabIndex = 0;
            this.cmdReplace.Text = "&Replace";
            this.cmdReplace.UseVisualStyleBackColor = true;
            this.cmdReplace.Click += new System.EventHandler(this.cmdReplace_Click);
            // 
            // listAllClues
            // 
            this.listAllClues.DisplayMember = "clueText";
            this.listAllClues.FormattingEnabled = true;
            this.listAllClues.Location = new System.Drawing.Point(12, 12);
            this.listAllClues.Name = "listAllClues";
            this.listAllClues.Size = new System.Drawing.Size(245, 212);
            this.listAllClues.TabIndex = 1;
            this.listAllClues.ValueMember = "id";
            // 
            // cmdAddNew
            // 
            this.cmdAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAddNew.Location = new System.Drawing.Point(12, 226);
            this.cmdAddNew.Name = "cmdAddNew";
            this.cmdAddNew.Size = new System.Drawing.Size(75, 23);
            this.cmdAddNew.TabIndex = 2;
            this.cmdAddNew.Text = "&Add New";
            this.cmdAddNew.UseVisualStyleBackColor = true;
            this.cmdAddNew.Click += new System.EventHandler(this.cmdAddNew_Click);
            // 
            // DictionaryClueList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 261);
            this.Controls.Add(this.cmdAddNew);
            this.Controls.Add(this.listAllClues);
            this.Controls.Add(this.cmdReplace);
            this.Name = "DictionaryClueList";
            this.Text = "DictionaryClueList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdReplace;
        private System.Windows.Forms.ListBox listAllClues;
        private System.Windows.Forms.Button cmdAddNew;
    }
}