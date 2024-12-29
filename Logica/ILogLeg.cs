using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionLaura.Logica
{
    public interface ILogLeg
    {
        public Task Cargar();
        public Task Guardar();
    }
}
