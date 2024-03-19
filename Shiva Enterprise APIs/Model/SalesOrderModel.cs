using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model
{
    public class SalesOrderModel
    {
        
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string SaleOrderStatus { get; set; }
        public string Doc_No { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetail { get; set; } = new List<SalesOrderDetail>();
    }
}
   