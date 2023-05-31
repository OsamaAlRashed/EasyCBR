# EasyCBR 1.0.3


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
 
  ![image](https://user-images.githubusercontent.com/61357303/227797277-67cf14f3-87a1-4cfd-bbcd-3ac4694f4349.png)
 
 - Write custom similarity function (if you need):
 
  ![image](https://user-images.githubusercontent.com/61357303/227797366-9e06609e-5a11-4084-abe0-41005ae90583.png)

 - Write custom table (if you need):
 
    ![image](https://github.com/OsamaAlRashed/EasyCBR/assets/61357303/e5cac7c7-d5ca-40a2-9221-344d56c46c46)

 - Run it:
    
    ![image](https://github.com/OsamaAlRashed/EasyCBR/assets/61357303/9c937675-e918-4b9a-aed0-274813cf2255)

 

 - Enjoy ..  
