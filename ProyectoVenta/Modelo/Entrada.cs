using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Modelo
{
    public class Entrada
    {
        public int IdEntrada { get; set; }
        public string NumeroDocumento { get; set; }
        public string FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public string DocumentoProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public int CantidadProductos { get; set; }
        public string MontoTotal { get; set; }
        public List<DetalleEntrada> olistaDetalle { get; set; }
    }
}
