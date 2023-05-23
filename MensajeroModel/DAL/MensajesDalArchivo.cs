using MensajeroModel.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroModel.DAL
{
 //despues del nombre de la clase escribir : y el mensaje de la interfaz para implementarla en esta clase, y
 //control + . para abrir el menu de la implementacion
    public class MensajesDalArchivo : MensajesDal
    {
        
        //implementaremos singleton
        //1ro constructor debe ser private

        private MensajesDalArchivo() { }

        //2do debe poseer un atributo del mismo tipo de la clase y ademas estatico

        private static MensajesDalArchivo instancia;

        //3ro debe tener un metodo getIntance, que devuelva una referencia al atributo

        public static MensajesDal GetInstancia()
        {
            if (instancia == null)
            {
                instancia=new MensajesDalArchivo();
            }


            return instancia;
        }
          //agregar referencia de mensajeroModel a las referencias de Mensajero
        //como vamos a hacer para que 2 hebras no accedan a la vez a este archivo?
        
        
        
        
        private static string url=Directory.GetCurrentDirectory(); //esto me trae la ruta del proyecto
        private static string archivo = url + "/mensajes.txt";


        public void AgregarMensaje(Mensaje mensaje)
        {
            try
            {
                using(StreamWriter write=new StreamWriter(archivo, true))
                {
                    write.WriteLine(mensaje.NumeroMedidor +"|"+ mensaje.Fecha + "|" + mensaje.ValorConsumo);
                    write.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<Mensaje> ObtenerMensajes()
        {
            List<Mensaje> lista= new List<Mensaje>();
            try
            {
                using (StreamReader read = new StreamReader(archivo))
                {
                    string texto="";

                    do
                    {
                        texto = read.ReadLine();
                        //con lo siguiente cazamos si el texto viene vacio
                        if(texto != null)
                        {
                            string[] arr = texto.Trim().Split('|'); //el split nos omite el ; que estabamos usando anteriormente para separar los datos del texto
                            
                            Mensaje mensaje = new Mensaje()
                            {
                                NumeroMedidor = Convert.ToInt32(arr[0]),
                                Fecha = Convert.ToDateTime(arr[1]),
                                ValorConsumo = Convert.ToDecimal(arr[2])
                            };
                            lista.Add(mensaje);
                        }

                    } while (texto != null);
                }

            }catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }
    }
}
