using MySql.Data.MySqlClient;
using System.Data;

class MySQLProvider : IProvider
{
    public string ConnectionString { get; set; }
    public MySQLProvider(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public IDbConnection dbConnection()
    {
        return new MySqlConnection(ConnectionString);
    }
}