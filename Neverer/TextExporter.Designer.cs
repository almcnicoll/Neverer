
namespace Neverer
{
    partial class TextExporter
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
            this.txtSource = new System.Windows.Forms.TextBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.chkQuestions = new System.Windows.Forms.CheckBox();
            this.chkQuestionNumbers = new System.Windows.Forms.CheckBox();
            this.chkSolutions = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.Location = new System.Drawing.Point(12, 35);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(776, 374);
            this.txtSource.TabIndex = 0;
            this.txtSource.TabStop = false;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(12, 415);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdCopy
            // 
            this.cmdCopy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdCopy.Location = new System.Drawing.Point(361, 415);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(75, 23);
            this.cmdCopy.TabIndex = 2;
            this.cmdCopy.Text = "&Copy";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(713, 415);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 3;
            this.cmdSave.Text = "&Save...";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // chkQuestions
            // 
            this.chkQuestions.AutoSize = true;
            this.chkQuestions.Checked = true;
            this.chkQuestions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQuestions.Location = new System.Drawing.Point(12, 12);
            this.chkQuestions.Name = "chkQuestions";
            this.chkQuestions.Size = new System.Drawing.Size(73, 17);
            this.chkQuestions.TabIndex = 4;
            this.chkQuestions.Text = "Questions";
            this.chkQuestions.UseVisualStyleBackColor = true;
            this.chkQuestions.CheckedChanged += new System.EventHandler(this.chkQuestions_CheckedChanged);
            // 
            // chkQuestionNumbers
            // 
            this.chkQuestionNumbers.AutoSize = true;
            this.chkQuestionNumbers.Checked = true;
            this.chkQuestionNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQuestionNumbers.Location = new System.Drawing.Point(91, 12);
            this.chkQuestionNumbers.Name = "chkQuestionNumbers";
            this.chkQuestionNumbers.Size = new System.Drawing.Size(113, 17);
            this.chkQuestionNumbers.TabIndex = 5;
            this.chkQuestionNumbers.Text = "Question Numbers";
            this.chkQuestionNumbers.UseVisualStyleBackColor = true;
            this.chkQuestionNumbers.CheckedChanged += new System.EventHandler(this.chkQuestionNumbers_CheckedChanged);
            // 
            // chkSolutions
            // 
            this.chkSolutions.AutoSize = true;
            this.chkSolutions.Checked = true;
            this.chkSolutions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSolutions.Location = new System.Drawing.Point(210, 12);
            this.chkSolutions.Name = "chkSolutions";
            this.chkSolutions.Size = new System.Drawing.Size(69, 17);
            this.chkSolutions.TabIndex = 6;
            this.chkSolutions.Text = "Solutions";
            this.chkSolutions.UseVisualStyleBackColor = true;
            this.chkSolutions.CheckedChanged += new System.EventHandler(this.chkSolutions_CheckedChanged);
            // 
            // TextExporter
            // 
            this.AcceptButton = this.cmdSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chkSolutions);
            this.Controls.Add(this.chkQuestionNumbers);
            this.Controls.Add(this.chkQuestions);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdCopy);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.txtSource);
            this.Name = "TextExporter";
            this.Text = "Export as Text...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdCopy;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.CheckBox chkQuestions;
        private System.Windows.Forms.CheckBox chkQuestionNumbers;
        private System.Windows.Forms.CheckBox chkSolutions;
    }
}