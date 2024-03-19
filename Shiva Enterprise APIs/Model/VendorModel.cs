﻿using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model.Vendor
{
    public class VendorModel
    {
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string VendorType { get; set; }
        public string? VendorAddress { get; set; }
        public string Phoneno { get; set; }
        public string Email { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public Guid? cityId { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
