using ProyectoVenta.Logica;
using ProyectoVenta.Modales;
using ProyectoVenta.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoVenta.Formularios.Salidas
{
    public partial class frmRegistrarSalida : Form
    {
        private static int _idproducto = 0;
        private static string _categoria = "";
        private static string _almacen = "";
        private static int _stock = 0;
        private static string _precioventa = "";

        private static string _NombreUsuario = "";

        public frmRegistrarSalida(string _usuario = "")
        {
            _NombreUsuario = _usuario;
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRegistrarSalida_Load(object sender, EventArgs e)
        {
            txtnumerodocumento.Focus();
            txtfecharegistro.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void btnbuscarclientes_Click(object sender, EventArgs e)
        {
            using (var form = new mdClientes())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtdoccliente.Text = form._DocumentoCliente;
                    txtnomcliente.Text = form._NombreCliente;
                }
            }
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            using (var form = new mdProductos())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtcodigoproducto.BackColor = Color.Honeydew;
                    _idproducto = form._id;
                    txtcodigoproducto.Text = form._codigo;
                    txtdescripcionproducto.Text = form._descripcion;
                    txtstock.Text = form._stock.ToString();
                    _categoria = form._categoria;
                    _almacen = form._almacen;
                    _stock = form._stock;
                    _precioventa = form._precioventa;

                    txtcantidad.Value = 1;
                    txtcantidad.Focus();
                }
            }
        }

        private void txtcantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtcodigoproducto_KeyDown(object sender, KeyEventArgs e)
        {
            string mensaje = string.Empty;
            if (e.KeyData == Keys.Enter)
            {
                Producto pr = ProductoLogica.Instancia.Listar(out mensaje).Where(p => p.Codigo.ToUpper() == txtcodigoproducto.Text.Trim().ToUpper()).FirstOrDefault();
                if (pr != null)
                {
                    txtcodigoproducto.BackColor = Color.Honeydew;
                    txtcodigoproducto.Text = pr.Codigo;
                    txtdescripcionproducto.Text = pr.Descripcion;
                    txtstock.Text = pr.Stock.ToString();
                    _idproducto = Convert.ToInt32(pr.IdProducto.ToString());
                    _categoria = pr.Categoria;
                    _almacen = pr.Almacen;
                    _stock = pr.Stock;
                    _precioventa = pr.PrecioVenta;

                    txtcantidad.Value = 1;
                    txtcantidad.Focus();
                }
                else
                {
                    txtcodigoproducto.BackColor = Color.MistyRose;
                }

            }
        }

        private bool producto_agregado()
        {

            bool respuesta = false;
            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dgvdata.Rows)
                {
                    if (fila.Cells["Id"].Value.ToString() == _idproducto.ToString())
                    {
                        respuesta = true;
                        break;
                    }
                }
            }

            return respuesta;
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            if (txtcodigoproducto.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el codigo de producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtdescripcionproducto.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el codigo de producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (producto_agregado())
            {
                MessageBox.Show("El producto ya está agregado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtcantidad.Value > _stock) {
                MessageBox.Show("La cantidad no puede ser mayor al stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            decimal precioventa = 0;
            decimal subtotal = 0;
            if (!decimal.TryParse(_precioventa, out precioventa))
            {
                MessageBox.Show("Error al convertir internamente el tipo de moneda - Precio Venta\nEjemplo Formato ##.##", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string mensaje = string.Empty;
            int operaciones = SalidaLogica.Instancia.reducirStock(_idproducto, Convert.ToInt32(txtcantidad.Value.ToString()), out mensaje);

            if (operaciones > 0)
            {
                subtotal = Convert.ToDecimal(txtcantidad.Value.ToString()) * precioventa;

                dgvdata.Rows.Add(new object[] {"",
                    _idproducto.ToString(),
                    txtcodigoproducto.Text,
                    txtdescripcionproducto.Text,
                    _categoria,
                    _almacen,
                    txtcantidad.Value.ToString(),
                    precioventa.ToString("0.00"),
                    subtotal.ToString("0.00")
                });

                calcularTotal();

                _idproducto = 0;
                txtcodigoproducto.Text = "";
                txtcodigoproducto.BackColor = Color.White;
                txtdescripcionproducto.Text = "";
                txtstock.Text = "";
                _categoria = "";
                _almacen = "";
                _stock = 0;
                _precioventa = "";
                txtcantidad.Value = 1;
                txtcodigoproducto.Focus();

            }
            else {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           
        }


        private void calcularTotal()
        {
            decimal total = 0;
            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
                }
            }
            lbltotal.Text = total.ToString("0.00");
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.delete17.Width;
                var h = Properties.Resources.delete17.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.delete17, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
                {
                    string mensaje = string.Empty;
                    int idproducto = Convert.ToInt32(dgvdata.Rows[index].Cells["Id"].Value.ToString());
                    int cantidad = Convert.ToInt32(dgvdata.Rows[index].Cells["Cantidad"].Value.ToString());
                    int operaciones = SalidaLogica.Instancia.aumentarStock(idproducto, cantidad, out mensaje);

                    if (operaciones > 0)
                    {
                        dgvdata.Rows.RemoveAt(index);
                        calcularTotal();
                    }
                    else {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    
                }
            }
        }

        private void btnguardarsalida_Click(object sender, EventArgs e)
        {
            if (txtdoccliente.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el documento del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtnomcliente.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el nombre del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            string mensaje = string.Empty;
            int cantidad_productos = 0;
            int idcorrelativo = SalidaLogica.Instancia.ObtenerCorrelativo(out mensaje);
            List<DetalleSalida> olista = new List<DetalleSalida>();

            if (idcorrelativo < 1)
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                olista.Add(new DetalleSalida()
                {
                    IdProducto = Convert.ToInt32(row.Cells["Id"].Value.ToString()),
                    CodigoProducto = row.Cells["Codigo"].Value.ToString(),
                    DescripcionProducto = row.Cells["Descripcion"].Value.ToString(),
                    CategoriaProducto = row.Cells["Categoria"].Value.ToString(),
                    AlmacenProducto = row.Cells["Almacen"].Value.ToString(),
                    PrecioVenta = row.Cells["PrecioUnit"].Value.ToString(),
                    Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value.ToString()),
                    SubTotal = row.Cells["SubTotal"].Value.ToString()
                });

                cantidad_productos += Convert.ToInt32(row.Cells["Cantidad"].Value.ToString());
            }

            Salida oSalida = new Salida()
            {
                NumeroDocumento = String.Format("{0:00000}", idcorrelativo),
                FechaRegistro = DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")),
                UsuarioRegistro = _NombreUsuario,
                DocumentoCliente = txtdoccliente.Text,
                NombreCliente = txtnomcliente.Text,
                CantidadProductos = cantidad_productos,
                MontoTotal = lbltotal.Text,
                olistaDetalle = olista
            };

            int operaciones = SalidaLogica.Instancia.Registrar(oSalida, out mensaje);

            if (operaciones < 1)
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                txtdoccliente.Text = "";
                txtnomcliente.Text = "";
                dgvdata.Rows.Clear();
                lbltotal.Text = "0.00";
                txtdoccliente.Focus();

                mdSalidaExitosa md = new mdSalidaExitosa();
                md._numerodocumento = String.Format("{0:00000}", idcorrelativo);
                md.ShowDialog();
            }


        }
    }
}
