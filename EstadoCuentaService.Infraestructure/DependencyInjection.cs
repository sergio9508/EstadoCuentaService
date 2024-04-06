using EstadoCuentaService.Application.Interfaces.Tarjeta.Query;
using EstadoCuentaService.Application.Interfaces.Transacciones.Command;
using EstadoCuentaService.Application.Interfaces.Transacciones.Query;
using EstadoCuentaService.Infraestructure.Command;
using EstadoCuentaService.Infraestructure.DbContext;
using EstadoCuentaService.Infraestructure.DbContext.IDbContext;
using EstadoCuentaService.Infraestructure.Queries;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Infraestructure
{
    public static class DependencyInjection
    {
        public static void AddInfraestructure(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddSingleton(Log.Logger);
            services.AddTransient<ISqlServerDBContext, SqlServerDBContext>();
            services.AddTransient<ITarjetaQuery, TarjetaQuery>();
            services.AddTransient<ITransaccionesCommand, TransaccionesCommand>();
            services.AddTransient<ITransaccionesQuery, TransaccionesQuery>();

        }
    }
}
