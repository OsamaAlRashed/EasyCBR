
using EasyCBR;
using EasyCBR.Attributes;
using EasyCBR.SimilarityFunctions;

var result = CBR<Order>
        .Create(new List<Order>()
        {
            new Order() { Id = 1, Name = "1", Price = 1},
            new Order() { Id = 2, Name = "2", Price = 2},
            new Order() { Id = 3, Name = "3", Price = 3},
            new Order() { Id = 4, Name = "4", Price = 4},
            new Order() { Id = 5, Name = "5", Price = 5}
        })
        .SetSimilarityFunctions(
            ("Id", new BasicSimilarityFunction(22)),
            ("Name", new BasicSimilarityFunction(1))
         )
        .Retrieve(new Order(), 2)
        .Reuse(EasyCBR.Enums.ChooseType.Top)
        .Revise(0)
        .Retain()
        .Run();

Console.WriteLine(result.Price);

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Output]
    public int Price { get; set; }

}
