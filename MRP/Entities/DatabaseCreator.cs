using System.Collections.Generic;
using System.Linq;

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
                    Unit = "шт"
                };
           
                var firstAssembly = new Assembly
                {
                    Name = "Душевая кабина Rosa",
                    StartComponent = washer
                };

                db.Specifications.Add(firstAssembly);
                db.SaveChanges();

                washer.AssemblyId = firstAssembly.Id;
                washer.Children = new List<Component>
                {
                    new Component {Name = "Каркас со стенками", Unit = "шт", AssemblyId = firstAssembly.Id},
                    new Component
                    {
                        Name = "Ручной душ", Unit = "шт", AssemblyId = firstAssembly.Id, Children = new List<Component>
                        {
                            new Component {Name = "Смеситель", Unit = "шт",  AssemblyId = firstAssembly.Id}
                        }
                    }
                };

                db.Entry(washer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }
        }
    }
}