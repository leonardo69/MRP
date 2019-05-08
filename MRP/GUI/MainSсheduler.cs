using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using MRP.Entities;

namespace MRP.GUI
{
    public partial class MainScheduler : Telerik.WinControls.UI.RadForm
    {
        private int _selectedAssemblyId;

        public MainScheduler()
        {
            InitializeComponent();
            LoadAssemblies();
        }

        private void LoadAssemblies()
        {
            using (var db = new DataContext())
            {
                var assemblies = db.Specifications.ToList();
                if (assemblies.Count <= 0) return;

                foreach (var x in assemblies)
                {
                    listView1.Items.Add(new ListViewItem
                    {
                        Text = x.Name,
                        Tag = new SelectedAssembly
                        {
                            AssemblyId = x.Id
                        }
                    });
                }
            }
        }

        private void Assembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) return;

            var selectedAssembly = (SelectedAssembly)listView1.SelectedItems[0].Tag;
            _selectedAssemblyId = selectedAssembly.AssemblyId;

            using (var db = new DataContext())
            {
                var plan = db.MainPlans.Include(x=>x.Assembly).FirstOrDefault(x => x.Assembly.Id == _selectedAssemblyId);
                if (plan == null) return;

                textBox1.Text = plan.Week1.ToString();
                textBox2.Text = plan.Week2.ToString();
                textBox3.Text = plan.Week3.ToString();
                textBox4.Text = plan.Week4.ToString();
                textBox5.Text = plan.Week5.ToString();
                textBox6.Text = plan.Week6.ToString();
                textBox7.Text = plan.Week7.ToString();
                textBox8.Text = plan.Week8.ToString();
                textBox9.Text = plan.Week9.ToString();

                dataGridView1.DataSource = ShowPlanTable(plan);
            }
        }

        private DataTable ShowPlanTable(MainPlan plan)
        {
            var planTable = new DataTable("Plan");
            DataColumn[] columns = {
                new DataColumn {Caption = "Assembly", ColumnName = "Изделие"},
                new DataColumn {Caption = "Week1", ColumnName = "Неделя 1"},
                new DataColumn {Caption = "Week2", ColumnName = "Неделя 2"},
                new DataColumn {Caption = "Week3", ColumnName = "Неделя 3"},
                new DataColumn {Caption = "Week4", ColumnName = "Неделя 4"},
                new DataColumn {Caption = "Week5", ColumnName = "Неделя 5"},
                new DataColumn {Caption = "Week6", ColumnName = "Неделя 6"},
                new DataColumn {Caption = "Week7", ColumnName = "Неделя 7"},
                new DataColumn {Caption = "Week8", ColumnName = "Неделя 8"},
                new DataColumn {Caption = "Week9", ColumnName = "Неделя 9"}
            };
            planTable.Columns.AddRange(columns);
            var items = new object[]
            {
                plan.Assembly.Name,
                plan.Week1,
                plan.Week2,
                plan.Week3,
                plan.Week4,
                plan.Week5,
                plan.Week6,
                plan.Week7,
                plan.Week8,
                plan.Week9
            };

            var row = planTable.NewRow();
            planTable.Rows.Add(row.ItemArray = items);
            return planTable;
        }

        private void UpdatePlan_Click(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var plan = db.MainPlans.Include(x=>x.Assembly).FirstOrDefault(x => x.Assembly.Id == _selectedAssemblyId);
                if (plan == null) return;

                try
                {
                    plan.Week1 = int.Parse(textBox1.Text);
                    plan.Week2 = int.Parse(textBox2.Text);
                    plan.Week3 = int.Parse(textBox3.Text);
                    plan.Week4 = int.Parse(textBox4.Text);
                    plan.Week5 = int.Parse(textBox5.Text);
                    plan.Week6 = int.Parse(textBox6.Text);
                    plan.Week7 = int.Parse(textBox7.Text);
                    plan.Week8 = int.Parse(textBox8.Text);
                    plan.Week9 = int.Parse(textBox9.Text);

                    db.Entry(plan).State = EntityState.Modified;
                    db.SaveChanges();

                    dataGridView1.DataSource = ShowPlanTable(plan);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Проверьте входные данные");
                }
            }
        }

    }
}
