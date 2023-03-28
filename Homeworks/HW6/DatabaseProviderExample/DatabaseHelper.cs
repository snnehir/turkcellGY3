using System.Data;

class DatabaseHelper
{
    // We are not dependent on specific Database Provider.
    private readonly IProvider DatabaseProvider;
    private IDbConnection db;
    public DatabaseHelper(IProvider databaseProvider)
    {
        DatabaseProvider = databaseProvider;
        db = DatabaseProvider.dbConnection();
    }
    public void OpenConnection()
    {
        try
        {
            db.Open();
            Console.WriteLine($"Connected via {DatabaseProvider.GetType()}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect: {ex.Message}");
        }
    }
    public void CloseConnection()
    {
        db.Close();
        Console.WriteLine("Connection closed.");
    }
}