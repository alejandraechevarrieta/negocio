using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negocio.Models;
using System.Diagnostics;

namespace Negocio.Controllers
{
    public class VentasController : Controller
    {
        private readonly NegocioContext _DBContext;

        public VentasController(NegocioContext context)
        {
            _DBContext = context;
        }
        [HttpPost]
        public IActionResult CrearVenta(Ventum nuevaVenta)
        {
            try
            {               
                if (ModelState.IsValid)
                {   
                    var producto = _DBContext.Productos.FirstOrDefault(p => p.IdProducto == nuevaVenta.IdProducto);
                    if (producto != null)
                    {
                        //actualizo stock
                        producto.Stock -= nuevaVenta.Unidades;                       
                        _DBContext.SaveChanges();
                       //completo datos y guardo
                         nuevaVenta.Total = nuevaVenta.PrecioUnidad * nuevaVenta.Unidades;
                         nuevaVenta.fechaVenta = DateTime.Now;
                        _DBContext.Venta.Add(nuevaVenta);
                        _DBContext.SaveChanges();
                       
                        return Json(new { success = true });
                    }
                    else
                    {
                        return NotFound(); 
                    }
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

        public IActionResult CargarProductos()
        {
            var list = _DBContext.Productos.ToList();
            return Json(list);
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

            if (producto.OTipoProducto == null)
            {                
                tipoProducto = new TipoProducto();
                _DBContext.TipoProductos.Add(tipoProducto);
            }
            else
            {                
                tipoProducto = producto.OTipoProducto;
            }
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
        public IActionResult EliminarVenta(int idVenta)
        {
            try
            {               
                var venta = _DBContext.Venta.FirstOrDefault(v => v.IdVenta == idVenta);
               
                if (venta == null)
                {
                    return NotFound(); 
                }
                var producto = _DBContext.Productos.FirstOrDefault(p => p.IdProducto == venta.IdProducto);                

                // actualizar el stock del producto
                producto.Stock += venta.Unidades;

                // eliminar la venta de la base de datos
                _DBContext.Venta.Remove(venta);
                _DBContext.SaveChanges();
                
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

       
        
    }
}