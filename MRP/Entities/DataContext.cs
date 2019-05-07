using System.Data.Entity;

namespace MRP.Entities
{
    public class DataContext: DbContext
    {
        public DataContext(): base("DbConnection"){ }

        public DbSet<User> Users { get; set; }

        public DbSet<Component> Components { get; set; }

        public DbSet<Assembly> Specifications { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<MainPlan> MainPlans { get; set; }
      
    }
}
