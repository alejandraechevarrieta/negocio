using System;
using System.Collections.Generic;

namespace Negocio.Models
{
    public partial class TipoProducto
    {
        public TipoProducto()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdTipo { get; set; }
        public string Detalle { get; set; } = null!;

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
