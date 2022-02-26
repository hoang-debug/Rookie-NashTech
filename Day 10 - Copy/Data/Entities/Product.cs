using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Day_11.Data.Entities
{
    // [Table("MyTable")] Custom name for table.
    public class Product : BaseEntity
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Manufacturer { get; set; }

        // Relationship
        [Required]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category? Category { get; set;}
    }
}