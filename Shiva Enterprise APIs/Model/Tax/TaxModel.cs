using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model.Tax
{
    public class TaxModel
    {
        public string TaxCode { get; set; }
        public string TaxName { get; set; }
        public string? TaxDescription { get; set; }
        public bool TaxStatus { get; set; }
        public string TaxType { get; set; }
        public string TaxRate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
