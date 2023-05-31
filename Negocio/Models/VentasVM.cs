using System;
using System.Collections.Generic;

namespace Negocio.Models
{
    public partial class VentasVM
    {
        public int IdVenta { get; set; }
        public int? IdProducto { get; set; }
        public int? Unidades { get; set; }
        public int? PrecioUnidad { get; set; }
        public int? Total { get; set; }
        public DateTime fechaVenta { get; set; }
        public string DetalleProducto { get; set; }

        public virtual Producto? OIdProducto{ get; set; }
    }
}
