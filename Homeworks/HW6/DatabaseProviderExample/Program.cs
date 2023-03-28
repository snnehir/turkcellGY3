using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Data;

string connectionString = "Server=localhost;Port=5432;Database=test_db;User Id=postgis;Password=password;";
PostgreSQLProvider postgresqlProvider = new PostgreSQLProvider(connectionString);
DatabaseHelper databaseHelper = new DatabaseHelper(postgresqlProvider);
databaseHelper.OpenConnection();
databaseHelper.CloseConnection();
