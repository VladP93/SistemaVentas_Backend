using Microsoft.EntityFrameworkCore;
using SistemaVentas.Datos.Mapping.Almacen;
using SistemaVentas.Datos.Mapping.Usuarios;
using SistemaVentas.Entidades.Almacen;
using SistemaVentas.Entidades.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Datos
{
    public class DbContextSistema: DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new ArticuloMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }
    }
}
