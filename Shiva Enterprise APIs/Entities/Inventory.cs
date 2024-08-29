using Shiva_Enterprise_APIs.Entities.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("Inventory")]
    public class Inventory
    {
        [Key]
        public Guid InventoryId { get; set; }

        [Required]
        public string? InventoryCode { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int OpeningQty { get; set; }

        [Required]
        public int ClosingQty { get; set; }

        [Required]
        public int InQuantity { get; set; }

        [Required]
        public int OutQuantity { get; set; }

        [Required]
        public decimal InventoryCost { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}
