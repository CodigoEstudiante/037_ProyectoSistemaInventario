using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Modelo
{
    public class Salida
    {
        public int IdSalida { get; set; }
        public string NumeroDocumento { get; set; }
        public string FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public int CantidadProductos { get; set; }
        public string MontoTotal { get; set; }
        public List<DetalleSalida> olistaDetalle { get; set; }

    }
}
