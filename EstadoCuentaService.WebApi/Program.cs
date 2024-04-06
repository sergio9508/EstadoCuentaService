using EstadoCuentaService.Application;
using EstadoCuentaService.Infraestructure;
using EstadoCuentaService.WebApi.Middleware;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Events;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpClient();
builder.Services.AddHealthChecks();

builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));


builder.Services.AddApplication();
builder.Services.AddInfraestructure();

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("AllowCors",
        builder =>
        {
            builder.AllowAnyOrigin().WithMethods(
                       HttpMethod.Get.Method,
                       HttpMethod.Put.Method,
                       HttpMethod.Post.Method,
                       HttpMethod.Delete.Method).AllowAnyHeader();
        });
    }
    );

#region Serilog
builder.Host.UseSerilog(
    (context, services, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    );

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();
app.UseCors("AllowCors");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.UseHealthChecks("/healthh", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();
