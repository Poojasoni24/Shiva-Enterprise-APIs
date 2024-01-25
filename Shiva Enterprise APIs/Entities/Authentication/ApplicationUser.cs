using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Authentication
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string First_Name { get; set; }

        [StringLength(200)]
        [Unicode(false)]
        public string Middle_Name { get; set; }

        [StringLength(200)]
        [Unicode(false)]
        public string Last_Name { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDateAndTime { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDateAndTime { get; set; }

    }
}
