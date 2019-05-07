using System;
using MRP.Admin;
using MRP.Entities;
using Telerik.WinControls;

namespace MRP.GUI
{
    public partial class MainForm : Telerik.WinControls.UI.RadForm
    {
        public MainForm()
        {
            InitializeComponent();
            ribbonTab1.IsSelected = true;
            var creator = new DatabaseCreator();
            creator.CreateUsers();
            creator.CreateComponents();
            setPermissionsForManager("RootManager");
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void radMenuItem7_Click(object sender, EventArgs e)
        {

        }

        private void radButtonElement4_Click(object sender, EventArgs e)
        {
            new MainScheduler().Show();
        }

        private void radButtonElement6_Click(object sender, EventArgs e)
        {
            new Store().Show();
        }

        private void radButtonElement7_Click(object sender, EventArgs e)
        {
            new TimeProduction().Show();
        }

        private void radButtonElement8_Click(object sender, EventArgs e)
        {
            new LotSizeComponent().Show();
        }

        private void radButtonElement5_Click(object sender, EventArgs e)
        {
            new Mrp().Show();
        }

        private void radMenuItem3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radButtonElement10_Click(object sender, EventArgs e)
        {
            new Order().Show();
        }

        private void radButtonElement9_Click(object sender, EventArgs e)
        {
            new About().Show();
        }

        /// <summary>
        /// Store
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElement13_Click(object sender, EventArgs e)
        {
            new Store().Show();
        }

        private void radButtonElement14_Click(object sender, EventArgs e)
        {
            new TimeProduction().Show();
        }

        private void radButtonElement15_Click(object sender, EventArgs e)
        {
            new LotSizeComponent().Show();
        }

        private void radButtonElement16_Click(object sender, EventArgs e)
        {
            new Mrp().Show();
        }

        private void radButtonElement12_Click(object sender, EventArgs e)
        {
            new MainScheduler().Show();
        }

        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            var loginForm = new Login();
            loginForm.OnUserAuthorized += setUserPermission; 
            loginForm.Show();

        }

        private void radMenuItem13_Click(object sender, EventArgs e)
        {

        }


        private void setUserPermission(object sender, LoginEventArgs e)
        {
            resetUserPermission();

            switch (e.UserRole)
            {
                case Role.Manager:
                    setPermissionsForManager(e.UserName);
                    break;
                case Role.Client:
                    setPermissionsForClient(e.UserName);
                    break;
                case Role.Admin:
                    setPermissionsForAdmin(e.UserName);
                    break;
                default:
                    resetUserPermission();
                    break;
            }
        }

        private void setPermissionsForManager(string managerName)
        {
            radLabelElement1.Text = @"Добро пожаловать, менеджер " + managerName;
            ribbonTab2.Visibility = ElementVisibility.Visible;
            ribbonTab5.Visibility = ElementVisibility.Collapsed;
            ribbonTab6.Visibility = ElementVisibility.Collapsed;
            ribbonTab2.IsSelected = true;
            radMenuItem7.Visibility = ElementVisibility.Visible;
            radMenuItem13.Visibility = ElementVisibility.Collapsed;
            radMenuItem14.Visibility = ElementVisibility.Collapsed;
        }

        private void setPermissionsForClient(string clientName)
        {
            radLabelElement1.Text = @"Добро пожаловать, клиент " + clientName;
            ribbonTab2.Visibility = ElementVisibility.Collapsed;
            ribbonTab5.Visibility = ElementVisibility.Visible;
            ribbonTab6.Visibility = ElementVisibility.Collapsed;
            ribbonTab5.IsSelected = true;
            radMenuItem7.Visibility = ElementVisibility.Collapsed;
            radMenuItem13.Visibility = ElementVisibility.Visible;
            radMenuItem14.Visibility = ElementVisibility.Collapsed;
        }

        private void setPermissionsForAdmin(string adminName)
        {
            radLabelElement1.Text = @"Добро пожаловать, администратор " + adminName;
            ribbonTab2.Visibility = ElementVisibility.Collapsed;
            ribbonTab5.Visibility = ElementVisibility.Collapsed;
            ribbonTab6.Visibility = ElementVisibility.Visible;
            ribbonTab6.IsSelected = true;
            radMenuItem7.Visibility = ElementVisibility.Collapsed;
            radMenuItem13.Visibility = ElementVisibility.Collapsed;
            radMenuItem14.Visibility = ElementVisibility.Visible;
        }

        private void resetUserPermission()
        {
            radLabelElement1.Text = @"Aвторизуйтесь в системе для дальнейшей работы.";
            ribbonTab2.Visibility = ElementVisibility.Collapsed;
            ribbonTab5.Visibility = ElementVisibility.Collapsed;
            ribbonTab6.Visibility = ElementVisibility.Collapsed;
            ribbonTab1.IsSelected = true;
            radMenuItem7.Visibility = ElementVisibility.Collapsed;
            radMenuItem13.Visibility = ElementVisibility.Collapsed;
            radMenuItem14.Visibility = ElementVisibility.Collapsed;
        }

        private void radButtonElement11_Click(object sender, EventArgs e)
        {
            // logout
            //hide all tabs except about and authorization
            //hide start menu tabs
            resetUserPermission();
        }

        private void radButtonElement17_Click(object sender, EventArgs e)
        {
            var addUserForm = new AddUser();
            addUserForm.Show();
        }

        private void radButtonElement18_Click(object sender, EventArgs e)
        {
            var deleteUserForm = new DeleteUser();
            deleteUserForm.Show();
        }

        private void radButtonElement19_Click(object sender, EventArgs e)
        {
            var changeUserRoleForm = new ChangeUserRole();
            changeUserRoleForm.Show();
        }

        private void radButtonElement3_Click(object sender, EventArgs e)
        {
            var assemblyForm = new AssemblyForm();
            assemblyForm.Show();
        }
    }
}
