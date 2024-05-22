using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QRMascotas.Models;

namespace QRMascotas.context;

public partial class QrmascotasContext : IdentityDbContext<ApplicationUser>
{
    public QrmascotasContext()
    {
    }

    public QrmascotasContext(DbContextOptions<QrmascotasContext> options)
        : base(options)
    {
    }


    public virtual DbSet<DuenoAlternativo> DuenoAlternativos { get; set; }

    public virtual DbSet<Especy> Especies { get; set; }

    public virtual DbSet<Mascota> Mascotas { get; set; }

    public virtual DbSet<Raza> Razas { get; set; }

    public virtual DbSet<Vacuna> Vacunas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=PRISCILLA;Initial Catalog=QRMascotas;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False;Command Timeout=0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DuenoAlternativo>(entity =>
        {
            entity.HasKey(e => e.IdDuenoAlternativo);

            entity.ToTable("DuenoAlternativo");

            entity.Property(e => e.ApellidoM).HasMaxLength(50);
            entity.Property(e => e.ApellidoP).HasMaxLength(50);
            entity.Property(e => e.Contacto).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Especy>(entity =>
        {
            entity.HasKey(e => e.IdEspecie);

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Mascota>(entity =>
        {
            entity.HasKey(e => e.IdMascota);

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.IdDueno).HasMaxLength(450);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Qr).HasColumnName("QR");

            entity.HasOne(d => d.IdDuenoNavigation).WithMany(p => p.Mascota)
                .HasForeignKey(d => d.IdDueno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mascotas_AspNetUsers");

            entity.HasOne(d => d.IdDuenoAlternativoNavigation).WithMany(p => p.Mascota)
                .HasForeignKey(d => d.IdDuenoAlternativo)
                .HasConstraintName("FK_Mascotas_DuenoAlternativo");

            entity.HasOne(d => d.IdEspecieNavigation).WithMany(p => p.Mascota)
                .HasForeignKey(d => d.IdEspecie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mascotas_Especies");
        });

        modelBuilder.Entity<Raza>(entity =>
        {
            entity.HasKey(e => e.IdRaza);

            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.IdEspecieNavigation).WithMany(p => p.Razas)
                .HasForeignKey(d => d.IdEspecie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Razas_Especies");
        });

        modelBuilder.Entity<Vacuna>(entity =>
        {
            entity.HasKey(e => e.IdVacuna);

            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.IdEspecieNavigation).WithMany(p => p.Vacunas)
                .HasForeignKey(d => d.IdEspecie)
                .HasConstraintName("FK_Vacunas_Especies");
        });
      
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
