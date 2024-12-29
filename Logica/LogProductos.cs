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
    public class LogProductos:ILogProd
    {
        public List<Producto> Prod { get; set; }
        public readonly string Rute= Path.Combine(FileSystem.AppDataDirectory, "Producto.json");
        public LogProductos()
        {
            Prod = new List<Producto>();
            

        }


        public async Task Cargar()
        {

            if (File.Exists(Rute) != true)//Verifica si existe el archivo
            {
                Prod = new List<Producto>();//caso negativo, "retorna" null
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
                    Prod = JsonSerializer.Deserialize<List<Producto>>(text) ?? new List<Producto>();//se deserealiza los datos y se lo guarda en una lista
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.Message);
                }
            }
        }

        public async Task Guardar()
        {
            string text = JsonSerializer.Serialize(Prod, new JsonSerializerOptions { WriteIndented = true });//Serealiza los datos de la lista prod
            using (StreamWriter sw = File.CreateText(Rute))
            {
                await sw.WriteLineAsync(text);
            }

        }
        public async Task GuardarProd(Producto servs)
        {
            await Cargar();
            Prod.Add(servs);
            await Guardar();//se guarda los datos
        }
        public async Task DescontarProd(List<Producto> Stock)
        {
            await Cargar();
            foreach (Producto p in Stock)
            {
                if (Tipo.Fifo == Prod.Where(s => s.Productos == p.Productos).First().Tipo)
                    Prod.Where(s => s.Productos == p.Productos).First().RestarFifo(p.StockProd);
                else
                    Prod.Where(s => s.Productos == p.Productos).First().RestarProm(p.StockProd);
            }
            await Guardar();
        }
        public async Task AddProd(ProdStock servs, string Nombre)
        {
            await Cargar();
            var p = Prod.Where(q => q.Productos == Nombre).First();
            if (p.Tipo == Tipo.Fifo)
                Prod.Where(q=>q.Productos==Nombre).First().AddFifo(servs);
            else
                Prod.Where(p=>p.Productos==Nombre).First().AddProm(servs);
            await Guardar();
        }
        public async Task EliminarProd(Producto p)
        {

            await Cargar();
            Prod.RemoveAll(q=>q.Equals(p));//se elimina el producto
            await Guardar();//se guarda los cambios
        }
        public async Task<List<Producto>> ObtenerProductos()
        {
            await Cargar();
            return Prod;//retorna todos los productos
        }
        public async Task DescargarProductos()
        {
            string rutas = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string descargar = Path.Combine(rutas, "Productos.xlsx");
            int counter = 2;
            await Cargar();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Productos");
                worksheet.Cell("A1").Value = "Producto";
                worksheet.Cell("B1").Value = "Cantidad";
                worksheet.Cell("C1").Value = "Precio";
                foreach (Producto p in Prod)
                {
                    worksheet.Cell("A" + counter).Value = p.Productos;
                    if (p.Tipo == Tipo.Promedio)
                    {
                        worksheet.Cell("B" + counter).Value = p.StockProd.Cantidad;
                        worksheet.Cell("C" + counter).Value = p.StockProd.Precio;
                        counter++;
                    }
                    else
                    {
                        foreach (ProdStock s in p.StocksFIfo)
                        {
                            worksheet.Cell("B" + counter).Value = s.Cantidad;
                            worksheet.Cell("C" + counter).Value = s.Precio;
                            counter++;
                        }
                    }
                    counter++;//este counter sirve para dejar un espacio entre productos
                }
                workbook.SaveAs(descargar);

            }
        }
    }
}
