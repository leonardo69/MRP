using System;
using System.Linq;
using System.Windows.Forms;
using MRP.Entities;

namespace MRP.Admin
{
    public partial class AddUser : Telerik.WinControls.UI.RadForm
    {
        public AddUser()
        {
            InitializeComponent();
            LoadRoles();
            LoadUsers();
        }

        private void LoadRoles()
        {
            radDropDownList1.DataSource = Enum.GetValues(typeof(Role));
            if (radDropDownList1.Items.Count > 0)
            {
                radDropDownList1.SelectedIndex = 0;
            }
        }

        private void LoadUsers()
        {
            using (var db = new UserContext())
            {
                var users = db.Users.ToList();
                radGridView1.DataSource = users;
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            if (radTextBox1.Text.Length == 0 || radTextBox2.Text.Length == 0)
            {
                MessageBox.Show(@"Заполните все поля");
            }

            using (var db = new UserContext())
            {
                Enum.TryParse<Role>(radDropDownList1.SelectedValue.ToString(), out var selectedRole);

                var user = new User
                {
                    Name = radTextBox1.Text,
                    Password = radTextBox2.Text,
                    Role = selectedRole
                };
                db.Users.Add(user);
                db.SaveChanges();
                LoadUsers();
            }
            
        }
    }
}
