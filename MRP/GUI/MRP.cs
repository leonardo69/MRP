using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;
using Kurswork.Model;

namespace Kurswork.GUI
{
    public partial class MRP : Form
    {
        public MRP()
        {
            InitializeComponent();
        }

        Manager manager;
        List<ComponentReport> reportComponent;
        private void button1_Click(object sender, EventArgs e)
        {
            //Грузим данные
            manager = new Manager();
            manager.LoadDataFromDB();
            manager.AnalyseData();
            reportComponent = manager.MakeReport();

            MessageBox.Show("Вычисления завершены. Выберите компонент для производства");
            comboBox1.Enabled = true;

            //Догружаем выборку в combobox
            foreach(ComponentReport report in reportComponent)
            {
                comboBox1.Items.Add(report.NameComponent);
            }


            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = reportComponent[comboBox1.SelectedIndex].Results;
            dataGridView1.Columns[0].HeaderText = "Неделя 1";
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].HeaderText = "Неделя 2";
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].HeaderText = "Неделя 3";
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].HeaderText = "Неделя 4";
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].HeaderText = "Неделя 5";
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].HeaderText = "Неделя 6";
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[6].HeaderText = "Неделя 7";
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[7].HeaderText = "Неделя 8";
            dataGridView1.Columns[7].Width = 80;
            dataGridView1.Columns[8].HeaderText = "Неделя 9";
            dataGridView1.Columns[8].Width = 80;

             
            dataGridView1.Rows[0].HeaderCell.Value = "Валовые требования";
            dataGridView1.Rows[1].HeaderCell.Value = "Поступления планового заказа";
            dataGridView1.Rows[2].HeaderCell.Value = "Доступно на складе";
            dataGridView1.Rows[3].HeaderCell.Value = "Планируемые выпуски заказа";
            dataGridView1.AutoResizeRowHeadersWidth(0, DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            textBox1.Text = reportComponent[comboBox1.SelectedIndex].Leadtime;
            textBox2.Text = reportComponent[comboBox1.SelectedIndex].AvaibleBalance;
            textBox4.Text = reportComponent[comboBox1.SelectedIndex].LotSize;
        }

        private void MRP_Load(object sender, EventArgs e)
        {

        }
    }
}
