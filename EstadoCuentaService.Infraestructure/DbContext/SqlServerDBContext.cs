using Azure.Core.Pipeline;
using EstadoCuentaService.Infraestructure.DbContext.IDbContext;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Infraestructure.DbContext
{
    public class SqlServerDBContext : ISqlServerDBContext
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _dbConnection;
        public SqlServerDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnectionSqlServer()
        {
           
            var connectionString = _configuration.GetConnectionString("SqlServerConnection");
            _dbConnection = new SqlConnection(connectionString);

            return _dbConnection;
        }

    }
}

