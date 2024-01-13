using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Model
{
    public class BranchModel
    {
        public Guid Branch_Id { get; set; }
        public string Branch_Code { get; set; }
        public string Branch_Name { get; set; }
        public Guid? Company_Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
