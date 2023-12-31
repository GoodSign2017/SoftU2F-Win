using System;
using System.Windows.Forms;

namespace SoftU2FDaemon
{
    public partial class KeyRename : Form
    {
        public string RenamedValue { get; set; }

        public KeyRename(string value)
        {
            InitializeComponent();
            RenameBox.Text = RenamedValue = value;
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            RenamedValue = RenameBox.Text;
            Close();
        }
    }
}
