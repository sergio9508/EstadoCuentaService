using EstadoCuentaService.Domain.Domain.Base;
using System.Net;
using System.Text.Json;

namespace EstadoCuentaService.WebApi.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(Exception e)
            {
                await HandlerException(context , e);
            }
        }

        private async Task HandlerException(HttpContext context, Exception e)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var responseGeneric = new GenericResponse
            {
                code = 0,
                message = $"Ocurrio un error {e.Message}"
            };

            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var message = $"Error {e.Message}, innerException: {e.InnerException}, servicesName: {e.TargetSite.ReflectedType.FullName}.{e.TargetSite.Name}"; 

            _logger.LogError(message);

            var result = JsonSerializer.Serialize(responseGeneric);
            await response.WriteAsync(result);
        }
    }
}
