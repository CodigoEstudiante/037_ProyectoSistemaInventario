using ProyectoVenta.Herrarmientas;
using ProyectoVenta.Logica;
using ProyectoVenta.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarcodeLib;
using System.IO;

namespace ProyectoVenta.Formularios
{
    public partial class frmRegistrarProducto : Form
    {
        private static int _id = 0;
        private static int _indice = 0;
        

        public frmRegistrarProducto()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRegistrarProducto_Load(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            List<Producto> lista = ProductoLogica.Instancia.Listar(out mensaje);

            foreach (Producto pr in lista)
            {
                dgvdata.Rows.Add(new object[] {
                    pr.IdProducto,
                    "",
                    pr.Codigo,
                    pr.Descripcion,
                    pr.Categoria,
                    pr.Almacen
                });
            }
            Limpiar();

            foreach (DataGridViewColumn cl in dgvdata.Columns)
            {
                if (cl.Visible == true && cl.Name != "btnseleccionar")
                {
                    cbobuscar.Items.Add(new OpcionCombo() { Valor = cl.Name, Texto = cl.HeaderText });
                }
            }
            cbobuscar.DisplayMember = "Texto";
            cbobuscar.ValueMember = "Valor";
            cbobuscar.SelectedIndex = 0;


            TipoBarra objtipobarra = TipoBarraLogica.Instancia.ObtenerTipoBarra();
            if (objtipobarra.IdTipoBarra != 0) {
                TipoBarraCodigo.TipoCodigo = (Enum.IsDefined(typeof(BarcodeLib.TYPE), objtipobarra.Value)) ? ((BarcodeLib.TYPE)objtipobarra.Value) : ((BarcodeLib.TYPE)0);
            }

        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 1)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check16.Width;
                var h = Properties.Resources.check16.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check16, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
                {
                    dgvdata.Rows[_indice].DefaultCellStyle.BackColor = Color.White;

                    _id = Convert.ToInt32(dgvdata.Rows[index].Cells["Id"].Value.ToString());
                    _indice = index;
                    txtcodigo.Text = dgvdata.Rows[index].Cells["Codigo"].Value.ToString();
                    txtdescripcion.Text = dgvdata.Rows[index].Cells["Descripcion"].Value.ToString();
                    txtcategoria.Text = dgvdata.Rows[index].Cells["Categoria"].Value.ToString();
                    txtalmacen.Text = dgvdata.Rows[index].Cells["Almacen"].Value.ToString();

                    txtcodigo.BackColor = Color.LemonChiffon;
                    txtdescripcion.BackColor = Color.LemonChiffon;
                    txtcategoria.BackColor = Color.LemonChiffon;
                    txtalmacen.BackColor = Color.LemonChiffon;
                    dgvdata.Rows[index].DefaultCellStyle.BackColor = Color.LemonChiffon;
                }

            }
        }

        private void Limpiar(bool vista = true) {
            txtcodigo.BackColor = Color.White;
            txtdescripcion.BackColor = Color.White;
            txtcategoria.BackColor = Color.White;
            txtalmacen.BackColor = Color.White;
            if (vista) {
                if (dgvdata.Rows.Count > 0)
                {
                    dgvdata.Rows[_indice].DefaultCellStyle.BackColor = Color.White;
                }
            }
            

            _id = 0;
            _indice = 0;
            txtcodigo.Text = "";
            txtdescripcion.Text = "";
            txtcategoria.Text = "";
            txtalmacen.Text = "";
            lblresultado.Text = "";

            txtcodigo.Focus();
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            if(txtcodigo.Text.Trim() == "")
            {
                lblresultado.Text = "Debe ingresar el codigo";
                lblresultado.ForeColor = Color.Red;
                return;
            }
            if (txtdescripcion.Text.Trim() == "")
            {
                lblresultado.Text = "Debe ingresar la descripción";
                lblresultado.ForeColor = Color.Red;
                return;
            }

            Producto obj = new Producto()
            {
                IdProducto = _id,
                Codigo = txtcodigo.Text,
                Descripcion = txtdescripcion.Text,
                Categoria = txtcategoria.Text,
                Almacen = txtalmacen.Text
            };


            int existe = ProductoLogica.Instancia.Existe(obj.Codigo, _id, out mensaje);
            if (existe > 0)
            {
                lblresultado.Text = mensaje;
                lblresultado.ForeColor = Color.Red;
                return;
            }

            if (_id == 0)
            {
               
                int idgenerado = ProductoLogica.Instancia.Guardar(obj, out mensaje);
                if (idgenerado > 0)
                {
                    Limpiar();
                    dgvdata.Rows.Add(new object[] { idgenerado, "", obj.Codigo, obj.Descripcion, obj.Categoria, obj.Almacen });
                    lblresultado.Text = "Registro Correcto";
                    lblresultado.ForeColor = Color.Green;
                }
                else {
                    lblresultado.Text = mensaje;
                    lblresultado.ForeColor = Color.Red; ;
                }
            }
            else {
                
                int afectados = ProductoLogica.Instancia.Editar(obj, out mensaje);
                if (afectados > 0)
                {
                    dgvdata.Rows[_indice].Cells["Codigo"].Value = obj.Codigo;
                    dgvdata.Rows[_indice].Cells["Descripcion"].Value = obj.Descripcion;
                    dgvdata.Rows[_indice].Cells["Categoria"].Value = obj.Categoria;
                    dgvdata.Rows[_indice].Cells["Almacen"].Value = obj.Almacen;
                    Limpiar();
                    lblresultado.Text = "Modificación Correcta";
                    lblresultado.ForeColor = Color.Green;
                }
                else {
                    lblresultado.Text = mensaje;
                    lblresultado.ForeColor = Color.Red;
                }

            }




        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (_id != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int respuesta = ProductoLogica.Instancia.Eliminar(_id);
                    if (respuesta > 0)
                    {
                        dgvdata.Rows.RemoveAt(_indice);
                        Limpiar(false);
                    }
                    else
                        MessageBox.Show("No se pudo eliminar el producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btngenerarcodigo_Click(object sender, EventArgs e)
        {

            if (txtcodigo.Text.Trim() == "")
            {
                lblresultado.Text = "Debe ingresar el codigo";
                lblresultado.ForeColor = Color.Red;
            }
            else {
                try
                {
                    BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                    Codigo.IncludeLabel = chkmostraretiqueta.Checked;
                    var imagen = Codigo.Encode(TipoBarraCodigo.TipoCodigo, txtcodigo.Text.Trim(), Color.Black, Color.White, 400, 100);

                    byte[] barcodeimagen = ImageToByte2(imagen);

                    SaveFileDialog savefile = new SaveFileDialog();
                    savefile.FileName = string.Format("{0}.png", txtcodigo.Text.Trim());
                    savefile.Filter = "Imagen|*.png";
                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(savefile.FileName, barcodeimagen);
                        MessageBox.Show("Codigo generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch(Exception ex) {
                    MessageBox.Show(string.Format("No se pudo generar el codigo.\nMayor Detalle:\n{0}",ex.Message), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                
            }
        }

        public static byte[] ImageToByte2(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cbobuscar.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbuscar.Text.ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            txtbuscar.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
