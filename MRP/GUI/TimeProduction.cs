using System;
using System.Data;
using System.Windows.Forms;
using MRP.Core;

namespace MRP.GUI
{
    public partial class TimeProduction : Form
    {
        public TimeProduction()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            string loadQuery = "SELECT Время_выполнения_заказа.ID, Компоненты.Наименование, Время_выполнения_заказа.Время "
            +"FROM Компоненты INNER JOIN Время_выполнения_заказа ON Компоненты.ID = Время_выполнения_заказа.ID_компонента;";
            DataTable table = DbAccess.ExecuteDataTable(loadQuery);
            dataGridView1.DataSource = table;

            textBox1.Text = table.Rows[0][2].ToString();
            textBox2.Text = table.Rows[1][2].ToString();
            textBox3.Text = table.Rows[2][2].ToString();
            textBox4.Text = table.Rows[3][2].ToString();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            UpdateData(1, textBox1.Text);
            UpdateData(2, textBox2.Text);
            UpdateData(3, textBox3.Text);
            UpdateData(4, textBox4.Text);
            LoadData();
        }

        private void UpdateData(int id, string volume)
        {
            var updateQuery = "UPDATE Время_выполнения_заказа SET Время_выполнения_заказа.Время = " + volume + " WHERE (((Время_выполнения_заказа.ID_компонента)=" + id + "));";
            DbAccess.ExecuteNonQuery(updateQuery);
        }
    }
}
