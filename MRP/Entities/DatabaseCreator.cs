using System.Collections.Generic;

namespace MRP.Entities
{
    public class DatabaseCreator
    {
        public void CreateUsers()
        {
            using (var db = new DataContext())
            {
                var user = new User {Name = "Alex", Password = "123", Role = Role.Manager};
                var user2 = new User {Name = "Petr", Password = "234", Role = Role.Client};
                var user3 = new User {Name = "Sergey", Password = "345", Role = Role.Admin};

                db.Users.Add(user);
                db.Users.Add(user2);
                db.Users.Add(user3);
                db.SaveChanges();
            }
        }

        public void CreateComponents()
        {
            using (var db = new DataContext())
            {
                var washer = new Component
                {
                    Name = "Душевая кабина",
                    Unit = "шт",
                    Volume = "MAX",
                    ExecutionTime = 1,
                    CountInStore = 150
                };
           
                var firstAssembly = new Assembly
                {
                    Name = "Душевая кабина Rosa",
                    StartComponent = washer
                };

                db.Assemblies.Add(firstAssembly);
                db.SaveChanges();

                washer.AssemblyId = firstAssembly.Id;
                washer.Children = new List<Component>
                {
                    new Component {
                        Name = "Каркас со стенками",
                        Unit = "шт",
                        AssemblyId = firstAssembly.Id,
                        Volume = "100",
                        ExecutionTime = 3 ,
                        CountInStore = 875
                    },
                    new Component
                    {
                        Name = "Ручной душ",
                        Unit = "шт",
                        AssemblyId = firstAssembly.Id,
                        CountInStore = 55,
                        ExecutionTime = 2,
                        Volume = "MAX",
                        Children = new List<Component>
                            {
                                new Component
                                {
                                    Name = "Смеситель",
                                    Unit = "шт",
                                    AssemblyId = firstAssembly.Id,
                                    Volume = "50",
                                    ExecutionTime =  1,
                                    CountInStore = 900
                                }
                            }
                    }
                };

                db.Entry(washer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var mainPlan = new MainPlan
                {
                    Assembly = firstAssembly,
                    Week1 = 50,
                    Week4 = 500,
                    Week6 = 500,
                    Week7 = 100,
                    Week9 = 95
                };

                db.MainPlans.Add(mainPlan);
                db.SaveChanges();


                List<Order> orders = new List<Order>
                {
                    new Order {Assembly = firstAssembly, WeekNumber = 1, Count = 50, Name = "Газпром"},
                    new Order {Assembly = firstAssembly, WeekNumber = 4, Count = 500, Name = "Петропром"},
                    new Order {Assembly = firstAssembly, WeekNumber = 6, Count = 500, Name = "АчайПром"},
                    new Order {Assembly = firstAssembly, WeekNumber = 7, Count = 100, Name = "ЗавКаб"},
                    new Order {Assembly = firstAssembly, WeekNumber = 9, Count = 95, Name = "ПитерСталь"}
                };

                db.Orders.AddRange(orders);
                db.SaveChanges();

            }
        }
    }
}