using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Datos;
using SistemaVentas.Entidades.Usuario;
using SistemaVentas.Web.Models.Usuarios.Rol;

namespace SistemaVentas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public RolesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Roles/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolViewModel>> Listar()
        {
            var rol = await _context.Roles.ToListAsync();

            return rol.Select(r => new RolViewModel
            {
                Idrol = r.Idrol,
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                Condicion = r.Condicion
            });

        }

        // GET: api/Roles/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var rol = await _context.Roles.Where(r => r.Condicion == true).ToListAsync();

            return rol.Select(r => new SelectViewModel
            {
                Idrol = r.Idrol,
                Nombre = r.Nombre
            });

        }

        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.Idrol == id);
        }
    }
}
