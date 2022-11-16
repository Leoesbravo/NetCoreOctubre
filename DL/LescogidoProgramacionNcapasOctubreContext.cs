using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class LescogidoProgramacionNcapasOctubreContext : DbContext
{
    public LescogidoProgramacionNcapasOctubreContext()
    {
    }

    public LescogidoProgramacionNcapasOctubreContext(DbContextOptions<LescogidoProgramacionNcapasOctubreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Materium> Materia { get; set; }

    public virtual DbSet<Plantel> Plantels { get; set; }

    public virtual DbSet<Semestre> Semestres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-VA31VKK7; Database= LEscogidoProgramacionNCapasOctubre; Trusted_Connection=True; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumno).HasName("PK__Alumno__460B474039805EFB");

            entity.ToTable("Alumno");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Imagen).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.IdSemestreNavigation).WithMany(p => p.Alumnos)
                .HasForeignKey(d => d.IdSemestre)
                .HasConstraintName("FK__Alumno__IdSemest__164452B1");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.IdGrupo).HasName("PK__Grupo__303F6FD9BBFE666F");

            entity.ToTable("Grupo");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPlantelNavigation).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.IdPlantel)
                .HasConstraintName("FK__Grupo__IdPlantel__38996AB5");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK__Horario__1539229BB8541D92");

            entity.ToTable("Horario");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Horarios)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("FK__Horario__IdAlumn__3E52440B");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.Horarios)
                .HasForeignKey(d => d.IdGrupo)
                .HasConstraintName("FK__Horario__IdGrupo__3B75D760");
        });

        modelBuilder.Entity<Materium>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PK__Materia__EC174670D7C0914E");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSemestreNavigation).WithMany(p => p.Materia)
                .HasForeignKey(d => d.IdSemestre)
                .HasConstraintName("FK__Materia__IdSemes__239E4DCF");
        });

        modelBuilder.Entity<Plantel>(entity =>
        {
            entity.HasKey(e => e.IdPlantel).HasName("PK__Plantel__485FDCFED89E8BDF");

            entity.ToTable("Plantel");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Semestre>(entity =>
        {
            entity.HasKey(e => e.IdSemestre).HasName("PK__Semestre__BD1FD7F893F502A7");

            entity.ToTable("Semestre");

            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
