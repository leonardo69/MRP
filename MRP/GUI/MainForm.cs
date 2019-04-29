using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kurswork.GUI;

namespace Kurswork
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
            this.Close();
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
            new MainSheduler().Show();
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
            new MRP().Show();
        }

        private void добавлениеЗаказаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Order().Show();
        }
    }
}
