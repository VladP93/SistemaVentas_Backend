using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Datos.Mapping.Ventas
{
    public class VentaMap : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.ToTable("venta").HasKey(v => v.Idventa);
            builder.HasOne(v => v.Persona).WithMany(p => p.Ventas).HasForeignKey(v => v.Idcliente);
        }
    }
}
