using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shiva_Enterprise_APIs.Entities.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Purchase
{
    public class PurchaseOrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PurchaseOrderDetailId { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid BrandId { get; set; }
        public Guid UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NetTotal { get; set; }
        public bool IsActive { get; set; }  

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }


        [ForeignKey("BrandId")]
        [InverseProperty("PurchaseOrderDetail")]
        public virtual Brand Brand { get; set; }

        [ForeignKey("UnitId")]
        [InverseProperty("PurchaseOrderDetail")]
        public virtual Unit Unit { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("PurchaseOrderDetail")]
        public virtual Product Product { get; set; }

        [ForeignKey("PurchaseOrderId")]
        [InverseProperty("PurchaseOrderDetail")]
        public virtual PurchaseOrder PurchaseOrder { get; set; }



    }
}
