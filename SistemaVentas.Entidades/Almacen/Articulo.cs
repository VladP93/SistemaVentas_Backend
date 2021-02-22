using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVentas.Entidades.Almacen
{
    public class Articulo
    {
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
        public bool Condicion { get; set; }

        [ForeignKey("Idcategoria")]
        public Categoria Categoria { get; set; }
    }
}
