namespace SoftU2FDaemon.PasswordProtection
{
    partial class SwitchToSecureDesktop
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
            switchLabel = new System.Windows.Forms.LinkLabel();
            SuspendLayout();
            // 
            // switchLabel
            // 
            switchLabel.AutoSize = true;
            switchLabel.Location = new System.Drawing.Point(12, 24);
            switchLabel.Name = "switchLabel";
            switchLabel.Size = new System.Drawing.Size(277, 30);
            switchLabel.TabIndex = 0;
            switchLabel.TabStop = true;
            switchLabel.Text = "password input is performed on the secure desktop\r\npress here to return if you see this window";
            switchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            switchLabel.LinkClicked += SwitchLabelClicked;
            // 
            // SwitchToSecureDesktop
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(301, 79);
            Controls.Add(switchLabel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SwitchToSecureDesktop";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Switch to Secure Desktop";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.LinkLabel switchLabel;
    }
}