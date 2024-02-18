

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("Issue")]
    public class Issue
    {
        [Key]
        public Guid IssueId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string IssueCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string IssueName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? IssueDescription { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}

