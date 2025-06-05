using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universe.DAL.ADO.Repositories
{
    public class RepositoryBase
    {
        protected readonly DbConnection _dbConnection;
        public RepositoryBase(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        protected void AddCommandParameters(DbCommand cmd, string name, object? value)
        {
            DbParameter param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(param);
        }
    }
}
