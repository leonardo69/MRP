using System;
using Telerik.WinControls;

namespace MRP.GUI
{
    public partial class MainForm : Telerik.WinControls.UI.RadForm
    {
        public MainForm()
        {
            InitializeComponent();
            // ThemeResolutionService.ApplicationThemeName = "TelerikMetroBlue";
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

        private void radButtonElement2_Click(object sender, EventArgs e)
        {
            new Specification().Show();
        }

        /// <summary>
        /// Main scheduler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonTab3_Click(object sender, EventArgs e)
        {
          
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
            new Login().Show();
        }

        private void radMenuItem13_Click(object sender, EventArgs e)
        {

        }
    }
}
