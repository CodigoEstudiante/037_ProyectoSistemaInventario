using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Modelo
{
    public class Inventario
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string Almacen { get; set; }
        public string Entradas { get; set; }
        public string Salidas { get; set; }
        public string Stock { get; set; }
        public string TotalEgresos { get; set; }
        public string TotalIngresos { get; set; }

    }
}
