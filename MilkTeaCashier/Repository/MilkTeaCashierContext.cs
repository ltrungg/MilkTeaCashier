using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database= MilkTeaCashier;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Account__C9F284572A836E2E");

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
            entity.HasKey(e => e.Id).HasName("PK__Beverage__3213E83F78162E11");

            entity.ToTable("Beverage");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValue("Chưa đặt tên")
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active")
                .HasColumnName("status");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Beverages)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Beverage__price__412EB0B6");
        });

        modelBuilder.Entity<BeverageCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Beverage__3213E83FC56457C3");

            entity.ToTable("BeverageCategory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValue("Chưa đặt tên")
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("active")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bill__3213E83F00423020");

            entity.ToTable("Bill");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCheckIn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EntryTime).HasDefaultValueSql("(CONVERT([time],getdate()))");
        });

        modelBuilder.Entity<BillInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BillInfo__3213E83F985DEB46");

            entity.ToTable("BillInfo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.IdBeverage).HasColumnName("idBeverage");
            entity.Property(e => e.IdBill).HasColumnName("idBill");

            entity.HasOne(d => d.IdBeverageNavigation).WithMany(p => p.BillInfos)
                .HasForeignKey(d => d.IdBeverage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillInfo__idBeve__49C3F6B7");

            entity.HasOne(d => d.IdBillNavigation).WithMany(p => p.BillInfos)
                .HasForeignKey(d => d.IdBill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillInfo__count__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
