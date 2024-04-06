using Dapper;
using EstadoCuentaService.Application.Interfaces.Tarjeta.Query;
using EstadoCuentaService.Domain.Domain;
using EstadoCuentaService.Infraestructure.DbContext.IDbContext;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Infraestructure.Queries
{
    public class TarjetaQuery : ITarjetaQuery
    {
        private readonly ISqlServerDBContext _dbContext;
        private readonly ILogger _logger;

        public TarjetaQuery(ISqlServerDBContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<InformacionTarjeta>> ObtenerInformacionTarjetas()
        {
            var query = "select NumeroTarjeta, Nombre, SaldoActual, Limite, SaldoDisponible from DatosTarjeta";
            var connection = _dbContext.GetConnectionSqlServer();
            var result = await connection.QueryAsync<InformacionTarjeta>(query);
            return result.ToList();
        }

        public async Task<InformacionTarjeta> ObtenrtInfomacionTarjeta(string NumeroTarjeta)
        {
            var query = "select NumeroTarjeta, Nombre, SaldoActual, Limite, SaldoDisponible from DatosTarjeta where NumeroTarjeta = @NumeroTarjeta";
            var connection = _dbContext.GetConnectionSqlServer();
            var result = await connection.QueryFirstOrDefaultAsync<InformacionTarjeta>(query, new { NumeroTarjeta });
            return result;
        }
    }
}
