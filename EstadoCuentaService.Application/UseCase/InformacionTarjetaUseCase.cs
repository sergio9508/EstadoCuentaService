using EstadoCuentaService.Application.Interfaces.Tarjeta.Query;
using EstadoCuentaService.Application.UseCase.Interfaces;
using EstadoCuentaService.Domain.Domain;
using EstadoCuentaService.Domain.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application.UseCase
{
    public class InformacionTarjetaUseCase : IInfoTarjetaUseCase
    {
        private readonly ITarjetaQuery _tarjetaQuery;

        public InformacionTarjetaUseCase(ITarjetaQuery tarjetaQuery)
        {
            _tarjetaQuery = tarjetaQuery;
        }

        public async Task<ObjectResponse<InformacionTarjeta>> ObtenerInfoTarjeta(string numeroTarjeta)
        {
            var response = new ObjectResponse<InformacionTarjeta>();

            var item = await _tarjetaQuery.ObtenrtInfomacionTarjeta(numeroTarjeta);

            if (item != null)
            {
                response.code = 1;
                response.message = "Exito";
                response.item = item;
            }
            else
            {
                response.code = 0;
                response.message = "No existe informacion de esa tarjeta";
            }
            return response;
        }
    }
}
