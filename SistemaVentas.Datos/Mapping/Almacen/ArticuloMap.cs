using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Entidades.Almacen;

namespace SistemaVentas.Datos.Mapping.Almacen
{
    public class ArticuloMap : IEntityTypeConfiguration<Articulo>
    {
        public void Configure(EntityTypeBuilder<Articulo> builder)
        {
            builder.ToTable("articulo").HasKey(a => a.Idarticulo);
        }
    }
}
