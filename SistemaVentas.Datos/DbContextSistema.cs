using Microsoft.EntityFrameworkCore;
using SistemaVentas.Datos.Mapping.Almacen;
using SistemaVentas.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Datos
{
    public class DbContextSistema: DbContext
    {
        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoriaMap());
        }
    }
}
