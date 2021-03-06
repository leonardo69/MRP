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
    public partial class Store : Form
    {
        public Store()
        {
            InitializeComponent();
            LoadData();
        }


        private void LoadData()
        {
            string loadQuery = "SELECT Склад.ID, Компоненты.Наименование, Склад.Кол_компонента "
            + "FROM Компоненты INNER JOIN Склад ON Компоненты.ID = Склад.ID_компонента;"; 
            DataTable table = DBAccess.ExecuteDataTable(loadQuery);
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
            string updateQuery = "UPDATE Склад SET Склад.Кол_компонента = \"" + volume + "\" WHERE (((Склад.ID_компонента)=" + id.ToString() + "));";
            DBAccess.ExecuteNonQuery(updateQuery);
        }

    }
}
