using Xunit;
namespace Task;

public class Test_Fibfib {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Fibfib(2);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Fibfib(1);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Fibfib(5);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Fibfib(8);
        Assert.Equal(24, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Fibfib(10);
        Assert.Equal(81, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Fibfib(12);
        Assert.Equal(274, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Fibfib(14);
        Assert.Equal(927, result);
    }
}