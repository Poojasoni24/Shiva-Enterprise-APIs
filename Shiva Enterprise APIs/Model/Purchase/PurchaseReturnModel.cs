using Shiva_Enterprise_APIs.Entities.Purchase;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model.Purchase
{
    public class PurchaseReturnModel
    {
        public Guid PurchaseReturnId { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ReturnReason { get; set; }
        public int ReturnQuantity { get; set; }
        public string Status { get; set; } = "Pending";
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; } = DateTime.Now;
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public int Quantity { get; set; }
        public Guid VendorId { get; set; }
        public Guid ProductId { get; set; }  // Added
        public Guid BrandId { get; set; }

    }
}
