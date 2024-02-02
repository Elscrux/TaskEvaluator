using Xunit;
namespace Task;

public class Test_BelowZero {
    [Fact]
    public void Test_0() {
        var result = TaskClass.BelowZero([]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.BelowZero([1, 2, -3, 1, 2, -3]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.BelowZero([1, 2, -4, 5, 6]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.BelowZero([1, -1, 2, -2, 5, -5, 4, -4]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.BelowZero([1, -1, 2, -2, 5, -5, 4, -5]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.BelowZero([1, -2, 2, -2, 5, -5, 4, -4]);
        Assert.Equal(true, result);
    }
}