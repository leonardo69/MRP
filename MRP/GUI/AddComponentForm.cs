using System;
using System.Windows.Forms;

namespace MRP.GUI
{
    public partial class AddComponentForm : Telerik.WinControls.UI.RadForm
    {
        public EventHandler<AddComponentEventArgs>  OnAddNewComponent { get; set; }
        public AddComponentForm()
        {
            InitializeComponent();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            if (radTextBox1.Text.Length == 0)
            {
                MessageBox.Show(@"Введите название компонента");
                return;
            }

            OnAddNewComponent?.Invoke(this, new AddComponentEventArgs
            {
                ComponentName = radTextBox1.Text
            });
            Close();
        }
    }

    public class AddComponentEventArgs : EventArgs
    {
        public string ComponentName { get; set; }
    }
}
