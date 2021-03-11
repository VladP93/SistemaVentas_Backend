using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaVentas.Entidades.Usuarios
{
    public class Rol
    {
        public int Idrol { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(256)]
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
