﻿using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities.Products;
using Shiva_Enterprise_APIs.Entities.TransportEntities;
using Shiva_Enterprise_APIs.Entities.VendorEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shiva_Enterprise_APIs.Entities
{
    [Table("Bank")]
    public class Bank
    {
        [Key]
        public Guid BankId { get; set; }

        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string BankCode { get; set; }

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string BankName { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? BankDescription { get; set; }
        public bool IsActive { get; set; }       

        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        [InverseProperty("Bank")]
        public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
    }
}
