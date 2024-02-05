using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model
{
    public class ModeofPaymentModel
    {
        public string MODCode { get; set; }
        public string MODName { get; set; }
        public string? MODDescription { get; set; }
        public bool IsActive { get; set; }
        public string MODType { get; set; }
        public string MODAccount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
