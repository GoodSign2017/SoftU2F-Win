using System;
using System.Windows.Forms;

namespace SoftU2FDaemon.PasswordProtection
{
    public class PasswordTextBox : TextBox
    {
        //
        // Summary:
        //     Occurs when the paste is performed into the text box
        public event EventHandler<PastingEventArgs> Pasting;

        protected override void WndProc(ref Message m)
        {
            // WM_PASTE
            if (m.Msg == 0x302)
            {
                var e = new PastingEventArgs();
                Pasting?.Invoke(this, e);
                if (e.Handled)
                {
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }

    public class PastingEventArgs : EventArgs
    {
        //
        // Summary:
        //     Gets or sets a value indicating whether the PasswordFormTextBox.Pasting
        //     event was handled.
        //
        // Returns:
        //     true if the event is handled; otherwise, false.
        public bool Handled { get; set; }
    }
}
