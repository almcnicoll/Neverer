namespace Neverer
{
    partial class DictionaryManagement
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.clbDictionaries = new System.Windows.Forms.CheckedListBox();
            this.lblDictionaryList = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.flpAddButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdAddLocal = new System.Windows.Forms.Button();
            this.cmdAddNetwork = new System.Windows.Forms.Button();
            this.ofdOpenDictionary = new System.Windows.Forms.OpenFileDialog();
            this.tlpMain.SuspendLayout();
            this.flpAddButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.clbDictionaries, 0, 1);
            this.tlpMain.Controls.Add(this.lblDictionaryList, 0, 0);
            this.tlpMain.Controls.Add(this.cmdClose, 1, 2);
            this.tlpMain.Controls.Add(this.flpAddButtons, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 7;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(419, 351);
            this.tlpMain.TabIndex = 0;
            // 
            // clbDictionaries
            // 
            this.clbDictionaries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbDictionaries.FormattingEnabled = true;
            this.clbDictionaries.Location = new System.Drawing.Point(3, 16);
            this.clbDictionaries.Name = "clbDictionaries";
            this.clbDictionaries.Size = new System.Drawing.Size(203, 289);
            this.clbDictionaries.TabIndex = 0;
            // 
            // lblDictionaryList
            // 
            this.lblDictionaryList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDictionaryList.AutoSize = true;
            this.lblDictionaryList.Location = new System.Drawing.Point(3, 0);
            this.lblDictionaryList.Name = "lblDictionaryList";
            this.lblDictionaryList.Size = new System.Drawing.Size(203, 13);
            this.lblDictionaryList.TabIndex = 1;
            this.lblDictionaryList.Text = "Current Dictionaries";
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Location = new System.Drawing.Point(341, 311);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 34);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // flpAddButtons
            // 
            this.flpAddButtons.Controls.Add(this.cmdAddLocal);
            this.flpAddButtons.Controls.Add(this.cmdAddNetwork);
            this.flpAddButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAddButtons.Location = new System.Drawing.Point(0, 308);
            this.flpAddButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flpAddButtons.Name = "flpAddButtons";
            this.flpAddButtons.Size = new System.Drawing.Size(209, 40);
            this.flpAddButtons.TabIndex = 4;
            // 
            // cmdAddLocal
            // 
            this.cmdAddLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddLocal.Location = new System.Drawing.Point(3, 3);
            this.cmdAddLocal.Name = "cmdAddLocal";
            this.cmdAddLocal.Size = new System.Drawing.Size(98, 34);
            this.cmdAddLocal.TabIndex = 3;
            this.cmdAddLocal.Text = "Add Local...";
            this.cmdAddLocal.UseVisualStyleBackColor = true;
            this.cmdAddLocal.Click += new System.EventHandler(this.cmdAddLocal_Click);
            // 
            // cmdAddNetwork
            // 
            this.cmdAddNetwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddNetwork.Location = new System.Drawing.Point(107, 3);
            this.cmdAddNetwork.Name = "cmdAddNetwork";
            this.cmdAddNetwork.Size = new System.Drawing.Size(98, 34);
            this.cmdAddNetwork.TabIndex = 4;
            this.cmdAddNetwork.Text = "Add Remote...";
            this.cmdAddNetwork.UseVisualStyleBackColor = true;
            // 
            // ofdOpenDictionary
            // 
            this.ofdOpenDictionary.DefaultExt = "*.dic";
            this.ofdOpenDictionary.Multiselect = true;
            this.ofdOpenDictionary.Title = "Add dictionary";
            // 
            // DictionaryManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 351);
            this.Controls.Add(this.tlpMain);
            this.Name = "DictionaryManagement";
            this.Text = "Dictionary Manager";
            this.Load += new System.EventHandler(this.DictionaryManagement_Load);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.flpAddButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.CheckedListBox clbDictionaries;
        private System.Windows.Forms.Label lblDictionaryList;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.FlowLayoutPanel flpAddButtons;
        private System.Windows.Forms.Button cmdAddLocal;
        private System.Windows.Forms.Button cmdAddNetwork;
        private System.Windows.Forms.OpenFileDialog ofdOpenDictionary;
    }
}