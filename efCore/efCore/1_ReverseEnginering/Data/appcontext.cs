using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using _1_ReverseEnginering.Models;
using _1_ReverseEnginering.Views;

namespace _1_ReverseEnginering.Data;

public partial class appcontext : DbContext
{
    public appcontext()
    {
    }

    public appcontext(DbContextOptions<appcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
    public virtual DbSet<OrderDetails> OrderDetails { get; set; }


    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerTransaction> CustomerTransactions { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderImage> OrderImages { get; set; }

    public virtual DbSet<OrderType> OrderTypes { get; set; }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<ReturnOrder> ReturnOrders { get; set; }

    public virtual DbSet<ReturnPart> ReturnParts { get; set; }

    public virtual DbSet<Statement> Statements { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Technical> Technicals { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-Q18AF5P\\SQLEXPRESS;Initial Catalog=db9253;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDetails>()
            .HasNoKey().ToView("OrderDetails");
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Code).HasDefaultValueSql("(N'')");
            entity.Property(e => e.IsMain)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))")
                .HasColumnName("isMain");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customers", tb => tb.HasTrigger("trg_AuditLog"));

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<CustomerTransaction>(entity =>
        {
            entity.ToTable("customerTransactions");

            entity.HasIndex(e => e.AppUserId, "IX_customerTransactions_AppUserId");

            entity.HasIndex(e => e.CustomerId, "IX_customerTransactions_CustomerId");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AppUser).WithMany(p => p.CustomerTransactions).HasForeignKey(d => d.AppUserId);

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerTransactions).HasForeignKey(d => d.CustomerId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders");

            entity.HasIndex(e => e.AppUserId, "IX_orders_AppUserId");

            entity.HasIndex(e => e.CustomerId, "IX_orders_CustomerId");

            entity.HasIndex(e => e.OrderTypeId, "IX_orders_OrderTypeId");

            entity.HasIndex(e => e.StoreId, "IX_orders_StoreId");

            entity.HasIndex(e => e.TechnicalId, "IX_orders_TechnicalId");

            entity.HasOne(d => d.AppUser).WithMany(p => p.Orders).HasForeignKey(d => d.AppUserId);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.OrderType).WithMany(p => p.Orders).HasForeignKey(d => d.OrderTypeId);

            entity.HasOne(d => d.Store).WithMany(p => p.Orders).HasForeignKey(d => d.StoreId);

            entity.HasOne(d => d.Technical).WithMany(p => p.Orders).HasForeignKey(d => d.TechnicalId);
        });

        modelBuilder.Entity<OrderImage>(entity =>
        {
            entity.ToTable("OrderImage");

            entity.HasIndex(e => e.OrderId, "IX_OrderImage_OrderId");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderImages).HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<OrderType>(entity =>
        {
            entity.ToTable("orderTypes");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Parts_CategoryId");

            entity.HasIndex(e => e.CustomerId, "IX_Parts_CustomerId");

            entity.HasIndex(e => e.OrderId, "IX_Parts_OrderId");

            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Parts).HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.Customer).WithMany(p => p.Parts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Order).WithMany(p => p.Parts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ReturnOrder>(entity =>
        {
            entity.ToTable("returnOrders");

            entity.HasIndex(e => e.CustomerId, "IX_returnOrders_CustomerId");

            entity.Property(e => e.RefundAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.ReturnOrders).HasForeignKey(d => d.CustomerId);
        });

        modelBuilder.Entity<ReturnPart>(entity =>
        {
            entity.ToTable("returnParts");

            entity.HasIndex(e => e.ReturnOrderId, "IX_returnParts_ReturnOrderId");

            entity.Property(e => e.PartName).HasDefaultValueSql("(N'')");
            entity.Property(e => e.PartPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ReturnOrder).WithMany(p => p.ReturnParts).HasForeignKey(d => d.ReturnOrderId);
        });

        modelBuilder.Entity<Statement>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Statements_CustomerId");

            entity.HasIndex(e => e.UserId, "IX_Statements_userId");

            entity.Property(e => e.Ammount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ammount");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.NewBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StatmentType).HasColumnName("statmentType");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Customer).WithMany(p => p.Statements).HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.User).WithMany(p => p.Statements).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.ToTable("stores");
        });

        modelBuilder.Entity<Technical>(entity =>
        {
            entity.ToTable("technicals");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Test");

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
