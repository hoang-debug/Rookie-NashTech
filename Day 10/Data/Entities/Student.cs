using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day_10.Data.Entities
{
    // [Table("MyTable")] Custom name for table.
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        [Required, MaxLength(50)]
        public string? FirstName { get; set; }
        [Required, MaxLength(50)]
        public string? LastName { get; set; }
        [MaxLength(50)]
        public string? City { get; set; }
        [NotMapped]
        public string? State { get; set; }
    }
}