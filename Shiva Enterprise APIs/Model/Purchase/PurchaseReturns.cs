using Shiva_Enterprise_APIs.Model.Vendor;

namespace Shiva_Enterprise_APIs.Model.Purchase
{
    public class PurchaseReturns
    {
        public int PurchaseReturnId { get; set; }
        public int PurchaseId { get; set; }
        public int VendorId { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ReturnReason { get; set; }
        public string Status { get; set; } = "Pending";
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public PurchaseOrderModel Purchase { get; set; }
        public VendorModel Vendor { get; set; }
    }

}
