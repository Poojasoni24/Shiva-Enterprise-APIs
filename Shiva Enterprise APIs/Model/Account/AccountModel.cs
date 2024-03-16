using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model.Account
{
    public class AccountModel
    {
        public string? AccountCode { get; set; }
        public string? AccontName { get; set; }
        public string? AccountDescription { get; set; }
        public bool IsActive { get; set; }
        public string? AccountGroup { get; set; }
        public string? AccountType { get; set; }
        public string? AccountCategory { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
