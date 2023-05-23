using Aw3.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Aw3.Data.Context
{
    public class Aw3DbContext : DbContext
    {
        public Aw3DbContext(DbContextOptions<Aw3DbContext> options) : base(options)
        {

        }

        // dbset
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

}
