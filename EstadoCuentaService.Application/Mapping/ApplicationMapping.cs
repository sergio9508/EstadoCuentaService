using AutoMapper;
using EstadoCuentaService.Application.Features.Transacciones.Command;
using EstadoCuentaService.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application.Mapping
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping() {
            Mapping();
        }

        private void Mapping()
        {
            CreateMap<GuardarPagoCommand, Pagos>();
            CreateMap<GuardarCompraCommand, Compras>();
        }
    }
}
