using EstadoCuentaService.Application.Features.Transacciones.Query;
using EstadoCuentaService.Domain.Domain.Base;
using EstadoCuentaService.Domain.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json;
using EstadoCuentaService.Application.Features.InfoTarjeta.Query;

namespace EstadoCuentaService.WebApi.Controllers.v1
{
    public class TarjetaController : BaseApiController
    {
        private readonly ILogger<TarjetaController> _log;

        public TarjetaController(ILogger<TarjetaController> log)
        {
            _log = log;
        }
        [HttpGet("GetInfoTarjeta")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        [ProducesResponseType(typeof(ObjectResponse<InformacionTarjeta>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInfoTarjeta([FromQuery] ObtenerInfoTarjetaQuery query)
        {
            try
            {
                _log.LogInformation(JsonSerializer.Serialize(query));
                return Ok(await Mediator.Send(query));
            }

            catch (Exception ex)
            {
                string message = string.Format("Ocurrio un error: {0}", ex.Message);
                _log.LogError(message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { code = 0, message = message });
            }
        }
    }
}
