using System;
using Telerik.WinControls;

namespace MRP.GUI
{
    public partial class MainForm : Telerik.WinControls.UI.RadForm
    {
        public MainForm()
        {
            InitializeComponent();
            ThemeResolutionService.ApplicationThemeName = "Material";
        }

#region Forms

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void About_Click(object sender, EventArgs e)
        {
            new About().Show();
        }

#endregion

        private void Specification_Click(object sender, EventArgs e)
        {
            new Specification().Show();
        }

        private void MainProductPlanning_Click(object sender, EventArgs e)
        {
            new MainScheduler().Show();
        }

        private void Store_Click(object sender, EventArgs e)
        {
            new Store().Show();
        }

        private void Time_Click(object sender, EventArgs e)
        {
            new TimeProduction().Show();
        }

        private void LotSize_Click(object sender, EventArgs e)
        {
            new LotSizeComponent().Show();
        }

        private void PlanningMRP_Click(object sender, EventArgs e)
        {
            new Mrp().Show();
        }

        private void ДобавлениеЗаказаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Order().Show();
        }
    }
}
