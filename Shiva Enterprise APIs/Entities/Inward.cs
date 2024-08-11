using Shiva_Enterprise_APIs.Entities.Products;
using Shiva_Enterprise_APIs.Entities.Purchase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("Inwards")]
    public class Inwards
    {
        [Key]
        public Guid InwardId { get; set; }


        [Required]
        public Guid PurchaseOrderId { get; set; }

        [Required]
        public Guid VendorId { get; set; }
        public string VendorName { get; set; }

        [Required]
        public DateTime ReceiptDate { get; set; }
        public string ReceivedBy { get; set; }

        [Required]  
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal QuantityReceived { get; set; }
        public string UnitOfMeasure { get; set; }
        public string BatchNumber { get; set; }
        public string QualityCheckStatus { get; set; }
        public string QualityCheckRemarks { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal CostPerUnit { get; set; }
        public decimal TotalCost { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy{ get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("VendorId")]
        [InverseProperty("Inwards")]
        public virtual Vendor Vendor { get; set; }

        [ForeignKey("PurchaseOrderId")]
        [InverseProperty("Inwards")]
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("Inwards")]
        public virtual Product Product { get; set; }
    }
}
