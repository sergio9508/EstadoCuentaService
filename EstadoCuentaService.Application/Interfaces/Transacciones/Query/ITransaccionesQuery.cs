using EstadoCuentaService.Domain.Domain.Base;
using EstadoCuentaService.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstadoCuentaService.Application.Features.Transacciones.Query;

namespace EstadoCuentaService.Application.Interfaces.Transacciones.Query
{
    public interface ITransaccionesQuery
    {
        Task<List<Transaccion>> ObtenerTransacciones(string numeroTarjeta, int Mes);
        Task<List<Compras>> ObtenerCompras(string numeroTarjeta);
    }
}
