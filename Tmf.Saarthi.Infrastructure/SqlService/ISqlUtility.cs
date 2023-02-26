using Microsoft.Data.SqlClient;
using System.Data;

namespace Tmf.Saarthi.Infrastructure.SqlService;

public interface ISqlUtility
{
    Task<DataTable> ExecuteCommandAsync(string connectionName, string storedProcName, List<SqlParameter> procParameters = null);

    Task<DataSet> ExecuteMultipleCommandAsync(string connectionName, string storedProcName, List<SqlParameter> procParameters = null);
}
