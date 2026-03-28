using System.Collections;
using System.Globalization;
using CsvHelper;

namespace CsvEnumerable;

public class CsvEnumerable<T> : IEnumerable<T>
{
    private readonly IEnumerable<T> _records;
    
    public CsvEnumerable(string csvFilePath)
    {
        using var reader = new StreamReader(csvFilePath);
        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csvReader.GetRecords<T>().ToList();
        _records = records;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _records.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}