using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

public partial class PersonDbContext : DbContext
{
    public PersonDbContext()
    {
    }

    public PersonDbContext(DbContextOptions<PersonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudio> Estudios { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Profesion> Profesions { get; set; }

    public virtual DbSet<Telefono> Telefonos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-644DMF7\\SQLEXPRESS;Database=person_db;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__estudios__3213E83FA3354FBA");

            entity.ToTable("estudios");

            entity.HasIndex(e => new { e.IdProf, e.CcPer }, "UQ_estudios_cc_per_id_prof").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CcPer).HasColumnName("cc_per");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdProf).HasColumnName("id_prof");
            entity.Property(e => e.Univer)
                .HasMaxLength(50)
                .HasColumnName("univer");

            entity.HasOne(d => d.CcPerNavigation).WithMany(p => p.Estudios)
                .HasForeignKey(d => d.CcPer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudio_persona_fk");

            entity.HasOne(d => d.IdProfNavigation).WithMany(p => p.Estudios)
                .HasForeignKey(d => d.IdProf)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudio_profesion_fk");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Cc).HasName("PK__persona__3213666D1F6636E0");

            entity.ToTable("persona");

            entity.Property(e => e.Cc)
                .ValueGeneratedNever()
                .HasColumnName("cc");
            entity.Property(e => e.Apellido)
                .HasMaxLength(45)
                .HasColumnName("apellido");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Profesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__profesio__3213E83FAE0ECB39");

            entity.ToTable("profesion");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Des).HasColumnName("des");
            entity.Property(e => e.Nom)
                .HasMaxLength(90)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.HasKey(e => e.Num).HasName("PK__telefono__DF908D652CC525EA");

            entity.ToTable("telefono");

            entity.Property(e => e.Num)
                .HasMaxLength(15)
                .HasColumnName("num");
            entity.Property(e => e.Duenio).HasColumnName("duenio");
            entity.Property(e => e.Oper)
                .HasMaxLength(45)
                .HasColumnName("oper");

            entity.HasOne(d => d.DuenioNavigation).WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.Duenio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("telefono_persona_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
