﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities.Accounts;
using Shiva_Enterprise_APIs.Entities.Authentication;
using Shiva_Enterprise_APIs.Entities.Products;
using Shiva_Enterprise_APIs.Entities.TaxEntities;
using Shiva_Enterprise_APIs.Entities.TransportEntities;

namespace Shiva_Enterprise_APIs.Entities;

public partial class ShivaEnterpriseContext : IdentityDbContext<ApplicationUser, ApplicationRole,string>
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

    public virtual DbSet<location> locations { get; set; }

    public virtual DbSet<salesmanAgent> salesmanAgents { get; set; }

    public virtual DbSet<State> states { get; set; }
    public virtual DbSet<AccountGroup> accountGroups { get; set; }
    public virtual DbSet<AccountType> accountTypes { get; set; }
    public virtual DbSet<AccountCategory> accountCategories { get; set; }
    public virtual DbSet<Account> accounts  { get; set; }
    public virtual DbSet<ProductGroup> productGroups { get; set; }
    public virtual DbSet<ProductType> productTypes { get; set; }
    public virtual DbSet<ProductCategory> productCategories { get; set; }
    public virtual DbSet<Product> products { get; set; }
    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<Issue> Issues { get; set; }
    public virtual DbSet<Unit> Units { get; set; }
    public virtual DbSet<ModeofPayment> ModeofPayments { get; set; }
    public virtual DbSet<Tax> Taxes { get; set; }
    public virtual DbSet<Transport> Transports { get; set; }
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
      // => optionsBuilder.UseSqlServer("Data Source=LAPTOP-DRBPPARM\\MSSQLSERVER2;Initial Catalog=ShivaEnterprise;User Id=sa;Password=Ps@1234;Integrated Security=true;TrustServerCertificate=True");
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
            entity.HasKey(e => e.City_Id).HasName("PK__city__DE9DE0205736F02F");

            entity.Property(e => e.City_Id).HasDefaultValueSql("(newid())");
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

        modelBuilder.Entity<location>(entity =>
        {
            entity.HasKey(e => e.Location_ID).HasName("PK__location__D2BA00C2438AF258");

            entity.Property(e => e.Location_ID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<salesmanAgent>(entity =>
        {
            entity.HasKey(e => e.SalesmanAgentID).HasName("PK__salesman__B94AD674532DF6E8");

            entity.Property(e => e.SalesmanAgentID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.State_Id).HasName("PK__state__AF9338D7D97D88B7");

            entity.Property(e => e.State_Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Country).WithMany(p => p.states).HasConstraintName("FK_state_Country");
        });

        modelBuilder.Entity<AccountGroup>(entity =>
        {
            entity.HasKey(e => e.AccountGroupId).HasName("PK__acgroup__B94AD674532DF6E8");

            entity.Property(e => e.AccountGroupId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
        });
        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.AccountTypeId).HasName("PK__actype__B94AD674532DF6E8");

            entity.Property(e => e.AccountTypeId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
        });
        modelBuilder.Entity<AccountCategory>(entity =>
        {
            entity.HasKey(e => e.AccountCategoryId).HasName("PK__accategory__B94AD674532DF6E8");

            entity.Property(e => e.AccountCategoryId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");

    });
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__account__AF9338D7D97D88B7");

            entity.Property(e => e.AccountId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AccountGroup).WithMany(p => p.Account).HasConstraintName("FK_account_accountgroup");
            entity.HasOne(d => d.AccountType).WithMany(p => p.Account).HasConstraintName("FK_account_accounttype");
            entity.HasOne(d => d.AccountCategory).WithMany(p => p.Account).HasConstraintName("FK_account_accountcategory");
        });

        modelBuilder.Entity<ProductGroup>(entity =>
        {
            entity.HasKey(e => e.ProductGroupId).HasName("PK__productgroup__B94AD674532DF6E8");

            entity.Property(e => e.ProductGroupId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
        });
        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ProductTypeId).HasName("PK__producttype__B94AD674532DF6E8");

            entity.Property(e => e.ProductTypeId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
        });
        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK__productcategory__B94AD674532DF6E8");

            entity.Property(e => e.ProductCategoryId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
        });
        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.BankId).HasName("PK__bank__B94AD674532DF6E8");

            entity.Property(e => e.BankId).HasDefaultValueSql("(newid())");
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
        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__unit__B94AD674532DF6E8");

            entity.Property(e => e.UnitId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedDateTime).HasDefaultValueSql("(getdate())");
        });
        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.IssueId).HasName("PK__issue__B94AD674532DF6E8");

            entity.Property(e => e.IssueId).HasDefaultValueSql("(newid())");
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
        modelBuilder.Entity<Tax>(entity =>
        {
            entity.HasKey(e => e.TaxId).HasName("PK__tax__B94AD674532DF6E8");

            entity.Property(e => e.TaxId).HasDefaultValueSql("(newid())");
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


            entity.HasOne(d => d.ProductGroup).WithMany(p => p.Products).HasConstraintName("FK_product_productgroup");
            entity.HasOne(d => d.ProductType).WithMany(p => p.Products).HasConstraintName("FK_product_producttype");
            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products).HasConstraintName("FK_product_productcategory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}