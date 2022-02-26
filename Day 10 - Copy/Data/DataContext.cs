using Day_11.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Day_11.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Category
            builder.Entity<Category>(e => e.ToTable("Categories"));
            // builder.Entity<Category>().HasKey(e => e.Id);
            // builder.Entity<Category>().Property(e => e.Name).IsRequired();
            
            builder.Entity<Category>()
            .HasMany(category => category.Products)
            .WithOne(product => product.Category)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired();

            var data = new List<Category>
            {
                new Category{Id = 1, Name="Food"},
                new Category{Id = 2, Name="Cosmetic"},
                new Category{Id = 3, Name="Drinks"},
                new Category{Id = 4, Name="Fashion"},
                new Category{Id = 5, Name="High Tech"},
            };
            builder.Entity<Category>().HasData(data);
            // Product
            builder.Entity<Product>(e => e.ToTable("Product"));
            // builder.Entity<Product>().HasKey(e => e.Id);
            // builder.Entity<Product>().Property(e => e.Name).IsRequired();

            // builder.Entity<Product>()
            // .HasOne(p => p.Category)
            // .WithMany(c => c.Products)
            // .HasForeignKey(p => p.CategoryId)
            // .IsRequired();
        }
        public virtual DbSet<Student>? Students { get; set; }
        // public virtual DbSet<Category>? Categories { get; set; }
        // public virtual DbSet<Product>? Products { get; set; }

    }
}