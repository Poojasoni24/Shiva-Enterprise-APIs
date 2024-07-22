using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("SalesReturn")]
    public class SalesReturn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SalesReturnID { get; set; }

        [ForeignKey("SalesOrder")]
        public Guid SalesOrderID { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string ReasonForReturn { get; set; }

        [Required]
        public int ReturnedQuantity { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? RestockingFee { get; set; }

        public string Comments { get; set; }

        public SalesOrder SalesOrder { get; set; }
    }

}