using EstadoCuentaService.Application.Interfaces.Tarjeta.Query;
using EstadoCuentaService.Application.Interfaces.Transacciones.Command;
using EstadoCuentaService.Application.Interfaces.Transacciones.Query;
using EstadoCuentaService.Infraestructure.Command;
using EstadoCuentaService.Infraestructure.Queries;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Infraestructure
{
    public static class DependencyInjection
    {
        public static void AddInfraestructure(this IServiceCollection services)
        {
            services.AddSingleton(Log.Logger);
            services.AddTransient<ITarjetaQuery, TarjetaQuery>();
            services.AddTransient<ITransaccionesCommand, TransaccionesCommand>();
            services.AddTransient<ITransaccionesQuery, TransaccionesQuery>();

        }
    }
}
