namespace SoftU2FDaemon.PasswordProtection
{
    partial class PasswordForm
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
            components = new System.ComponentModel.Container();
            PasswordLabel = new System.Windows.Forms.Label();
            ButtonOk = new System.Windows.Forms.Button();
            ButtonCancel = new System.Windows.Forms.Button();
            PasswordBox = new PasswordTextBox();
            CbAddPepper = new System.Windows.Forms.CheckBox();
            EstimateTimer = new System.Windows.Forms.Timer(components);
            EstimationBox = new System.Windows.Forms.TextBox();
            ScoreIndicator = new System.Windows.Forms.Label();
            LinkReveal = new System.Windows.Forms.LinkLabel();
            SuspendLayout();
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.Location = new System.Drawing.Point(12, 9);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new System.Drawing.Size(90, 15);
            PasswordLabel.TabIndex = 0;
            PasswordLabel.Text = "Enter password:";
            // 
            // ButtonOk
            // 
            ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            ButtonOk.Enabled = false;
            ButtonOk.Location = new System.Drawing.Point(198, 56);
            ButtonOk.Name = "ButtonOk";
            ButtonOk.Size = new System.Drawing.Size(75, 23);
            ButtonOk.TabIndex = 2;
            ButtonOk.Text = "Hash";
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
            // PasswordBox
            // 
            PasswordBox.BackColor = System.Drawing.SystemColors.Window;
            PasswordBox.Location = new System.Drawing.Point(12, 27);
            PasswordBox.Name = "PasswordBox";
            PasswordBox.Size = new System.Drawing.Size(342, 23);
            PasswordBox.TabIndex = 0;
            PasswordBox.Pasting += PasswordBox_Pasting;
            PasswordBox.TextChanged += PasswordBox_TextChanged;
            PasswordBox.KeyPress += PasswordBox_KeyPress;
            // 
            // CbAddPepper
            // 
            CbAddPepper.AutoSize = true;
            CbAddPepper.Location = new System.Drawing.Point(12, 59);
            CbAddPepper.Name = "CbAddPepper";
            CbAddPepper.Size = new System.Drawing.Size(88, 19);
            CbAddPepper.TabIndex = 1;
            CbAddPepper.Text = "Add pepper";
            CbAddPepper.UseVisualStyleBackColor = true;
            // 
            // ValidateTimer
            // 
            EstimateTimer.Enabled = true;
            EstimateTimer.Tick += EstimateTimer_Tick;
            // 
            // ValidationBox
            // 
            EstimationBox.Location = new System.Drawing.Point(12, 95);
            EstimationBox.Multiline = true;
            EstimationBox.Name = "ValidationBox";
            EstimationBox.ReadOnly = true;
            EstimationBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            EstimationBox.Size = new System.Drawing.Size(342, 174);
            EstimationBox.TabIndex = 7;
            EstimationBox.Text = "The password will be estimated as you type";
            EstimationBox.Visible = false;
            // 
            // ScoreIndicator
            // 
            ScoreIndicator.BackColor = System.Drawing.Color.LightGray;
            ScoreIndicator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ScoreIndicator.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            ScoreIndicator.ForeColor = System.Drawing.Color.DimGray;
            ScoreIndicator.Location = new System.Drawing.Point(106, 56);
            ScoreIndicator.Margin = new System.Windows.Forms.Padding(3);
            ScoreIndicator.Name = "ScoreIndicator";
            ScoreIndicator.Size = new System.Drawing.Size(86, 23);
            ScoreIndicator.TabIndex = 8;
            ScoreIndicator.Text = "Score";
            ScoreIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            ScoreIndicator.Visible = false;
            // 
            // LinkReveal
            // 
            LinkReveal.BackColor = System.Drawing.SystemColors.Window;
            LinkReveal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LinkReveal.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            LinkReveal.Location = new System.Drawing.Point(334, 29);
            LinkReveal.Margin = new System.Windows.Forms.Padding(0);
            LinkReveal.Name = "LinkReveal";
            LinkReveal.Size = new System.Drawing.Size(17, 17);
            LinkReveal.TabIndex = 9;
            LinkReveal.TabStop = true;
            LinkReveal.Text = "👁";
            LinkReveal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            LinkReveal.Visible = false;
            LinkReveal.MouseDown += LinkReveal_MouseDown;
            LinkReveal.MouseUp += LinkReveal_MouseUp;
            // 
            // KeyPassword
            // 
            AcceptButton = ButtonOk;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = ButtonCancel;
            ClientSize = new System.Drawing.Size(366, 89);
            Controls.Add(LinkReveal);
            Controls.Add(ScoreIndicator);
            Controls.Add(EstimationBox);
            Controls.Add(CbAddPepper);
            Controls.Add(PasswordBox);
            Controls.Add(ButtonCancel);
            Controls.Add(ButtonOk);
            Controls.Add(PasswordLabel);
            ForeColor = System.Drawing.Color.Black;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "KeyPassword";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Password protection - SoftU2FDaemon";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private PasswordTextBox PasswordBox;
        private System.Windows.Forms.CheckBox CbAddPepper;
        private System.Windows.Forms.Timer EstimateTimer;
        private System.Windows.Forms.TextBox EstimationBox;
        private System.Windows.Forms.Label ScoreIndicator;
        private System.Windows.Forms.LinkLabel LinkReveal;
    }
}