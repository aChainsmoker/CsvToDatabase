using System.Data;
using CsvToDatabase.DataAccess.Abstractions;
using Microsoft.Data.Sqlite;

namespace CsvToDatabase.DataAccess.Connections;

public class SqliteConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqliteConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        
        return connection;
    }
}