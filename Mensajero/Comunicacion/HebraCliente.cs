using MensajeroModel.DAL;
using MensajeroModel.DTO;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensajero.Comunicacion
{
    public class HebraCliente
    {
        private ClienteCom clienteCom;
        private MensajesDal mensajesDal = MensajesDalArchivo.GetInstancia();

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }


        public void ejecutar()
        {
            {
                clienteCom.Escribir("Bienvenido al lector");
                clienteCom.Escribir("Ingrese el numero de medidor: ");
                int numeroMedidor = Convert.ToInt32(clienteCom.Leer());
                clienteCom.Escribir("Ingrese la fecha(AAAA-MM-DD HH-MM-SS): ");
                DateTime fecha = Convert.ToDateTime(clienteCom.Leer());
                clienteCom.Escribir("Ingrese el valor de consumo(kw/h): ");
                decimal valorConsumo = Convert.ToDecimal(clienteCom.Leer());
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

                clienteCom.Desconectar();
            }
        }
    }
}
