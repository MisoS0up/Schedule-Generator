using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sched.Models.Domain;

public partial class SchedContext : DbContext
{
    public SchedContext()
    {
    }

    public SchedContext(DbContextOptions<SchedContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Timeslot> Timeslots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= .\\SQLEXPress; Database= Sched ; Trusted_connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__C92D71A7BE967BCC");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.HasKey(e => e.DayId).HasName("PK__Day__BF3DD8C54C7C4089");

            entity.ToTable("Day");

            entity.Property(e => e.DayId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BEDD55B96CF");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.ProfessorId).HasName("PK__Professo__9003594952B57BF8");

            entity.ToTable("Professor");

            entity.Property(e => e.ProfessorId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Professors)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Professor__Depar__4BAC3F29");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__32863939E7BCD6E8");

            entity.ToTable("Room");

            entity.Property(e => e.RoomId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__9C8A5B498545FAFD");

            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).ValueGeneratedNever();

            entity.HasOne(d => d.Course).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Schedule__Course__571DF1D5");

            entity.HasOne(d => d.Day).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.DayId)
                .HasConstraintName("FK__Schedule__DayId__59FA5E80");

            entity.HasOne(d => d.Professor).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ProfessorId)
                .HasConstraintName("FK__Schedule__Profes__5629CD9C");

            entity.HasOne(d => d.Room).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Schedule__RoomId__5812160E");

            entity.HasOne(d => d.Timeslot).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.TimeslotId)
                .HasConstraintName("FK__Schedule__Timesl__59063A47");
        });

        modelBuilder.Entity<Timeslot>(entity =>
        {
            entity.HasKey(e => e.TimeslotId).HasName("PK__Timeslot__333FE971D6D62E71");

            entity.ToTable("Timeslot");

            entity.Property(e => e.TimeslotId).ValueGeneratedNever();
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
