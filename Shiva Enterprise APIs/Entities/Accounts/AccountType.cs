using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Accounts
{
    [Table("AccountType")]
    public class AccountType
    {
        [Key]
        public Guid AccountTypeId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string AccountTypeCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string AccountTypeName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? AccountTypeDescription { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [InverseProperty("AccountType")]
        public virtual ICollection<Account> Account { get; set; } = new List<Account>();
    }
}
