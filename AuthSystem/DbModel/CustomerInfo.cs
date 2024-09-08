using System.ComponentModel.DataAnnotations;

namespace AuthSystem.DbModel
{
    //public class CustomerInfo
    //{
    //    [Key]
    //    public int CustomerId { get; set; } // Primary Key

    //    [Required]
    //    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    //    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
    //    public string Name { get; set; } // Name: Required, only alphabets allowed

    //    [Required]
    //    public byte GenderId { get; set; } // GenderId: Foreign key

    //    [Required]
    //    public short StateId { get; set; } // StateId: Foreign key

    //    [Required]
    //    public short DistrictId { get; set; } // DistrictId: Foreign key

    //    public Gender Gender { get; set; } // Navigation property for Gender
    //    public District District { get; set; } // Navigation property for District
    //}

    public class CustomerInfo
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }

        [Required]
        public int GenderId { get; set; }

        [Required]
        public int DistrictId { get; set; } // Change to short to match District.Id

        // Navigation properties
        public Gender Gender { get; set; }
        public District District { get; set; }
    }

}
