using Xunit;
namespace Task;

public class Test_Eat {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Eat(4, 8, 9);
        Assert.Equal([12, 1], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Eat(1, 10, 10);
        Assert.Equal([11, 0], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Eat(2, 11, 5);
        Assert.Equal([7, 0], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Eat(4, 5, 7);
        Assert.Equal([9, 2], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Eat(4, 5, 1);
        Assert.Equal([5, 0], result);
    }
}