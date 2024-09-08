using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [MaxLength(10)]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Domain name can only contain alphanumeric characters and underscores.")]
    public string DomainName { get; set; }
}

