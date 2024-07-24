using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.Entities;

namespace Repositories;

public partial class MilkTeaCashierContext : DbContext
{
    public MilkTeaCashierContext()
    {
    }

    public MilkTeaCashierContext(DbContextOptions<MilkTeaCashierContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Beverage> Beverages { get; set; }

    public virtual DbSet<BeverageCategory> BeverageCategories { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillInfo> BillInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());


    private string? GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnectionStringDB"];
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Account__C9F284578A0FE073");

            entity.ToTable("Account");

            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.DisplayedName)
                .HasMaxLength(100)
                .HasDefaultValue("Cafe");
            entity.Property(e => e.PassWord)
                .HasMaxLength(1000)
                .HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Beverage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Beverage__3213E83F6F6A88D4");

            entity.ToTable("Beverage");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValue("Chưa đặt tên")
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Beverages)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Beverage__price__534D60F1");
        });

        modelBuilder.Entity<BeverageCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Beverage__3213E83FB8AA26CB");

            entity.ToTable("BeverageCategory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValue("Chưa đặt tên")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bill__3213E83F134BDC57");

            entity.ToTable("Bill");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCheckIn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EntryTime).HasDefaultValueSql("(CONVERT([time],getdate()))");
        });

        modelBuilder.Entity<BillInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BillInfo__3213E83F55F1C4F1");

            entity.ToTable("BillInfo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.IdBeverage).HasColumnName("idBeverage");
            entity.Property(e => e.IdBill).HasColumnName("idBill");

            entity.HasOne(d => d.IdBeverageNavigation).WithMany(p => p.BillInfos)
                .HasForeignKey(d => d.IdBeverage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillInfo__idBeve__5BE2A6F2");

            entity.HasOne(d => d.IdBillNavigation).WithMany(p => p.BillInfos)
                .HasForeignKey(d => d.IdBill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillInfo__count__5AEE82B9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
