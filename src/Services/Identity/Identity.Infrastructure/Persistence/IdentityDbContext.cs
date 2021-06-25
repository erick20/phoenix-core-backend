using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Identity.Domain.Entities;

#nullable disable

namespace Identity.Infrastructure.Persistence
{
    public partial class IdentityDbContext : DbContext
    {
        public IdentityDbContext()
        {
        }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<CredentialHistory> CredentialHistories { get; set; }
        public virtual DbSet<CustomerExtraPermission> CustomerExtraPermissions { get; set; }
        public virtual DbSet<Limit> Limits { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionGroup> PermissionGroups { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleGroup> RoleGroups { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF8");

            modelBuilder.Entity<Credential>(entity =>
            {
                entity.ToTable("credential");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CustomerState)
                    .HasColumnName("customer_state")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("password");

                entity.Property(e => e.Pin)
                    .HasMaxLength(250)
                    .HasColumnName("pin");

                entity.Property(e => e.ResetDate).HasColumnName("reset_date");
            });

            modelBuilder.Entity<CredentialHistory>(entity =>
            {
                entity.ToTable("credential_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CredentialId).HasColumnName("credential_id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("password");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            });

            modelBuilder.Entity<CustomerExtraPermission>(entity =>
            {
                entity.ToTable("customer_extra_permission");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('credential_id_seq'::regclass)");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            });

            modelBuilder.Entity<Limit>(entity =>
            {
                entity.ToTable("limit");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

                entity.Property(e => e.MaxLimit).HasColumnName("max_limit");

                entity.Property(e => e.MinLimit).HasColumnName("min_limit");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Limits)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("limit_role_fk");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission");

                entity.HasIndex(e => e.ActionKey, "permission_action_key_un")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "permission_key_un")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionKey)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("action_key");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("display_name");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("key");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("permission_fk");
            });

            modelBuilder.Entity<PermissionGroup>(entity =>
            {
                entity.ToTable("permission_group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('role_id_seq1'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.RoleGroupId).HasColumnName("role_group_id");

                entity.Property(e => e.WarehouseTypeId).HasColumnName("warehouse_type_id");

                entity.HasOne(d => d.RoleGroup)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.RoleGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("role_fk");
            });

            modelBuilder.Entity<RoleGroup>(entity =>
            {
                entity.ToTable("role_group");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('roll_group_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("role_permission");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("role_permission_fk");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("role_permission_fk_role");
            });

            modelBuilder.HasSequence("permission_id_seq");

            modelBuilder.HasSequence("role_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
