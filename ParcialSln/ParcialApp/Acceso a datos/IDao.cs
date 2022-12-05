using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Acceso_a_datos
{
    interface IDao
    {

        DataTable GetProductos();
        bool Save(Factura oFactura);
        int ProximoId();

    }
}
