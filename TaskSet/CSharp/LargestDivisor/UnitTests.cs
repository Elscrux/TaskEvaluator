using Xunit;
namespace Task;

public class Test_LargestDivisor {
    [Fact]
    public void Test_0() {
        var result = TaskClass.LargestDivisor(3);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.LargestDivisor(7);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.LargestDivisor(10);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.LargestDivisor(100);
        Assert.Equal(50, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.LargestDivisor(49);
        Assert.Equal(7, result);
    }
}