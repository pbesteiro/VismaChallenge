using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VismaUserCore.Entities;

namespace VismaUserInsfrestucture.Data
{
    public partial class VismaChallengeContext : DbContext
    {
        public VismaChallengeContext()
        {
        }

        public VismaChallengeContext(DbContextOptions<VismaChallengeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<RolPermissions> RolPermissions { get; set; }
        public virtual DbSet<Rols> Rols { get; set; }
        public virtual DbSet<RolsUser> RolsUser { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=VismaChallenge;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departments>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserManager)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.UserManagerId)
                    .HasConstraintName("FK_Departments_Manager");
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolPermissions>(entity =>
            {
                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_RolPermissions_Permission");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.RolPermissions)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_RolPermissions_Rol");
            });

            modelBuilder.Entity<Rols>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolsUser>(entity =>
            {
                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.RolsUser)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_RolsUser_Rol");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RolsUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RolsUser_User");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

        }

    }
}
