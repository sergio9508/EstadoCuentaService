using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadoCuentaService.Infraestructure.DbContext.IDbContext
{
    public interface ISqlServerDBContext
    {
        IDbConnection GetConnectionSqlServer();
    }
}
