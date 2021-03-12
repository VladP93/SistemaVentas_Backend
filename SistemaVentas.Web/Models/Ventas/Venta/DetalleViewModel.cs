using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.Web.Models.Ventas.Venta
{
    public class DetalleViewModel
    {
        [Required]
        public int Idarticulo { get; set; }
        public string Articulo { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public decimal Descuento { get; set; }
    }
}
