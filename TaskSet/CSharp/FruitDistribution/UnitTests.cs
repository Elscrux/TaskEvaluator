using Xunit;
namespace Task;

public class Test_FruitDistribution {
    [Fact]
    public void Test_0() {
        var result = TaskClass.FruitDistribution("5 apples and 6 oranges",19);
        Assert.Equal(8, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.FruitDistribution("5 apples and 6 oranges",21);
        Assert.Equal(10, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.FruitDistribution("0 apples and 1 oranges",3);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.FruitDistribution("1 apples and 0 oranges",3);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.FruitDistribution("2 apples and 3 oranges",100);
        Assert.Equal(95, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.FruitDistribution("2 apples and 3 oranges",5);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.FruitDistribution("1 apples and 100 oranges",120);
        Assert.Equal(19, result);
    }
}