using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Purchase
{
    [Table("PurchaseReturn")]
    public class PurchaseReturn
    {
        [Key]
        public Guid PurchaseReturnId { get; set; }

        [Required]
        [ForeignKey("PurchaseOrderId")]
        public Guid PurchaseOrderId { get; set; }

        [Required]
        [ForeignKey("VendorId")]
        public Guid VendorId { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ReturnReason { get; set; }
        public string Status { get; set; } = "Pending";
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; } = DateTime.Now;

        public virtual Vendor Vendor { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }

    }

}
