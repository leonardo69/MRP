using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using MRP.Entities;
using Telerik.WinControls.UI;

namespace MRP.GUI
{
    public partial class Order : RadForm
    {
        public Order()
        {
            InitializeComponent();
            LoadAssemblies();
            LoadOrders();
        }

        private void LoadOrders()
        {
            using (var db = new DataContext())
            {
                var orders = db.Orders.Include(x => x.Assembly)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.WeekNumber,
                        x.Count,
                        Assembly = x.Assembly.Name
                    }).ToList();
                ordersDgv.DataSource = orders;
                ordersDgv.Columns[1].HeaderText = @"Название";
                ordersDgv.Columns[2].HeaderText = @"Неделя";
                ordersDgv.Columns[3].HeaderText = @"Количество";
                ordersDgv.Columns[4].HeaderText = @"Изделие";
            }
        }

        private void LoadAssemblies()
        {
            using (var db = new DataContext())
            {
                var assemblies = db.Assemblies.ToList();
                assembliesDdl.ValueMember = "Id";
                assembliesDdl.DisplayMember = "Name";
                assembliesDdl.DataSource = assemblies;
            }
        }

        private bool ValidateNewOrderInfo()
        {
            return radTextBox1.Text.Length > 0 && assembliesDdl.SelectedValue != null && radTextBox2.Text.Length > 0;
        }

        private void addOrderBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateNewOrderInfo())
            {
                MessageBox.Show(@"Проверьте входные данные");
                return;
            }
            

            using (var db = new DataContext())
            {
                var assembly = db.Assemblies.FirstOrDefault(x => x.Id == (int) assembliesDdl.SelectedValue);

                var order = new Entities.Order
                {
                    Name = radTextBox1.Text,
                    Assembly = assembly,
                    Count = int.Parse(radTextBox2.Text),
                    WeekNumber = int.Parse(radDropDownList2.Text)
                };

                db.Orders.Add(order);
                db.SaveChanges();

                var plan = GetMainPlan(order.Assembly.Id,db);
                var orders = GetAllAssemblyOrders(order.Assembly.Id,db);

                UpdatePlan(plan, orders);
                db.Entry(plan).State = EntityState.Modified;
                db.SaveChanges();
            }

            LoadOrders();
        }

        private void UpdatePlan(MainPlan plan, List<Entities.Order> orders)
        {
            plan.Week1 = orders.Where(x => x.WeekNumber == 1).Sum(x => x.Count);
            plan.Week2 = orders.Where(x => x.WeekNumber == 2).Sum(x => x.Count);
            plan.Week3 = orders.Where(x => x.WeekNumber == 3).Sum(x => x.Count);
            plan.Week4 = orders.Where(x => x.WeekNumber == 4).Sum(x => x.Count);
            plan.Week5 = orders.Where(x => x.WeekNumber == 5).Sum(x => x.Count);
            plan.Week6 = orders.Where(x => x.WeekNumber == 6).Sum(x => x.Count);
            plan.Week7 = orders.Where(x => x.WeekNumber == 7).Sum(x => x.Count);
            plan.Week8 = orders.Where(x => x.WeekNumber == 8).Sum(x => x.Count);
            plan.Week9 = orders.Where(x => x.WeekNumber == 9).Sum(x => x.Count);
        }



        private List<Entities.Order> GetAllAssemblyOrders(int assemblyId, DataContext db)
        {
             return db.Orders.Include(x=>x.Assembly).Where(x=>x.Assembly.Id == assemblyId).ToList();
        }

        private MainPlan GetMainPlan(int assemblyId,DataContext db)
        {
             return db.MainPlans.Include(x => x.Assembly).FirstOrDefault(x => x.Assembly.Id == assemblyId);
        }

        private void deleteOrderBtn_Click(object sender, EventArgs e)
        {
            if (ordersDgv.SelectedRows.Count == 0)
            {
                MessageBox.Show(@"Сначала выберите строку заказа");
                return;
            }

            var orderId = (int) ordersDgv.SelectedRows[0].Cells[0].Value;

            using (var db = new DataContext())
            {
                var order = db.Orders.Include(x=>x.Assembly).FirstOrDefault(x => x.Id == orderId);
                if (order == null)
                {
                    MessageBox.Show(@"Заказ не найден");
                    return;
                }

                var assemblyId = order.Assembly.Id;

                db.Orders.Remove(order);
                db.SaveChanges();
                
                var plan = GetMainPlan(assemblyId,db);
                var orders = GetAllAssemblyOrders(assemblyId, db);
                UpdatePlan(plan, orders);
                db.Entry(plan).State = EntityState.Modified;
                db.SaveChanges();
            }

            LoadOrders();
        }

    }
}