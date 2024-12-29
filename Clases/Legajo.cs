using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionLaura.Clases
{
    public class Legajo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Historial { get; set; }
        public List<Byte[]> Images { get; set; }
        public Legajo()
        {
            Id = "";
            Name = "";
            SurName = "";
            Historial = "";
            Images = new List<Byte[]>();
        }
    }
}
