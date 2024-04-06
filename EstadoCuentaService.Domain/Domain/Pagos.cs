using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Domain.Domain
{
    public class Pagos
    {
        public string NumeroTarjeta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
    }
}
