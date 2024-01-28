using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities.TaxEntities
{
    [Table("Tax")]
    public class Tax
    {
        [Key]
        public Guid TaxId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string TaxCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string TaxName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? TaxDescription { get; set; }
        public bool TaxStatus { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string TaxType { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string TaxRate { get; set; }        

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
