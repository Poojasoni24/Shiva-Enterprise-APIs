using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model
{
    public class SalesmanAgentModel
    {
        public string Salesman_Name { get; set; }
        public string Salesman_email { get; set; }
        public string Salesman_code { get; set; }
        public bool IsActive { get; set; }
        public string Salesmanphone { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}

    

