using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Products
{
    [Table("ProductGroup")]
    public class ProductGroup
    {
        [Key]
        public Guid ProductGroupId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string ProductGroupCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string ProductGroupName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? ProductGroupDescription { get; set; }
        public bool ProductGroupStatus { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [InverseProperty("ProductGroup")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
