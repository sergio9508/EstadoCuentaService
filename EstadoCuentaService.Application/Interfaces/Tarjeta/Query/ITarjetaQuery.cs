using EstadoCuentaService.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application.Interfaces.Tarjeta.Query
{
    public interface ITarjetaQuery
    {
        Task<List<InformacionTarjeta>> ObtenerInformacionTarjetas();
        Task<InformacionTarjeta> ObtenrtInfomacionTarjeta(string NumeroTarjeta);
    }
}
