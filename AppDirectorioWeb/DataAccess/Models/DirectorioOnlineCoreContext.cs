﻿using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccess.Models
{
    public partial class DirectorioOnlineCoreContext : IdentityDbContext
    {
        public DirectorioOnlineCoreContext()
        {
        }

        public DirectorioOnlineCoreContext(DbContextOptions<DirectorioOnlineCoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CatCategorium> CatCategoria { get; set; }
        public virtual DbSet<CatDepartamento> CatDepartamentos { get; set; }
        public virtual DbSet<CatPai> CatPais { get; set; }
        public virtual DbSet<CatPlan> CatPlans { get; set; }
        public virtual DbSet<CatTipoPagoXcatalogoConfig> CatTipoPagoXcatalogoConfigs { get; set; }
        public virtual DbSet<ConfigCatalogo> ConfigCatalogos { get; set; }
        public virtual DbSet<CuponNegocio> CuponNegocios { get; set; }
        public virtual DbSet<CuponRedencionUsuario> CuponRedencionUsuarios { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<FeatureNegocio> FeatureNegocios { get; set; }
        public virtual DbSet<HorarioNegocio> HorarioNegocios { get; set; }
        public virtual DbSet<ImagenesNegocio> ImagenesNegocios { get; set; }
        public virtual DbSet<ItemCatalogo> ItemCatalogos { get; set; }
        public virtual DbSet<Negocio> Negocios { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<CatCategorium>(entity =>
            {
                entity.ToTable("CatCategoria", "bs");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IdUserCreate)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.IdUserUpdate).HasMaxLength(450);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CatDepartamento>(entity =>
            {
                entity.ToTable("CatDepartamento", "bs");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IdUserCreate)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.IdUserUpdate).HasMaxLength(450);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.CatDepartamentos)
                    .HasForeignKey(d => d.IdPais)
                    .HasConstraintName("FK_CatDepartamento_CatPais");
            });

            modelBuilder.Entity<CatPai>(entity =>
            {
                entity.ToTable("CatPais", "bs");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IdUserCreate)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.IdUserUpdate).HasMaxLength(450);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CatPlan>(entity =>
            {
                entity.ToTable("CatPlan", "bs");

                entity.Property(e => e.PlanName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CatTipoPagoXcatalogoConfig>(entity =>
            {
                entity.ToTable("CatTipoPagoXcatalogoConfig", "bs");
            });

            modelBuilder.Entity<ConfigCatalogo>(entity =>
            {
                entity.ToTable("ConfigCatalogo", "bs");

                entity.HasComment("para saber si el catalogo es para prductos o servicios");

                entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(500);

                entity.Property(e => e.IdUsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.NombreCatalogo)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CuponNegocio>(entity =>
            {
                entity.ToTable("CuponNegocio", "bs");

                entity.Property(e => e.DescripcionPromocion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaExpiracionCupon).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.IdUsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.IdUsuarioModificacion).HasMaxLength(300);

                entity.Property(e => e.ImagenCupon).IsUnicode(false);

                entity.Property(e => e.NombrePromocion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ValorCupon).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<CuponRedencionUsuario>(entity =>
            {
                entity.ToTable("CuponRedencionUsuario", "bs");

                entity.Property(e => e.FechaRedencion).HasColumnType("datetime");

                entity.Property(e => e.IdUsuario)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.IdCuponNavigation)
                    .WithMany(p => p.CuponRedencionUsuarios)
                    .HasForeignKey(d => d.IdCupon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CuponRedencionUsuario_CuponNegocio");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Factura", "bs");

                entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaPago).HasColumnType("datetime");

                entity.Property(e => e.IdUserCreate)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.IdUserUpdate).HasMaxLength(450);

                entity.Property(e => e.MontoPagado).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NoAutorizacionPago)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<FeatureNegocio>(entity =>
            {
                entity.ToTable("FeatureNegocio", "bs");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IdUserCreate)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.IdUserUpdate).HasMaxLength(450);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdFeatureNavigation)
                    .WithMany(p => p.FeatureNegocios)
                    .HasForeignKey(d => d.IdFeature)
                    .HasConstraintName("FK_FeatureNegocio_CatCategoria");

                entity.HasOne(d => d.IdNegocioNavigation)
                    .WithMany(p => p.FeatureNegocios)
                    .HasForeignKey(d => d.IdNegocio)
                    .HasConstraintName("FK_FeatureNegocio_Negocio");
            });

            modelBuilder.Entity<HorarioNegocio>(entity =>
            {
                entity.ToTable("HorarioNegocio", "bs");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.HoraDesde)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoraHasta)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdUserCreate)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.IdUserUpdate).HasMaxLength(450);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdNegocioNavigation)
                    .WithMany(p => p.HorarioNegocios)
                    .HasForeignKey(d => d.IdNegocio)
                    .HasConstraintName("FK_HorarioNegocio_Negocio");
            });

            modelBuilder.Entity<ImagenesNegocio>(entity =>
            {
                entity.ToTable("ImagenesNegocio", "bs");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IdUserCreate)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.IdUserUpdate).HasMaxLength(450);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdNegocioNavigation)
                    .WithMany(p => p.ImagenesNegocios)
                    .HasForeignKey(d => d.IdNegocio)
                    .HasConstraintName("FK_ImagenesNegocio_Negocio");
            });

            modelBuilder.Entity<ItemCatalogo>(entity =>
            {
                entity.ToTable("ItemCatalogo", "bs");

                entity.Property(e => e.CodigoItemRef)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.DescripcionItem)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.IdUsuarioActualizacion).HasMaxLength(500);

                entity.Property(e => e.IdUsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ImagenItem)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.NombreItem)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdConfigCatalogoNavigation)
                    .WithMany(p => p.ItemCatalogos)
                    .HasForeignKey(d => d.IdConfigCatalogo)
                    .HasConstraintName("FK_ItemCatalogo_ConfigCatalogo");
            });

            modelBuilder.Entity<Negocio>(entity =>
            {
                entity.ToTable("Negocio", "bs");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DescripcionNegocio)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.DireccionNegocio)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.EmailNegocio)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FacebookUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("FacebookURL");

                entity.Property(e => e.IdUserCreate)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.IdUserOwner).HasMaxLength(450);

                entity.Property(e => e.IdUserUpdate).HasMaxLength(450);

                entity.Property(e => e.InstagramUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("InstagramURL");

                entity.Property(e => e.LinkedInUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("LinkedInURL");

                entity.Property(e => e.LogoNegocio).IsUnicode(false);

                entity.Property(e => e.NombreNegocio)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.SitioWebNegocio)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Tags).IsUnicode(false);

                entity.Property(e => e.TelefonoNegocio1)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoNegocio2)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoWhatsApp)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TwitterUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TwitterURL");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Negocios)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Negocio_CatCategoria");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Negocios)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Negocio_CatDepartamento");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Reviews", "bs");

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.EmailComment)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IdUser)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.IdBusinessNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.IdBusiness)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reviews_Negocio");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.ToTable("UserDetails", "bs");

                entity.Property(e => e.FullName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IdPlan).HasDefaultValueSql("((1))");

                entity.Property(e => e.IdUserCreate).HasMaxLength(450);

                entity.Property(e => e.IdUserUpdate).HasMaxLength(450);

                entity.Property(e => e.PlanExpirationDate).HasColumnType("date");

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.UserPicture).IsUnicode(false);

                entity.HasOne(d => d.IdPlanNavigation)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.IdPlan)
                    .HasConstraintName("FK_UserDetails_CatPlan");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
