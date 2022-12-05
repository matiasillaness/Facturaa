using EquipoQ22.Domino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EquipoQ22.Datos
{
    internal class Acceso
    {
        protected SqlConnection conn = new SqlConnection(Properties.Resources.CnnString);
        protected SqlCommand comando = new SqlCommand();
        protected SqlParameter parametro = new SqlParameter();

        protected void Conectar()
        {
            conn.Open();
            comando.Connection = conn;
            comando.CommandType = CommandType.StoredProcedure;
        }

        protected void Desconectar() 
        {
            conn.Close();
        }


    }
}
