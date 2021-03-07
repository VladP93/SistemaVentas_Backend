using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Datos;
using SistemaVentas.Entidades.Ventas;
using SistemaVentas.Web.Models.Ventas.Persona;

namespace SistemaVentas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public PersonasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Personas/ListarClientes
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> ListarClientes()
        {
            var persona = await _context.Personas.Where(p=>p.Tipo_persona=="Cliente").ToListAsync();

            return persona.Select(p => new PersonaViewModel
            {
                Idpersona = p.Idpersona,
                Tipo_persona = p.Tipo_persona,
                Nombre = p.Nombre,
                Tipo_documento = p.Tipo_documento,
                Num_documento = p.Num_documento,
                Direccion = p.Direccion,
                Telefono = p.Telefono,
                Email = p.Email
            });
        }
        // GET: api/Personas/ListarProveedores
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> ListarProveedores()
        {
            var persona = await _context.Personas.Where(p => p.Tipo_persona == "Proveedor").ToListAsync();

            return persona.Select(p => new PersonaViewModel
            {
                Idpersona = p.Idpersona,
                Tipo_persona = p.Tipo_persona,
                Nombre = p.Nombre,
                Tipo_documento = p.Tipo_documento,
                Num_documento = p.Num_documento,
                Direccion = p.Direccion,
                Telefono = p.Telefono,
                Email = p.Email
            });
        }

        // POST: api/Persona/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.Email.ToLower();

            if (await _context.Personas.AnyAsync(p => p.Email == email))
            {
                return BadRequest("El Email ya existe");
            }

            Persona persona = new Persona
            {
                Tipo_persona = model.Tipo_persona,
                Nombre = model.Nombre,
                Tipo_documento = model.Tipo_documento,
                Num_documento = model.Num_documento,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Email = model.Email.ToLower()
            };

            _context.Personas.Add(persona);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // PUT: api/Personas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Idpersona <= 0)
            {
                return BadRequest();
            }

            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.Idpersona == model.Idpersona);

            if (persona == null)
            {
                return NotFound();
            }

            persona.Tipo_persona = model.Tipo_persona;
            persona.Nombre = model.Nombre;
            persona.Tipo_documento = model.Tipo_documento;
            persona.Num_documento = model.Num_documento;
            persona.Direccion = model.Direccion;
            persona.Telefono = model.Telefono;
            persona.Email = model.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.Idpersona == id);
        }
    }
}
