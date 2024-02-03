using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities.Accounts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.Products
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string ProductCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string ProductName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? ProductDescription { get; set; }
        public bool IsActive { get; set; }
        public string ProductImage { get; set; }
        public Guid ProductCategoryId {  get; set; }
        public Guid ProductGroupId {  get; set; }
        public Guid ProductTypeId {  get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [ForeignKey("ProductGroupId")]
        [InverseProperty("Products")]
        public virtual ProductGroup ProductGroup { get; set; }

        [ForeignKey("ProductTypeId")]
        [InverseProperty("Products")]
        public virtual ProductType ProductType { get; set; }

        [ForeignKey("ProductCategoryId")]
        [InverseProperty("Products")]
        public virtual ProductCategory ProductCategory { get; set; }

    }
}
