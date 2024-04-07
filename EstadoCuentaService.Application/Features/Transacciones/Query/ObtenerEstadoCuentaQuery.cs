using EstadoCuentaService.Application.UseCase.Interfaces;
using EstadoCuentaService.Domain.Domain.Base;
using EstadoCuentaService.Domain.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application.Features.Transacciones.Query
{
    public class ObtenerEstadoCuentaQuery : IRequest<ObjectResponse<byte[]>>
    {
        public string NumeroTarjeta { get; set; }
        public int Mes { get; set; }

        public class ObtenerEstadoCuentaQueryHandler : IRequestHandler<ObtenerEstadoCuentaQuery, ObjectResponse<byte[]>>
        {
            private readonly ITransaccionesUseCase _transaccionesUseCase;

            public ObtenerEstadoCuentaQueryHandler(ITransaccionesUseCase transaccionesUseCase)
            {
                _transaccionesUseCase = transaccionesUseCase;
            }

            public Task<ObjectResponse<byte[]>> Handle(ObtenerEstadoCuentaQuery request, CancellationToken cancellationToken)
            {
                return _transaccionesUseCase.ObtenerEstadoCuenta(request);
            }
        }
    }
}
