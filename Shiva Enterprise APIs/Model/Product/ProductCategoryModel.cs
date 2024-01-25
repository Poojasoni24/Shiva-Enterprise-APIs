﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model.Product
{
    public class ProductCategoryModel
    {
        public string ProductCategoryCode { get; set; }
        public string ProductCategoryName { get; set; }
        public string? ProductCategoryDescription { get; set; }
        public bool ProductCategoryStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
