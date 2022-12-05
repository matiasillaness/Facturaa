using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Dominio
{
    public class Factura
    {
        public int Nro { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public int FormaPago { get; set; }
        public double Total { get; set; }
        public DateTime FechaBaja { get; set; }

        public List<DetalleFactura> DetalleFacturaList = new List<DetalleFactura>();

        public Factura()
        {

        }

        public Factura(int nro, DateTime fecha, string cliente, int formaPago, double total, DateTime fechaBaja)
        {
            Nro = nro;
            Cliente = cliente;
            FormaPago = formaPago;
            Total = total;
            FormaPago = formaPago;
            Total = total;
            FechaBaja = fechaBaja;
        }
        public void AgregarDetalle(DetalleFactura deta)
        {
            DetalleFacturaList.Add(deta);
        }

        public void QuitarDetalle(int index)
        {
            DetalleFacturaList.RemoveAt(index);
        }
        public double CalcularTotal()
        {
            double total = 0;
            foreach (DetalleFactura item in DetalleFacturaList)
                total += item.CalcularSub();
            return total;
        }
    }
}
