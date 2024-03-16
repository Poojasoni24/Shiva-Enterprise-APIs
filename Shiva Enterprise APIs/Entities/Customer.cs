using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities.Purchase;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CustomerCode { get; set; }

        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string CustomerType { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? CustomerAddress { get; set; }

        [StringLength(20)]
        [Unicode(false)]
        public string Phoneno { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string Email { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }

        public Guid? CityId { get; set; }

        [ForeignKey("CityId")]
        [InverseProperty("Customer")]
        public virtual City City { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? Remark { get; set; }
        public bool IsActive { get; set; }

        public decimal? CustomerDiscount { get; set; }   

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<SalesOrder> SalesOrder { get; set; } = new List<SalesOrder>();
    }
}
