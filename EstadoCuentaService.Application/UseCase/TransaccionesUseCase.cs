using AutoMapper;
using EstadoCuentaService.Application.Features.Transacciones.Command;
using EstadoCuentaService.Application.Features.Transacciones.Query;
using EstadoCuentaService.Application.Interfaces.Transacciones.Command;
using EstadoCuentaService.Application.Interfaces.Transacciones.Query;
using EstadoCuentaService.Application.UseCase.Interfaces;
using EstadoCuentaService.Domain.Domain;
using EstadoCuentaService.Domain.Domain.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application.UseCase
{
    public class TransaccionesUseCase : ITransaccionesUseCase
    {
        private readonly ITransaccionesQuery _transaccionesQuery;
        private readonly ITransaccionesCommand _transaccionesCommand;
        private readonly IMapper _mapper;
        public TransaccionesUseCase(ITransaccionesCommand transaccionesCommand, ITransaccionesQuery transaccionesQuery, IMapper mapper)
        {
            _transaccionesCommand = transaccionesCommand;
            _transaccionesQuery = transaccionesQuery;
            _mapper = mapper;
        }

        public async Task<GenericResponse> GuardarCompra(GuardarCompraCommand command)
        {
            var response = new GenericResponse();

            var compra = _mapper.Map<Compras>(command);

            var items = await _transaccionesCommand.GuardarCompra(compra);

            if (items)
            {
                response.code = 1;
                response.message = "Exito";
            }
            else
            {
                response.code = 0;
                response.message = "Ocurrio un error al procesar el pago";
            }

            return response;
        }

        public async Task<GenericResponse> GuardarPago(GuardarPagoCommand command)
        {
            var response = new GenericResponse();

            var pago = _mapper.Map<Pagos>(command);

            var items = await _transaccionesCommand.GuardarPago(pago);

            if (items)
            {
                response.code = 1;
                response.message = "Exito";
            }
            else
            {
                response.code = 0;
                response.message = "Ocurrio un error al procesar el pago";
            }

            return response;
        }

        public async Task<ListResponse<Transaccion>> ObtenerTransacciones(ObtenerTransaccionesQuery query)
        {
            var response = new ListResponse<Transaccion>();

            var items = await _transaccionesQuery.ObtenerTransacciones(query.NumeroTarjeta, query.Mes);

            if (items.Count > 0)
            {
                response.code = 1;
                response.message = "Exito";
                response.items = items;
            }
            else
            {
                response.code = 0;
                response.message = "No existen Transacciones";
            }

            return response;
        }

    }
}
