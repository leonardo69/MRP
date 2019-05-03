using System;
using System.Data;
using System.Windows.Forms;
using MRP.Core;

namespace MRP.GUI
{
    public partial class MainScheduler : Form
    {
        public MainScheduler()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            string selectQuery = "SELECT Глав_производ_план.Неделя1, Глав_производ_план.Неделя2, Глав_производ_план.Неделя3, Глав_производ_план.Неделя4, " +
                                "Глав_производ_план.Неделя5, Глав_производ_план.Неделя6, Глав_производ_план.Неделя7, Глав_производ_план.Неделя8, " +
                                "Глав_производ_план.Неделя9 FROM Глав_производ_план;";
            DataTable table = DbAccess.ExecuteDataTable(selectQuery);
            dataGridView1.DataSource = table;

            textBox1.Text = table.Rows[0][0].ToString();
            textBox2.Text = table.Rows[0][1].ToString();
            textBox3.Text = table.Rows[0][2].ToString();
            textBox4.Text = table.Rows[0][3].ToString();
            textBox5.Text = table.Rows[0][4].ToString();
            textBox6.Text = table.Rows[0][5].ToString();
            textBox7.Text = table.Rows[0][6].ToString();
            textBox8.Text = table.Rows[0][7].ToString();
            textBox9.Text = table.Rows[0][8].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE Глав_производ_план SET Глав_производ_план.Неделя1 = "+textBox1.Text+", Глав_производ_план.Неделя2 = "+textBox2.Text+", Глав_производ_план.Неделя3 = "+textBox3.Text+", Глав_производ_план.Неделя4 = "+textBox4.Text+", Глав_производ_план.Неделя5 = "+textBox5.Text+", Глав_производ_план.Неделя6 = "+textBox6.Text+", Глав_производ_план.Неделя7 = "+textBox7.Text+", Глав_производ_план.Неделя8 = "+textBox8.Text+", Глав_производ_план.Неделя9 = "+textBox9.Text+" " 
            +"WHERE (((Глав_производ_план.ID_компонента)=1)); ";
            DbAccess.ExecuteNonQuery(updateQuery);
            LoadData();
        }
    }
}
