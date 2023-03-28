using System.Data;

interface IProvider
{
    IDbConnection dbConnection();
}
