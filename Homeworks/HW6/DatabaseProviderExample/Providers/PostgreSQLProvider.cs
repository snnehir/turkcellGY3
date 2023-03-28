using Npgsql;
using System.Data;

class PostgreSQLProvider : IProvider
{
    public string ConnectionString { get; set; }
    public PostgreSQLProvider(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public IDbConnection dbConnection()
    {
        return new NpgsqlConnection(ConnectionString);
    }
}