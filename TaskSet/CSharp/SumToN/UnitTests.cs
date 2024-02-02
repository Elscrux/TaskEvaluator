using Xunit;
namespace Task;

public class Test_SumToN {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SumToN(1);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SumToN(6);
        Assert.Equal(21, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SumToN(11);
        Assert.Equal(66, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SumToN(30);
        Assert.Equal(465, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SumToN(100);
        Assert.Equal(5050, result);
    }
}