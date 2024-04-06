using EstadoCuentaService.Application.UseCase.Interfaces;
using EstadoCuentaService.Domain.Domain.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application.Features.Transacciones.Command
{
    public class GuardarPagoCommand : IRequest<GenericResponse>
    {
        public string NumeroTarjeta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }

        public class GuardarPagoCommandHandler : IRequestHandler<GuardarPagoCommand, GenericResponse>
        {
            private readonly ITransaccionesUseCase _transaccionesUseCase;
            public GuardarPagoCommandHandler(ITransaccionesUseCase transaccionesUseCase)
            {
                _transaccionesUseCase = transaccionesUseCase;
            }

            public Task<GenericResponse> Handle(GuardarPagoCommand request, CancellationToken cancellationToken)
            {
                return _transaccionesUseCase.GuardarPago(request);
            }
        }
    }
}
