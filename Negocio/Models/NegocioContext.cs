using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Negocio.Models
{
    public partial class NegocioContext : DbContext
    {
        public NegocioContext()
        {
        }

        public NegocioContext(DbContextOptions<NegocioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<TipoProducto> TipoProductos { get; set; } = null!;
        public virtual DbSet<Ventum> Venta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__07F4A132C3E564DE");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.Detalle)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("detalle");

                entity.Property(e => e.IdTipo).HasColumnName("idTipo");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.StockNuevo).HasColumnName("stockNuevo");

                entity.HasOne(d => d.OTipoProducto)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("FK_Tipo");
            });

            modelBuilder.Entity<TipoProducto>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK__TipoProd__BDD0DFE1AAB8EA9C");

                entity.ToTable("TipoProducto");

                entity.Property(e => e.IdTipo).HasColumnName("idTipo");

                entity.Property(e => e.Detalle)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__Venta__077D561497DF5542");

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.PrecioUnidad).HasColumnName("precioUnidad");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.Unidades).HasColumnName("unidades");

                entity.Property(e => e.fechaVenta).HasColumnName("fechaVenta");

                entity.HasOne(d => d.OIdProducto)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_Productos");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
