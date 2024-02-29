using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities.Purchase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("Brand")]
    public class Brand
    {
        [Key]
        public Guid BrandId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string BrandCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string BrandName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? BrandDescription { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [InverseProperty("Brand")]
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; } = new List<PurchaseOrderDetail>();
    }
}
