using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParcialApp.Acceso_a_datos
{
    
    class Dao : IDao
    {
        SqlConnection cnn = new SqlConnection(Properties.Resources.StringCNN);
        int proximo;
        
        public DataTable GetProductos()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CONSULTAR_PRODUCTOS", cnn);
            cnn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            dt.Load(cmd.ExecuteReader());
            cnn.Close();
            return dt;
        }

        public int ProximoId()
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("SP_PROXIMO_ID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter("@next", SqlDbType.Int);
            cmd.Parameters.Add(param);
            param.Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            proximo = Convert.ToInt32(param.Value);
            cnn.Close();
            return proximo;
        }

        public bool Save(Factura oFactura)
        {
            bool aux = false;
            int nrodet = 1;
            SqlTransaction t = null;
            cnn.Open();
            t = cnn.BeginTransaction();

            try
            {

                SqlCommand cmd = new SqlCommand("SP_INSERTAR_FACTURA", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cliente", oFactura.Cliente);
                cmd.Parameters.AddWithValue("@forma", oFactura.FormaPago);
                cmd.Parameters.AddWithValue("@total", oFactura.CalcularTotal());
                cmd.Parameters.AddWithValue("@nro", proximo);
                cmd.ExecuteNonQuery();

                foreach (DetalleFactura item in oFactura.DetalleFacturaList)
                {
                    SqlCommand cmdDet = new SqlCommand("SP_INSERTAR_DETALLES", cnn, t);
                    cmdDet.CommandType = CommandType.StoredProcedure;
                    cmdDet.Parameters.AddWithValue("@nro", proximo);
                    cmdDet.Parameters.AddWithValue("@detalle", nrodet);
                    cmdDet.Parameters.AddWithValue("@id_producto", item.oProducto.idProducto);
                    cmdDet.Parameters.AddWithValue("@cantidad", item.Cantidad);
                    cmdDet.ExecuteNonQuery();
                    nrodet++;
                }
                t.Commit();
                aux = true;

            }
            catch (Exception ex)
            {
                t.Rollback();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return aux;
        }

       
    }
}

