using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Dominio
{
    public class DetalleFactura
    {
        
        public Producto oProducto { get; set; }
        public int Cantidad { get; set; }

        public DetalleFactura()
        {

        }
        public DetalleFactura(Producto oproducto, int cantidad)
        {
            oProducto = oproducto;
            Cantidad = cantidad;
        }
        public double CalcularSub()
        {
            return Cantidad * oProducto.Precio;
        }
    }
}
