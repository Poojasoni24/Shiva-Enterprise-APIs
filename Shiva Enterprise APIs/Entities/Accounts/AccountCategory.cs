using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Accounts
{
    [Table("AccountCategory")]
    public partial class AccountCategory
    {
        [Key]
        public Guid AccountCategoryId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string AccountCategoryCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string AccountCategoryName { get; set;}

        [StringLength(100)]
        [Unicode(false)]
        public string? AccountCategoryDescription { get; set;}
        public bool AccountCategoryStatus { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [InverseProperty("AccountCategory")]
        public virtual ICollection<Account> Account { get; set; } = new List<Account>();
    }
}
