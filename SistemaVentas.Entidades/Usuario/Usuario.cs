using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaVentas.Entidades.Usuario
{
    public class Usuario
    {
        public int Idusuario { get; set; }
        [Required]
        public int Idrol { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string Nombre { get; set; }
        public string Tipo_documento { get; set; }
        public string Num_documento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] Password_hash { get; set; }
        [Required]
        public byte[] Password_salt { get; set; }
        public bool Condicion { get; set; }
        [ForeignKey("Idrol")]
        public Rol Rol { get; set; }
    }
}
