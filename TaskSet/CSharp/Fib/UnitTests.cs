using Xunit;
namespace Task;

public class Test_Fib {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Fib(10);
        Assert.Equal(55, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Fib(1);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Fib(8);
        Assert.Equal(21, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Fib(11);
        Assert.Equal(89, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Fib(12);
        Assert.Equal(144, result);
    }
}