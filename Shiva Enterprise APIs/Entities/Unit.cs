using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities.Purchase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("Unit")]
    public class Unit
    {
        [Key]
        public Guid UnitId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string UnitCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string UnitName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? UnitDescription { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [InverseProperty("Unit")]
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; } = new List<PurchaseOrderDetail>();
    }
}

