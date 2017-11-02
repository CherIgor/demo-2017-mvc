using System.Data.Entity;

namespace Demo.Data.Models.Entity
{
    public class DemoContext : DbContext
    {
        public DemoContext()
            : base("name=Entities")
        {
        }

        public DbSet<UserText> UserTexts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set relationships between entities here
        }
    }
}