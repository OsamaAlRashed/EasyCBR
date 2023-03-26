# EasyCBR 1.0.0

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
   
   // Todo
   - ScalingSimilarityFunction
   
  ** Demo **
 
 - Prepare your data:
  ![image](https://user-images.githubusercontent.com/61357303/227797277-67cf14f3-87a1-4cfd-bbcd-3ac4694f4349.png)
 
 - Do it by one step!!
 ![image](https://user-images.githubusercontent.com/61357303/227797320-bd60009e-5235-4930-9f09-24ea897162a5.png)
 
 - Write custom similarity function (if you need):
 ![image](https://user-images.githubusercontent.com/61357303/227797366-9e06609e-5a11-4084-abe0-41005ae90583.png)

 - Enjoy ..  
