using Day_10.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Day_10.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public virtual DbSet<Student>? Students { get; set; }
    }
}