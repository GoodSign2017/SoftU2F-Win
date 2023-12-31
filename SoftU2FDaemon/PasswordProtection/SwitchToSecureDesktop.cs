using System;
using System.Windows.Forms;
using U2FLib.Security;

namespace SoftU2FDaemon.PasswordProtection
{
    public partial class SwitchToSecureDesktop : Form
    {
        private readonly Action switchCallback;

        public static async void CreateAsync(Action action)
        {
            using var secureDesktop = new SecureDesktop();
            using var switcher = new SwitchToSecureDesktop(secureDesktop.Switch);
            switcher.Show();
            await secureDesktop.Run(action).DoneTask();
        }

        public SwitchToSecureDesktop(Action switchCallback)
        {
            InitializeComponent();
            this.switchCallback = switchCallback;
        }

        private void SwitchLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switchCallback();
        }
    }
}
