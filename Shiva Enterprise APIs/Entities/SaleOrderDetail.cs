using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities.Products;
using Shiva_Enterprise_APIs.Entities.Purchase;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("SalesOrderDetail")]
    public class SalesOrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SalesOrderDetailId { get; set; }
        public Guid SalesOrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid BrandId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NetTotal { get; set; }
        public string Tax_Percentage { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }


        [ForeignKey("BrandId")]
        [InverseProperty("SalesOrderDetail")]
        public virtual Brand Brand { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("SalesOrderDetail")]
        public virtual Product Product { get; set; }

        [ForeignKey("SalesOrderId")]
        [InverseProperty("SalesOrderDetail")]
        public virtual SalesOrder SalesOrder { get; set; }       
    }
}