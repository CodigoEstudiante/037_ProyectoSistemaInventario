using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using ProyectoVenta.Logica;
using ProyectoVenta.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoVenta.Formularios.Entradas
{
    public partial class frmDetalleEntrada : Form
    {
        public frmDetalleEntrada()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmDetalleEntrada_Load(object sender, EventArgs e)
        {
            lblnrodocumento.Text = "";
            txtnumerodocumento.Focus();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (txtnumerodocumento.Text.Trim() == "") {
                MessageBox.Show("Ingrese el numero de documento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Entrada obj = EntradaLogica.Instancia.Obtener(txtnumerodocumento.Text);

            if (obj != null)
            {
                lblnrodocumento.Text = obj.NumeroDocumento;
                txtfecha.Text = obj.FechaRegistro;
                txtusuario.Text = obj.UsuarioRegistro;
                txtdocumentoproveedor.Text = obj.DocumentoProveedor;
                txtnombreproveedor.Text = obj.NombreProveedor;

                List<DetalleEntrada> olista = EntradaLogica.Instancia.ListarDetalle(obj.IdEntrada);
                dgvdata.Rows.Clear();
                foreach(DetalleEntrada de in olista) {
                    dgvdata.Rows.Add(new object[] {de.CodigoProducto,de.DescripcionProducto,de.CategoriaProducto,de.AlmacenProducto,de.Cantidad,de.PrecioCompra,de.PrecioVenta,de.SubTotal });
                }

                lbltotal.Text = obj.MontoTotal;
            }
            else {
                limpiar();
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtnumerodocumento.Focus();
            }
        }

        private void chkocultarprecios_CheckedChanged(object sender, EventArgs e)
        {
            if (chkocultarprecios.Checked)
            {
                dgvdata.Columns["PrecioCompra"].Visible = false;
                dgvdata.Columns["PrecioVenta"].Visible = false;
                dgvdata.Columns["SubTotal"].Visible = false;

                lbltextototal.Visible = false;
                lbltotal.Visible = false;
            }
            else {
                dgvdata.Columns["PrecioCompra"].Visible = true;
                dgvdata.Columns["PrecioVenta"].Visible = true;
                dgvdata.Columns["SubTotal"].Visible = true;

                lbltextototal.Visible = true;
                lbltotal.Visible = true;
            }
        }

        private void btndescargarpdf_Click(object sender, EventArgs e)
        {
            if (lblnrodocumento.Text == "") {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_Html = string.Empty;
            if (chkocultarprecios.Checked)
            {
                Texto_Html = Properties.Resources.PlantillaEntradaSinPrecio.ToString();
            }
            else {
                Texto_Html = Properties.Resources.PlantillaEntradaConPrecio.ToString();
            }

            Datos odatos = DatoLogica.Instancia.Obtener();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", odatos.RazonSocial.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", odatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", odatos.Direccion);

            Texto_Html = Texto_Html.Replace("@numerodocumento", lblnrodocumento.Text);

            Texto_Html = Texto_Html.Replace("@docproveedor", txtdocumentoproveedor.Text);
            Texto_Html = Texto_Html.Replace("@nombreproveedor", txtnombreproveedor.Text);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtfecha.Text);


            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Codigo"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Descripcion"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Categoria"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                if (!chkocultarprecios.Checked) {
                    filas += "<td>" + row.Cells["PrecioCompra"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["PrecioVenta"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                }
                filas += "</tr>";
            }
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", lbltotal.Text);


            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Entrada_{0}.pdf", lblnrodocumento.Text);
            savefile.Filter = "Pdf Files|*.pdf";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    //Creamos un nuevo documento y lo definimos como PDF
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    bool obtenido = true;
                    byte[] byteimage = DatoLogica.Instancia.ObtenerLogo(out obtenido);
                    if (obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteimage);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51));
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void limpiar() {
            txtnumerodocumento.Text = "";
            lblnrodocumento.Text = "";
            txtfecha.Text = "";
            txtusuario.Text = "";
            txtdocumentoproveedor.Text = "";
            txtnombreproveedor.Text = "";
            dgvdata.Rows.Clear();
            chkocultarprecios.Checked = false;
            lbltotal.Text = "0.00";
            txtnumerodocumento.Focus();
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
