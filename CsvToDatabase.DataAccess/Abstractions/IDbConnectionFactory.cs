using System.Data;

namespace CsvToDatabase.DataAccess.Abstractions;

public interface IDbConnectionFactory
{
    Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken = default);
}