namespace MRP.Entities
{
    public class DatabaseCreator
    {
        public void Create()
        {
            using (var db = new UserContext())
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
    }
}
