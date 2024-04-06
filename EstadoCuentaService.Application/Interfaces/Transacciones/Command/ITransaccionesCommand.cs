using EstadoCuentaService.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application.Interfaces.Transacciones.Command
{
    public interface ITransaccionesCommand
    {
        Task<bool> GuardarPago(Pagos pago);
        Task<bool> GuardarCompra(Compras compra);
    }
}
