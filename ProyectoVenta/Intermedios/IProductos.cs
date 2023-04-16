using ProyectoVenta.Formularios;
using ProyectoVenta.Formularios.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoVenta.Intermedios
{
    public partial class IProductos : Form
    {
        public Form FormularioVista { get; set; }
        public IProductos()
        {
            InitializeComponent();
        }

        private void IProductos_Load(object sender, EventArgs e)
        {

        }

        private void btnagregarproductos_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmRegistrarProducto();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncargar_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmCargarProducto();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
