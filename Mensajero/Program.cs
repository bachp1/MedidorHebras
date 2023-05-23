using Mensajero.Comunicacion;
using MensajeroModel.DAL;
using MensajeroModel.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;

using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mensajero
{
    public class Program
    {
        
        private static MensajesDal mensajesDal = MensajesDalArchivo.GetInstancia();

        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("------------------REGISTRO DE MEDIDORES-------------------");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Hola buenas tardes, seleccione una opcion del menu: ");
            Console.WriteLine("1.- Ingresar un Medidor");
            Console.WriteLine("2.- Mostrar");
            Console.WriteLine("0.- Salir");
            Console.WriteLine("----------------------------------------------------------");

            switch (Console.ReadLine().Trim())
            {
                case "1": Ingresar();
                    break;
                case "2": Mostrar();
                    break;
                case "0": continuar= false; 
                    break;
                default: Console.WriteLine("Ingrese datos validos");
                    break;
            }
            return continuar;
        }


        

        static void Main(string[] args)
        {
            HebraServidor hebra= new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();
            
            while (Menu()) ;
        }

        static void Ingresar()
        {

            Console.WriteLine("Ingrese el numero del medidor: ");
            int numeroMedidor=Convert.ToInt32(Console.ReadLine().Trim());

            Console.WriteLine("Ingrese la fecha seguida de la hora en el formato sugerido (aaaa-MM-DD HH-MM-SS): ");
            Console.WriteLine("Favor de no usar simbolos y guion");
            DateTime fecha=Convert.ToDateTime(Console.ReadLine().Trim());

            Console.WriteLine("Proceda a ingresar el consumo de Kw/H: ");
            decimal valorConsumo = Convert.ToDecimal(Console.ReadLine().Trim());

            Mensaje mensaje = new Mensaje()
            {
                NumeroMedidor = numeroMedidor,
                Fecha = fecha,
                ValorConsumo = valorConsumo
            };

            lock (mensajesDal)
            {
                mensajesDal.AgregarMensaje(mensaje);
            }
        }

        static void Mostrar()
        {
            List<Mensaje> mensajes = null;

            lock(mensajesDal)
            {
                mensajes = mensajesDal.ObtenerMensajes();
            }

            foreach(Mensaje mensaje in mensajes)
            {
                Console.WriteLine(mensaje);
            }
        }

}
}
