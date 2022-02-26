using System.ComponentModel.DataAnnotations;

namespace Day_11.Models
{
    public class CategoryCreateModel
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }
        public IEnumerable<ProductCreateModel>? Products { get; set; }

    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<ProductViewModel>? Products { get; set; }
    }

    public class CategoryEditModel
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }

    }

}
