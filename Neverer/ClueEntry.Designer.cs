namespace Neverer
{
    partial class ClueEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClueEntry));
            this.lblAnswer = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudCol = new System.Windows.Forms.NumericUpDown();
            this.nudRow = new System.Windows.Forms.NumericUpDown();
            this.comboOrientation = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAnswer
            // 
            this.lblAnswer.AutoSize = true;
            this.lblAnswer.Location = new System.Drawing.Point(12, 9);
            this.lblAnswer.Name = "lblAnswer";
            this.lblAnswer.Size = new System.Drawing.Size(42, 13);
            this.lblAnswer.TabIndex = 0;
            this.lblAnswer.Text = "Answer";
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Location = new System.Drawing.Point(12, 35);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(49, 13);
            this.lblQuestion.TabIndex = 2;
            this.lblQuestion.Text = "Question";
            // 
            // txtAnswer
            // 
            this.txtAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAnswer.Location = new System.Drawing.Point(67, 6);
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(381, 20);
            this.txtAnswer.TabIndex = 1;
            this.txtAnswer.TextChanged += new System.EventHandler(this.txtAnswer_TextChanged);
            this.txtAnswer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAnswer_KeyPress);
            // 
            // txtQuestion
            // 
            this.txtQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuestion.Location = new System.Drawing.Point(67, 32);
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(381, 20);
            this.txtQuestion.TabIndex = 3;
            this.txtQuestion.TextChanged += new System.EventHandler(this.txtQuestion_TextChanged);
            this.txtQuestion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuestion_KeyPress);
            // 
            // txtPreview
            // 
            this.txtPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPreview.BackColor = System.Drawing.SystemColors.Control;
            this.txtPreview.Location = new System.Drawing.Point(15, 58);
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ReadOnly = true;
            this.txtPreview.Size = new System.Drawing.Size(433, 20);
            this.txtPreview.TabIndex = 4;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(15, 111);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 11;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(373, 111);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 10;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Starting column";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Starting row";
            // 
            // nudCol
            // 
            this.nudCol.Location = new System.Drawing.Point(98, 85);
            this.nudCol.Name = "nudCol";
            this.nudCol.Size = new System.Drawing.Size(45, 20);
            this.nudCol.TabIndex = 6;
            this.nudCol.ValueChanged += new System.EventHandler(this.nudCol_ValueChanged);
            this.nudCol.Enter += new System.EventHandler(this.nudCol_Enter);
            // 
            // nudRow
            // 
            this.nudRow.Location = new System.Drawing.Point(238, 85);
            this.nudRow.Name = "nudRow";
            this.nudRow.Size = new System.Drawing.Size(45, 20);
            this.nudRow.TabIndex = 8;
            this.nudRow.ValueChanged += new System.EventHandler(this.nudRow_ValueChanged);
            this.nudRow.Enter += new System.EventHandler(this.nudRow_Enter);
            // 
            // comboOrientation
            // 
            this.comboOrientation.FormattingEnabled = true;
            this.comboOrientation.Items.AddRange(new object[] {
            "Across",
            "Down"});
            this.comboOrientation.Location = new System.Drawing.Point(327, 84);
            this.comboOrientation.Name = "comboOrientation";
            this.comboOrientation.Size = new System.Drawing.Size(121, 21);
            this.comboOrientation.TabIndex = 9;
            this.comboOrientation.SelectedIndexChanged += new System.EventHandler(this.comboOrientation_SelectedIndexChanged);
            this.comboOrientation.TextUpdate += new System.EventHandler(this.comboOrientation_TextUpdate);
            // 
            // ClueEntry
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(460, 146);
            this.Controls.Add(this.comboOrientation);
            this.Controls.Add(this.nudRow);
            this.Controls.Add(this.nudCol);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.txtPreview);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.txtAnswer);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.lblAnswer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClueEntry";
            this.Text = "ClueEntry";
            ((System.ComponentModel.ISupportInitialize)(this.nudCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAnswer;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.TextBox txtPreview;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudCol;
        private System.Windows.Forms.NumericUpDown nudRow;
        private System.Windows.Forms.ComboBox comboOrientation;
    }
}