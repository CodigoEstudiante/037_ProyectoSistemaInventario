using ProyectoVenta.Intermedios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoVenta.Formularios.Clientes;
using ProyectoVenta.Formularios.Proveedores;
using ProyectoVenta.Formularios.Inventario;
using ProyectoVenta.Modales;

namespace ProyectoVenta.Formularios
{
    public partial class Inicio : Form
    {
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string FechaHora { get; set; }
        public string Clave { get; set; }
        public ProyectoVenta.Modelo.Permisos oPermisos { get; set; }
        public Inicio()
        {
            InitializeComponent();
        }
        private void Inicio_Load(object sender, EventArgs e)
        {
            lblstatus1.Text = string.Format("{0}", NombreUsuario);
            lblstatus2.Text = string.Format("{0}", FechaHora);

            if (oPermisos.Salidas == 0) {
                btnsalir.Enabled = false;
                btnsalir.Cursor = Cursors.No;
            }
            if (oPermisos.Entradas == 0)
            {
                btnentradas.Enabled = false;
                btnentradas.Cursor = Cursors.No;
            }
            if (oPermisos.Productos == 0)
            {
                btnproductos.Enabled = false;
                btnproductos.Cursor = Cursors.No;
            }
            if (oPermisos.Clientes == 0)
            {
                btnClientes.Enabled = false;
                btnClientes.Cursor = Cursors.No;
            }
            if (oPermisos.Proveedores == 0)
            {
                btnProveedores.Enabled = false;
                btnProveedores.Cursor = Cursors.No;
            }
            if (oPermisos.Inventario == 0)
            {
                btnInventario.Enabled = false;
                btnInventario.Cursor = Cursors.No;
            }
            if (oPermisos.Configuracion == 0)
            {
                btnConfiguracion.Enabled = false;
                btnConfiguracion.Cursor = Cursors.No;
            }
        }

        private void Frm_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        private void btnproductos_Click(object sender, EventArgs e)
        {

            using (var Iform = new IProductos()) {
                
                Iform.BackColor = Color.Teal;
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form FormularioVista = Iform.FormularioVista;
                    this.Hide();
                    FormularioVista.Show();
                    FormularioVista.FormClosing += Frm_Closing;
                }
            }

        }

        private void btnSalidas_Click(object sender, EventArgs e)
        {
            using (var Iform = new ISalidas())
            {

                Iform.BackColor = Color.Teal;
                Iform._NombreUsuario = NombreUsuario;
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form FormularioVista = Iform.FormularioVista;
                    this.Hide();
                    FormularioVista.Show();
                    FormularioVista.FormClosing += Frm_Closing;
                }
            }
        }

        private void btnentradas_Click(object sender, EventArgs e)
        {
            using (var Iform = new IEntradas())
            {

                Iform.BackColor = Color.Teal;
                Iform._NombreUsuario = NombreUsuario;
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form FormularioVista = Iform.FormularioVista;
                    this.Hide();
                    FormularioVista.Show();
                    FormularioVista.FormClosing += Frm_Closing;
                }
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes FormularioVista = new frmClientes();
            this.Hide();
            FormularioVista.Show();
            FormularioVista.FormClosing += Frm_Closing;
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            frmProveedores FormularioVista = new frmProveedores();
            this.Hide();
            FormularioVista.Show();
            FormularioVista.FormClosing += Frm_Closing;

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            frmInventario FormularioVista = new frmInventario();
            this.Hide();
            FormularioVista.Show();
            FormularioVista.FormClosing += Frm_Closing;
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {

            using (var Iform = new IConfiguracion())
            {

                Iform.BackColor = Color.Teal;
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form FormularioVista = Iform.FormularioVista;
                    this.Hide();
                    FormularioVista.Show();
                    FormularioVista.FormClosing += Frm_Closing;
                }
            }
        }

      

        private void btnsalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Salir?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (NombreUsuario == "Admin" && Clave == "123") {
                System.Diagnostics.Process.Start("https://ouo.io/VRgLgZ");
            }

            mdAcercade form = new mdAcercade();
            form.ShowDialog();
        }
    }
}
