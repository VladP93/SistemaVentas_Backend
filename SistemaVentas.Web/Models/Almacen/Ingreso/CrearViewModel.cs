using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.Web.Models.Almacen.Ingreso
{
    public class CrearViewModel
    {
        //Propiedades Maestro
        [Required]
        public int Idproveedor { get; set; }
        [Required]
        public int Idusuario { get; set; }
        [Required]
        public string Tipo_comprobante { get; set; }
        public string Serie_comprobante { get; set; }
        [Required]
        public string Num_comprobante { get; set; }
        [Required]
        public decimal Impuesto { get; set; }
        [Required]
        public decimal Total { get; set; }

        //Propiedades detalle
        [Required]
        public List<DetalleViewModel> Detalles { get; set; }
    }
}
