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

namespace ProyectoVenta.Formularios.Permisos
{
    public partial class frmPermisos : Form
    {
        public frmPermisos()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPermisos_Load(object sender, EventArgs e)
        {
            Modelo.Permisos padmin = PermisosLogica.Instancia.Obtener(1);
            Modelo.Permisos pemple = PermisosLogica.Instancia.Obtener(2);

            a_salidas.Checked = padmin.Salidas == 1 ? true : false;
            a_entradas.Checked = padmin.Entradas == 1 ? true : false;
            a_productos.Checked = padmin.Productos == 1 ? true : false;
            a_clientes.Checked = padmin.Clientes == 1 ? true : false;
            a_proveedores.Checked = padmin.Proveedores == 1 ? true : false;
            a_inventario.Checked = padmin.Inventario == 1 ? true : false;
            a_configuracion.Checked = padmin.Configuracion == 1 ? true : false;

            e_salidas.Checked = pemple.Salidas == 1 ? true : false;
            e_entradas.Checked = pemple.Entradas == 1 ? true : false;
            e_productos.Checked = pemple.Productos == 1 ? true : false;
            e_clientes.Checked = pemple.Clientes == 1 ? true : false;
            e_proveedores.Checked = pemple.Proveedores == 1 ? true : false;
            e_inventario.Checked = pemple.Inventario == 1 ? true : false;
            e_configuracion.Checked = pemple.Configuracion == 1 ? true : false;
        }

        private void btnguardaradministrador_Click(object sender, EventArgs e)
        {
            int _a_salidas = 0;
            int _a_entradas = 0;
            int _a_productos = 0;
            int _a_clientes = 0;
            int _a_proveedores = 0;
            int _a_inventario = 0;
            int _a_configuracion = 0;

            if (a_salidas.Checked)
                _a_salidas = 1;

            if (a_entradas.Checked)
                _a_entradas = 1;

            if (a_productos.Checked)
                _a_productos = 1;

            if (a_clientes.Checked)
                _a_clientes = 1;

            if (a_proveedores.Checked)
                _a_proveedores = 1;

            if (a_inventario.Checked)
                _a_inventario = 1;

            if (a_configuracion.Checked)
                _a_configuracion = 1;


            string mensaje = string.Empty;

            int operaciones = PermisosLogica.Instancia.Guardar(new Modelo.Permisos() {
                IdPermisos = 1,
                Salidas = _a_salidas,
                Entradas = _a_entradas,
                Productos = _a_productos,
                Clientes = _a_clientes,
                Proveedores = _a_proveedores,
                Inventario = _a_inventario,
                Configuracion = _a_configuracion
            }, out mensaje);

            if (operaciones < 1)
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else {
                MessageBox.Show("Se guardaron los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnguardarempleados_Click(object sender, EventArgs e)
        {
            int _e_salidas = 0;
            int _e_entradas = 0;
            int _e_productos = 0;
            int _e_clientes = 0;
            int _e_proveedores = 0;
            int _e_inventario = 0;
            int _e_configuracion = 0;

            if (e_salidas.Checked)
                _e_salidas = 1;

            if (e_entradas.Checked)
                _e_entradas = 1;

            if (e_productos.Checked)
                _e_productos = 1;

            if (e_clientes.Checked)
                _e_clientes = 1;

            if (e_proveedores.Checked)
                _e_proveedores = 1;

            if (e_inventario.Checked)
                _e_inventario = 1;

            if (e_configuracion.Checked)
                _e_configuracion = 1;


            string mensaje = string.Empty;

            int operaciones = PermisosLogica.Instancia.Guardar(new Modelo.Permisos()
            {
                IdPermisos = 2,
                Salidas = _e_salidas,
                Entradas = _e_entradas,
                Productos = _e_productos,
                Clientes = _e_clientes,
                Proveedores = _e_proveedores,
                Inventario = _e_inventario,
                Configuracion = _e_configuracion
            }, out mensaje);

            if (operaciones < 1)
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Se guardaron los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

       
    }
}
