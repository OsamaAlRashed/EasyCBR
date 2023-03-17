using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text;

namespace EasyCBR.IO;

internal class IOHelpers
{
    internal static List<T> ReadCsvFile<T>(string path, bool hasHeaders = true)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = hasHeaders
        };
        using var reader = new StreamReader(path, Encoding.Default);
        using var csv = new CsvReader(reader, config);

        return csv.GetRecords<T>().ToList();
    }

    internal static void InsertRecordToCsvFile<T>(T newItem, string path, bool hasHeaders = true)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = hasHeaders
        };
        using var writer = new StreamWriter(path, true);
        using var csv = new CsvWriter(writer, config);
        csv.WriteRecord(newItem);
    }
}
