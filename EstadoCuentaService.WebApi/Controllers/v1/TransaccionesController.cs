using EstadoCuentaService.Application.Features.Transacciones.Command;
using EstadoCuentaService.Application.Features.Transacciones.Query;
using EstadoCuentaService.Domain.Domain;
using EstadoCuentaService.Domain.Domain.Base;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EstadoCuentaService.WebApi.Controllers.v1
{
    public class TransaccionesController : BaseApiController
    {
        private readonly ILogger<TransaccionesController> _log;

        public TransaccionesController(ILogger<TransaccionesController> log)
        {
            _log = log;
        }

        [HttpGet("GetTransacciones")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        [ProducesResponseType(typeof(ListResponse<Transaccion>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransacciones([FromQuery] ObtenerTransaccionesQuery query)
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

        [HttpPost("GuardarPago")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GuardarPago([FromBody] GuardarPagoCommand command)
        {
            try
            {
                _log.LogInformation(JsonSerializer.Serialize(command));
                return Ok(await Mediator.Send(command));
            }

            catch (Exception ex)
            {
                string message = string.Format("Ocurrio un error: {0}", ex.Message);
                _log.LogError(message);
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { code = 0, message = message });
            }
        }

        [HttpPost("GuardarCompra")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GuardarCompra([FromBody] GuardarCompraCommand command)
        {
            try
            {
                _log.LogInformation(JsonSerializer.Serialize(command));
                return Ok(await Mediator.Send(command));
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
