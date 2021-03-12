using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.Web.Models.Ventas.Venta
{
    public class VentaViewModel
    {
        public int Idventa { get; set; }
        public int Idcliente { get; set; }
        public string Cliente { get; set; }
        public int Idusuario { get; set; }
        public string Usuario { get; set; }
        public string Tipo_comprobante { get; set; }
        public string Serie_comprobante { get; set; }
        public string Num_comprobante { get; set; }
        public DateTime Fecha_hora { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
    }
}
