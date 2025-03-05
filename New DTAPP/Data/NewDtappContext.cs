using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace New_DTAPP.Data;

public partial class NewDtappContext : DbContext
{
    public NewDtappContext()
    {
    }

    public NewDtappContext(DbContextOptions<NewDtappContext> options)
        : base(options)
    {
    }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<System> Systems { get; set; }

    public virtual DbSet<Transfer> Transfers { get; set; }

    public virtual DbSet<TransferType> TransferType { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<File>(entity =>
        {
            entity.ToTable("file");

            entity.Property(e => e.FileId).HasColumnName("file_id");
            entity.Property(e => e.Comment)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.FileName)
                .IsUnicode(false)
                .HasColumnName("file_name");
            entity.Property(e => e.FileSent).HasColumnName("file_sent");
            entity.Property(e => e.FileSize)
                .IsUnicode(false)
                .HasColumnName("file_size");
            entity.Property(e => e.FileExt)
                .IsUnicode(false)
                .HasColumnName("file_ext");
            entity.Property(e => e.TransferId).HasColumnName("transfer_id");

            entity.HasOne(d => d.Transfer).WithMany(p => p.Files)
                .HasForeignKey(d => d.TransferId)
                .HasConstraintName("FK_file_transfer");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.ToTable("operation");

            entity.Property(e => e.OperationId).HasColumnName("operation_id");
            entity.Property(e => e.Archived).HasColumnName("archived");
            entity.Property(e => e.OperationName)
                .IsUnicode(false)
                .HasColumnName("operation_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Ordering).HasColumnName("ordering");
            entity.Property(e => e.RoleName)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<System>(entity =>
        {
            entity.ToTable("system");

            entity.Property(e => e.SystemId).HasColumnName("system_id");
            entity.Property(e => e.Archived).HasColumnName("archived");
            entity.Property(e => e.SystemName)
                .IsUnicode(false)
                .HasColumnName("system_name");
        });

        modelBuilder.Entity<TransferType>(entity =>
        {
            entity.ToTable("transferType");

            entity.Property(e => e.TransferTypeId).HasColumnName("id");
            entity.Property(e => e.TransferTypeDesc).IsUnicode(false).HasColumnName("transfer_type");
            entity.Property(e => e.Archived).HasColumnName("archived");
            entity.Property(e => e.Ordering).HasColumnName("ordering");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.ToTable("transfer");

            entity.Property(e => e.TransferId).HasColumnName("transfer_id");
            entity.Property(e => e.ClientName)
                .IsUnicode(false)
                .HasColumnName("client_name");
            entity.Property(e => e.ClientUnitId).HasColumnName("client_unit_id");
            entity.Property(e => e.Comments)
                .IsUnicode(false)
                .HasColumnName("comments");
            entity.Property(e => e.Completed).HasColumnName("completed");
            entity.Property(e => e.CompletedAt)
                .HasColumnType("datetime")
                .HasColumnName("completed_at");
            entity.Property(e => e.CompletedUserId).HasColumnName("completed_user_id");
            entity.Property(e => e.DestSystemId).HasColumnName("dest_system_id");
            entity.Property(e => e.IssoApproval).HasColumnName("isso_approval");
            entity.Property(e => e.IssueReported).HasColumnName("issue_reported");
            entity.Property(e => e.MachineName)
                .IsUnicode(false)
                .HasColumnName("machine_name");
            entity.Property(e => e.OperationId).HasColumnName("operation_id");
            entity.Property(e => e.OrigSystemId).HasColumnName("orig_system_id");
            entity.Property(e => e.RequestCreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("request_created_at");
            entity.Property(e => e.Reviewed).HasColumnName("reviewed");
            entity.Property(e => e.ReviewedAt)
                .HasColumnType("datetime")
                .HasColumnName("reviewed_at");
            entity.Property(e => e.ReviewedUserId).HasColumnName("reviewed_user_id");
            entity.Property(e => e.SentTime)
                .HasColumnType("datetime")
                .HasColumnName("sent_time");
            entity.Property(e => e.SpillOccurred).HasColumnName("spill_occurred");
            entity.Property(e => e.SpillPrevented).HasColumnName("spill_prevented");
            entity.Property(e => e.Urgent).HasColumnName("urgent");
            entity.Property(e => e.VirusDefinitionDate)
                .HasColumnType("datetime")
                .HasColumnName("virus_definition_date");
            entity.Property(e => e.TransferTypeId).HasColumnName("transfer_type");

            
            entity.HasOne(d => d.ClientUnit).WithMany(p => p.Transfers)
                .HasForeignKey(d => d.ClientUnitId)
                .HasConstraintName("FK_transfer_unit");

            entity.HasOne(d => d.CompletedUser).WithMany(p => p.TransferCompletedUsers)
                .HasForeignKey(d => d.CompletedUserId)
                .HasConstraintName("FK_transfer_user");

            entity.HasOne(d => d.DestSystem).WithMany(p => p.TransferDestSystems)
                .HasForeignKey(d => d.DestSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transfer_system1");

            entity.HasOne(d => d.Operation).WithMany(p => p.Transfers)
                .HasForeignKey(d => d.OperationId)
                .HasConstraintName("FK_transfer_operation");

            entity.HasOne(d => d.OrigSystem).WithMany(p => p.TransferOrigSystems)
                .HasForeignKey(d => d.OrigSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transfer_system");

            entity.HasOne(d => d.ReviewedUser).WithMany(p => p.TransferReviewedUsers)
                .HasForeignKey(d => d.ReviewedUserId)
                .HasConstraintName("FK_transfer_user1");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("unit");

            entity.Property(e => e.UnitId).HasColumnName("unit_id");
            entity.Property(e => e.Archived).HasColumnName("archived");
            entity.Property(e => e.UnitName)
                .IsUnicode(false)
                .HasColumnName("unit_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Alias)
                .IsUnicode(false)
                .HasColumnName("alias");
            entity.Property(e => e.Disabled).HasColumnName("disabled");
            entity.Property(e => e.Email)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Username)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
