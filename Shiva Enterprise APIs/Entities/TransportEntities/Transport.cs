using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Entities.TransportEntities
{
    [Table("Transport")]
    public class Transport
    {
        [Key]
        public Guid TransportId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string TransportCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string TransportName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? TransportDescription { get; set; }
        public bool TransportStatus { get; set; }

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
