using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("ModeofPayment")]
    public class ModeofPayment
    {
        [Key]
        public Guid MODId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string MODCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string MODName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? MODDescription { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string MODType { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string MODAccount { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
