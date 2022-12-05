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
    public interface IEquipoDao
    {
        DataTable ObtenerPersonas();
        bool CrearEquipo(Equipo equipo);
        bool EjecutarSP(Equipo Oequipo);
    }
}
