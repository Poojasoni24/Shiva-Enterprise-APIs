using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Authentication
{
    
    public class ApplicationRole : IdentityRole
    {
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
