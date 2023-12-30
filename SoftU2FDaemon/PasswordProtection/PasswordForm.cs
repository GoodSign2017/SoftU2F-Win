using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SoftU2FDaemon.PasswordProtection
{
    public partial class PasswordForm : Form
    {
        private const char SECRET_CHAR = '●';

        private readonly SecureString _secret = new();
        public byte[] Salt { get; set; } = RandomNumberGenerator.GetBytes(64);

        private bool _allowReveal = true;
        public bool AllowReveal
        {
            get { return _allowReveal; }
            set { _allowReveal = value; InitLinkReveal(); }
        }

        public PasswordForm()
        {
            InitializeComponent();
            InitLinkReveal();
            Focus();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Typing and pasting events

        private void PasswordBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                // intercept user typing and type into SecureString _secret instead of PasswordFormTextBox
                var changeStart = PasswordBox.SelectionStart;
                _secret.InsertAt(changeStart, e.KeyChar);
                // replace the event character with ● even before it gets handled by PasswordFormTextBox
                e.KeyChar = SECRET_CHAR;
                ChangedAtEvent?.Invoke(this, new() { ChangedStart = changeStart });
            }
        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {
            // should not contain non-hidden chars, parts or whole passwords in *most* cases (typing, paste)
            var chars = PasswordBox.Text.ToCharArray();
            var lenDiff = chars.Length - _secret.Length;
            var changeEnd = PasswordBox.SelectionStart;
            var changeStart = changeEnd;

            // if there are other chars than ●, then there are non-hidden password chars
            // should be skipped in *most* cases (typing, paste)
            while (changeStart > 0 && chars[changeStart - 1] != SECRET_CHAR)
            {
                --changeStart;
            }

            for (; lenDiff > 0; --lenDiff)
            {
                _secret.InsertAt(changeStart, SECRET_CHAR);
            }

            // should handle backspace and delete
            for (; lenDiff < 0; ++lenDiff)
            {
                _secret.RemoveAt(changeStart);
            }

            // if there were non-hidden password chars, hide now
            for (var i = changeStart; i < changeEnd; ++i)
            {
                _secret.SetAt(i, chars[i]);
                chars[i] = SECRET_CHAR;
            }

            // guaranteed to not contain non-hidden chars, parts or whole passwords
            PasswordBox.Text = new string(chars);
            PasswordBox.SelectionStart = changeEnd;
            ChangedAtEvent?.Invoke(this, new() { ChangedStart = changeStart });
            ButtonOk.Enabled = chars.Length > 10;
        }

        private void PasswordBox_Pasting(object sender, PastingEventArgs e)
        {
            // contains parts or whole passwords, does not decrease security more than clipboard
            var toPaste = Clipboard.GetText().ToCharArray();

            // PasswordFormTextBox.Text property doesn't contain passwords, only a string of ● symbols
            // Get selection bounds so we can know which characters in _secret to replaces from toPaste
            var changeStart = PasswordBox.SelectionStart;
            var changeEnd = changeStart + PasswordBox.SelectionLength - 1;

            var pasteLength = toPaste.Length;
            var newEnd = changeStart + pasteLength;

            // paste into _secret without exposing to PasswordFormTextBox
            for (var i = 0; i < pasteLength; ++i)
            {
                var pos = changeStart + i;
                if (pos <= changeEnd)
                {
                    _secret.SetAt(pos, toPaste[i]);
                }
                else
                {
                    _secret.InsertAt(pos, toPaste[i]);
                }

                // replace already pasted chars with ● symbols in memory
                toPaste[i] = SECRET_CHAR;
            }

            // If pasted less that was selected
            for (var i = newEnd; i < changeEnd; ++i)
            {
                _secret.RemoveAt(newEnd);
            }

            // Paste a string of ● symbols into PasswordFormTextBox
            PasswordBox.SelectedText = new string(toPaste);

            // Prevent default paste into PasswordFormTextBox behaviour
            e.Handled = true;
            ChangedAtEvent?.Invoke(this, new() { ChangedStart = changeStart });
        }

        #endregion

        #region Reveal link

        private void InitLinkReveal()
        {
            LinkReveal.Visible = _allowReveal;
        }

        private void LinkReveal_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_allowReveal)
            {
                return;
            }

            var len = _secret.Length;
            var toHide = new char[len];
            for (var i = 0; i < len; ++i)
            {
                toHide[i] = SECRET_CHAR;
            }
            PasswordBox.PasswordChar = SECRET_CHAR;
            PasswordBox.Text = new string(toHide);
            GC.Collect();
        }

        private void LinkReveal_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_allowReveal)
            {
                return;
            }

            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(_secret);
                PasswordBox.PasswordChar = '\0';
                PasswordBox.Text = Marshal.PtrToStringUni(valuePtr);
                LinkReveal.LinkVisited = true;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        #endregion
    }
}
