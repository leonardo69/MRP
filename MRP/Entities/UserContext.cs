using System.Data.Entity;

namespace MRP.Entities
{
    public class UserContext: DbContext
    {
        public UserContext(): base("DbConnection"){ }

        public DbSet<User> Users { get; set; }
    }
}
