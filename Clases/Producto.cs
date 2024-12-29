using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionLaura.Clases
{
    public enum Tipo { Promedio, Fifo }
    /*
        el tipo refiere a como se calcula el nuevo precio
        segun su ingreso
        promedio en caso de que sea PPP (segun contavilidad)
        Fifo caso first in first out(Estilo pilas)
    */
    public class Producto
    {
        public string Productos { get; set; }//nombre del producto
        public Tipo Tipo { get; set; }//como se consume el producto
        public List<ProdStock> StocksFIfo { get; set; }///lista de productos en caso de que sea first in first out

        public ProdStock StockProd { get; set; }//caso de que se promedien los 

        public Producto()//constructor en blanco
        {
            Productos = "";
            StocksFIfo = new List<ProdStock>();
            StockProd = new ProdStock();
        }
        public Producto(string Nombre, Tipo tipos, ProdStock prodStock)
        {//constructor con datos
            this.Productos = Nombre;
            this.Tipo = tipos;
            this.StockProd = new ProdStock();
            this.StocksFIfo = new List<ProdStock>();
            if (tipos == Tipo.Fifo)
            {
                StocksFIfo = new List<ProdStock>();
                StocksFIfo.Add(prodStock);
            }
            else
            {
                StockProd = prodStock;
            }
        }
        public bool RestarProm(ProdStock consumir)
        {//se consume el producto ingresado por parametro
            bool result;
            if (this.StockProd.Cantidad < consumir.Cantidad)//se calcula si se puede consumir
            {
                result = false;//caso negativo retorna falso
            }
            else
            {
                this.StockProd.Cantidad = this.StockProd.Cantidad - consumir.Cantidad;
                result = true;//caso positivo retorna verdadero
            }

            return result;
        }
        public bool RestarFifo(ProdStock consumir)
        {
            bool result = true;
            int cantidad = this.StocksFIfo.Count;//Se que no es el metodo mas eficiente pero estoy bloqueado y solo se me ocurre este
            int contador = 0;
            while (contador < cantidad)
            {
                if (this.StocksFIfo[contador].Cantidad > consumir.Cantidad)
                    this.StocksFIfo[contador].Cantidad = -consumir.Cantidad;
                else
                {
                    if (this.StocksFIfo[(contador + 1)] == null)
                    {
                        result = false;
                    }
                }
                contador++;
            }
            return result;
        }
        public void AddFifo(ProdStock newProd)
        {
            this.StocksFIfo.Add(newProd);//se añade el nuevo producto en el final de la lista en el caso fifo
        }
        public void AddProm(ProdStock newStock)
        {// se agrega el nuevo producto en el caso promedio
            this.StockProd.Cantidad = this.StockProd.Cantidad+newStock.Cantidad;
            this.StockProd.Precio = (newStock.Precio + this.StockProd.Precio) / 2;
        }
    }
}
