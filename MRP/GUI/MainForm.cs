using System;
using System.Windows.Forms;

namespace MRP.GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

#region Forms

        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void about_Click(object sender, EventArgs e)
        {
            new About().Show();
        }

#endregion

        private void specification_Click(object sender, EventArgs e)
        {
            new Specification().Show();
        }

        private void mainProductPlanning_Click(object sender, EventArgs e)
        {
            new MainScheduler().Show();
        }

        private void store_Click(object sender, EventArgs e)
        {
            new Store().Show();
        }

        private void time_Click(object sender, EventArgs e)
        {
            new TimeProduction().Show();
        }

        private void lotSize_Click(object sender, EventArgs e)
        {
            new LotSizeComponent().Show();
        }

        private void planningMRP_Click(object sender, EventArgs e)
        {
            new Mrp().Show();
        }

        private void добавлениеЗаказаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Order().Show();
        }
    }
}
