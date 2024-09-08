using System.ComponentModel.DataAnnotations;

namespace AuthSystem.DbModel
{
    //public class District
    //{
    //    public int Id { get; set; }
    //    public int StateId { get; set; }
    //    public string DistrictName { get; set; }
    //    public State State { get; set; }
    //}

    public class District
    {
        public int Id { get; set; } // Change to int to match CustomerInfo.DistrictId

        [Required]
        public int StateId { get; set; }

        [Required]
        public string DistrictName { get; set; }

        // Navigation property for State
        public State State { get; set; }

        // Navigation property for CustomerInfo
        public ICollection<CustomerInfo> CustomerInfos { get; set; }
    }

}
