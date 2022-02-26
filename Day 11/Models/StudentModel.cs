using System.ComponentModel.DataAnnotations;

namespace Day_11.Models
{
    public class StudentCreateModel
    {
        public string? FirstName { get; set; }
        [Required, MaxLength(50)]
        public string? LastName { get; set; }
        [MaxLength(50)]
        public string? City { get; set; }
        public string? State { get; set; }
    }

    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string? FullName { get; set; }
        public string? City { get; set; }
    }

}
