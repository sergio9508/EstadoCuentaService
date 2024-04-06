using AutoMapper;
using EstadoCuentaService.Application.Mapping;
using EstadoCuentaService.Application.UseCase;
using EstadoCuentaService.Application.UseCase.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient<ITransaccionesUseCase, TransaccionesUseCase>();
            services.AddTransient<IInfoTarjetaUseCase, InformacionTarjetaUseCase>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationMapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
