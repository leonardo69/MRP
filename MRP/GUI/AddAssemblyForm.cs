using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace MRP.GUI
{
    public partial class AddAssemblyForm : RadForm
    {
        public AddAssemblyForm()
        {
            InitializeComponent();
        }

        public EventHandler<AddAssemblyEventArgs> OnAddNewAssembly { get; set; }

        private void radButton1_Click(object sender, EventArgs e)
        {
            if (radTextBox1.Text.Length == 0)
            {
                MessageBox.Show(@"Введите название сборки");
                return;
            }

            OnAddNewAssembly?.Invoke(this, new AddAssemblyEventArgs
            {
                AssemblyName = radTextBox1.Text
            });
            Close();
        }
    }

    public class AddAssemblyEventArgs : EventArgs
    {
        public string AssemblyName { get; set; }
    }
}