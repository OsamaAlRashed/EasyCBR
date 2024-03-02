using EasyCBR;
using EasyCBR.Enums;
using EasyCBR.Helpers;
using EasyCBR.Models;
using EasyCBR.SimilarityFunctions;
using System.Net.Http.Headers;

var laptopList = new List<Laptop>()
{
   new ("ModelX1", Manufacture.Asus, 4, "I3_G5", false, 200),
   new ("ModelX2", Manufacture.Dell, 4, "I3_G6", false, 220),
   new ("ModelX3", Manufacture.Asus, 4, "I3_G7", false, 250),
   new ("ModelX4", Manufacture.Hp, 8, "I5_G8", false, 300),
   new ("ModelX5", Manufacture.Lenovo, 8, "I5_G8", false, 280),
   new ("ModelX6", Manufacture.Dell, 16, "I5_G11", true, 500),
   new ("ModelX7", Manufacture.Lenovo, 12, "I5_G10", false, 400),
   new ("ModelX8", Manufacture.Hp, 16, "I5_G12", true, 600),
   new ("ModelX9", Manufacture.Lenovo, 32, "I5_G12", true, 650),
   new ("ModelX10", Manufacture.Asus, 16, "I5_G11", true, 450),
};

Func<string, string, double> cpuSimilarity = (value, queryValue) =>
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

Table<Manufacture> _manufactureSimilarity = new()
{
    [Manufacture.Dell, Manufacture.Hp] = 0.5,
    [Manufacture.Asus, Manufacture.Dell] = 0.5
};

var result = CBR<Laptop, decimal>
    .Create(laptopList)
    .Output(order => order.Price)
    .SetSimilarityFunctions
    (
        (nameof(Laptop.Manufacture), new TableSimilarityFunction<Manufacture>(_manufactureSimilarity)),
        (nameof(Laptop.RAM), new LinearSimilarityFunction<int>(4, 32, 2)),
        (nameof(Laptop.SSD), new BasicSimilarityFunction<bool>(2)),
        (nameof(Laptop.CPU), new CustomSimilarityFunction<string>(cpuSimilarity, 4))
    )
    .Retrieve(new Laptop("ModelX", Manufacture.Asus, 32, "I5_G11", true, 0), 3)
    .Reuse(SelectType.AverageValue)
    .Revise()
    .Retain()
    .Run();


Console.WriteLine(result.Price);