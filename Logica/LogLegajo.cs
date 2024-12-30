using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionLaura.Clases;
using System.Text.Json;
namespace GestionLaura.Logica
{
    public class LogLegajo:ILogLeg
    {
        public List<Legajo> Legajos { get; set; }
        public string Rute = Path.Combine(FileSystem.AppDataDirectory, "Legajo.json");
        public LogLegajo()
        {
            Legajos = new List<Legajo>();
        }

        public async Task Cargar()
        {

            if (File.Exists(Rute) != true)//Verifica si existe el archivo
            {
                Legajos = new List<Legajo>();//caso negativo, "retorna" null
            }
            else
            {
                string text = "{}";
                using (StreamReader sr = File.OpenText(Rute))
                {
                    text = await sr.ReadToEndAsync();
                }
                try
                {
                    Legajos = JsonSerializer.Deserialize<List<Legajo>>(text) ?? new List<Legajo>();//se deserealiza los datos y se lo guarda en una lista
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.Message);
                }
            }
        }

        public async Task Guardar()
        {
            string text = JsonSerializer.Serialize(Legajos, new JsonSerializerOptions { WriteIndented = true });//Serealiza los datos de la lista prod
            using (StreamWriter sw = File.CreateText(Rute))
            {
                await sw.WriteLineAsync(text);
            }
        }
        public async Task<List<Legajo>> ObtenerLegajos()//se obtiene los legajos de los clientes
        {
            await Cargar();
            return Legajos;
        }
        public async Task ActualizarLegajos(Legajo legajo)
        {
            await Cargar();//se cargan los legajos de los clientes
            Legajos.Where(l => l.Id == legajo.Id).First().Name = legajo.Name;
            Legajos.Where(l => l.Id == legajo.Id).First().SurName = legajo.SurName;
            Legajos.Where(l => l.Id == legajo.Id).First().Images = legajo.Images;
            Legajos.Where(l => l.Id == legajo.Id).First().Historial = legajo.Historial;//se hace los cambios
            await Guardar();//se guarda los cambios

        }
        public async Task NuevoLegajo(Legajo legajo)
        {
            await Cargar();
            Legajos.Add(legajo);//se agrega un legajo de un cliente
            await Guardar();//se guarda los cambios
        }
        public async Task<Legajo> BuscarLegajo(string Id)
        {
            await Cargar();
            if (Legajos.Exists(l=>l.Id==Id) )
                return Legajos.Where(l => l.Id == Id).First();//retorna el legajo de el cliente en base al id
            else
                return new Legajo();//si no existe el id buscado, retorna null
        }
    }
}
