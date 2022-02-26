using System.ComponentModel.DataAnnotations;

namespace Day_11.Models
{
    public class ProductCreateModel
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }
        public string? Manufacturer { get; set; }

    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Manufacturer { get; set; }
    }

}
