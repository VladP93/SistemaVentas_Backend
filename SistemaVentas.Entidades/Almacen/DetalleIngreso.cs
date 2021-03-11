using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaVentas.Entidades.Almacen
{
    public class DetalleIngreso
    {
        public int Iddetalle_ingreso { get; set; }
        [Required]
        public int Idingreso { get; set; }
        [Required]
        public int Idarticulo { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [ForeignKey("Idingreso")]
        public Ingreso Ingreso { get; set; }
        [ForeignKey("Idarticulo")]
        public Articulo Articulo { get; set; }
    }
}
