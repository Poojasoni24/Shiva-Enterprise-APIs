using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities.Products;
using Shiva_Enterprise_APIs.Entities.Purchase;
using Shiva_Enterprise_APIs.Model;
using Shiva_Enterprise_APIs.Model.Purchase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("Outwards")]
    public class Outwards
    {
        [Key]
        public Guid OutwardId { get; set; }

        [Required]
        public Guid SalesOrderId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string CustomerName { get; set; }

        [Required]
        public DateTime ShipmentDate { get; set; }
        public string ShippedBy { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }

        [Required]
        public decimal QuantityShipped { get; set; }
        public string UnitOfMeasure { get; set; }
        public string BatchNumber { get; set; }
        public int CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string TrackingNumber { get; set; }
        public string ShippingMethod { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal CostPerUnit { get; set; }
        public decimal TotalCost { get; set; }
        public string Currency { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("Outwards")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("SalesOrderId")]
        [InverseProperty("Outwards")]
        public virtual SalesOrder SalesOrder { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("Outwards")]
        public virtual Product Product { get; set; }

        public static class MappingHelper
        {
            public static Outwards MapToEntity(OutwardModel outwardDetails)
            {
                return new Outwards
                {
                    SalesOrderId = outwardDetails.SalesOrderId,
                    CustomerId = outwardDetails.CustomerId,
                    CustomerName = outwardDetails.CustomerName,
                    ShipmentDate = outwardDetails.ShipmentDate,
                    ShippedBy = outwardDetails.ShippedBy,
                    ProductId = outwardDetails.ProductId,
                    ProductName = outwardDetails.ProductName,
                    QuantityShipped = outwardDetails.QuantityShipped,
                    UnitOfMeasure = outwardDetails.UnitOfMeasure,
                    BatchNumber = outwardDetails.BatchNumber,
                    CarrierId = outwardDetails.CarrierId,
                    CarrierName = outwardDetails.CarrierName,
                    TrackingNumber = outwardDetails.TrackingNumber,
                    ShippingMethod = outwardDetails.ShippingMethod,
                    DeliveryDate = outwardDetails.DeliveryDate,
                    DeliveryAddress = outwardDetails.DeliveryAddress,
                    InvoiceDate = outwardDetails.InvoiceDate,
                    CostPerUnit = outwardDetails.CostPerUnit,
                    TotalCost = outwardDetails.TotalCost,
                    Remarks = outwardDetails.Remarks,
                    CreatedBy = outwardDetails.CreatedBy,
                    CreatedDate = outwardDetails.CreatedDate,
                    ModifiedBy = outwardDetails.ModifiedBy,
                    Currency = outwardDetails.Currency
                };
            }
        }

    }
}
