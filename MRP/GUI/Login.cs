using System;
using System.Linq;
using System.Windows.Forms;
using MRP.Entities;

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
            if (radTextBox1.Text.Length == 0 || radButton2.Text.Length == 0)
            {
                MessageBox.Show(@"Введите логин и пароль");
            }

            using (var db = new DataContext())
            {
                var foundUser = db.Users.FirstOrDefault(x => x.Name == radTextBox1.Text && x.Password == radTextBox2.Text);
                if (foundUser == null)
                {
                    MessageBox.Show(@"Пользователь не найден");
                    return;
                }

                var args = new LoginEventArgs
                {
                    UserName = foundUser.Name,
                    UserRole = foundUser.Role
                };

                OnUserAuthorized?.Invoke(this, args);
                Close();
            }
        }
    }

    public class LoginEventArgs : EventArgs
    {
        public string UserName { get; set; }
        public Role UserRole { get; set; }
    }
}
