using SistemaVentas.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaVentas.Entidades.Ventas
{
    public class DetalleVenta
    {
        public int Iddetalle_venta { get; set; }
        [Required]
        public int Idventa { get; set; }
        [Required]
        public int Idarticulo { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public decimal Descuento { get; set; }
        [ForeignKey("Idventa")]
        public Venta Venta { get; set; }
        [ForeignKey("Idarticulo")]
        public Articulo Articulo { get; set; }
    }
}
