using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model.Product
{
    public class ProductTypeModel
    {
        public string ProductTypeCode { get; set; }
        public string ProductTypeName { get; set; }
        public string? ProductTypeDescription { get; set; }
        public bool ProductTypeStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

    }
}
