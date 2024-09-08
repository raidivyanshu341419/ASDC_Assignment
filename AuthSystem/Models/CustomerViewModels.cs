using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{

    public class CustomerViewModels
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public string Gender { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public int GenderId { get; set; }

        [Required]
        public int StateId { get; set; }
        public int ddlState { get; set; }

        [Required]
        public int DistrictId { get; set; }
        public int ddldistrict { get; set; }

        [Required(ErrorMessage = "Please Select Organizer.")]
        public IList<SelectListItem> States { get; set; }

        [Required(ErrorMessage = "Please Select Event.")]
        public IList<SelectListItem> Districts { get; set; }

    }

}
