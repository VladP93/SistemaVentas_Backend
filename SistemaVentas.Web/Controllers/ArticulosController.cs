using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Datos;
using SistemaVentas.Entidades.Almacen;
using SistemaVentas.Web.Models.Almacen.Articulo;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ArticulosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Articulos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloViewModel>> Listar()
        {
            var articulo = await _context.Articulos.Include(a => a.Categoria).ToListAsync();

            return articulo.Select(a => new ArticuloViewModel
            {
                Idarticulo = a.Idarticulo,
                Idcategoria = a.Idcategoria,
                Categoria = a.Categoria.Nombre,
                Codigo = a.Codigo,
                Nombre = a.Nombre,
                Stock = a.Stock,
                Precio_venta = a.Precio_venta,
                Descripcion = a.Descripcion,
                Condicion = a.Condicion
            });

        }

        // GET: api/Categorias/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var articulo = await _context.Articulos.Include(a => a.Categoria).SingleOrDefaultAsync(a => a.Idarticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(new ArticuloViewModel
            {
                Idarticulo = articulo.Idarticulo,
                Idcategoria = articulo.Idcategoria,
                Categoria = articulo.Categoria.Nombre,
                Codigo = articulo.Codigo,
                Nombre = articulo.Nombre,
                Stock = articulo.Stock,
                Precio_venta = articulo.Precio_venta,
                Descripcion = articulo.Descripcion,
                Condicion = articulo.Condicion
            });
        }

        // PUT: api/Articulos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Idarticulo <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Idarticulo == model.Idarticulo);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Idcategoria = model.Idcategoria;
            articulo.Codigo = model.Codigo;
            articulo.Nombre = model.Nombre;
            articulo.Precio_venta = model.Precio_venta;
            articulo.Stock = model.Stock;
            articulo.Descripcion = model.Descripcion;

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

        // POST: api/Categorias/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("algo salió mal en el modelstate");
                return BadRequest(ModelState);
            }

            Articulo articulo = new Articulo
            {
                Idcategoria = model.Idcategoria,
                Codigo = model.Codigo,
                Nombre = model.Nombre,
                Precio_venta = model.Precio_venta,
                Stock = model.Stock,
                Descripcion = model.Descripcion,
                Condicion = true
            };

            _context.Articulos.Add(articulo);
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

        // PUT: api/Articulos/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Idarticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Condicion = false;

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

        // PUT: api/Articulos/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.Idarticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Condicion = true;

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

        private bool ArticuloExists(int id)
        {
            return _context.Articulos.Any(e => e.Idarticulo == id);
        }
    }
}
