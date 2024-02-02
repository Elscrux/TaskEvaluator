using Xunit;
namespace Task;

public class Test_Fib4 {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Fib4(5);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Fib4(8);
        Assert.Equal(28, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Fib4(10);
        Assert.Equal(104, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Fib4(12);
        Assert.Equal(386, result);
    }
}