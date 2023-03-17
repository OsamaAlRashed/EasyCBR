
using EasyCBR;
using EasyCBR.Attributes;
using EasyCBR.Helpers;
using EasyCBR.SimilarityFunctions;

var orderList = new List<Order>()
{
    new Order() { Id = 1, Name = "1", Type = OrderType.Type2, Price = 1 },
    new Order() { Id = 2, Name = "2", Type = OrderType.Type1, Price = 2 },
    new Order() { Id = 3, Name = "3", Type = OrderType.Type2, Price = 3 },
    new Order() { Id = 4, Name = "4", Type = OrderType.Type2, Price = 4 },
    new Order() { Id = 5, Name = "5", Type = OrderType.Type2, Price = 5 }
};

var result = CBR<Order>
        .Create(orderList)
        .Output(x => x.Price)
        .SetSimilarityFunctions(
            ("Id", new BasicSimilarityFunction<int>(2)),
            ("Name", new BasicSimilarityFunction<string>(1)),
            ("Type", new TableSimilarityFunction<OrderType>(2))
         )
        .Retrieve(new Order(), 2)
        .Reuse(EasyCBR.Enums.ChooseType.MaxSimilarity)
        .Revise(0)
        .Retain()
        .Run();

Console.WriteLine(result.Price);

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; }

    public OrderType Type { get; set; }

    [Output]
    public int Price { get; set; }

}

public enum OrderType
{
    Type1,
    Type2,
    Type3,
}
