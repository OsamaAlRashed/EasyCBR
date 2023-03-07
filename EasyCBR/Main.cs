using EasyCBR.SimilarityFunctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCBR
{
    public class Main
    {
        public static void Run()
        {
            var result = CBR<Order>
                .Create(new List<Order>())
                .SetSimilarityFunctions(
                    ("test1", new LinearSimilarityFunction(22)),
                    ("test2", new TableSimilarityFunction(1))
                 )
                .Retrieve(new Order(), 5)
                .Reuse(Enums.ChooseType.Top)
                .Revise(15)
                .Retain()
                .Run();

        }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

    }
}
