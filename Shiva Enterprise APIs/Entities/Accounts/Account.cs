using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Accounts
{
    [Table("Account")]
    public partial class Account
    {
        [Key]
        public Guid AccountId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]    
        public string AccountCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string AccountName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? AccountDescription { get; set; }
        public bool IsActive { get; set; }
        public string? AccountGroup { get; set; }
        public string? AccountType { get; set; }
        public string? AccountCategory { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
