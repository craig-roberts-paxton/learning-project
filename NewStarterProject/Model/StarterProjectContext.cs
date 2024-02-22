using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NewStarterProject.Model;

public partial class StarterProjectContext : DbContext
{
    public StarterProjectContext()
    {
    }

    public StarterProjectContext(DbContextOptions<StarterProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessAudit> AccessAudits { get; set; }

    public virtual DbSet<AccessControlDoorsToUser> AccessControlDoorsToUsers { get; set; }

    public virtual DbSet<Door> Doors { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DatabaseConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessAudit>(entity =>
        {
            entity.HasKey(e => e.AccessAuditId).HasName("PK__AccessAu__32678262023BFAA8");

            entity.ToTable("AccessAudit");

            entity.Property(e => e.AuditDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");

            entity.HasOne(d => d.Door).WithMany(p => p.AccessAudits)
                .HasForeignKey(d => d.DoorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccessAud__DoorI__276EDEB3");

            entity.HasOne(d => d.User).WithMany(p => p.AccessAudits)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccessAud__UserI__286302EC");
        });

        modelBuilder.Entity<AccessControlDoorsToUser>(entity =>
        {
            entity.HasKey(e => e.AccessControlDoorsToUsersId).HasName("PK__AccessCo__0E0BC36B1D906E1A");

            entity.HasOne(d => d.Door).WithMany(p => p.AccessControlDoorsToUsers)
                .HasForeignKey(d => d.DoorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccessCon__DoorI__21B6055D");

            entity.HasOne(d => d.User).WithMany(p => p.AccessControlDoorsToUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccessCon__UserI__22AA2996");
        });

        modelBuilder.Entity<Door>(entity =>
        {
            entity.HasKey(e => e.DoorId).HasName("PK__Doors__C5F818D920850A9B");

            entity.Property(e => e.DoorName).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C5339CB1D");

            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.UserName).HasMaxLength(20);
            entity.Property(e => e.PinCode).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
