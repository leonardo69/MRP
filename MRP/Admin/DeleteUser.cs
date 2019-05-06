using System;
using System.Linq;
using System.Windows.Forms;
using MRP.Entities;
using Telerik.WinControls.UI;

namespace MRP.Admin
{
    public partial class DeleteUser : RadForm
    {
        public DeleteUser()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var db = new DataContext())
            {
                var users = db.Users.ToList();
                radGridView1.DataSource = users;
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            var userId = radGridView1.CurrentRow.Cells[0].Value;
            if (userId == null) MessageBox.Show(@"Выберите пользователя в таблице");

            using (var db = new DataContext())
            {
                var userToDelete = db.Users.FirstOrDefault(x => x.Id == (int) userId);
                if (userToDelete != null)
                {
                    db.Users.Remove(userToDelete);
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show(@"Пользователь не найден");
                }
            }
        }

    }
}