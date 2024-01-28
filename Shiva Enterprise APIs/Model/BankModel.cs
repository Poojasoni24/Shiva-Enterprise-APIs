using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model
{
    public class BankModel
    {
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string? BankDescription { get; set; }
        public bool BankStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
