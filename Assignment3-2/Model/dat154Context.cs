using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment3_2.Model
{
    public partial class dat154Context : DbContext
    {
        public dat154Context()
        {
        }

        public dat154Context(DbContextOptions<dat154Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=dat154.hvl.no,1443;Initial Catalog=dat154;Persist Security Info=False;User ID=dat154_rw;Password=dat154_rw;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Coursecode)
                    .HasName("pk_course");

                entity.ToTable("course");

                entity.Property(e => e.Coursecode)
                    .HasColumnName("coursecode")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Coursename)
                    .IsRequired()
                    .HasColumnName("coursename")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Semester)
                    .IsRequired()
                    .HasColumnName("semester")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Teacher)
                    .IsRequired()
                    .HasColumnName("teacher")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => new { e.Coursecode, e.Studentid })
                    .HasName("pk_grade");

                entity.ToTable("grade");

                entity.Property(e => e.Coursecode)
                    .HasColumnName("coursecode")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Studentid).HasColumnName("studentid");

                entity.Property(e => e.Grade1)
                    .IsRequired()
                    .HasColumnName("grade")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.CoursecodeNavigation)
                    .WithMany(p => p.Grade)
                    .HasForeignKey(d => d.Coursecode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Grade)
                    .HasForeignKey(d => d.Studentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_student");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Studentage).HasColumnName("studentage");

                entity.Property(e => e.Studentname)
                    .IsRequired()
                    .HasColumnName("studentname")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
