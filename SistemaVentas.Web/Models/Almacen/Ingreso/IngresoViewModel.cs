using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.Web.Models.Almacen.Ingreso
{
    public class IngresoViewModel
    {
        public int Idingreso { get; set; }
        public int Idproveedor { get; set; }
        public int Idusuario { get; set; }
        public string Tipo_comprobante { get; set; }
        public string Serie_comprobante { get; set; }
        public string Num_comprobante { get; set; }
        public DateTime Fecha_hora { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string Usuario { get; set; }
        public string Proveedor{ get; set; }
    }
}
