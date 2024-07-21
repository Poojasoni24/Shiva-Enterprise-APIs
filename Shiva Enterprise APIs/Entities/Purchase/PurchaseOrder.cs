using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shiva_Enterprise_APIs.Entities.TransportEntities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Model.Purchase;

namespace Shiva_Enterprise_APIs.Entities.Purchase
{
    [Table("PurchaseOrder")]
    public class PurchaseOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PurchaseOrderId { get; set; }
        public Guid VendorID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PurchaseOrderStatus { get; set; }
        public string Doc_No { get; set;}

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [ForeignKey("VendorID")]
        [InverseProperty("PurchaseOrder")]
        public virtual Vendor Vendor { get; set; }

        [InverseProperty("PurchaseOrder")]
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; } = new List<PurchaseOrderDetail>();

        [InverseProperty("PurchaseOrder")]
        public virtual ICollection<Inwards> Inwards { get; set; } = new List<Inwards>();

    }


    public static class MappingHelper
    {
        public static PurchaseOrder MapToEntity(PurchaseOrderModel model)
        {
            return new PurchaseOrder
            {
                VendorID = model.VendorID,
                OrderDate = model.OrderDate,
                DeliveryDate = model.DeliveryDate,
                TotalAmount = model.TotalAmount,
                PurchaseOrderStatus = model.PurchaseOrderStatus,
                Doc_No = model.Doc_No,
                CreatedBy = model.CreatedBy,
                CreatedDateTime = model.CreatedDateTime,
                ModifiedBy = model.ModifiedBy,
                ModifiedDateTime = model.ModifiedDateTime,
            };
        }
    }
}
