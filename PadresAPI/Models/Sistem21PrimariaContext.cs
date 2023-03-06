using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PadresAPI.Models;

public partial class Sistem21PrimariaContext : DbContext
{
    public Sistem21PrimariaContext()
    {
    }

    public Sistem21PrimariaContext(DbContextOptions<Sistem21PrimariaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumno { get; set; }

    public virtual DbSet<AlumnoTutor> AlumnoTutor { get; set; }

    public virtual DbSet<Asignatura> Asignatura { get; set; }

    public virtual DbSet<Calificacion> Calificacion { get; set; }

    public virtual DbSet<Director> Director { get; set; }

    public virtual DbSet<Docente> Docente { get; set; }

    public virtual DbSet<DocenteAsignatura> DocenteAsignatura { get; set; }

    public virtual DbSet<Grupo> Grupo { get; set; }

    public virtual DbSet<Periodo> Periodo { get; set; }

    public virtual DbSet<TipoAsignatura> TipoAsignatura { get; set; }

    public virtual DbSet<Tipodocente> Tipodocente { get; set; }

    public virtual DbSet<Tutor> Tutor { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql
        ("server=sistemas19.com;database=sistem21_primaria;user=sistem21_primaria;password=sistemas19_"
            , Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("alumno");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<AlumnoTutor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("alumno_tutor");

            entity.HasIndex(e => e.IdAlumno, "fkAlumno_idx");

            entity.HasIndex(e => e.IdTutor, "fkTutor_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdAlumno).HasColumnType("int(11)");
            entity.Property(e => e.IdTutor).HasColumnType("int(11)");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.AlumnoTutor)
                .HasForeignKey(d => d.IdAlumno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkAlumno");

            entity.HasOne(d => d.IdTutorNavigation).WithMany(p => p.AlumnoTutor)
                .HasForeignKey(d => d.IdTutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkTutor");
        });

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignatura");

            entity.HasIndex(e => e.IdTipo, "fkAsignatura_Tipo_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdTipo).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(200);

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Asignatura)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkAsignatura_Tipo");
        });

        modelBuilder.Entity<Calificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("calificacion");

            entity.HasIndex(e => e.IdAlumno, "fkCal_Alumno_idx");

            entity.HasIndex(e => e.IdAsignatura, "fkCal_Asignatura_idx");

            entity.HasIndex(e => e.IdDocente, "fkCal_Docente_idx");

            entity.HasIndex(e => e.IdPeriodo, "fkCal_Periodo_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Calificacion1).HasColumnName("Calificacion");
            entity.Property(e => e.IdAlumno).HasColumnType("int(11)");
            entity.Property(e => e.IdAsignatura).HasColumnType("int(11)");
            entity.Property(e => e.IdDocente).HasColumnType("int(11)");
            entity.Property(e => e.IdPeriodo).HasColumnType("int(11)");
            entity.Property(e => e.Unidad).HasColumnType("int(11)");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Calificacion)
                .HasForeignKey(d => d.IdAlumno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkCal_Alumno");

            entity.HasOne(d => d.IdAsignaturaNavigation).WithMany(p => p.Calificacion)
                .HasForeignKey(d => d.IdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkCal_Asignatura");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.Calificacion)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkCal_Docente");

            entity.HasOne(d => d.IdPeriodoNavigation).WithMany(p => p.Calificacion)
                .HasForeignKey(d => d.IdPeriodo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkCal_Periodo");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("director");

            entity.HasIndex(e => e.Idusuario, "fkDocente_Usaurio_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Idusuario)
                .HasColumnType("int(11)")
                .HasColumnName("idusuario");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(10);

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Director)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkdirector_Usaurio");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("docente");

            entity.HasIndex(e => e.IdTipoDocente, "fkDocente_TipoDocente_idx");

            entity.HasIndex(e => e.Idusuario, "fkDocente_usuario_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdTipoDocente).HasColumnType("int(11)");
            entity.Property(e => e.Idusuario)
                .HasColumnType("int(11)")
                .HasColumnName("idusuario");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdTipoDocenteNavigation).WithMany(p => p.Docente)
                .HasForeignKey(d => d.IdTipoDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkDocente_TipoDocente");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Docente)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkDocente_usuario");
        });

        modelBuilder.Entity<DocenteAsignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("docente_asignatura");

            entity.HasIndex(e => e.IdAsignatura, "fkAsignatura_idx");

            entity.HasIndex(e => e.IdDocente, "fkDocente_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdAsignatura).HasColumnType("int(11)");
            entity.Property(e => e.IdDocente).HasColumnType("int(11)");

            entity.HasOne(d => d.IdAsignaturaNavigation).WithMany(p => p.DocenteAsignatura)
                .HasForeignKey(d => d.IdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkAsignatura");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.DocenteAsignatura)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkDocente");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("grupo");

            entity.HasIndex(e => e.IdDocente, "fkGrupoDocente_Grupo_idx");

            entity.HasIndex(e => e.IdAlumno, "fkGrupo_Alumno_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdAlumno).HasColumnType("int(11)");
            entity.Property(e => e.IdDocente).HasColumnType("int(11)");
            entity.Property(e => e.Seccion)
                .HasMaxLength(2)
                .HasColumnName("seccion");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Grupo)
                .HasForeignKey(d => d.IdAlumno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkGrupo_Alumno");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.Grupo)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkGrupo_Docente");
        });

        modelBuilder.Entity<Periodo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("periodo");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Año).HasColumnType("year(4)");
        });

        modelBuilder.Entity<TipoAsignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipo_asignatura");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.TipoAsignatura1)
                .HasMaxLength(20)
                .HasColumnName("TipoAsignatura");
        });

        modelBuilder.Entity<Tipodocente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipodocente");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Tipo).HasMaxLength(45);
        });

        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tutor");

            entity.HasIndex(e => e.Idusuario, "fkPadre_Usuario_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Idusuario)
                .HasColumnType("int(11)")
                .HasColumnName("idusuario");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Telefono).HasMaxLength(10);

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Tutor)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPadre_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Contraseña)
                .HasColumnType("tinytext")
                .HasColumnName("contraseña");
            entity.Property(e => e.Rol)
                .HasColumnType("int(11)")
                .HasColumnName("rol");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(45)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
