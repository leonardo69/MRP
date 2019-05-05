using System;

namespace MRP.GUI
{
    public partial class Login : Telerik.WinControls.UI.RadForm
    {
        public event EventHandler<LoginEventArgs> OnUserAuthorized ;

        public Login()
        {
            InitializeComponent();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            var args = new LoginEventArgs
            {
                UserName = "Alex",
                UserRole = int.Parse(radTextBox1.Text)
            };

            OnUserAuthorized?.Invoke(this, args);
            Close();
            //login
            // Close()
        }
    }

    public class LoginEventArgs : EventArgs
    {
        public string UserName { get; set; }
        public int UserRole { get; set; }
    }
}
