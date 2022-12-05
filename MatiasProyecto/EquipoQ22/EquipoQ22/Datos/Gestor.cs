using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipoQ22.Domino;
using System.Data.SqlClient;
using System.Data;

namespace EquipoQ22.Datos
{
    internal class Gestor 
    {
        private IEquipoDao Edao;
        public DataTable ObtenerPersonas()
        {
            Edao = new EquipoDAO();
            return Edao.ObtenerPersonas();
        }
        public bool CrearEquipo(Equipo equipo)
        {
            Edao = new EquipoDAO();
            return Edao.CrearEquipo(equipo);
        }
        public bool EjecutarSP(Equipo Oequipo)
        {
            Edao = new EquipoDAO();
            return Edao.EjecutarSP(Oequipo);
        }
    }
}
