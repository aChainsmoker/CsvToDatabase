using System.Globalization;
using CsvEnumerable;
using CsvHelper;
using CsvToDatabase.DataAccess.Abstractions;
using CsvToDatabase.DataAccess.Connections;
using CsvToDatabase.DataAccess.Repositories;
using CsvToDatabase.Domain.Models;
using CustomLogger.Logging;
using LogginProxy;
using Microsoft.Data.Sqlite;

namespace CsvToDatabase;

class Program
{
    async static Task Main(string[] args)
    {
        var connectionString = "Data Source = ..\\..\\..\\..\\database.db";
        var dbConnectionFactory = new SqliteConnectionFactory(connectionString);
        var repository = new PersonRepository(dbConnectionFactory);
        var logger = new Logger([new FileLogger("..\\..\\..\\log.txt")]);
        var loggingProxy = LoggingProxy<IPersonRepository>.CreateInstance(repository, logger);
        
        // var records = new List<Person>
        // {
        //     new Person(1, "James", "Bonk", 40),
        //     new Person(2, "Jerald", "Ponk", 35),
        // };
        // using var writer = new StreamWriter("..//..//..//file.csv");
        // using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
        // csvWriter.WriteRecords(records);
        
        var csvEnumearble = new CsvEnumerable<Person>("..\\..\\..\\file.csv");
        foreach (var el in csvEnumearble)
        {
            await loggingProxy.AddPersonAsync(el);
        }
        
        var people = await loggingProxy.GetPeopleAsync();
        Console.WriteLine($"Number of people: {people.Count()}");
        var firstId = people.First().Id;
        
        
        var person = await loggingProxy.GetPersonByIdAsync(firstId);
        Console.WriteLine($"Person with Id = {firstId}: {person}");

        var updatedPerson = new Person(person.Id, "Updated", "Person", 18);
        await loggingProxy.UpdatePersonAsync(updatedPerson);
        person = await loggingProxy.GetPersonByIdAsync(firstId);
        Console.WriteLine($"Updated Person with Id =  {firstId}: {person}");

        await loggingProxy.DeletePersonAsync(firstId);
        person = await loggingProxy.GetPersonByIdAsync(firstId);
        if (person == null)
        {
            Console.WriteLine($"Person with Id = {firstId} is not found");
        }
    }
}