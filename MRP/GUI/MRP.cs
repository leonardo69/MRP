using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MRP.Model;

namespace MRP.GUI
{
    public partial class Mrp : Telerik.WinControls.UI.RadForm
    {
        private Manager _manager;
        private List<ComponentReport> _reportComponent;

        public Mrp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Грузим данные
            _manager = new Manager();
            _manager.LoadDataFromDb();
            _manager.AnalyseData();
            _reportComponent = _manager.MakeReport();

            MessageBox.Show(@"Вычисления завершены. Выберите компонент для производства");
            comboBox1.Enabled = true;

            //Догружаем выборку в combobox
            foreach (var report in _reportComponent) comboBox1.Items.Add(report.NameComponent);

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _reportComponent[comboBox1.SelectedIndex].Results;
            dataGridView1.Columns[0].HeaderText = @"Неделя 1";
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].HeaderText = @"Неделя 2";
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].HeaderText = @"Неделя 3";
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].HeaderText = @"Неделя 4";
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].HeaderText = @"Неделя 5";
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].HeaderText = @"Неделя 6";
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[6].HeaderText = @"Неделя 7";
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[7].HeaderText = @"Неделя 8";
            dataGridView1.Columns[7].Width = 80;
            dataGridView1.Columns[8].HeaderText = @"Неделя 9";
            dataGridView1.Columns[8].Width = 80;


            dataGridView1.Rows[0].HeaderCell.Value = "Валовые требования";
            dataGridView1.Rows[1].HeaderCell.Value = "Поступления планового заказа";
            dataGridView1.Rows[2].HeaderCell.Value = "Доступно на складе";
            dataGridView1.Rows[3].HeaderCell.Value = "Планируемые выпуски заказа";
            dataGridView1.AutoResizeRowHeadersWidth(0, DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            textBox1.Text = _reportComponent[comboBox1.SelectedIndex].LeadTime;
            textBox2.Text = _reportComponent[comboBox1.SelectedIndex].AvailableBalance;
            textBox4.Text = _reportComponent[comboBox1.SelectedIndex].LotSize;
        }

        private void MRP_Load(object sender, EventArgs e)
        {
        }
    }
}