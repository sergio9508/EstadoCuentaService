using EstadoCuentaService.Application.Features.Transacciones.Command;
using EstadoCuentaService.Application.Features.Transacciones.Query;
using EstadoCuentaService.Domain.Domain;
using EstadoCuentaService.Domain.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application.UseCase.Interfaces
{
    public interface ITransaccionesUseCase
    {
        Task<ListResponse<Transaccion>> ObtenerTransacciones(ObtenerTransaccionesQuery query);
        Task<GenericResponse> GuardarPago(GuardarPagoCommand command);
        Task<GenericResponse> GuardarCompra(GuardarCompraCommand command);

    }
}
