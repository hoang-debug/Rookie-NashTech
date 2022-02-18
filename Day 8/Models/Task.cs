using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Day_8.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [DefaultValue(0)]
        public bool Completed { get; set; }
    }
}