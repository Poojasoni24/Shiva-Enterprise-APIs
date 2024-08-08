using Shiva_Enterprise_APIs.Entities.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        public Guid StockId { get; set; }

        [Required]
        public string? StockCode { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int QuantityOnHand { get; set; }
        public string? ReorderLevel { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
