using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NidecUniform.Models;

public partial class NidecUniformContext : DbContext
{
    public NidecUniformContext()
    {
    }

    public NidecUniformContext(DbContextOptions<NidecUniformContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DeliveryDetail> DeliveryDetails { get; set; }

    public virtual DbSet<MDelivery> MDeliveries { get; set; }

    public virtual DbSet<MEmployee> MEmployees { get; set; }

    public virtual DbSet<MProduct> MProducts { get; set; }

    public virtual DbSet<MRequest> MRequests { get; set; }

    public virtual DbSet<RequestDetail> RequestDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=10.234.1.89;Initial Catalog=NIDEC_UNIFORM;Persist Security Info=True;User ID=sa;Password=sa;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeliveryDetail>(entity =>
        {
            entity.HasKey(e => e.DeliveryDetailId).HasName("PK__Delivery__EFD2C287B58D7321");

            entity.Property(e => e.DeliveryDetailId).HasColumnName("DeliveryDetailID");
            entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Size).HasMaxLength(50);
            entity.Property(e => e.Unit).HasMaxLength(50);

            entity.HasOne(d => d.Delivery).WithMany(p => p.DeliveryDetails)
                .HasForeignKey(d => d.DeliveryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeliveryDetails_Delivery");

            entity.HasOne(d => d.Product).WithMany(p => p.DeliveryDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeliveryDetails_Product");
        });

        modelBuilder.Entity<MDelivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryId).HasName("PK__Deliveri__626D8FEED074CDB0");

            entity.ToTable("M_Deliveries");

            entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeliveredBy).HasMaxLength(255);
            entity.Property(e => e.DeliveryDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmpolyeeId).HasColumnName("Empolyee_ID");
            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Empolyee).WithMany(p => p.MDeliveries)
                .HasForeignKey(d => d.EmpolyeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deliveries_Employee");

            entity.HasOne(d => d.Request).WithMany(p => p.MDeliveries)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deliveries_Request");
        });

        modelBuilder.Entity<MEmployee>(entity =>
        {
            entity.HasKey(e => e.EmpolyeeId).HasName("PK_M_Empolyee");

            entity.ToTable("M_Employees");

            entity.Property(e => e.EmpolyeeId)
                .ValueGeneratedNever()
                .HasColumnName("Empolyee_ID");
            entity.Property(e => e.Department).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Section).HasMaxLength(255);
        });

        modelBuilder.Entity<MProduct>(entity =>
        {
            entity.ToTable("M_Products");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Unit).HasMaxLength(255);
        });

        modelBuilder.Entity<MRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__M_Reques__33A8519A60486747");

            entity.ToTable("M_Requests");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmpolyeeId).HasColumnName("Empolyee_ID");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Empolyee).WithMany(p => p.MRequests)
                .HasForeignKey(d => d.EmpolyeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Employee");
        });

        modelBuilder.Entity<RequestDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__RequestD__135C314DC9F0A3FF");

            entity.Property(e => e.DetailId).HasColumnName("DetailID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.QuantityApproved).HasDefaultValue(0);
            entity.Property(e => e.QuantityDelivered).HasDefaultValue(0);
            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.Size).HasMaxLength(50);
            entity.Property(e => e.Unit).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.RequestDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestDetails_Product");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestDetails)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestDetails_Request");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
