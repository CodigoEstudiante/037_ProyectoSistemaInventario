using ProyectoVenta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Logica
{
    public class InventarioLogica
    {
        private static InventarioLogica _instancia = null;

        public InventarioLogica()
        {

        }

        public static InventarioLogica Instancia
        {

            get
            {
                if (_instancia == null) _instancia = new InventarioLogica();
                return _instancia;
            }
        }



        public List<Inventario> Resumen(string fechainicio = "", string fechafin = "")
        {
            List<Inventario> oLista = new List<Inventario>();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select prod.Codigo,prod.Descripcion,prod.Categoria,prod.Almacen,");
                    query.AppendLine("ifnull(ent.Entradas,0)[Entradas],ifnull(sal.Salidas,0)[Salidas],");
                    query.AppendLine("prod.Stock,");
                    query.AppendLine("printf(\"%.2f\", ifnull(ent.TotalEgresos,0))[TotalEgresos],printf(\" % .2f\", ifnull(sal.TotalIngresos,0))[TotalIngresos]");
                    query.AppendLine("from (");
                    query.AppendLine("select DISTINCT * from (");
                    query.AppendLine("select p.IdProducto,p.Codigo,p.Descripcion,p.Categoria,p.Almacen,p.Stock from DETALLE_ENTRADA de");
                    query.AppendLine("inner join ENTRADA e on e.IdEntrada = de.IdEntrada");
                    query.AppendLine("inner join PRODUCTO p on p.IdProducto = de.IdProducto where DATE(e.FechaRegistro) BETWEEN DATE(@pfechainicio1) AND DATE(@pfechafin1)");
                    query.AppendLine("UNION ALL");
                    query.AppendLine("select p.IdProducto,p.Codigo,p.Descripcion,p.Categoria,p.Almacen,p.Stock from DETALLE_SALIDA ds");
                    query.AppendLine("inner join SALIDA s on s.IdSalida = ds.IdSalida");
                    query.AppendLine("inner join PRODUCTO p on p.IdProducto = ds.IdProducto where DATE(s.FechaRegistro) BETWEEN DATE(@pfechainicio2) AND DATE(@pfechafin2)");
                    query.AppendLine(") temp");
                    query.AppendLine(") prod");
                    query.AppendLine("left join (");
                    query.AppendLine("select p.IdProducto,sum(de.Cantidad)[Entradas],sum(CAST(de.SubTotal as NUMERIC))[TotalEgresos] from PRODUCTO p");
                    query.AppendLine("inner join DETALLE_ENTRADA de on de.IdProducto = p.IdProducto");
                    query.AppendLine("inner join ENTRADA e on e.IdEntrada = de.IdEntrada where DATE(e.FechaRegistro) BETWEEN DATE(@pfechainicio3) AND DATE(@pfechafin3)");
                    query.AppendLine("group by p.IdProducto,p.Codigo,p.Descripcion,p.Categoria,p.Almacen");
                    query.AppendLine(") ent on ent.IdProducto = prod.IdProducto");
                    query.AppendLine("left join (");
                    query.AppendLine("select p.IdProducto,sum(ds.Cantidad)[Salidas],sum(CAST(ds.SubTotal as NUMERIC))[TotalIngresos] from PRODUCTO p");
                    query.AppendLine("inner join DETALLE_SALIDA ds on ds.IdProducto = p.IdProducto");
                    query.AppendLine("inner join SALIDA s on s.IdSalida = ds.IdSalida where DATE(s.FechaRegistro) BETWEEN DATE(@pfechainicio4) AND DATE(@pfechafin4)");
                    query.AppendLine("group by p.IdProducto");
                    query.AppendLine(") sal on sal.IdProducto = prod.IdProducto");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pfechainicio1", fechainicio));
                    cmd.Parameters.Add(new SQLiteParameter("@pfechafin1", fechafin));
                    cmd.Parameters.Add(new SQLiteParameter("@pfechainicio2", fechainicio));
                    cmd.Parameters.Add(new SQLiteParameter("@pfechafin2", fechafin));
                    cmd.Parameters.Add(new SQLiteParameter("@pfechainicio3", fechainicio));
                    cmd.Parameters.Add(new SQLiteParameter("@pfechafin3", fechafin));
                    cmd.Parameters.Add(new SQLiteParameter("@pfechainicio4", fechainicio));
                    cmd.Parameters.Add(new SQLiteParameter("@pfechafin4", fechafin));
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Inventario()
                            {
                                Codigo = dr["Codigo"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                Almacen = dr["Almacen"].ToString(),
                                Entradas = dr["Entradas"].ToString(),
                                Salidas = dr["Salidas"].ToString(),
                                Stock = dr["Stock"].ToString(),
                                TotalEgresos = dr["TotalEgresos"].ToString(),
                                TotalIngresos = dr["TotalIngresos"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<Inventario>();
            }
            return oLista;
        }


    }
}
