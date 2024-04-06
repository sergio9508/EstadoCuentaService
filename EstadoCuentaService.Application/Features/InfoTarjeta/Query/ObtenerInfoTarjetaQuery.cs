using EstadoCuentaService.Application.UseCase.Interfaces;
using EstadoCuentaService.Domain.Domain;
using EstadoCuentaService.Domain.Domain.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application.Features.InfoTarjeta.Query
{
    public class ObtenerInfoTarjetaQuery: IRequest<ObjectResponse<InformacionTarjeta>>
    {
        public string numeroTarjeta { get; set; }

        public class ObtenerInfoTarjetaQueryhandler : IRequestHandler<ObtenerInfoTarjetaQuery, ObjectResponse<InformacionTarjeta>>
        {
            private readonly IInfoTarjetaUseCase _infoTarjetaUseCase;
            public ObtenerInfoTarjetaQueryhandler(IInfoTarjetaUseCase infoTarjetaUseCase)
            {
                _infoTarjetaUseCase = infoTarjetaUseCase;
            }

            public Task<ObjectResponse<InformacionTarjeta>> Handle(ObtenerInfoTarjetaQuery request, CancellationToken cancellationToken)
            {
                return _infoTarjetaUseCase.ObtenerInfoTarjeta(request.numeroTarjeta);
            }
        }
    }
}
