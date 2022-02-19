using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace Day_9.Models
{
    public class PersonCreateModel
    {
        
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(10)]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthPlace { get; set; }

       
    }
}

