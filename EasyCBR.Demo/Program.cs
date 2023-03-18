using EasyCBR;
using EasyCBR.Enums;
using EasyCBR.Helpers;
using EasyCBR.SimilarityFunctions;

var orderList = new List<Order>()
{
    new Order(1, "Osama", OrderType.A, 100),
    new Order(2, "Mohammed", OrderType.C, 120),
    new Order(3, "Hussam", OrderType.B, 430),
    new Order(4, "Mohanned", OrderType.B, 90),
    new Order(5, "Abd Alqader", OrderType.A, 5)
};

var result = CBR<Order>  
        .Create(orderList)
        .Output(order => order.Price)
        .SetSimilarityFunctions(
            (nameof(Order.CustomerName), new BasicSimilarityFunction<string>(2)),
            (nameof(Order.Type), new TableSimilarityFunction<OrderType>(4))
         )
        .Retrieve(new Order(6, "Osama", OrderType.C, 0), 2)
        .Reuse(SelectType.MaxSimilarity)
        .Revise()
        .Retain()
        .Run();

Console.WriteLine(result.Price);