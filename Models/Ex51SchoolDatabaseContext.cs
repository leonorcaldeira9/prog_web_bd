using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolApp.Models;

public partial class Ex51SchoolDatabaseContext : DbContext
{
    public Ex51SchoolDatabaseContext()
    {
    }

    public Ex51SchoolDatabaseContext(DbContextOptions<Ex51SchoolDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassDetail> ClassDetails { get; set; }

    public virtual DbSet<CurricularUnit> CurricularUnits { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-75ANAD1T\\SQLEXPRESS;Database=Ex.5.1_SchoolDatabase; Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Idclass).HasColumnName("IDClass");

            entity.HasOne(d => d.IdclassNavigation).WithMany()
                .HasForeignKey(d => d.Idclass)
                .HasConstraintName("FK__Classes__IDClass__300424B4");

            entity.HasOne(d => d.StudentNavigation).WithMany()
                .HasForeignKey(d => d.Student)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Classes__Student__30F848ED");
        });

        modelBuilder.Entity<ClassDetail>(entity =>
        {
            entity.HasKey(e => e.IdclassDetail).HasName("PK__ClassDet__C002D3AFA2D183A1");

            entity.Property(e => e.IdclassDetail).HasColumnName("IDClassDetail");
            entity.Property(e => e.Name).HasMaxLength(80);

            entity.HasOne(d => d.CurricularUnitNavigation).WithMany(p => p.ClassDetails)
                .HasForeignKey(d => d.CurricularUnit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClassDeta__Curri__2E1BDC42");

            entity.HasOne(d => d.TeacherNavigation).WithMany(p => p.ClassDetails)
                .HasForeignKey(d => d.Teacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClassDeta__Teach__2D27B809");
        });

        modelBuilder.Entity<CurricularUnit>(entity =>
        {
            entity.HasKey(e => e.IdcurricularUnit).HasName("PK__Curricul__ABCB86339488AC29");

            entity.Property(e => e.IdcurricularUnit).HasColumnName("IDCurricularUnit");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Objectives).HasMaxLength(500);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Idpeople).HasName("PK__People__74193719EA98017E");

            entity.Property(e => e.Idpeople).HasColumnName("IDPeople");
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.RolesNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.Roles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__People__Roles__286302EC");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Idrole).HasName("PK__Roles__A1BA16C404D6C578");

            entity.Property(e => e.Idrole).HasColumnName("IDRole");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Label).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
