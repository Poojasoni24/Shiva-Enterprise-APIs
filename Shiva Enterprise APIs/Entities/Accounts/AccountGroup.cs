using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Accounts
{
    [Table("AccountGroup")]
    public partial class AccountGroup
    {
        [Key]
        public Guid AccountGroupId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string AccountGroupCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string AccountGroupName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? AccountGroupDescription { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [InverseProperty("AccountGroup")]
        public virtual ICollection<Account> Account { get; set; } = new List<Account>();
    }
}
