using Xunit;
namespace Task;

public class Test_Solution {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Solution([5, 8, 7, 1]);
        Assert.Equal(12, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Solution([3, 3, 3, 3, 3]);
        Assert.Equal(9, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Solution([30, 13, 24, 321]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Solution([5, 9]);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Solution([2, 4, 8]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Solution([30, 13, 23, 32]);
        Assert.Equal(23, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Solution([3, 13, 2, 9]);
        Assert.Equal(3, result);
    }
}