using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model
{
    public class SalesOrderDetailModel
    {
        public Guid SalesOrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid BrandId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NetTotal { get; set; }
        public string Tax_Percentage { get; set; }
        public bool IsActive { get; set; }

        
        public string CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }


    }
}
