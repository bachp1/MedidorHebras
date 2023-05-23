using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroModel.DTO
{
    public class Mensaje
    {
        private int numeroMedidor;
        private DateTime fecha;
        private decimal valorConsumo;

        public int NumeroMedidor { get => numeroMedidor; set => numeroMedidor = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public decimal ValorConsumo { get => valorConsumo; set => valorConsumo = value; }

        public override string ToString()
        {
            return NumeroMedidor + "|" + Fecha + "|" + ValorConsumo;
        }
    }
}
