using System;
using System.Drawing;
using System.Windows.Forms;
using U2FLib.Storage;

namespace SoftU2FDaemon
{
    public partial class KeyControl : Form
    {
        public KeyControl()
        {
            InitializeComponent();
            Icon = new Icon("tray.ico");
            InitKeys();
        }

        private void InitKeys()
        {
            keysBox.Items.Clear();
            using var context = new AppDbContext();
            foreach (var keyPair in context.KeyPairs)
            {
                keysBox.Items.Add(new KeyControlListItem(keyPair, keysBox));
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnRename_Click(object sender, EventArgs e)
        {
            var item = (KeyControlListItem)keysBox.SelectedItem;
            if (item != null)
            {
                item?.RenameKey();
                keysBox.Items[keysBox.SelectedIndex] = item;
            }
        }

        private void BtnDetails_Click(object sender, EventArgs e)
        {
            var item = (KeyControlListItem)keysBox.SelectedItem;
            item?.ShowDetails();
        }

        private void KeysBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = keysBox.SelectedItem != null;
            BtnRename.Enabled = selected;
            BtnDelete.Enabled = selected;
            BtnDetails.Enabled = selected;
        }

        private void KeysBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = (KeyControlListItem)keysBox.SelectedItem;
            item?.RenameKey();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var item = (KeyControlListItem)keysBox.SelectedItem;
            item?.DeleteKey();
        }

        private void KeysBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var item = (KeyControlListItem)keysBox.SelectedItem;
                item?.RenameKey();
            }
            else if (e.KeyCode == Keys.D)
            {
                var item = (KeyControlListItem)keysBox.SelectedItem;
                item?.ShowDetails();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                var item = (KeyControlListItem)keysBox.SelectedItem;
                item?.DeleteKey();
            }
        }
    }
    public class KeyControlListItem
    {
        private readonly KeyPair _keyPair;
        private readonly ListBox _keysBox;

        public KeyControlListItem(KeyPair keyPair, ListBox keysBox)
        {
            _keyPair = keyPair;
            _keysBox = keysBox;
        }

        public override string ToString()
        {
            var handle = Convert.ToHexString(Convert.FromBase64String(_keyPair.KeyHandle)[^4..]);
            return $"{_keyPair.Label} ({handle})";
        }

        public void ShowDetails()
        {
            var publicKey = Convert.ToHexString(_keyPair.PublicKey);
            var applicationTag = Convert.ToHexString(_keyPair.ApplicationTag);
            MessageBox.Show($"Public key:\n{publicKey}\n\nApplication tag:\n{applicationTag}", this.ToString());
        }

        public void RenameKey()
        {
            using var dialog = new KeyRename(_keyPair.Label);
            var result = dialog.ShowDialog();

            if (result != DialogResult.OK || dialog.RenamedValue == _keyPair.Label)
            {
                return;
            }

            using var context = new AppDbContext();
            _keyPair.Label = dialog.RenamedValue;
            context.Update(_keyPair);
            context.SaveChanges();
        }

        public void DeleteKey()
        {
            var confirm = MessageBox.Show(@"Do you want to delete the key?",
                @$"Delete {this}", MessageBoxButtons.YesNo);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            using var context = new AppDbContext();
            context.Remove(_keyPair);
            context.SaveChanges();
            _keysBox.Items.Remove(this);
        }
    }
}
