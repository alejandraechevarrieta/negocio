using System;
using System.Collections.Generic;

namespace Negocio.Models
{
    public partial class ProductoVM
    {
        public ProductoVM()
        {
            Venta = new HashSet<Ventum>();
        }

        public int IdProducto { get; set; }
        public int? IdTipo { get; set; }
        public string Detalle { get; set; } = null!;
        public int? Stock { get; set; }
        public int? StockNuevo { get; set; }
        public int? Precio { get; set; }
        public string DetalleTipo { get; set; } = null!;

        public virtual TipoProducto? OTipoProducto { get; set; }
        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
