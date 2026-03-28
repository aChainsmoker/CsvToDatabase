using CsvToDatabase.Domain.Models;

namespace CsvToDatabase.DataAccess.Abstractions;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetPeopleAsync(CancellationToken cancellationToken = default);
    Task<Person?> GetPersonByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddPersonAsync(Person person, CancellationToken cancellationToken = default);
    Task UpdatePersonAsync(Person person, CancellationToken cancellationToken = default);
    Task DeletePersonAsync(int id, CancellationToken cancellationToken = default);
}