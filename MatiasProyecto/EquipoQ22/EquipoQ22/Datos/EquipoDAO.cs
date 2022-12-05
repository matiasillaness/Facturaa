using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EquipoQ22.Domino;

namespace EquipoQ22.Datos
{
    internal class EquipoDAO : Acceso, IEquipoDao
    {
        private static EquipoDAO instancia;

        public bool EjecutarSP(Equipo Oequipo) {
            bool ok = true;
            SqlTransaction t = null;
            try
            {
                Conectar();
                t = conn.BeginTransaction();
                comando.Transaction = t;
                comando.CommandText = "SP_INSERTAR_EQUIPO";
                comando.Parameters.AddWithValue("@pais", Oequipo.pais);
                comando.Parameters.AddWithValue("@director_tecnico", Oequipo.DirectorTecnico);
               
                
                SqlParameter pOut = new SqlParameter();
                pOut.SqlDbType = SqlDbType.Int;
                pOut.ParameterName = "@id";
                pOut.Direction = ParameterDirection.Output;
                comando.Parameters.Add(pOut);
                
                
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();
                int count = 1;
                foreach (Jugador jugador in Oequipo.LPersonas)
                {
                    comando.CommandText = "SP_INSERTAR_DETALLES_EQUIPO";
                    //comando.Parameters.AddWithValue("@id_receta", oReceta.RecetaNro);
                    comando.Parameters.AddWithValue("@id_equipo", (int)pOut.Value);
                    comando.Parameters.AddWithValue("@id_persona", jugador.Persona.IdPersona);
                    comando.Parameters.AddWithValue("@camiseta", jugador.Camiseta);
                    comando.Parameters.AddWithValue("@posicion", jugador.Posicion);
                    count++;
                    comando.ExecuteNonQuery();
                    comando.Parameters.Clear();

                }
                t.Commit();
            }
            catch (Exception)
            {
                t.Rollback();
                ok = false;
            }
            finally
            {
                Desconectar();
            }
            return ok;
        }

        public DataTable ObtenerPersonas()
        {
            comando.Parameters.Clear();
            Conectar();
            comando.CommandText = "SP_CONSULTAR_PERSONAS";
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            Desconectar();
            return tabla;

            //public List<Persona> ObtenerPersonas()
            //{
            //    List<Persona> lPersona = new List<Persona>();
            //    DataTable tabla = HelperDao.ObtenerInstancia().ConsultarPersonas("SP_CONSULTAR_PERSONAS");
            //    foreach (DataRow data in tabla.Rows)
            //    {
            //        Persona persona = new Persona();
            //        persona.IdPersona = Convert.ToInt32(data["id_persona"].ToString());
            //        persona.NombreCompleto = data["nombre_completo"].ToString();
            //        persona.Clase = Convert.ToInt32(data["clase"].ToString());
            //        lPersona.Add(persona);
            //    }
            //    return lPersona;
            //}
        }
        
        public bool CrearEquipo(Equipo equipo)
        {
            return true;
        }
        public static EquipoDAO ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new EquipoDAO();
            }
            return instancia;
        }

    }
}
