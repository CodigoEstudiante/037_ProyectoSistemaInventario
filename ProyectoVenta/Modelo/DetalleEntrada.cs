using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Modelo
{
    public class DetalleEntrada
    {
        public int IdDetalleEntrada { get; set; }
        public int IdEntrada { get; set; }
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string CategoriaProducto { get; set; }
        public string AlmacenProducto { get; set; }
        public string PrecioCompra { get; set; }
        public string PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public string SubTotal { get; set; }
    }
}
