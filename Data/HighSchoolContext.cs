using System;
using System.Collections.Generic;
using HighSchool.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HighSchool.Data;

public partial class HighSchoolContext : DbContext
{
    private readonly string _connectionString;

    public HighSchoolContext()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        var configuration = builder.Build();
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public HighSchoolContext(DbContextOptions<HighSchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseEnrollment> CourseEnrollments { get; set; }

    public virtual DbSet<GradeValue> GradeValues { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<VwCourseStatistic> VwCourseStatistics { get; set; }

    public virtual DbSet<VwRecentGrade> VwRecentGrades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927C0A7CF8F41");

            entity.HasIndex(e => e.ClassName, "UQ__Classes__F8BF561B91D13535").IsUnique();

            entity.Property(e => e.ClassName).HasMaxLength(10);
            entity.Property(e => e.Section)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71A797958928");

            entity.HasIndex(e => e.CourseCode, "UQ__Courses__FC00E0002FBB3991").IsUnique();

            entity.Property(e => e.CourseCode).HasMaxLength(10);
            entity.Property(e => e.CourseName).HasMaxLength(250);
        });

        modelBuilder.Entity<CourseEnrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__CourseEn__7F68771BB2EC3939");

            entity.Property(e => e.FkcourseId).HasColumnName("FKCourseId");
            entity.Property(e => e.FkstudentId).HasColumnName("FKStudentId");
            entity.Property(e => e.FkteacherId).HasColumnName("FKTeacherId");
            entity.Property(e => e.Grade)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.GradeAssignedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Fkcourse).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.FkcourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollment_Course");

            entity.HasOne(d => d.Fkstudent).WithMany(p => p.CourseEnrollmentFkstudents)
                .HasForeignKey(d => d.FkstudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollment_Student");

            entity.HasOne(d => d.Fkteacher).WithMany(p => p.CourseEnrollmentFkteachers)
                .HasForeignKey(d => d.FkteacherId)
                .HasConstraintName("FK_Enrollment_Teacher");

            entity.HasOne(d => d.GradeNavigation).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.Grade)
                .HasConstraintName("FK_Enrollment_Grade");
        });

        modelBuilder.Entity<GradeValue>(entity =>
        {
            entity.HasKey(e => e.Grade).HasName("PK__GradeVal__DF0ADB7B253E5516");

            entity.HasIndex(e => e.GradeValue1, "UC_GradeValue").IsUnique();

            entity.Property(e => e.Grade)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.GradeValue1).HasColumnName("GradeValue");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__People__AA2FFBE58AE1BBAF");

            entity.HasIndex(e => e.Pin, "UC_PIN").IsUnique();

            entity.Property(e => e.FirstName).HasMaxLength(200);
            entity.Property(e => e.FkclassId).HasColumnName("FKClassId");
            entity.Property(e => e.FkpositionId).HasColumnName("FKPositionId");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Pin)
                .HasMaxLength(13)
                .HasColumnName("PIN");

            entity.HasOne(d => d.Fkclass).WithMany(p => p.People)
                .HasForeignKey(d => d.FkclassId)
                .HasConstraintName("FK_People_Class");

            entity.HasOne(d => d.Fkposition).WithMany(p => p.People)
                .HasForeignKey(d => d.FkpositionId)
                .HasConstraintName("FK_People_Position");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A798A530271");

            entity.HasIndex(e => e.PositionName, "UQ__Position__E46AEF42004C9077").IsUnique();

            entity.Property(e => e.PositionName).HasMaxLength(50);
        });

        modelBuilder.Entity<VwCourseStatistic>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_CourseStatistics");

            entity.Property(e => e.CourseName).HasMaxLength(250);
            entity.Property(e => e.HighestGrade)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LowestGrade)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<VwRecentGrade>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_RecentGrades");

            entity.Property(e => e.CourseName).HasMaxLength(250);
            entity.Property(e => e.Grade)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.GradeAssignedDate).HasColumnType("datetime");
            entity.Property(e => e.StudentName).HasMaxLength(301);
            entity.Property(e => e.TeacherName).HasMaxLength(301);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
