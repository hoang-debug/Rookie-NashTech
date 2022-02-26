using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day_11.Data.Entities
{
    // [Table("MyTable")] Custom name for table.
    public class Category : BaseEntity
    {
        
        [Required, MaxLength(50)]
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}