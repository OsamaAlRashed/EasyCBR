using EasyCBR;
using EasyCBR.Enums;
using EasyCBR.Helpers;
using EasyCBR.SimilarityFunctions;

var laptopList = new List<Laptop>()
{
   new Laptop("ModelX1", Manufacture.Asus, 4, "I3_G5", false, 200),
   new Laptop("ModelX2", Manufacture.Dell, 4, "I3_G6", false, 220),
   new Laptop("ModelX3", Manufacture.Asus, 4, "I3_G7", false, 250),
   new Laptop("ModelX4", Manufacture.Hp, 8, "I5_G8", false, 300),
   new Laptop("ModelX5", Manufacture.Lenovo, 8, "I5_G8", false, 280),
   new Laptop("ModelX6", Manufacture.Dell, 16, "I5_G11", true, 500),
   new Laptop("ModelX7", Manufacture.Lenovo, 12, "I5_G10", false, 400),
   new Laptop("ModelX8", Manufacture.Hp, 16, "I5_G12", true, 600),
   new Laptop("ModelX9", Manufacture.Lenovo, 32, "I5_G12", true, 650),
   new Laptop("ModelX10", Manufacture.Asus, 16, "I5_G11", true, 450),
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

var tableManyfactureSimilarity = new double[4, 4]
{
    { 1.0, 0.5, 0.7, 0.2 },
    { 0.5, 1.0, 0.3, 0.3 },
    { 0.7, 0.3, 1.0, 0.1 },
    { 0.2, 0.3, 0.1, 1.0 }
};

var result = CBR<Laptop>
    .Create(laptopList)
    .Output(order => order.Price)
    .SetSimilarityFunctions
    (
        (nameof(Laptop.Manufacture), new TableSimilarityFunction<Manufacture>(tableManyfactureSimilarity)),
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