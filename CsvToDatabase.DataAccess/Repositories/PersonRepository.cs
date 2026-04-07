using CsvToDatabase.DataAccess.Abstractions;
using CsvToDatabase.Domain.Models;
using Dapper;

namespace CsvToDatabase.DataAccess.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public PersonRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    public async Task<IEnumerable<Person>> GetPeopleAsync(CancellationToken cancellationToken = default)
    {
        using var dbConnection = await _dbConnectionFactory.GetConnectionAsync(cancellationToken);

        return await dbConnection.QueryAsync<Person>("select * from People");
    }

    public async Task<Person?> GetPersonByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        using var dbConnection = await _dbConnectionFactory.GetConnectionAsync(cancellationToken);
        
        var person = await dbConnection.QuerySingleOrDefaultAsync<Person>($"select * from People where Id = @Id limit 1", new {Id = id});
        
        return person;
    }

    public async Task AddPersonAsync(Person person, CancellationToken cancellationToken = default)
    {
        using var dbConnection = await _dbConnectionFactory.GetConnectionAsync(cancellationToken);
        
        await dbConnection.ExecuteAsync("insert into People (FirstName, LastName, Age) values (@FirstName, @LastName, @Age)", person);
    }

    public async Task UpdatePersonAsync(Person person, CancellationToken cancellationToken = default)
    {
        using var dbConnection = await _dbConnectionFactory.GetConnectionAsync(cancellationToken);
        
        await dbConnection.ExecuteAsync("update People set FirstName=@FirstName, LastName=@LastName, Age=@Age where Id=@Id", person);
    }

    public async Task DeletePersonAsync(int id, CancellationToken cancellationToken = default)
    {
        using var dbConnection = await _dbConnectionFactory.GetConnectionAsync(cancellationToken);
        
        await dbConnection.ExecuteAsync("delete from People where Id = @Id", new {Id = id});
    }
}