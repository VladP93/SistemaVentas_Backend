using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Datos;
using SistemaVentas.Entidades.Ventas;
using SistemaVentas.Web.Models.Ventas.Venta;

namespace SistemaVentas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public VentasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Ventas/Listar
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<VentaViewModel>> Listar()
        {
            var venta = await _context.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.Persona)
                .OrderByDescending(v => v.Idventa)
                .Take(100)
                .ToListAsync();

            return venta.Select(v => new VentaViewModel
            {
                Idventa = v.Idventa,
                Idcliente = v.Idcliente,
                Cliente = v.Persona.Nombre,
                Num_documento = v.Persona.Num_documento,
                Direccion = v.Persona.Direccion,
                Telefono = v.Persona.Telefono,
                Email = v.Persona.Email,
                Idusuario = v.Idusuario,
                Usuario = v.Usuario.Nombre,
                Tipo_comprobante = v.Tipo_comprobante,
                Num_comprobante = v.Num_comprobante,
                Serie_comprobante = v.Serie_comprobante,
                Fecha_hora = v.Fecha_hora,
                Impuesto = v.Impuesto,
                Total = v.Total,
                Estado = v.Estado
            });

        }

        // GET: api/Ventas/ListarDetalles
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]/{idventa}")]
        public async Task<IEnumerable<DetalleViewModel>> ListarDetalles([FromRoute] int idventa)
        {
            var detalle = await _context.DetallesVentas
                .Include(a => a.Articulo)
                .Where(d => d.Idventa == idventa)
                .ToListAsync();

            return detalle.Select(d => new DetalleViewModel
            {
                Idarticulo = d.Idarticulo,
                Articulo = d.Articulo.Nombre,
                Cantidad = d.Cantidad,
                Precio = d.Precio,
                Descuento = d.Descuento
            });

        }

        // GET: api/Ventas/ListarFiltro
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<VentaViewModel>> ListarFiltro([FromRoute] string texto)
        {
            var venta = await _context.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.Persona)
                .Where(v => v.Num_comprobante.Contains(texto))
                .OrderByDescending(v => v.Idventa)
                .ToListAsync();

            return venta.Select(v => new VentaViewModel
            {
                Idventa = v.Idventa,
                Idcliente = v.Idcliente,
                Cliente = v.Persona.Nombre,
                Num_documento = v.Persona.Num_documento,
                Direccion = v.Persona.Direccion,
                Telefono = v.Persona.Telefono,
                Email = v.Persona.Email,
                Idusuario = v.Idusuario,
                Usuario = v.Usuario.Nombre,
                Tipo_comprobante = v.Tipo_comprobante,
                Num_comprobante = v.Num_comprobante,
                Serie_comprobante = v.Serie_comprobante,
                Fecha_hora = v.Fecha_hora,
                Impuesto = v.Impuesto,
                Total = v.Total,
                Estado = v.Estado
            });

        }

        // GET: api/Ventas/ConsultaFechas/{incio}/{fin}
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]/{FechaInicio}/{FechaFin}")]
        public async Task<IEnumerable<VentaViewModel>> ConsultaFechas([FromRoute] DateTime FechaInicio, DateTime FechaFin)
        {
            var venta = await _context.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.Persona)
                .Where(i=>i.Fecha_hora>=FechaInicio)
                .Where(i=>i.Fecha_hora<=FechaFin)
                .OrderByDescending(v => v.Idventa)
                .Take(100)
                .ToListAsync();

            return venta.Select(v => new VentaViewModel
            {
                Idventa = v.Idventa,
                Idcliente = v.Idcliente,
                Cliente = v.Persona.Nombre,
                Num_documento = v.Persona.Num_documento,
                Direccion = v.Persona.Direccion,
                Telefono = v.Persona.Telefono,
                Email = v.Persona.Email,
                Idusuario = v.Idusuario,
                Usuario = v.Usuario.Nombre,
                Tipo_comprobante = v.Tipo_comprobante,
                Num_comprobante = v.Num_comprobante,
                Serie_comprobante = v.Serie_comprobante,
                Fecha_hora = v.Fecha_hora,
                Impuesto = v.Impuesto,
                Total = v.Total,
                Estado = v.Estado
            });

        }

        // POST: api/Ventas/Crear
        //[Authorize(Roles = "Vendedor,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechahora = DateTime.Now;
            Venta venta = new Venta
            {
                Idcliente = model.Idcliente,
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
                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                var id = venta.Idventa;
                foreach (var det in model.Detalles)
                {
                    DetalleVenta detalle = new DetalleVenta
                    {
                        Idventa = id,
                        Idarticulo = det.Idarticulo,
                        Cantidad = det.Cantidad,
                        Precio = det.Precio,
                        Descuento = det.Descuento
                    };

                    _context.DetallesVentas.Add(detalle);

                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // PUT: api/Ventas/Anular
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var venta = await _context.Ventas.FirstOrDefaultAsync(v => v.Idventa == id);

            if (venta == null)
            {
                return NotFound();
            }

            venta.Estado = "Anulado";

            try
            {
                await _context.SaveChangesAsync();

                var detalle = await _context.DetallesVentas.Include(a => a.Articulo).Where(d => d.Idventa == id).ToArrayAsync();

                foreach (var det in detalle)
                {
                    var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Idarticulo == det.Articulo.Idarticulo);

                    articulo.Stock = det.Articulo.Stock + det.Cantidad;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.Idventa == id);
        }
    }
}
