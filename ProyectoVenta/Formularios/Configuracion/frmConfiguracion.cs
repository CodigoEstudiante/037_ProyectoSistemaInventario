using ProyectoVenta.Herrarmientas;
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
using BarcodeLib;

namespace ProyectoVenta.Formularios.Configuracion
{
    public partial class frmConfiguracion : Form
    {
        public frmConfiguracion()
        {
            InitializeComponent();
        }

        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] byteimage = DatoLogica.Instancia.ObtenerLogo(out obtenido);
            if (obtenido)
                picLogo.Image = ByteToImage(byteimage);


            Datos da = DatoLogica.Instancia.Obtener();
            txtrazonsocial.Text = da.RazonSocial;
            txtruc.Text = da.RUC;
            txtdireccion.Text = da.Direccion;


            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 0, Texto = "UNSPECIFIED" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 1, Texto = "UPCA" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 2, Texto = "UPCE" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 3, Texto = "UPC_SUPPLEMENTAL_2DIGIT" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 4, Texto = "UPC_SUPPLEMENTAL_5DIGIT" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 5, Texto = "EAN13" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 6, Texto = "EAN8" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 7, Texto = "Interleaved2of5" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 8, Texto = "Standard2of5" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 9, Texto = "Industrial2of5" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 10, Texto = "CODE39" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 11, Texto = "CODE39Extended" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 12, Texto = "CODE39_Mod43" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 13, Texto = "Codabar" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 14, Texto = "PostNet" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 15, Texto = "BOOKLAND" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 16, Texto = "ISBN" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 17, Texto = "JAN13" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 18, Texto = "MSI_Mod10" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 19, Texto = "MSI_2Mod10" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 20, Texto = "MSI_Mod11" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 21, Texto = "MSI_Mod11_Mod10" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 22, Texto = "Modified_Plessey" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 23, Texto = "CODE11" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 24, Texto = "USD8" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 25, Texto = "UCC12" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 26, Texto = "UCC13" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 27, Texto = "LOGMARS" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 28, Texto = "CODE128" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 29, Texto = "CODE128A" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 30, Texto = "CODE128B" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 31, Texto = "CODE128C" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 32, Texto = "ITF14" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 33, Texto = "CODE93" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 34, Texto = "TELEPEN" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 35, Texto = "FIM" });
            cbotipobarra.Items.Add(new OpcionCombo() { Valor = 36, Texto = "PHARMACODE" });

            cbotipobarra.DisplayMember = "Texto";
            cbotipobarra.ValueMember = "Valor";
            cbotipobarra.SelectedIndex = 0;



            TipoBarra objtipobarra = TipoBarraLogica.Instancia.ObtenerTipoBarra();
            int indexcombo = 0;
            if (objtipobarra.IdTipoBarra != 0)
            {
                int i = 0;
                foreach (OpcionCombo op in cbotipobarra.Items)
                {

                    if (Convert.ToInt32(op.Valor) == objtipobarra.Value)
                    {
                        indexcombo = i;
                        break;
                    }
                    i++;
                }
            }

            cbotipobarra.SelectedIndex = indexcombo;
        }

        public Image ByteToImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = new Bitmap(ms);
            return image;
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnguardarcambios_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            if (txtrazonsocial.Text == "") {
                MessageBox.Show("Debe ingresar Razon Social", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtruc.Text == "")
            {
                MessageBox.Show("Debe ingresar R.U.C", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtdireccion.Text == "")
            {
                MessageBox.Show("Debe ingresar direccion", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            int nrooperacion = DatoLogica.Instancia.Guardar(new Datos()
            {
                RazonSocial = txtrazonsocial.Text,
                RUC = txtruc.Text,
                Direccion = txtdireccion.Text
            }, out mensaje);

            if (nrooperacion < 1)
            {
                MessageBox.Show("No se pudo guardar los cambios, intente más tarde", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Los cambios fueron guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnsubir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            OpenFileDialog oOpenFileDialog = new OpenFileDialog();
            oOpenFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png";

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] byteImagen = File.ReadAllBytes(oOpenFileDialog.FileName);
                int numerooperacion = DatoLogica.Instancia.ActualizarLogo(byteImagen, out mensaje);

                if (numerooperacion < 1)
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    picLogo.Image = ByteToImage(byteImagen);
                }
            }
        }

        private void btnguardartipocodigo_Click(object sender, EventArgs e)
        {
            try {
                string mensaje = string.Empty;
                int valor = Convert.ToInt32(((OpcionCombo)cbotipobarra.SelectedItem).Valor.ToString());

                int respuesta = TipoBarraLogica.Instancia.Guardar(valor, out mensaje);

                if (respuesta < 1) {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                TipoBarraCodigo.TipoCodigo = (Enum.IsDefined(typeof(BarcodeLib.TYPE), valor)) ? ((BarcodeLib.TYPE)valor) : ((BarcodeLib.TYPE)0);

                MessageBox.Show("Se guardaron los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) {
                MessageBox.Show("No se pudo guardar el tipo\nMayor Detalle:\n" + ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        
    }
}
