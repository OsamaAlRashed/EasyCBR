# EasyCBR 1.0.0

- The easy way to implement CBR (Case Based Reasoning) in C#.

**Documentation** 
- You do it by one step!!
  `var result = CBR<Order>  
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
        .Run();`
       
 - 4R methods:
    - `Retrieve(query case, number of the closest cases that you want)`: Retrive the closest cases.
    - `Reuse(select type)`: Reuse the one of cases depending on a way.
    - `Revise(correct value)`: Revise the value, you may not pass any value.
    - `Retain()`: Retain the case. 
 
 - Similarity functions:
   - BasicSimilarityFunction
   - LinearSimilarityFunction
   - TableSimilarityFunction
   // Todo
   - ScalingSimilarityFunction
   - CustomSimilarityFunction
