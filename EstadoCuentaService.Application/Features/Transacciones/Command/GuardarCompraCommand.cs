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
    public class GuardarCompraCommand : IRequest<GenericResponse>
    {
        public string NumeroTarjeta { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }

        public class GuardarCompraCommandHandler : IRequestHandler<GuardarCompraCommand, GenericResponse>
        {
            private readonly ITransaccionesUseCase _transaccionesUseCase;
            public GuardarCompraCommandHandler(ITransaccionesUseCase transaccionesUseCase)
            {
                _transaccionesUseCase = transaccionesUseCase;
            }

            public Task<GenericResponse> Handle(GuardarCompraCommand request, CancellationToken cancellationToken)
            {
                return _transaccionesUseCase.GuardarCompra(request);
            }
        }
    }
}
