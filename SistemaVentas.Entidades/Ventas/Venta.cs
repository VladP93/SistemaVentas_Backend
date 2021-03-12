using SistemaVentas.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaVentas.Entidades.Ventas
{
    public class Venta
    {
        public int Idventa { get; set; }
        [Required]
        public int Idcliente { get; set; }
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
        public ICollection<DetalleVenta> Detalles { get; set; }
        [ForeignKey("Idusuario")]
        public Usuario Usuario { get; set; }
        [ForeignKey("Idcliente")]
        public Persona Persona { get; set; }
    }
}
