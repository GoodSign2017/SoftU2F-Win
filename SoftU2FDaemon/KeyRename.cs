using System;
using System.Windows.Forms;

namespace SoftU2FDaemon
{
    public partial class KeyRename : Form
    {
        public string renamedValue { get; set; }

        public KeyRename(string value)
        {
            InitializeComponent();
            RenameBox.Text = renamedValue = value;
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            renamedValue = RenameBox.Text;
            Close();
        }
    }
}
