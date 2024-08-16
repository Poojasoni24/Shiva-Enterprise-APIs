namespace Shiva_Enterprise_APIs.Model
{
    public class SalesReturnModel
    {
        public Guid SalesReturnID { get; set; }
        public Guid SalesOrderID { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ReasonForReturn { get; set; }
        public int ReturnedQuantity { get; set; }
        public decimal? RestockingFee { get; set; }
        public string Comments { get; set; }

        public int Quantity { get; set; }
        public virtual SalesOrderModel SalesOrder { get; set; }
        public Guid ProductId { get; set; }  // Added
        public Guid BrandId { get; set; }
    }
}
