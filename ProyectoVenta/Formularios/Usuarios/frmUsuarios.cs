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

namespace ProyectoVenta.Formularios.Usuarios
{
    public partial class frmUsuarios : Form
    {
        private static int _id = 0;
        private static int _indice = 0;

        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            List<Usuario> lista = UsuarioLogica.Instancia.Listar(out mensaje);

            foreach (Usuario us in lista)
            {
                dgvdata.Rows.Add(new object[] {
                    us.IdUsuario,
                    "",
                    us.NombreCompleto,
                    us.NombreUsuario,
                    us.IdPermisos,
                    us.Descripcion,
                    us.Clave
                });
            }

            cborol.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Administrador" });
            cborol.Items.Add(new OpcionCombo() { Valor = 2, Texto = "Empleado" });
            cborol.DisplayMember = "Texto";
            cborol.ValueMember = "Valor";
            cborol.SelectedIndex = 0;


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
        }

        private void Limpiar(bool vista = true)
        {
            txtnombre.BackColor = Color.White;
            txtusuario.BackColor = Color.White;
            txtclave.BackColor = Color.White;
            txtconfirmarclave.BackColor = Color.White;

            if (vista)
            {
                if (dgvdata.Rows.Count > 0)
                {
                    dgvdata.Rows[_indice].DefaultCellStyle.BackColor = Color.White;
                }
            }

            _id = 0;
            _indice = 0;
            txtnombre.Text = "";
            txtusuario.Text = "";
            cborol.SelectedIndex = 0;
            txtclave.Text = "";
            txtconfirmarclave.Text = "";
            lblresultado.Text = "";
            txtnombre.Focus();
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
                    txtnombre.Text = dgvdata.Rows[index].Cells["NombreCompleto"].Value.ToString();
                    txtusuario.Text = dgvdata.Rows[index].Cells["Usuario"].Value.ToString();
                    int idrol = Convert.ToInt32(dgvdata.Rows[index].Cells["IdRol"].Value.ToString());
                   
                    int encontrado = 0;
                    foreach (OpcionCombo oc in cborol.Items) {
                        if (Convert.ToInt32(oc.Valor.ToString()) == idrol) {
                            break;
                        }
                        encontrado++;
                    }
                    cborol.SelectedIndex = encontrado;
                    txtclave.Text = dgvdata.Rows[index].Cells["Clave"].Value.ToString();
                    txtconfirmarclave.Text = dgvdata.Rows[index].Cells["Clave"].Value.ToString();

                    txtnombre.BackColor = Color.LemonChiffon;
                    txtusuario.BackColor = Color.LemonChiffon;
                    txtclave.BackColor = Color.LemonChiffon;
                    txtconfirmarclave.BackColor = Color.LemonChiffon;
                    dgvdata.Rows[index].DefaultCellStyle.BackColor = Color.LemonChiffon;
                }
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            if (txtnombre.Text.Trim() == "")
            {
                lblresultado.Text = "Debe ingresar el nombre";
                lblresultado.ForeColor = Color.Red;
                return;
            }
            if (txtusuario.Text.Trim() == "")
            {
                lblresultado.Text = "Debe ingresar el usuario";
                lblresultado.ForeColor = Color.Red;
                return;
            }

            if (txtclave.Text.Trim() == "" || txtconfirmarclave.Text.Trim() == "")
            {
                lblresultado.Text = "Debe ingresar las claves";
                lblresultado.ForeColor = Color.Red;
                return;
            }

            if (txtclave.Text.Trim() != txtconfirmarclave.Text.Trim())
            {
                lblresultado.Text = "Las contraseñas no coinciden";
                lblresultado.ForeColor = Color.Red;
                return;
            }

            Usuario obj = new Modelo.Usuario() {
                IdUsuario = _id,
                NombreCompleto = txtnombre.Text,
                NombreUsuario = txtusuario.Text,
                IdPermisos = Convert.ToInt32(((OpcionCombo)cborol.SelectedItem).Valor.ToString()),
                Descripcion = ((OpcionCombo)cborol.SelectedItem).Texto.ToString(),
                Clave = txtclave.Text
            };

            int existe = UsuarioLogica.Instancia.Existe(obj.NombreUsuario, _id, out mensaje);
            if (existe > 0)
            {
                lblresultado.Text = mensaje;
                lblresultado.ForeColor = Color.Red;
                return;
            }

            if (_id == 0)
            {
                int idgenerado = UsuarioLogica.Instancia.Guardar(obj, out mensaje);
                if (idgenerado > 0)
                {
                    Limpiar();
                    dgvdata.Rows.Add(new object[] { idgenerado, "", obj.NombreCompleto, obj.NombreUsuario,obj.IdPermisos,obj.Descripcion,obj.Clave });
                    lblresultado.Text = "Registro Correcto";
                    lblresultado.ForeColor = Color.Green;
                }
                else
                {
                    lblresultado.Text = mensaje;
                    lblresultado.ForeColor = Color.Red;
                }
            }
            else
            {
                int afectados = UsuarioLogica.Instancia.Editar(obj, out mensaje);
                if (afectados > 0)
                {
                    dgvdata.Rows[_indice].Cells["NombreCompleto"].Value = obj.NombreCompleto;
                    dgvdata.Rows[_indice].Cells["Usuario"].Value = obj.NombreUsuario;
                    dgvdata.Rows[_indice].Cells["IdRol"].Value = obj.IdPermisos;
                    dgvdata.Rows[_indice].Cells["Rol"].Value = obj.Descripcion;
                    dgvdata.Rows[_indice].Cells["Clave"].Value = obj.Clave;
                    Limpiar();
                    lblresultado.Text = "Modificación Correcta";
                    lblresultado.ForeColor = Color.Green;
                }
                else
                {
                    lblresultado.Text = mensaje;
                    lblresultado.ForeColor = Color.Red;
                }

            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (_id != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int respuesta = UsuarioLogica.Instancia.Eliminar(_id);
                    if (respuesta > 0)
                    {
                        dgvdata.Rows.RemoveAt(_indice);
                        Limpiar(false);
                    }
                    else
                        MessageBox.Show("No se pudo eliminar el usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            dgvdata.ClearSelection();
        }
    }
}
