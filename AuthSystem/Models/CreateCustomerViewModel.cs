using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class CreateCustomerViewModel
    {
        public int customerId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Name can only contain alphabets.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select a gender.")]
        public byte GenderId { get; set; }

        [Required(ErrorMessage = "Please select a district.")]
        public int DistrictId { get; set; }
    }
}
