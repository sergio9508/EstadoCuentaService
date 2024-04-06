using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Domain.Domain
{
    public class InformacionTarjeta
    {
        public string NumeroTarjeta { get; set; }
        public string Nombre {  get; set; }
        public decimal SaldoActual { get; set; }
        public decimal Limite {  get; set; }
        public decimal SaldoDisponible { get; set; }
    }
}
