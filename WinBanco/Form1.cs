using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinBanco
{
    public enum Acciones
    {
        RegistrarCliente,
        RegistrarCuenta,
        EditarCuenta
    }
    public partial class frmCuentas : Form
    {
        private ConexionDB conexion;
        private Acciones accion;

        public frmCuentas()
        {
            InitializeComponent();
            conexion = new ConexionDB();
            accion = new Acciones();

        }

        private void frmCuentas_Load(object sender, EventArgs e)
        {
            VolverACero();
        }

        private void VolverACero()
        {
            HabilitarControles(false);
            btnEditar.Enabled = false;
            btnRegistrarCliente.Enabled = true;
            btnRegistrarCuenta.Enabled = true;
            btnCancelar.Enabled = false;
            btnGrabar.Enabled = false;
            dgvCuentas.Enabled = true;
            CargarTiposCuentas();
            cmbTipoCuenta.SelectedIndex = -1;
            cmbCliente.SelectedIndex = -1;
            CargarListaClientes();
            
            dtpMovimiento.Enabled = false;
            CargarGrilla();
        }

        private void CargarListaClientes()
        {
            DataTable clientes = conexion.EjecutarSP("SP_LISTAR_CLIENTES");
            cmbCliente.DataSource = clientes;
            cmbCliente.ValueMember = clientes.Columns[0].ColumnName;
            cmbCliente.DisplayMember = clientes.Columns[1].ColumnName;
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CargarGrilla()
        {
            DataTable detalle = conexion.EjecutarSP("SP_LISTAR_DETALLE");
            dgvCuentas.Rows.Clear();
            foreach (DataRow row in detalle.Rows)
            {
                Cuenta c = new Cuenta();
                Cliente cl = new Cliente();
                TipoCuenta tc = new TipoCuenta();
                c.Cbu = Convert.ToInt32(row["cbu"].ToString());
                tc.NombreTipo = row["nombre_Cuenta"].ToString();
                cl.Apellido = row["apellido"].ToString();
                cl.Nombre = row["nombre"].ToString();
                cl.Dni = Convert.ToInt32(row["dni"].ToString());
                c.Saldo = Convert.ToDouble(row["saldo"].ToString());
                c.UltimoMovimiento = Convert.ToDateTime(row["ultimo_Movimiento"].ToString());
                dgvCuentas.Rows.Add(new object[] { c.Cbu, tc.NombreTipo, cl.Apellido, cl.Nombre, cl.Dni, c.Saldo, c.UltimoMovimiento });
            }
            CargarListaClientes();
        }

        private void CargarTiposCuentas()
        {
            DataTable tipo = conexion.EjecutarSP("SP_LISTAR_TIPOS_CUENTAS");
            cmbTipoCuenta.DataSource = tipo;
            cmbTipoCuenta.ValueMember = tipo.Columns[0].ColumnName;
            cmbTipoCuenta.DisplayMember = tipo.Columns[1].ColumnName;
            cmbTipoCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void HabilitarControles(bool v)
        {
            gbCliente.Enabled = v;
            gbCuenta.Enabled = v;
            dtpMovimiento.Value=DateTime.Now;
        }

        private void dgvCuentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HabilitarControles(false);
            btnRegistrarCliente.Enabled = false;
            btnEditar.Enabled = true;
            btnRegistrarCuenta.Enabled = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            HabilitarControles(true);
            btnEditar.Enabled = false;
            btnRegistrarCliente.Enabled = false;
            btnGrabar.Enabled = true;
            btnCancelar.Enabled = true;
            dgvCuentas.Enabled = false;
            accion = Acciones.EditarCuenta;
        }

        private void dgvCuentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
            btnRegistrarCliente.Enabled = false;
            try
            {
                txtCbu.Text = dgvCuentas.CurrentRow.Cells[0].Value.ToString();
                cmbTipoCuenta.Text = dgvCuentas.CurrentRow.Cells[1].Value.ToString();
                txtApellido.Text = dgvCuentas.CurrentRow.Cells[2].Value.ToString();
                txtNombre.Text = dgvCuentas.CurrentRow.Cells[3].Value.ToString();
                txtDni.Text = dgvCuentas.CurrentRow.Cells[4].Value.ToString();
                txtSaldo.Text = dgvCuentas.CurrentRow.Cells[5].Value.ToString();
                dtpMovimiento.Text = dgvCuentas.CurrentRow.Cells[6].Value.ToString();
                cmbCliente.Text = dgvCuentas.CurrentRow.Cells[7].Value.ToString();
            }
            catch
            {

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            VolverACero();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                switch (accion)
                {
                    case Acciones.RegistrarCliente:
                        string sP1 = "SP_INSERTAR_CLIENTESNUEVOS ";
                        Cliente cl = new Cliente();
                        cl.Apellido = txtApellido.Text;
                        cl.Nombre = txtNombre.Text;
                        cl.Dni = Convert.ToInt32(txtDni.Text);
                        conexion.EjecutarInsertSPCliente(sP1, cl);
                        CargarListaClientes();
                        CargarTiposCuentas();
                        CargarGrilla();
                        break;

                    case Acciones.RegistrarCuenta:
                        string sP2 = "SP_INSERTAR_CUENTANUEVA";
                        Cuenta c = new Cuenta();
                        c.Cbu = Convert.ToInt32(txtCbu.Text);
                        c.Saldo = Convert.ToDouble(txtSaldo.Text);
                        c.Tipo.IdTipoCuenta = Convert.ToInt32(cmbTipoCuenta.SelectedValue);
                        c.UltimoMovimiento = Convert.ToDateTime(dtpMovimiento.Value);
                        c.Cliente.IdCliente = Convert.ToInt32(cmbCliente.SelectedValue);
                        conexion.EjecutarInsertSPCuenta(sP2, c);
                        CargarListaClientes();
                        CargarGrilla();
                        break;

                    case Acciones.EditarCuenta:
                        string sP3 = "SP_EDITAR_CUENTAEXISTENTE";
                        Cuenta c1 = new Cuenta();
                        c1.Cbu = Convert.ToInt32(txtCbu.Text);
                        c1.Saldo = Convert.ToDouble(txtSaldo.Text);
                        c1.Tipo.IdTipoCuenta = Convert.ToInt32(cmbTipoCuenta.SelectedValue);
                        c1.UltimoMovimiento = Convert.ToDateTime(dtpMovimiento.Value);
                        c1.Cliente.IdCliente = Convert.ToInt32(cmbCliente.SelectedValue);
                        conexion.EjecutarInsertSPCuenta(sP3, c1);
                        CargarListaClientes();
                        CargarTiposCuentas();
                        CargarGrilla();
                        break;
                }
            }
            VolverACero();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Salir de la Aplicacion?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private bool Validar()
        {
            return true;
        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            HabilitarControles(true);
            gbCuenta.Enabled = false;
            btnRegistrarCliente.Enabled = false;
            btnRegistrarCuenta.Enabled = false;
            btnGrabar.Enabled = true;
            dgvCuentas.Enabled = false;
            btnCancelar.Enabled = true;
            accion = Acciones.RegistrarCliente;
        }

        private void btnRegistrarCuenta_Click(object sender, EventArgs e)
        {
            HabilitarControles(true);
            gbCliente.Enabled = false;
            btnRegistrarCliente.Enabled = false;
            btnGrabar.Enabled = true;
            dgvCuentas.Enabled = false;
            btnCancelar.Enabled = true;
            accion = Acciones.RegistrarCuenta;
        }

        
    }
}





