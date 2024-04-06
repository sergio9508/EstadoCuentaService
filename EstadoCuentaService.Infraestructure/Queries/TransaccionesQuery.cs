using Dapper;
using EstadoCuentaService.Application.Interfaces.Transacciones.Query;
using EstadoCuentaService.Domain.Domain;
using EstadoCuentaService.Domain.Domain.Base;
using EstadoCuentaService.Infraestructure.DbContext.IDbContext;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Infraestructure.Queries
{
    public class TransaccionesQuery : ITransaccionesQuery
    {
        private readonly ISqlServerDBContext _dbContext;
        private readonly ILogger _logger;

        public TransaccionesQuery(ISqlServerDBContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<List<Transaccion>> ObtenerTransacciones(string numeroTarjeta, int Mes)
        {
            var query = "exec ObtenerTransacciones @numeroTarjeta, @Mes";
            var connection = _dbContext.GetConnectionSqlServer();
            var result = await connection.QueryAsync<Transaccion>(query, new { numeroTarjeta, Mes  });
            return result.ToList();
        }

       
    }
}
