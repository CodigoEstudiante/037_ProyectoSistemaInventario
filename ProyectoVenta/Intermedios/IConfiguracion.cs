using ProyectoVenta.Formularios.Configuracion;
using ProyectoVenta.Formularios.Permisos;
using ProyectoVenta.Formularios.Usuarios;
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
    public partial class IConfiguracion : Form
    {
        public Form FormularioVista { get; set; }
        public IConfiguracion()
        {
            InitializeComponent();
        }

        private void IConfiguracion_Load(object sender, EventArgs e)
        {

        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnotros_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmConfiguracion();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnpermisos_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmPermisos();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnusuarios_Click(object sender, EventArgs e)
        {
            FormularioVista = new frmUsuarios();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
