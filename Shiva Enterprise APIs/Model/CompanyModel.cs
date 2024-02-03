using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model
{
    public class CompanyModel
    {
        public string Company_Code { get; set; }
        public string Company_Name { get; set; }
        public DateTime? Company_Startyear { get; set; }
        public DateTime? Company_Endyear { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsActive { get; set; }

    }
}
