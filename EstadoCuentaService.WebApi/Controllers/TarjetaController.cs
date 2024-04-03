using EstadoCuentaService.Domain.Domain.Base;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuentaService.WebApi.Controllers
{
    
    public class TarjetaController : BaseApiController
    {
        private readonly ILogger<TarjetaController> _logger;

        public TarjetaController(ILogger<TarjetaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetName()
        {
            try
            {
                _logger.LogInformation("Request GetName");
                return Ok(await Task.Run(() => new GenericResponse
                {
                    code = 0,
                    message = "Hola"
                }));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
