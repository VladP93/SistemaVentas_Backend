using SistemaVentas.Entidades.Ventas;
using SistemaVentas.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVentas.Entidades.Almacen
{
    public class Ingreso
    {
        public int Idingreso { get; set; }
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
        public DateTime Fecha_hora { get; set; }
        [Required]
        public decimal Impuesto { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public string Estado { get; set; }
        public ICollection<DetalleIngreso> Detalles { get; set; }
        [ForeignKey("Idusuario")]
        public Usuario Usuario { get; set; }
        [ForeignKey("Idproveedor")]
        public Persona Persona { get; set; }
    }
}
