using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EquipoQ22.Datos;
using EquipoQ22.Domino;

namespace EquipoQ22
{
  
    public partial class FrmAlta : Form
    {
        private Equipo equipoNuevo;
        private Gestor GestorDB;
        
        public FrmAlta()
        {

            InitializeComponent();
            equipoNuevo = new Equipo();
            GestorDB = new Gestor();
        }
        private void FrmAlta_Load(object sender, EventArgs e)
        {
            cargarCombo();
        }

        private void cargarCombo()
        {
            DataTable T = GestorDB.ObtenerPersonas();
            cboPersona.DataSource = T;
            cboPersona.ValueMember = T.Columns[0].ColumnName;
            cboPersona.DisplayMember = T.Columns[1].ColumnName;
            cboPersona.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //DataRowView item = (DataRowView)cboProducto.SelectedItem;
            //int ingr = Convert.ToInt32(item.Row.ItemArray[0]);
            //string nom = item.Row.ItemArray[1].ToString();

            //Ingrediente i = new Ingrediente(ingr, nom);
            //int cant = Convert.ToInt32(nudCantidad.Value);
            //DetalleReceta detalle = new DetalleReceta(i, cant);

            //nueva.AgregarReceta(detalle);

            //dgvDetalles.Rows.Add(new object[] { ingr, nom, cant });

            //TotalIngredientes();

            if (validarJugador())
            {
                DataRowView item = (DataRowView)cboPersona.SelectedItem;
                int Id_persona = Convert.ToInt32(item.Row.ItemArray[0]);
                string NombreCompleto = item.Row.ItemArray[1].ToString();
                int Clase = Convert.ToInt32(item.Row.ItemArray[2]);
                Persona persona = new Persona(Id_persona, NombreCompleto, Clase);


                int Camiseta = (int)nudCamiseta.Value;
                string Posicion = cboPosicion.Text;
                Jugador jugador = new Jugador(persona, Camiseta, Posicion);

                //public int EquipoNro { get; set; }
                //public string pais { get; set; }
                //public string DirectorTecnico { get; set; }

                equipoNuevo.pais = txtPais.Text;
                equipoNuevo.DirectorTecnico = txtDT.Text;

                equipoNuevo.AgregarDetalle(jugador);
                dgvDetalles.Rows.Add(new object[] { jugador.Persona.IdPersona, jugador.Persona.NombreCompleto, jugador.Camiseta, jugador.Posicion });
            }
           
        }

        private void agregarJugador()
        {
           
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            GestorDB.EjecutarSP(equipoNuevo);
        }

        private bool validarJugador()
        {
            if (nudCamiseta.Value > 23 && nudCamiseta.Value < 1)
            {
                MessageBox.Show("el nro de camiseta debe ser de 1 a 23");
                return false; 
            }
            if (txtPais.Text == string.Empty)
            {
                MessageBox.Show("ingrese pais");
                return false; 
            }
            if (txtDT.Text == string.Empty)
            {
                MessageBox.Show("ingrese DT");
                return false; 
            }
            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["posicion"].Value.ToString().Equals(cboPosicion.Text))
                {
                    MessageBox.Show("el jugador no puede cubrir 2 puestos");
                    return false; 
                }
            }
            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["Camiseta"].Value.ToString().Equals(nudCamiseta.Text))
                {
                    MessageBox.Show("la camiseta no puede estar repetida");
                    return false;
                }
            }
            if (cboPersona.Text == string.Empty)
            {
                return false; 
                MessageBox.Show("debe elegir una persona");
            }
            if (cboPosicion.Text == string.Empty)
            {
                return false; ;
                MessageBox.Show("debe elegir una posicion");
            }
            return true;
        }
        private void LimpiarCampos()
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
        }



    }
}
