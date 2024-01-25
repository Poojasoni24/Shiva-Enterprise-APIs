using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model.Account
{
    public class AccountModel
    {
        public string AccountCode { get; set; }
        public string AccontName { get; set; }
        public string? AccountDescription { get; set; }
        public bool AccountStatus { get; set; }
        public Guid AccountGroupId { get; set; }
        public Guid AccountTypeId { get; set; }
        public Guid? AccountCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
