using Dapper;
using EstadoCuentaService.Application.Interfaces.Transacciones.Command;
using EstadoCuentaService.Domain.Domain;
using EstadoCuentaService.Infraestructure.DbContext.IDbContext;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Infraestructure.Command
{
    internal class TransaccionesCommand : ITransaccionesCommand
    {
        private readonly ISqlServerDBContext _dbContext;
        private readonly ILogger _logger;

        public TransaccionesCommand(ISqlServerDBContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<bool> GuardarCompra(Compras compra)
        {
            var query = "exec GuardarCompra @Monto, @NumeroTarjeta, @Descripcion, @Fecha"; 
            var connection = _dbContext.GetConnectionSqlServer();
            var result = await connection.ExecuteAsync(query, new { compra.Monto, compra.NumeroTarjeta, compra.Descripcion, compra.Fecha});
            return result > 0;

        }

        public async Task<bool> GuardarPago(Pagos pago)
        {
            var query = "exec GuardarPago @Monto, @NumeroTarjeta, @Descripcion, @Fecha";
            var connection = _dbContext.GetConnectionSqlServer();
            var result = await connection.ExecuteAsync(query, new { pago.Monto, pago.NumeroTarjeta, pago.Fecha });
            return result > 0;
        }
    }
}
