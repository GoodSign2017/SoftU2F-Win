namespace SoftU2FDaemon
{
    partial class KeyRename
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
            RenameLabel = new System.Windows.Forms.Label();
            RenameBox = new System.Windows.Forms.TextBox();
            ButtonOk = new System.Windows.Forms.Button();
            ButtonCancel = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // RenameLabel
            // 
            RenameLabel.AutoSize = true;
            RenameLabel.Location = new System.Drawing.Point(12, 9);
            RenameLabel.Name = "RenameLabel";
            RenameLabel.Size = new System.Drawing.Size(81, 15);
            RenameLabel.TabIndex = 0;
            RenameLabel.Text = "Name for key:";
            // 
            // RenameBox
            // 
            RenameBox.Location = new System.Drawing.Point(12, 27);
            RenameBox.MaxLength = 128;
            RenameBox.Name = "RenameBox";
            RenameBox.Size = new System.Drawing.Size(342, 23);
            RenameBox.TabIndex = 1;
            // 
            // ButtonOk
            // 
            ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            ButtonOk.Location = new System.Drawing.Point(198, 56);
            ButtonOk.Name = "ButtonOk";
            ButtonOk.Size = new System.Drawing.Size(75, 23);
            ButtonOk.TabIndex = 2;
            ButtonOk.Text = "OK";
            ButtonOk.UseVisualStyleBackColor = true;
            ButtonOk.Click += ButtonOk_Click;
            // 
            // ButtonCancel
            // 
            ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            ButtonCancel.Location = new System.Drawing.Point(279, 56);
            ButtonCancel.Name = "ButtonCancel";
            ButtonCancel.Size = new System.Drawing.Size(75, 23);
            ButtonCancel.TabIndex = 3;
            ButtonCancel.Text = "Cancel";
            ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // KeyRename
            // 
            AcceptButton = ButtonOk;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = ButtonCancel;
            ClientSize = new System.Drawing.Size(366, 89);
            Controls.Add(ButtonCancel);
            Controls.Add(ButtonOk);
            Controls.Add(RenameBox);
            Controls.Add(RenameLabel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "KeyRename";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Rename key";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label RenameLabel;
        private System.Windows.Forms.TextBox RenameBox;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
    }
}