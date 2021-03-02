using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Entidades.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Datos.Mapping.Usuarios
{
    public class RolMap : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("rol").HasKey(r => r.Idrol);
        }
    }
}
