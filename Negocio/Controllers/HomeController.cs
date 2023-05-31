using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negocio.Models;
using System.Diagnostics;

namespace Negocio.Controllers
{
    public class HomeController : Controller
    {
        private readonly NegocioContext _DBContext;

        public HomeController(NegocioContext context)
        {
            _DBContext = context;
        }

        public IActionResult Index()
        {
            List<Producto> productos = _DBContext.Productos.Include(t => t.OTipoProducto).ToList();
            return View(productos);
        }
        [HttpPost]
        public IActionResult ObtenerProducto(int id)
        {            
            Producto producto = _DBContext.Productos.Include(p => p.OTipoProducto).FirstOrDefault(p => p.IdProducto == id);
           
            if (producto == null)
            {
                return NotFound(); 
            }
            TipoProducto tipoProducto;           
                
            tipoProducto = producto.OTipoProducto;
            

            tipoProducto.Detalle = producto.OTipoProducto.Detalle; 

            producto.OTipoProducto = tipoProducto;

            _DBContext.SaveChanges();
            
            var productoData = new
            {
                nombre = producto.Detalle,
                tipo = producto.OTipoProducto != null ? producto.OTipoProducto.Detalle : string.Empty,
                stock = producto.Stock,
                stockNuevo = producto.Stock,
                precio = producto.Precio
            };
            return Json(productoData); 
        }
        [HttpPost]
        public IActionResult CrearProducto(Producto nuevoProducto)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    _DBContext.Productos.Add(nuevoProducto);
                    _DBContext.SaveChanges();
                    
                    return Json(new { success = true });
                }
                else
                {                   
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                    return Json(new { success = false, errors = errors });
                }
            }
            catch (Exception ex)
            {
                
                return Json(new { success = false, error = ex.Message });
            }
        }
        [HttpPost]
        public IActionResult CrearTipo(TipoProducto nuevoTipo)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    _DBContext.TipoProductos.Add(nuevoTipo);
                    _DBContext.SaveChanges();
                    
                    return Json(new { success = true });
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                    return Json(new { success = false, errors = errors });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, error = ex.Message });
            }
        }
        public IActionResult CargarTipos()
        {
            var tipos = _DBContext.TipoProductos.ToList();
            return Json(tipos);
        }


       
        [HttpPost]
        public IActionResult GuardarCambios(Producto producto)
        {
            if (ModelState.IsValid)
            {               
                Producto productoExistente = _DBContext.Productos.Find(producto.IdProducto);
               
                if (productoExistente == null)
                {
                    return NotFound(); 
                }
                // Actualizar los datos del producto existente
                productoExistente.Detalle = producto.Detalle;
                productoExistente.Precio = producto.Precio;
                productoExistente.Stock = producto.Stock;
                productoExistente.StockNuevo = producto.Stock;

                // Guardar los cambios en la base de datos
                _DBContext.SaveChanges();

                return RedirectToAction("Index");
            }
            
            return View("EditarProducto", producto);
        }
               
        [HttpPost]
        public IActionResult BuscarProductosPorTipo(int tipoId)
        {
            try
            {
                var productos = (from p in _DBContext.Productos
                                 join t in _DBContext.TipoProductos on p.IdTipo equals t.IdTipo
                                 where tipoId == 0 || p.IdTipo == tipoId
                                 select new ProductoVM
                                 {
                                     IdProducto = p.IdProducto,
                                     Detalle = p.Detalle,
                                     Precio = p.Precio,
                                     Stock = p.Stock,
                                     IdTipo = p.IdTipo,
                                     DetalleTipo = t.Detalle
                                     
                                 }).OrderByDescending(p => p.Detalle).ToList();

                return Json(productos);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }



        public IActionResult Ventas()
        {
            List<Ventum> ventas = _DBContext.Venta.Include(t => t.OIdProducto).ToList();
            return View(ventas);
        }

        
    }
}