using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model
{
    public class CustomerModel
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerType { get; set; }
        public string? CustomerAddress { get; set; }
        public string Phoneno { get; set; }
        public string Email { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public Guid? CityId { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }
        public decimal? CustomerDiscount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<SalesOrder> SalesOrder { get; set; } = new List<SalesOrder>();
    }
}

