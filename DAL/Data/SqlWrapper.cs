namespace MrDAL.Data;

public class SqlWrapper
{
    public SqlWrapper()
    {
    }

    public SqlWrapper(string dbConnection)
    {
        ConnectionString = dbConnection;
    }

    public string ConnectionString { get; set; }
}