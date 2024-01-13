using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model
{
    public class UserModel
    {
        [Required]
        [StringLength(200)]
       
        public string First_Name { get; set; }

        [StringLength(200)]       
        public string Middle_Name { get; set; }

        [StringLength(200)]       
        public string Last_Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Phonenumber { get; set; }

        public bool IsActive { get; set; }
    }
}
