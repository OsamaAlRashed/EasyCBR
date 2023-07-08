# EasyCBR 1.0.4


  <table>
    <tbody>
      <tr>
        <td> 
          <a href="https://www.nuget.org/packages/EasyCBR/">
            <img alt="Nuget" src="https://img.shields.io/nuget/dt/EasyCBR?color=blue&label=EasyCBR&logo=nuget&style=flate">
          </a>
        </td> 
      </tr>
    </tbody>
  <table>

- The easy way to implement CBR (Case Based Reasoning) in C#.

**Documentation** 
 - 4R methods:
    - `Retrieve(query case, number of the closest cases that you want)`: Retrive the closest cases.
    - `Reuse(select type)`: Reuse the one of cases depending on a way.
    - `Revise(correct value)`: Revise the value, you may not pass any value.
    - `Retain()`: Retain the case. 
 
 - Similarity functions:
   - BasicSimilarityFunction
   - LinearSimilarityFunction
   - TableSimilarityFunction
   - CustomSimilarityFunction
   
  **Demo**
 
 - Prepare your data:

 ```cs
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
 ```
 
 - Write a custom similarity function (if you need it):

 ```cs
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
 ```

 - Write a custom table (if you need it):

 ```cs
  Table<Manufacture> _manufactureSimilarity = new()
  {
      [Manufacture.Dell, Manufacture.Hp] = 0.5,
      [Manufacture.Asus, Manufacture.Dell] = 0.5
  };
 ```
 

 - Run it:
    
 ```cs
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
 ```


 - Enjoy ..  
