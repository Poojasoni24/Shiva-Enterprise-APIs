﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shiva_Enterprise_APIs.Entities;

[Table("location")]
public partial class location
{
    [Key]
    public Guid Location_ID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Location_name { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ICreatedDateAndTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDateAndTime { get; set; }
}