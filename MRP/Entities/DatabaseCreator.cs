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
           
                var firstSpecification = new Specification
                {
                    Name = "Душевая кабина Rosa",
                    StartComponent = washer
                };

                db.Specifications.Add(firstSpecification);
                db.SaveChanges();

                washer.SpecificationId = firstSpecification.Id;
                washer.Childrens = new List<Component>
                {
                    new Component {Name = "Каркас со стенками", Unit = "шт", SpecificationId = firstSpecification.Id},
                    new Component
                    {
                        Name = "Ручной душ", Unit = "шт", SpecificationId = firstSpecification.Id, Childrens = new List<Component>
                        {
                            new Component {Name = "Смеситель", Unit = "шт",  SpecificationId = firstSpecification.Id}
                        }
                    }
                };

                db.Entry(washer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var components = db.Components.ToList();
            }
        }
    }
}