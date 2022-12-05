using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Dominio
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Activo { get; set; }

        public Producto()
        {

        }
        public Producto(int IdProducto, string nombre, double precio, string activo)
        {
            idProducto = IdProducto;
            Nombre = nombre;
            Precio = precio;
            Activo = activo;
        }
    }
}
