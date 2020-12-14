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
            this.flpDictionaryAddButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdAddLocal = new System.Windows.Forms.Button();
            this.cmdAddNetwork = new System.Windows.Forms.Button();
            this.lblWordListList = new System.Windows.Forms.Label();
            this.clbWordLists = new System.Windows.Forms.CheckedListBox();
            this.flpWordListAddButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ofdOpenDictionary = new System.Windows.Forms.OpenFileDialog();
            this.tlpMain.SuspendLayout();
            this.flpDictionaryAddButtons.SuspendLayout();
            this.flpWordListAddButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.clbDictionaries, 0, 1);
            this.tlpMain.Controls.Add(this.lblDictionaryList, 0, 0);
            this.tlpMain.Controls.Add(this.flpDictionaryAddButtons, 0, 2);
            this.tlpMain.Controls.Add(this.lblWordListList, 1, 0);
            this.tlpMain.Controls.Add(this.clbWordLists, 1, 1);
            this.tlpMain.Controls.Add(this.flpWordListAddButtons, 1, 2);
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
            // flpDictionaryAddButtons
            // 
            this.flpDictionaryAddButtons.Controls.Add(this.cmdAddLocal);
            this.flpDictionaryAddButtons.Controls.Add(this.cmdAddNetwork);
            this.flpDictionaryAddButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDictionaryAddButtons.Location = new System.Drawing.Point(0, 308);
            this.flpDictionaryAddButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flpDictionaryAddButtons.Name = "flpDictionaryAddButtons";
            this.flpDictionaryAddButtons.Size = new System.Drawing.Size(209, 40);
            this.flpDictionaryAddButtons.TabIndex = 4;
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
            // lblWordListList
            // 
            this.lblWordListList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWordListList.AutoSize = true;
            this.lblWordListList.Location = new System.Drawing.Point(212, 0);
            this.lblWordListList.Name = "lblWordListList";
            this.lblWordListList.Size = new System.Drawing.Size(204, 13);
            this.lblWordListList.TabIndex = 1;
            this.lblWordListList.Text = "Current Word Lists";
            // 
            // clbWordLists
            // 
            this.clbWordLists.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbWordLists.FormattingEnabled = true;
            this.clbWordLists.Location = new System.Drawing.Point(212, 16);
            this.clbWordLists.Name = "clbWordLists";
            this.clbWordLists.Size = new System.Drawing.Size(204, 289);
            this.clbWordLists.TabIndex = 0;
            // 
            // flpWordListAddButtons
            // 
            this.flpWordListAddButtons.Controls.Add(this.button1);
            this.flpWordListAddButtons.Controls.Add(this.button2);
            this.flpWordListAddButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpWordListAddButtons.Location = new System.Drawing.Point(209, 308);
            this.flpWordListAddButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flpWordListAddButtons.Name = "flpWordListAddButtons";
            this.flpWordListAddButtons.Size = new System.Drawing.Size(210, 40);
            this.flpWordListAddButtons.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 34);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add Local...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(107, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 34);
            this.button2.TabIndex = 4;
            this.button2.Text = "Add Remote...";
            this.button2.UseVisualStyleBackColor = true;
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
            this.flpDictionaryAddButtons.ResumeLayout(false);
            this.flpWordListAddButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.CheckedListBox clbDictionaries;
        private System.Windows.Forms.Label lblDictionaryList;
        private System.Windows.Forms.FlowLayoutPanel flpDictionaryAddButtons;
        private System.Windows.Forms.Button cmdAddLocal;
        private System.Windows.Forms.Button cmdAddNetwork;
        private System.Windows.Forms.OpenFileDialog ofdOpenDictionary;
        private System.Windows.Forms.Label lblWordListList;
        private System.Windows.Forms.CheckedListBox clbWordLists;
        private System.Windows.Forms.FlowLayoutPanel flpWordListAddButtons;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}