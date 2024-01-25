using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model.Product
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public bool ProductStatus { get; set; }
        public string ProductImage { get; set; }
        public Guid ProductCategoryId { get; set; }
        public Guid ProductGroupId { get; set; }
        public Guid ProductTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

    }
}
