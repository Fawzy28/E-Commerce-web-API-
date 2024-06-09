using Ecommerce.Models;
using Ecommerce.Models.auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Context.Context
{
    public class EcContext : IdentityDbContext<CustomizedUser>
    {
        public EcContext(DbContextOptions<EcContext> options) : base(options)
        {
        }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Order_Product> Order_Product { get; set; }
        public virtual DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order_Product>()
               .HasOne(c => c.Order)
               .WithMany(o => o.Order_Products);

            modelBuilder.Entity<Order_Product>()
              .HasOne(c => c.Product)
              .WithMany(p => p.Order_Product);

            modelBuilder.Entity<Order_Product>()
              .HasKey(op => new { op.OrderId, op.ProductId });


            modelBuilder.Entity<Category>()
                       .HasMany(p => p.Products)
                       .WithOne(pt => pt.CategoryName)
                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(u => u.Customer)
                .WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .Property(pr => pr.status).HasDefaultValue(OrderStatus.newOrder);

            base.OnModelCreating(modelBuilder);

        }

    }
}