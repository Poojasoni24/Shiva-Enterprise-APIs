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
        public string AccontName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? AccountDescription { get; set; }
        public bool IsActive { get; set; }
        public Guid AccountGroupId { get; set; }
        public Guid AccountTypeId { get; set; }

        public Guid? AccountCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [ForeignKey("AccountGroupId")]
        [InverseProperty("Account")]
        public virtual AccountGroup AccountGroup { get; set; }

        [ForeignKey("AccountTypeId")]
        [InverseProperty("Account")]
        public virtual AccountType AccountType { get; set; }
        
        [ForeignKey("AccountCategoryId")]
        [InverseProperty("Account")]

        public virtual AccountCategory AccountCategory { get; set; }
    }
}
