using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InoviDataAccessLayer.EntityModel
{
    public partial class QueryProContext : DbContext
    {
        public QueryProContext()
        {
        }

        public QueryProContext(DbContextOptions<QueryProContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAttachment> TblAttachments { get; set; } = null!;
        public virtual DbSet<TblProfile> TblProfiles { get; set; } = null!;
        public virtual DbSet<TblQuery> TblQueries { get; set; } = null!;
        public virtual DbSet<TblQueryAttachment> TblQueryAttachments { get; set; } = null!;
        public virtual DbSet<TblQueryStatus> TblQueryStatuses { get; set; } = null!;
        public virtual DbSet<TblRole> TblRoles { get; set; } = null!;
        public virtual DbSet<TblStatus> TblStatuses { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAttachment>(entity =>
            {
                entity.HasKey(e => e.AttachmentLinkId);

                entity.ToTable("tblAttachment");

                entity.Property(e => e.AttachmentLinkId).HasColumnName("attachment_link_id");

                entity.Property(e => e.AttachmentLink).HasColumnName("attachment_link");

                entity.Property(e => e.CreatedById).HasColumnName("created_by_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.Filebytes).HasColumnName("filebytes");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ModifiedById).HasColumnName("modified_by_id");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Path).HasColumnName("path");
            });

            modelBuilder.Entity<TblProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId);

                entity.ToTable("tblProfile");

                entity.Property(e => e.ProfileId).HasColumnName("profile_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.ProfileLink).HasColumnName("profile_link");

                entity.Property(e => e.ProfilePath).HasColumnName("profile_path");

                entity.Property(e => e.Profilebyte).HasColumnName("profilebyte");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblProfiles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tblProfile_tblUser");
            });

            modelBuilder.Entity<TblQuery>(entity =>
            {
                entity.HasKey(e => e.QueryId)
                    .HasName("PK_Query");

                entity.ToTable("tblQuery");

                entity.Property(e => e.QueryId).HasColumnName("query_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("current_status");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on");

                entity.Property(e => e.Remarks).HasColumnName("remarks");

                entity.Property(e => e.RemarksBy).HasColumnName("remarks_by");

                entity.Property(e => e.RemarksOn)
                    .HasColumnType("datetime")
                    .HasColumnName("remarks_on");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblQueries)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Query_User");
            });

            modelBuilder.Entity<TblQueryAttachment>(entity =>
            {
                entity.HasKey(e => e.QueryAttachmentId);

                entity.ToTable("tblQueryAttachment");

                entity.Property(e => e.QueryAttachmentId).HasColumnName("query_attachment_id");

                entity.Property(e => e.AttachmentLinkId).HasColumnName("attachment_link_id");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.QueryId).HasColumnName("query_id");

                entity.HasOne(d => d.AttachmentLink)
                    .WithMany(p => p.TblQueryAttachments)
                    .HasForeignKey(d => d.AttachmentLinkId)
                    .HasConstraintName("FK_tblQueryAttachment_tblAttachment1");

                entity.HasOne(d => d.Query)
                    .WithMany(p => p.TblQueryAttachments)
                    .HasForeignKey(d => d.QueryId)
                    .HasConstraintName("FK_tblQueryAttachment_tblQuery");
            });

            modelBuilder.Entity<TblQueryStatus>(entity =>
            {
                entity.ToTable("tblQueryStatus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.QueryId).HasColumnName("query_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Query)
                    .WithMany(p => p.TblQueryStatuses)
                    .HasForeignKey(d => d.QueryId)
                    .HasConstraintName("FK_QueryStatus_Query");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblQueryStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_QueryStatus_Status");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.HasKey(e => e.UserRoleId)
                    .HasName("PK_UserRole");

                entity.ToTable("tblRole");

                entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");
            });

            modelBuilder.Entity<TblStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK_Status");

                entity.ToTable("tblStatus");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_User");

                entity.ToTable("tblUser");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .HasColumnName("email_address");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_on")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.UserRoleId)
                    .HasConstraintName("FK_User_UserRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
