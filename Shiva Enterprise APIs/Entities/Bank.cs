using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("Bank")]
    public class Bank
    {
        [Key]
        public Guid BankId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string BankCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string BankName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? BankDescription { get; set; }
        public bool BankStatus { get; set; }       

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
