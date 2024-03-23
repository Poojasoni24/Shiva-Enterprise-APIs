﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities.Accounts;
using Shiva_Enterprise_APIs.Entities.Authentication;
using Shiva_Enterprise_APIs.Entities.Products;
using Shiva_Enterprise_APIs.Entities.Purchase;
using Shiva_Enterprise_APIs.Entities.TransportEntities;

namespace Shiva_Enterprise_APIs.Entities;

public partial class ShivaEnterpriseContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ShivaEnterpriseContext()
    {
    }

    public ShivaEnterpriseContext(DbContextOptions<ShivaEnterpriseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<State> states { get; set; }
    public virtual DbSet<Account> accounts { get; set; }
    public virtual DbSet<Product> products { get; set; }
    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<ModeofPayment> ModeofPayments { get; set; }
    public virtual DbSet<Transport> Transports { get; set; }
    public virtual DbSet<Vendor> Vendors { get; set; }
    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<SalesOrder> SalesOrders { get; set; }
    public virtual DbSet<SalesOrderDetail>SalesOrderDetails { get; set; }
    public DbSet<ApplicationUser> applicationUsers { get; set; }
    public DbSet<ApplicationRole> applicationRoles { get; set; }
    public DbSet<IdentityUserClaim<Guid>> IdentityUserClaims { get; set; }
    public DbSet<IdentityUserClaim<string>> IdentityUserClaim
    {
        get;
        set;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //  => optionsBuilder.UseSqlServer("Data Source=LAPTOP-DRBPPARM\\MSSQLSERVER2;Initial Catalog=ShivaEnterprise;User Id=sa;Password=Ps@1234;Integrated Security=true;TrustServerCertificate=True");
    => optionsBuilder.UseSqlServer("Data Source=DESKTOP-MQBBGG8\\MSSQLSERVER19;Initial Catalog=ShivaEnterprise;User Id=sa;Password=yash6006;Integrated Security=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Branch_Id).HasName("PK__Branch__12CEB06137D1AE14");

            entity.Property(e => e.Branch_Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Branches).HasConstraintName("FK_Branch_Company");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__city__DE9DE0205736F02F");

            entity.Property(e => e.CityId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.State).WithMany(p => p.Cities).HasConstraintName("FK_City_state");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.Company_Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(false);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Country_Id).HasName("PK__country__8036CB4EB0037A51");

            entity.Property(e => e.Country_Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.State_Id).HasName("PK__state__AF9338D7D97D88B7");

            entity.Property(e => e.State_Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Country).WithMany(p => p.states).HasConstraintName("FK_state_Country");
        });


        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__account__AF9338D7D97D88B7");

            entity.Property(e => e.AccountId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
        });
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__brand__B94AD674532DF6E8");

            entity.Property(e => e.BrandId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
        });
        modelBuilder.Entity<ModeofPayment>(entity =>
        {
            entity.HasKey(e => e.MODId).HasName("PK__mod__B94AD674532DF6E8");

            entity.Property(e => e.MODId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
        });
        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.TransportId).HasName("PK__transport__B94AD674532DF6E8");

            entity.Property(e => e.TransportId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
        });
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__product__AF9338D7D97D88B7");

            entity.Property(e => e.ProductId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
        });
        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__vendor__B94AD674532DF6E8");
            
            entity.Property(e => e.VendorId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.City).WithMany(p => p.VendorCity).HasConstraintName("FK_City_Vendor");

        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__B94AD674532DF6E8");

            entity.Property(e => e.CustomerId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.City).WithMany(p => p.Customer).HasConstraintName("FK_City_Customer");

        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderId).HasName("PK__purchaseorder__B94AD674532DF6E8");

            entity.Property(e => e.PurchaseOrderId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrder).HasConstraintName("FK_purchaseorder_vendor");
        });
        modelBuilder.Entity<PurchaseOrderDetail>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderDetailId).HasName("PK__purchaseorderdetail__B94AD674532DF6E8");

            entity.Property(e => e.PurchaseOrderDetailId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
            entity.HasOne(d => d.Brand).WithMany(p => p.PurchaseOrderDetail).HasConstraintName("FK_purchaseorderdetail_bank");
            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseOrderDetail).HasConstraintName("FK_purchaseorderdetail_product");
            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.PurchaseOrderDetail).HasConstraintName("FK_purchaseorderdetail_purchaseorder");
        });
        modelBuilder.Entity<SalesOrder>(entity =>
        {
            entity.HasKey(e => e.SalesOrderId).HasName("PK__salesorder__B94AD674532DF6E8");

            entity.Property(e => e.SalesOrderId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
            entity.HasOne(d => d.Customer).WithMany(p => p.SalesOrder).HasConstraintName("FK_salesorder_customer");
        });
        modelBuilder.Entity<SalesOrderDetail>(entity =>
        {
            entity.HasKey(e => e.SalesOrderDetailId).HasName("PK__salesorderdetail__B94AD674532DF6E8");

            entity.Property(e => e.SalesOrderDetailId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
            entity.HasOne(d => d.Brand).WithMany(p => p.SalesOrderDetail).HasConstraintName("FK_salesorderdetail_bank");
            entity.HasOne(d => d.Product).WithMany(p => p.SalesOrderDetail).HasConstraintName("FK_salesorderdetail_product");
            entity.HasOne(d => d.SalesOrder).WithMany(p => p.SalesOrderDetail).HasConstraintName("FK_salesorderdetail_purchaseorder");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}