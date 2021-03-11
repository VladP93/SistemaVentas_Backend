using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Datos.Mapping.Almacen
{
    public class DetalleIngresoMap : IEntityTypeConfiguration<DetalleIngreso>
    {
        public void Configure(EntityTypeBuilder<DetalleIngreso> builder)
        {

            builder.ToTable("detalle_ingreso").HasKey(d => d.Iddetalle_ingreso);

        }
    }
}
