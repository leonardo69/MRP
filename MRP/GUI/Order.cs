﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace Kurswork.GUI
{
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
            LoadData();
            LoadCombobox();
        }

        private void LoadCombobox()
        {
            string seluniv = "SELECT * FROM Заказчик;";
            DataTable qwerty = DBAccess.ExecuteDataTable(seluniv);
            comboBox1.DataSource = qwerty;
            comboBox1.ValueMember = qwerty.Columns[0].ColumnName;
            comboBox1.DisplayMember = qwerty.Columns[1].ColumnName;
            comboBox1.SelectedValue = qwerty.Columns[0].DefaultValue;
            comboBox1.Text = qwerty.Rows[0][1].ToString();
        }

        private void LoadData()
        {
            string loadQuery = "SELECT Договор.Код, Заказчик.Название, Договор.Неделя, Договор.Количество "
            +"FROM Заказчик INNER JOIN Договор ON Заказчик.Код = Договор.ID_заказчика;";
            DataTable table = DBAccess.ExecuteDataTable(loadQuery);
            dataGridView1.DataSource = table;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 110;
            dataGridView1.Columns[1].HeaderText = "Компания";
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[3].HeaderText = "Величина заказа";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //добавляем в таблицу заказов
            string addString = "INSERT INTO Договор ([ID_заказчика],[Неделя],[Количество]) " +
               " VALUES(" +
               comboBox1.SelectedValue + ",\"" +
               comboBox2.Text + "\",\"" +
               textBox1.Text + "\")";
            DBAccess.ExecuteNonQuery(addString);
            LoadData();

            //добавляем в план
            string selectValue = "SELECT Глав_производ_план.Неделя" + comboBox2.Text + " FROM Глав_производ_план WHERE (((Глав_производ_план.ID_компонента)=1));";
            int value = DBAccess.ExecuteScalar(selectValue);

            int newValue = value+int.Parse(textBox1.Text);

            string updateQuery = "UPDATE Глав_производ_план SET Глав_производ_план.Неделя" + comboBox2.Text + " = " + newValue.ToString() + " WHERE (((Глав_производ_план.ID_компонента)=1));";
            DBAccess.ExecuteNonQuery(updateQuery);
            //забираем значение, плюсуем и обновляем

        }
    }
}
