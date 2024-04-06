using EstadoCuentaService.Domain.Domain;
using EstadoCuentaService.Domain.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application.UseCase.Interfaces
{
    public interface IInfoTarjetaUseCase
    {
        Task<ObjectResponse<InformacionTarjeta>> ObtenerInfoTarjeta(string numeroTarjeta);
    }
}
