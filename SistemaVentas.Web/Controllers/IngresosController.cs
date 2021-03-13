using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Datos;
using SistemaVentas.Entidades.Almacen;
using SistemaVentas.Web.Models.Almacen.Ingreso;

namespace SistemaVentas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public IngresosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Ingresos/Listar
        [Authorize(Roles = "Bodeguero,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<IngresoViewModel>> Listar()
        {
            var ingreso = await _context.Ingresos
                .Include(i => i.Usuario)
                .Include(i => i.Persona)
                .OrderByDescending(i => i.Idingreso)
                .Take(100)
                .ToListAsync();

            return ingreso.Select(i => new IngresoViewModel
            {
                Idingreso = i.Idingreso,
                Idproveedor = i.Idproveedor,
                Proveedor = i.Persona.Nombre,
                Idusuario = i.Idusuario,
                Usuario = i.Usuario.Nombre,
                Tipo_comprobante = i.Tipo_comprobante,
                Num_comprobante = i.Num_comprobante,
                Serie_comprobante = i.Serie_comprobante,
                Fecha_hora = i.Fecha_hora,
                Impuesto = i.Impuesto,
                Total = i.Total,
                Estado = i.Estado
            });

        }

        // GET: api/Ingresos/ListarDetalles
        [Authorize(Roles = "Bodeguero,Administrador")]
        [HttpGet("[action]/{idingreso}")]
        public async Task<IEnumerable<DetalleViewModel>> ListarDetalles([FromRoute] int idingreso)
        {
            var detalle = await _context.DetallesIngresos
                .Include(a => a.Articulo)
                .Where(d => d.Idingreso==idingreso)
                .ToListAsync();

            return detalle.Select(d => new DetalleViewModel
            {
                Idarticulo = d.Idarticulo,
                Articulo = d.Articulo.Nombre,
                Cantidad = d.Cantidad,
                Precio = d.Precio
            });

        }

        // GET: api/Ingresos/Listar
        [Authorize(Roles = "Bodeguero,Administrador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<IngresoViewModel>> ListarFiltro([FromRoute] string texto)
        {
            var ingreso = await _context.Ingresos
                .Include(i => i.Usuario)
                .Include(i => i.Persona)
                .Where(i=>i.Num_comprobante.Contains(texto))
                .OrderByDescending(i => i.Idingreso)
                .ToListAsync();

            return ingreso.Select(i => new IngresoViewModel
            {
                Idingreso = i.Idingreso,
                Idproveedor = i.Idproveedor,
                Proveedor = i.Persona.Nombre,
                Idusuario = i.Idusuario,
                Usuario = i.Usuario.Nombre,
                Tipo_comprobante = i.Tipo_comprobante,
                Num_comprobante = i.Num_comprobante,
                Serie_comprobante = i.Serie_comprobante,
                Fecha_hora = i.Fecha_hora,
                Impuesto = i.Impuesto,
                Total = i.Total,
                Estado = i.Estado
            });

        }

        // GET: api/Ingresos/ConsultaFechas/{incio}/{fin}
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]/{FechaInicio}/{FechaFin}")]
        public async Task<IEnumerable<IngresoViewModel>> ConsultaFechas([FromRoute] DateTime FechaInicio, DateTime FechaFin)
        {
            var ingreso = await _context.Ingresos
                .Include(i => i.Usuario)
                .Include(i => i.Persona)
                .Where(i => i.Fecha_hora >= FechaInicio)
                .Where(i => i.Fecha_hora <= FechaFin)
                .OrderByDescending(i => i.Idingreso)
                .Take(100)
                .ToListAsync();

            return ingreso.Select(i => new IngresoViewModel
            {
                Idingreso = i.Idingreso,
                Idproveedor = i.Idproveedor,
                Proveedor = i.Persona.Nombre,
                Idusuario = i.Idusuario,
                Usuario = i.Usuario.Nombre,
                Tipo_comprobante = i.Tipo_comprobante,
                Num_comprobante = i.Num_comprobante,
                Serie_comprobante = i.Serie_comprobante,
                Fecha_hora = i.Fecha_hora,
                Impuesto = i.Impuesto,
                Total = i.Total,
                Estado = i.Estado
            });

        }

        // POST: api/Ingresos/Crear
        [Authorize(Roles = "Bodeguero,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechahora = DateTime.Now;
            Ingreso ingreso = new Ingreso
            {
                Idproveedor = model.Idproveedor,
                Idusuario = model.Idusuario,
                Tipo_comprobante = model.Tipo_comprobante,
                Serie_comprobante = model.Serie_comprobante,
                Num_comprobante = model.Num_comprobante,
                Fecha_hora = fechahora,
                Impuesto = model.Impuesto,
                Total = model.Total,
                Estado = "Aceptado"
            };

            try
            {
                _context.Ingresos.Add(ingreso);
                await _context.SaveChangesAsync();

                var id = ingreso.Idingreso;
                foreach (var det in model.Detalles)
                {
                    DetalleIngreso detalle = new DetalleIngreso
                    {
                        Idingreso = id,
                        Idarticulo = det.Idarticulo,
                        Cantidad = det.Cantidad,
                        Precio = det.Precio
                    };

                    _context.DetallesIngresos.Add(detalle);

                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // PUT: api/Ingresos/Anular
        [Authorize(Roles = "Bodeguero,Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var ingreso = await _context.Ingresos.FirstOrDefaultAsync(i => i.Idingreso == id);

            if (ingreso == null)
            {
                return NotFound();
            }

            ingreso.Estado = "Anulado";

            try
            {
                await _context.SaveChangesAsync();

                var detalle = await _context.DetallesIngresos.Include(a => a.Articulo).Where(d => d.Idingreso == id).ToArrayAsync();

                foreach (var det in detalle)
                {
                    var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Idarticulo == det.Articulo.Idarticulo);

                    articulo.Stock = det.Articulo.Stock - det.Cantidad;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        private bool IngresoExists(int id)
        {
            return _context.Ingresos.Any(e => e.Idingreso == id);
        }
    }
}
