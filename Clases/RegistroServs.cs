using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionLaura.Clases
{
    public class RegistroPersonal
    {
        public List<string> Servicios { get; set; }
        public DateTime Fecha { get; set; }
        public RegistroPersonal()
        {
            Servicios = new List<string>();
            Fecha = DateTime.Now;
        }
       
    }
    public class RegistroServs
    {
        public string Concepto { get; set; }
        public string ClienteId { get; set; }
        public float Costo { get; set; }
        public DateTime Fecha { get; set; }
        public float PrecioFinal {  get; set; }
        public List<RegistroPersonal> registroPersonals { get; set; }
        public RegistroServs()
        {
            Concepto = "";
            ClienteId = "";
            Costo = 0;
            PrecioFinal = 0;
            registroPersonals= new List<RegistroPersonal>();
            Fecha = DateTime.Now;
        }
        public override string ToString()
        {
            return Concepto+" "+Costo+" "+PrecioFinal+" "+Fecha+" "+ClienteId;
        }
    }

    /*
      * los servicios de los clientes se basan en
      * el concepto(Los que se le hizo)
      * el costo (La suma de lo consumido para el sevicio sobre el precio de los productos)
      * y la fecha de realizarse el servicio
      */
}
