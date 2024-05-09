using CursVN.Persistance.Configurations;
using CursVN.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace CursVN.Persistance
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ParameterEntity> Parameters { get; set; } 
        public DbSet<TypeEntity> Types { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<OrderProduct> OrdersProducts { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
            modelBuilder.ApplyConfiguration(new ParameterConfiguration());
            modelBuilder.ApplyConfiguration(new TypeConfiguration());
        }
    }
}
