using EasyCBR.Abstractions;

namespace EasyCBR.IO;

public static class CBRExtensions
{
    public static CBR<TCase, TOutput> Create<TCase, TOutput>(this CBR<TCase, TOutput> cbr, string path)
        where TCase : class
    {
        CBR<TCase, TOutput>.Init(cbr, new List<TCase>());
        cbr.FilePath = path;

        cbr.Cases = IOHelpers.ReadCsvFile<TCase>(path);

        return cbr;
    }

    public static IRetainStage<TCase> Retain<TCase, TOutput>(this CBR<TCase, TOutput> cbr)
        where TCase : class
    {
        IOHelpers.InsertRecordToCsvFile(cbr.ResultCase.Case, cbr.FilePath);
        cbr.Cases.Add(cbr.ResultCase.Case);

        return cbr;
    }
}
