namespace CsvToDatabase.Domain.Models;

public class Person
{
    public int Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public int Age { get; init; }
    public Person(int id, string firstName, string lastName, int age)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }
    public Person()
    {
        
    }

    public override string ToString()
    {
        return "{ " +$"Id: {Id}, FirstName: {FirstName}, LastName: {LastName}, Age: {Age}" + " }";
    }
}
