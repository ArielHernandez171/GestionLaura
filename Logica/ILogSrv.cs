using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionLaura.Logica
{
    public interface ILogSrv
    {
        public Task Cargar();
        public Task Guardar();
    }
}
