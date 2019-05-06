using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MRP.Entities;
using Telerik.WinControls;

namespace MRP.Admin
{
    public partial class ChangeUserRole : Telerik.WinControls.UI.RadForm
    {
        public ChangeUserRole()
        {
            InitializeComponent();
            LoadRoles();
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

        private void LoadRoles()
        {
            radDropDownList1.DataSource = Enum.GetValues(typeof(Role));
            if (radDropDownList1.Items.Count > 0)
            {
                radDropDownList1.SelectedIndex = 0;
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            var userId = radGridView1.CurrentRow.Cells[0].Value;
            if (userId == null) MessageBox.Show(@"Выберите пользователя в таблице");

            using (var db = new DataContext())
            {
                var userToChange = db.Users.FirstOrDefault(x => x.Id == (int)userId);
                if (userToChange != null)
                {
                    Enum.TryParse<Role>(radDropDownList1.SelectedValue.ToString(), out var selectedRole);
                    userToChange.Role = selectedRole;
                    db.Entry(userToChange).State = EntityState.Modified;
                    db.SaveChanges();
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show(@"Пользователь не найден");
                }
            }
        }
    }
}
