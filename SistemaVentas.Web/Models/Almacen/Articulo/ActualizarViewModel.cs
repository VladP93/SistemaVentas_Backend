using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.Web.Models.Almacen.Articulo
{
    public class ActualizarViewModel
    {
        [Required]
        public int Idarticulo { get; set; }
        [Required]
        public int Idcategoria { get; set; }
        public string Codigo { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string Nombre { get; set; }
        [Required]
        public decimal Precio_venta { get; set; }
        [Required]
        public int Stock { get; set; }
        public string Descripcion { get; set; }
    }
}
