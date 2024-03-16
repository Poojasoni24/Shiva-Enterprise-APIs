﻿using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shiva_Enterprise_APIs.Entities.Purchase;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("Vendor")]
    public class Vendor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid VendorId { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string VendorCode { get; set; }

        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string VendorName { get; set; }

        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string VendorType { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string? VendorAddress { get; set; }

        [StringLength(20)]
        [Unicode(false)]
        public string Phoneno { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string Email { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }

        public Guid? cityId { get; set; }

        [ForeignKey("cityId")]
        [InverseProperty("VendorCity")]
        public virtual City City { get; set; }  

        [StringLength(100)]
        [Unicode(false)]
        public string? Remark { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [InverseProperty("Vendor")]
        public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; } = new List<PurchaseOrder>();
    }
}