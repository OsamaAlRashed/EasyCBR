using BenchmarkDotNet.Attributes;
using EasyCBR.Contract.IStage;
using EasyCBR.Enums;
using EasyCBR.Helpers;
using EasyCBR.SimilarityFunctions;

namespace EasyCBR.Benchmark;

[MemoryDiagnoser(false)]
public class Benchmark
{
    #region Setup
    //[Params( 100)]
    //public int ListSize { get; set; }

    //[GlobalSetup]
    //public void GlobalSetup()
    //{
    //    _laptops = Enumerable.Range(1, ListSize)
    //        .Select(x => new Laptop("Fake Name", Manufacture.Dell, 16, "I5_G5", false, 0))
    //        .ToList();
    //}

    private static List<Laptop> _laptops = Enumerable.Range(1, 1000_000)
            .Select(x => new Laptop("Fake Name", Manufacture.Dell, 16, "I5_G5", false, 0))
            .ToList();

    private readonly static Func<string, string, double> cpuSimilarity = (value, queryValue) =>
    {
        double result = 0;
        var values = value.Split('_');
        var queryValues = queryValue.Split('_');
        if (values[0] == queryValues[0])
        {
            result += 0.5;
        }

        if (values[1] == queryValues[1])
        {
            result += 0.5;
        }

        return result;
    };

    private readonly IRetriveStage<Laptop, decimal> laptopsCBR = CBR<Laptop, decimal>
            .Create(_laptops)
            .Output(order => order.Price)
            .SetSimilarityFunctions
            (
                (nameof(Laptop.Manufacture), new TableSimilarityFunction<Manufacture>()),
                (nameof(Laptop.RAM), new LinearSimilarityFunction<int>(4, 32, 2)),
                (nameof(Laptop.SSD), new BasicSimilarityFunction<bool>(2)),
                (nameof(Laptop.CPU), new CustomSimilarityFunction<string>(cpuSimilarity, 4))
            )
            .Retrieve(new Laptop("ModelX", Manufacture.Asus, 32, "I5_G11", true, 0), 5);
    #endregion

    [Benchmark]
    public void WhenSelectTypeIsAverageValue()
    {
        laptopsCBR
            .Reuse(SelectType.AverageValue)
            .Revise()
            .Retain()
            .Run();
    }

    [Benchmark]
    public void WhenSelectTypeIsMinValue()
    {
        laptopsCBR
            .Reuse(SelectType.MinValue)
            .Revise()
            .Retain()
            .Run();
    }

    [Benchmark]
    public void WhenSelectTypeIsMaxValue()
    {
        laptopsCBR
            .Reuse(SelectType.MaxValue)
            .Revise()
            .Retain()
            .Run();
    }

    [Benchmark]
    public void WhenSelectTypeIsMaxSimilarity()
    {
        laptopsCBR
            .Reuse(SelectType.MaxSimilarity)
            .Revise()
            .Retain()
            .Run();
    }

    [Benchmark]
    public void WhenSelectTypeIsMinSimilarity()
    {
        laptopsCBR
            .Reuse(SelectType.MinSimilarity)
            .Revise()
            .Retain()
            .Run();
    }

    [Benchmark]
    public void WhenSelectTypeIsAverageSimilarity()
    {
        laptopsCBR
            .Reuse(SelectType.AverageSimilarity)
            .Revise()
            .Retain()
            .Run();
    }

}
