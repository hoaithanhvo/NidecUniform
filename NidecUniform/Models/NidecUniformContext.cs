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

    public virtual DbSet<M_Delivery> M_Deliveries { get; set; }

    public virtual DbSet<M_Employee> M_Employees { get; set; }

    public virtual DbSet<M_Product> M_Products { get; set; }

    public virtual DbSet<M_Request> M_Requests { get; set; }

    public virtual DbSet<RequestDetail> RequestDetails { get; set; }

    public virtual DbSet<RawData> RawData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseSqlServer("Data Source=LAPTOP-99421S3D\\SQLEXPRESS; Initial Catalog=NidecUniform; Integrated Security=True; Encrypt=True; Trust Server Certificate=True");
        => optionsBuilder.UseSqlServer("Data Source = 10.234.1.89; Initial Catalog = NidecUniform; Persist Security Info=True;User ID = sa; Password=sa;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeliveryDetail>(entity =>
        {
            entity.Property(e => e.ID).HasColumnName("ID");
        });

        modelBuilder.Entity<M_Delivery>(entity =>
        {
            entity.ToTable("M_Delivery");

            entity.Property(e => e.ID).HasColumnName("ID");
        });

        modelBuilder.Entity<M_Employee>(entity =>
        {
            entity.ToTable("M_Employee");

            //entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.EmployeeID).HasMaxLength(20);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Position).HasMaxLength(50);
        });

        modelBuilder.Entity<M_Product>(entity =>
        {
            entity.ToTable("M_Product");

            //entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.ProductID)
                .HasMaxLength(20)
                .HasColumnName("ProductID");
            entity.Property(e => e.ProductName).HasMaxLength(50);
        });

        modelBuilder.Entity<M_Request>(entity =>
        {
            entity.ToTable("M_Request");

            entity.HasIndex(e => e.EmployeeID, "IX_MRequests_EmpolyeeId");

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.RequestType).HasMaxLength(30);
        });

        modelBuilder.Entity<RequestDetail>(entity =>
        {
            entity.HasIndex(e => e.RequestID, "IX_RequestDetails_RequestId");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestDetails).HasForeignKey(d => d.RequestID);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
