using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Entidades.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Datos.Mapping.Usuarios
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario").HasKey(u => u.Idusuario);
        }
    }
}
