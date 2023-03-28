using Microsoft.Data.SqlClient;
using System.Data;

class MsSQLProvider : IProvider
{
    public string ConnectionString { get; set; }
    public MsSQLProvider(string connectionString)
    {
        ConnectionString = connectionString;
    }
    public IDbConnection dbConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}