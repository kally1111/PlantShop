using Microsoft.EntityFrameworkCore;
using PlantShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PlantShop.DataAccess
{
    public class PlantShopDbContext:DbContext
    {
        public PlantShopDbContext(DbContextOptions<PlantShopDbContext> opt)
            : base(opt)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Shop> Shopes { get; set; }
        // public IEnumerable<object> Users { get; internal set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

            //modelBuilder.Entity<ShopPlant>()
            //    .HasKey(sp => new { sp.PlantId, sp.ShopId });
            //modelBuilder.Entity<ShopPlant>()
            //    .HasOne(sp => sp.Plant)
            //    .WithMany(p => p.ShopPlants)
            //    .HasForeignKey(sp => sp.PlantId);
        //    modelBuilder.Entity<Shop>()
        //        .HasMany(s => s.Plants)
        //        .WithOne(p => p.Shop);
        //    modelBuilder.Entity<Shop>()
        //        .HasMany(s => s.Employees)
        //        .WithOne(e => e.Shop).IsRequired();
        //}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Shop>()
               .HasData(
                new Shop
                {
                    Id = 1,
                    ShopName = " "

                });
            builder.Entity<Employee>()
                .HasData(
                new Employee
                {
                    Id=1,
                    ShopId=1,
                    FirstName="first",
                    LastName="first",
                    Password="first"
                });
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            base.OnModelCreating(builder);
           
        }

    }
}
