using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities.Accounts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Products
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        [Key]
        public Guid ProductCategoryId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string ProductCategoryCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string ProductCategoryName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? ProductCategoryDescription { get; set; }
        public bool ProductCategoryStatus { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [InverseProperty("ProductCategory")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
