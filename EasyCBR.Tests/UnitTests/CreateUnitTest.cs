using EasyCBR.Contract.IStage;
using EasyCBR.Enums;
using EasyCBR.Helpers;
using EasyCBR.SimilarityFunctions;

namespace EasyCBR.Tests.UnitTests;

//ToDo Rename the methods
public class CreateUnitTest
{
    public List<Order> orders = new List<Order>()
    {
        new Order(1, "Osama", OrderType.A, 100),
        new Order(2, "Mohammed", OrderType.C, 120),
        new Order(3, "Hussam", OrderType.B, 430),
        new Order(4, "Mohanned", OrderType.B, 90),
        new Order(5, "Abd Alqader", OrderType.A, 5)
    };

    [Fact]
    public void Test1()
    {
        //Arrange
        var cbr = CBR<Order>.Create(orders);

        //Act
        var expected = 5;

        //Assert
        Assert.Equal(cbr.Cases.Count, expected);
    }

    [Fact]
    public void Test2()
    {
        //Arrange
        var create = CBR<Order>.Create;

        //Act

        //Assert
        Assert.Throws<ArgumentNullException>(() => create(null));
    }

    [Fact]
    public void Test3()
    {
        //Arrange
        var create = CBR<Order>.Create;

        //Act

        //Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => create(new List<Order>()));
    }

    [Fact]
    public void Test4()
    {
        //Arrange
        var cbr = CBR<Order>.Create(orders).Output(x => x.Price);

        //Act
        var expected = nameof(Order.Price);

        //Assert
        Assert.Equal(cbr.Output.Name, expected);
    }

    [Fact]
    public void Test5()
    {
        //Arrange
        var cbr = CBR<Order>
            .Create(orders)
            .Output(x => x.Price)
            .SetSimilarityFunctions((nameof(Order.CustomerName), new BasicSimilarityFunction<string>()));

        //Act

        //Assert
        Assert.True(true);
    }

    [Fact]
    public void Test6()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<ArgumentException>(() => CBR<Order>
            .Create(orders)
            .Output(x => x.Price)
            .SetSimilarityFunctions());
    }

    [Fact]
    public void Test7()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<ArgumentNullException>(() => CBR<Order>
            .Create(orders)
            .Output(x => x.Price)
            .SetSimilarityFunctions(null));
    }

    [Fact]
    public void Test8()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<ArgumentException>(() => CBR<Order>
            .Create(orders)
            .Output(x => x.Price)
            .SetSimilarityFunctions(
                ("test", new BasicSimilarityFunction<string>())
            ));
    }

    [Fact]
    public void Test9()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<ArgumentException>(() => CBR<Order>
            .Create(orders)
            .Output(x => x.Price)
            .SetSimilarityFunctions(
                (nameof(Order.Id), new BasicSimilarityFunction<string>())
            ));
    }

    [Fact]
    public void Test10()
    {
        //Arrange
        var cbr = CBR<Order>
            .Create(orders)
            .Output(x => x.Price)
            .SetSimilarityFunctions(
                (nameof(Order.CustomerName), new BasicSimilarityFunction<string>())
            ).Retrieve(new Order(6, "test", OrderType.A, 0), 2);
        //Act
        
        //Assert
        Assert.True(cbr.Run().Count == 2);
    }

    [Fact]
    public void Test11()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<ArgumentNullException>(() => CBR<Order>
            .Create(orders)
            .Output(x => x.Price)
            .SetSimilarityFunctions(
                (nameof(Order.CustomerName), new BasicSimilarityFunction<string>())
            ).Retrieve(null, 2));
    }

    [Fact]
    public void Test12()
    {
        //Arrange
        
        //Act

        //Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => CBR<Order>
            .Create(orders)
            .Output(x => x.Price)
            .SetSimilarityFunctions(
                (nameof(Order.CustomerName), new BasicSimilarityFunction<string>())
            ).Retrieve(new Order(6, "test", OrderType.A, 0), 0));
    }

    [Fact]
    public void Test13()
    {
        //Arrange
        var cbr = CBR<Order>
            .Create(orders)
            .Output(x => x.Price)
            .SetSimilarityFunctions(
                (nameof(Order.CustomerName), new BasicSimilarityFunction<string>())
            ).Retrieve(new Order(6, "test", OrderType.A, 0), 2)
            .Reuse();
        //Act

        //Assert
        Assert.True(cbr.Run() is not null);
    }

    [Fact]
    public void Test14()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => CBR<Order>
            .Create(orders)
            .Output(x => x.Price)
            .SetSimilarityFunctions(
                (nameof(Order.CustomerName), new BasicSimilarityFunction<string>())
            ).Retrieve(new Order(6, "test", OrderType.A, 0), 2)
            .Reuse((SelectType)10));
    }
}
