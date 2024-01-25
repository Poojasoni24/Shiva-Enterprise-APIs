using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Authentication
{
    
    public class ApplicationRole : IdentityRole
    {
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
