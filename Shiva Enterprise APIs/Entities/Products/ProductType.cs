using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Products
{
    [Table("ProductType")]
    public class ProductType
    {
        [Key]
        public Guid ProductTypeId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string ProductTypeCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string ProductTypeName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? ProductTypeDescription { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [InverseProperty("ProductType")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
