using EstadoCuentaService.Application.UseCase.Interfaces;
using EstadoCuentaService.Domain.Domain;
using EstadoCuentaService.Domain.Domain.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application.Features.Transacciones.Query
{
    public class ObtenerTransaccionesQuery : IRequest<ListResponse<Transaccion>>   
    {
        public string NumeroTarjeta { get; set; }
        public int Mes { get; set; }

        public class ObtenerTransaccionesQueryHandler : IRequestHandler<ObtenerTransaccionesQuery, ListResponse<Transaccion>>
        {
            private readonly ITransaccionesUseCase _transaccionesUseCase;

            public ObtenerTransaccionesQueryHandler(ITransaccionesUseCase transaccionesUseCase)
            {
                _transaccionesUseCase = transaccionesUseCase;
            }

            public Task<ListResponse<Transaccion>> Handle(ObtenerTransaccionesQuery request, CancellationToken cancellationToken)
            {
                return _transaccionesUseCase.ObtenerTransacciones(request);
            }
        }
    }
}
