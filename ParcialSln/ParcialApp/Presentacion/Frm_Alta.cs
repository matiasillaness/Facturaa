
using ParcialApp.Acceso_a_datos;
using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParcialApp.Presentacion
{
    public partial class Frm_Alta : Form
    {
        Factura oFactura;
        private IDao dao;
        DetalleFactura oDetalleFactura;
       
        public Frm_Alta()
        {
            InitializeComponent();
            dao = new Dao();
            oFactura = new Factura();
            txtCliente.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvDetalles.Rows.Count == 0)
            {
                MessageBox.Show("Ingrese al menos un detalle!", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrEmpty(txtCliente.Text))
            {
                MessageBox.Show("Ingrese el cliente", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrEmpty(cboForma.Text))
            {
                MessageBox.Show("Ingrese la forma de pago!", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            oFactura.Nro = dao.ProximoId();
            oFactura.Cliente = txtCliente.Text;
            oFactura.FormaPago = Convert.ToInt32(cboForma.SelectedIndex) + 1;
            oFactura.Total = oFactura.CalcularTotal();
            var resultado = dao.Save(oFactura);
            if (resultado)
            {
                MessageBox.Show("La factura nro : " + oFactura.Nro + " se guardó correctamente" );
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Verifique los campos");
            }
        }

        private void LimpiarCampos()
        {
            txtCliente.Text = String.Empty;
            cboForma.SelectedIndex = -1;
            cboProducto.SelectedIndex = -1;
            nudCantidad.Value = 1;
            dgvDetalles.Rows.Clear();
            lblTotal.Text = "Total: " + 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea salir del programa?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
            else
            {
                return;
            }
        }

        private void Frm_Alta_Presupuesto_Load(object sender, EventArgs e)
        {
            CargarCombo();
            cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboForma.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CargarCombo()
        {
            cboProducto.DataSource = dao.GetProductos();
            cboProducto.ValueMember = "id_producto";
            cboProducto.DisplayMember = "n_producto";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DataRowView Item = (DataRowView)cboProducto.SelectedItem;
            int id = Convert.ToInt32(Item.Row.ItemArray[0]);
            string nombre = Item.Row.ItemArray[1].ToString();
            if (ExisteProductoEnGrilla(nombre))
            {
                MessageBox.Show("El producto ingresado ya se cargó", "VALIDACION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            double precio = Convert.ToDouble(Item.Row.ItemArray[2].ToString());
            int cantidad = Convert.ToInt32(nudCantidad.Value);
            double sub = precio * cantidad;
            //if (Convert.ToInt32(cboForma.SelectedValue) == 0)
            //{
            //    sub = precio * cantidad;
            //}
            //else
            //{
            //    sub = (precio * cantidad) * 1.1;
            //}
            Producto oProducto = new Producto(id, nombre, precio, "S");
            oDetalleFactura = new DetalleFactura(oProducto, cantidad);
            dgvDetalles.Rows.Add(new object[] { id, nombre, precio, cantidad, sub });
            oFactura.AgregarDetalle(oDetalleFactura);
            lblTotal.Text = "Total: " + oFactura.CalcularTotal();
        }

        private bool ExisteProductoEnGrilla(string text)
        {
            foreach (DataGridViewRow fila in dgvDetalles.Rows)
            {
                if (fila.Cells["producto"].Value.Equals(text))
                    return true;
            }
            return false;
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 5)
            {
                if (dgvDetalles.CurrentRow.Index < 0)
                {
                    return;
                }
                oFactura.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
                lblTotal.Text = "Total: " + oFactura.CalcularTotal();
            }
        }
    }
}
