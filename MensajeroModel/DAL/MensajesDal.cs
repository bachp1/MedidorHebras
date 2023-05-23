using MensajeroModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//esta clase es una interfaz

namespace MensajeroModel.DAL
{
    public interface MensajesDal
    {
        void AgregarMensaje(Mensaje mensaje);
        List<Mensaje> ObtenerMensajes();
    }
}
