using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using GestionLaura.Clases;
using ClosedXML.Excel;
namespace GestionLaura.Logica
{
    public class LogServ:ILogSrv
    {
        public string Rute = "registros.json";
        public List<RegistroServs> registros;
        public LogServ()
        {
            registros = new List<RegistroServs>();
        }
        public async Task Cargar()
        {
            if (File.Exists(Rute) != true)//Verifica si existe el archivo
            {
                registros = new List<RegistroServs>();//caso negativo, "retorna" null
            }
            else
            {
                //var text = File.ReadAllText(Rute);//se pasa del archvo al texto
                //File.OpenRead(Rute);//Caso positivo, abre y retorna archivos
                string text = "{}";
                using (StreamReader sr = File.OpenText(Rute))
                {

                    text = await sr.ReadToEndAsync();
                }
                try
                {
                    registros = JsonSerializer.Deserialize<List<RegistroServs>>(text) ?? new List<RegistroServs>();//se deserealiza los datos y se lo guarda en una lista
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.Message);
                }
            }
        }

        public async Task Guardar()
        {
            string text = "{}";

            text = JsonSerializer.Serialize(registros, new JsonSerializerOptions { WriteIndented = true });//Serealiza los datos de la lista prod
            using (StreamWriter sw = File.CreateText(Rute))
            {

                await sw.WriteLineAsync(text);
            }
        }
        public async Task GuardarServicio(RegistroServs servs)
        {
            await Cargar();
            registros.Add(servs);
            await Guardar();//se guarda los cambios
        }
        public async Task<List<RegistroServs>?> ObtenerServiDate(DateTime inicio, DateTime fin)
        {
            await Cargar();
            return (from p in registros
                    where p.Fecha > inicio && p.Fecha < fin
                    select p).ToList();//se retorna una lista de servicios dados en de entre dos fechas
        }
        public async Task<List<RegistroServs>>ObtenerServiName(string name)
        {
            await Cargar();
            return (from p in registros
                    where p.ClienteId== name
                    select p).ToList();
        }
        public void Descargardo(List<RegistroServs> servs)
        {
            int counter = 2;
            string descargar = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Servicios.xlsm");
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Servicios");
                worksheet.Cell("A1").Value = "Fecha";
                worksheet.Cell("B1").Value = "Costo";
                worksheet.Cell("C1").Value = "Precio";
                worksheet.Cell("D1").Value = "Concepto";
                worksheet.Cell("E1").Value = "Id del cliente";
                foreach (var servo in servs)
                {
                    worksheet.Cell("A" + counter).Value = servo.Fecha.ToString();
                    worksheet.Cell("B" + counter).Value = servo.Costo;
                    worksheet.Cell("C" + counter).Value = servo.PrecioFinal;
                    worksheet.Cell("D" + counter).Value = servo.Concepto;
                    worksheet.Cell("E" + counter).Value = servo.ClienteId;
                    counter = counter + 2;
                }
                workbook.SaveAs(descargar);
            }

        }
    }
}
