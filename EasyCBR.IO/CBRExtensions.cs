using EasyCBR.Contract.IStage;

namespace EasyCBR.IO;

public static class CBRExtensions
{
    public static CBR<TCase> Create<TCase>(this CBR<TCase> cbr, string path)
        where TCase : class
    {
        CBR<TCase>.Init(cbr, new List<TCase>());
        cbr.FilePath = path;

        cbr.Cases = IOHelpers.ReadCsvFile<TCase>(path);

        return cbr;
    }

    public static IRetainStage<TCase> Retain<TCase>(this CBR<TCase> cbr)
        where TCase : class
    {
        IOHelpers.InsertRecordToCsvFile(cbr.ResultCase.Case, cbr.FilePath);
        cbr.Cases.Add(cbr.ResultCase.Case);

        return cbr;
    }
}
