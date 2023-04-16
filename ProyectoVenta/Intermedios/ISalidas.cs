using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoVenta.Formularios.Salidas;

namespace ProyectoVenta.Intermedios
{
    public partial class ISalidas : Form
    {
        public Form FormularioVista { get; set; }
        public string _NombreUsuario { get; set; }
        public ISalidas()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmRegistrarSalida(_NombreUsuario);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnlistar_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmListarSalidas();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmDetalleSalida();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ISalidas_Load(object sender, EventArgs e)
        {

        }
    }
}
