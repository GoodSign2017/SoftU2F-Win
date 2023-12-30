namespace SoftU2FDaemon
{
    partial class KeyControl
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
            keysBox = new System.Windows.Forms.ListBox();
            BtnRename = new System.Windows.Forms.Button();
            BtnDelete = new System.Windows.Forms.Button();
            BtnExport = new System.Windows.Forms.Button();
            BtnImport = new System.Windows.Forms.Button();
            BtnClose = new System.Windows.Forms.Button();
            BtnDetails = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // keysBox
            // 
            keysBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            keysBox.FormattingEnabled = true;
            keysBox.ItemHeight = 15;
            keysBox.Items.AddRange(new object[] { "Key1", "Key2", "Key3", "Key4", "Key5", "Key6", "Key7", "Key8", "Key9", "Key10", "Key11", "Key12", "Key13", "Key14", "Key15", "Key16", "Key17", "Key18", "Key19", "Key20", "Key21", "Key22", "Key23", "Key24", "Key25", "Key26", "Key27", "Key28", "Key29" });
            keysBox.Location = new System.Drawing.Point(12, 12);
            keysBox.Name = "keysBox";
            keysBox.Size = new System.Drawing.Size(697, 394);
            keysBox.TabIndex = 0;
            keysBox.SelectedIndexChanged += KeysBox_SelectedIndexChanged;
            keysBox.KeyDown += KeysBox_KeyDown;
            keysBox.MouseDoubleClick += KeysBox_MouseDoubleClick;
            // 
            // BtnRename
            // 
            BtnRename.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnRename.Enabled = false;
            BtnRename.Location = new System.Drawing.Point(174, 415);
            BtnRename.Name = "BtnRename";
            BtnRename.Size = new System.Drawing.Size(75, 23);
            BtnRename.TabIndex = 3;
            BtnRename.Text = "Rename";
            BtnRename.UseVisualStyleBackColor = true;
            BtnRename.Click += BtnRename_Click;
            // 
            // BtnDelete
            // 
            BtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnDelete.Enabled = false;
            BtnDelete.Location = new System.Drawing.Point(336, 415);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new System.Drawing.Size(75, 23);
            BtnDelete.TabIndex = 5;
            BtnDelete.Text = "Delete";
            BtnDelete.UseVisualStyleBackColor = true;
            BtnDelete.Click += BtnDelete_Click;
            // 
            // BtnExport
            // 
            BtnExport.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnExport.Enabled = false;
            BtnExport.Location = new System.Drawing.Point(12, 415);
            BtnExport.Name = "BtnExport";
            BtnExport.Size = new System.Drawing.Size(75, 23);
            BtnExport.TabIndex = 1;
            BtnExport.Text = "Export";
            BtnExport.UseVisualStyleBackColor = true;
            // 
            // BtnImport
            // 
            BtnImport.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnImport.Enabled = false;
            BtnImport.Location = new System.Drawing.Point(93, 415);
            BtnImport.Name = "BtnImport";
            BtnImport.Size = new System.Drawing.Size(75, 23);
            BtnImport.TabIndex = 2;
            BtnImport.Text = "Import";
            BtnImport.UseVisualStyleBackColor = true;
            // 
            // BtnClose
            // 
            BtnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            BtnClose.Location = new System.Drawing.Point(634, 415);
            BtnClose.Name = "BtnClose";
            BtnClose.Size = new System.Drawing.Size(75, 23);
            BtnClose.TabIndex = 6;
            BtnClose.Text = "Close";
            BtnClose.UseVisualStyleBackColor = true;
            BtnClose.Click += Close_Click;
            // 
            // BtnDetails
            // 
            BtnDetails.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnDetails.Enabled = false;
            BtnDetails.Location = new System.Drawing.Point(255, 415);
            BtnDetails.Name = "BtnDetails";
            BtnDetails.Size = new System.Drawing.Size(75, 23);
            BtnDetails.TabIndex = 4;
            BtnDetails.Text = "Details";
            BtnDetails.UseVisualStyleBackColor = true;
            BtnDetails.Click += BtnDetails_Click;
            // 
            // KeyControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = BtnClose;
            ClientSize = new System.Drawing.Size(721, 450);
            Controls.Add(BtnDetails);
            Controls.Add(BtnClose);
            Controls.Add(BtnImport);
            Controls.Add(BtnExport);
            Controls.Add(BtnDelete);
            Controls.Add(BtnRename);
            Controls.Add(keysBox);
            Name = "KeyControl";
            Text = "Keys - SoftU2F Daemon";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox keysBox;
        private System.Windows.Forms.Button BtnRename;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Button BtnImport;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnDetails;
    }
}