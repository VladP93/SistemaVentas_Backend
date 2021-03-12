using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Datos.Mapping.Ventas
{
    public class DetalleVentaMap : IEntityTypeConfiguration<DetalleVenta>
    {
        public void Configure(EntityTypeBuilder<DetalleVenta> builder)
        {
            builder.ToTable("detalle_venta").HasKey(d => d.Iddetalle_venta);
        }
    }
}
