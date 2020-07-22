using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFusionProtocal.Interfaces
{
    public interface IDapper
    {
        int BulkInsert<T>(List<T> list, string destinationTableName);
        int BulkInsert(DataTable datasource, string destinationTableName);
        int BulkUpdate(DataTable datasource, string destinationTableName, params string[] keys);

        IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }
}
