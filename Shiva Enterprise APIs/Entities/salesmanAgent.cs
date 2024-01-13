﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shiva_Enterprise_APIs.Entities;

[Table("salesmanAgent")]
public partial class salesmanAgent
{
    [Key]
    public Guid SalesmanAgentID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Salesman_Name { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Salesman_email { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Salesman_code { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Status { get; set; }

    [Required]
    [StringLength(20)]
    public string Salesmanphone { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDateAndTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDateAndTime { get; set; }
}