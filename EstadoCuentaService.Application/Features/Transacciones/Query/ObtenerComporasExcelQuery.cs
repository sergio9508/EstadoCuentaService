using EstadoCuentaService.Application.UseCase.Interfaces;
using EstadoCuentaService.Domain.Domain.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application.Features.Transacciones.Query
{
    public class ObtenerComporasExcelQuery: IRequest<ObjectResponse<byte[]>>
    {
        public string numeroTarjeta { get; set; }
        public class ObtenerComporasExcelQueryHandler : IRequestHandler<ObtenerComporasExcelQuery, ObjectResponse<byte[]>>
        {
            private readonly ITransaccionesUseCase _transaccionesUseCase;
            public ObtenerComporasExcelQueryHandler(ITransaccionesUseCase transaccionesUseCase)
            {
                _transaccionesUseCase = transaccionesUseCase;
            }

            public Task<ObjectResponse<byte[]>> Handle(ObtenerComporasExcelQuery request, CancellationToken cancellationToken)
            {
                return _transaccionesUseCase.ObtenerCompras(request.numeroTarjeta);
            }
        }
    }
}
