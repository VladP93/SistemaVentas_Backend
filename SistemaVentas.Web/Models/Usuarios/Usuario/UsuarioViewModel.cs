using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.Web.Models.Usuarios.Usuario
{
    public class UsuarioViewModel
    {
        public int Idusuario { get; set; }
        public int Idrol { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
        public string Tipo_documento { get; set; }
        public string Num_documento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public byte[] Password_hash { get; set; }
        public bool Condicion { get; set; }
    }
}
