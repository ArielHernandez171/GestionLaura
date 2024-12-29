using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionLaura.Clases
{
    public class RegistroServs
    {
        public string Concepto { get; set; }
        public float Costo { get; set; }
        public DateTime Fecha { get; set; }
        public RegistroServs()
        {
            Concepto = "";
            Costo = 0;
            Fecha = DateTime.Now;
        }
    }
    /*
      * los servicios de los clientes se basan en
      * el concepto(Los que se le hizo)
      * el costo (La suma de lo consumido para el sevicio sobre el precio de los productos)
      * y la fecha de realizarse el servicio
      */
}
