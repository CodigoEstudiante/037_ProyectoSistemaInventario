using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoVenta.Formularios.Entradas;

namespace ProyectoVenta.Intermedios
{
    public partial class IEntradas : Form
    {
        public Form FormularioVista { get; set; }
        public string _NombreUsuario { get; set; }
        public IEntradas()
        {
            InitializeComponent();
        }

        private void IEntradas_Load(object sender, EventArgs e)
        {

        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmRegistrarEntrada(_NombreUsuario);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnlistar_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmListarEntradas();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmDetalleEntrada();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
