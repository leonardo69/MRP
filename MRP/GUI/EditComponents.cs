using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using MRP.Entities;
using Telerik.WinControls.UI;

namespace MRP.GUI
{
    public partial class EditComponents : RadForm
    {
        private int _selectedComponentId;

        public EditComponents()
        {
            InitializeComponent();
            LoadComponents();
            listView1.HideSelection = false;
        }

        private void LoadComponents()
        {
            using (var db = new DataContext())
            {
                var allComponents = db.Components.ToList();
                if (allComponents.Count <= 0) return;
                foreach (var x in allComponents)
                    listView1.Items.Add(new ListViewItem
                    {
                        Text = x.Name,
                        Tag = x.Id
                    });
            }
        }

        private void ComponentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) return;

            _selectedComponentId = (int) listView1.SelectedItems[0].Tag;

            using (var db = new DataContext())
            {
                var currentComponent = db.Components.FirstOrDefault(x => x.Id == _selectedComponentId);
                if (currentComponent != null)
                {
                    textBox1.Text = currentComponent.Volume;
                    textBox2.Text = currentComponent.ExecutionTime?.ToString();
                    textBox3.Text = currentComponent.CountInStore?.ToString();
                }
            }

            LoadMainTable();
        }

        private void LoadMainTable()
        {
            using (var db = new DataContext())
            {
                var allComponents = db.Components.ToList();

                var allComponentsTable = new DataTable("AllComponents");
                DataColumn[] columns =
                {
                    new DataColumn {ColumnName = "Name", Caption = "Наименование"},
                    new DataColumn {ColumnName = "Unit", Caption = "Единица измерения"},
                    new DataColumn {ColumnName = "CountInStore", Caption = "Количество на складе"},
                    new DataColumn {ColumnName = "Volume", Caption = "Объём партии"},
                    new DataColumn {ColumnName = "TimeExecute", Caption = "Время выполнения"},
                    new DataColumn {ColumnName = "Assembly", Caption = "Сборка"}
                };

                allComponentsTable.Columns.AddRange(columns);

                foreach (var component in allComponents)
                {
                    var items = new object[]
                    {
                        component.Name,
                        component.Unit,
                        component.CountInStore,
                        component.Volume,
                        component.ExecutionTime,
                        component.AssemblyId
                    };

                    var row = allComponentsTable.NewRow();
                    allComponentsTable.Rows.Add(row.ItemArray = items);
                }

                radGridView1.DataSource = allComponentsTable;
                radGridView1.BestFitColumns();
            }
        }

        private void UpdateComponent_Click(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var currentComponent = db.Components.FirstOrDefault(x => x.Id == _selectedComponentId);
                if (currentComponent == null) return;

                currentComponent.Volume = textBox1.Text;

                if (int.TryParse(textBox2.Text, out var executionTime)) currentComponent.ExecutionTime = executionTime;

                if (int.TryParse(textBox3.Text, out var countInStore)) currentComponent.CountInStore = countInStore;

                db.Entry(currentComponent).State = EntityState.Modified;
                db.SaveChanges();
            }

            LoadMainTable();
        }
    }
}