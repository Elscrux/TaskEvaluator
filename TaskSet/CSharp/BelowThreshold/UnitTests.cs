using Xunit;
namespace Task;

public class Test_BelowThreshold {
    [Fact]
    public void Test_0() {
        var result = TaskClass.BelowThreshold([1, 2, 4, 10], 100);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.BelowThreshold([1, 20, 4, 10], 5);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.BelowThreshold([1, 20, 4, 10], 21);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.BelowThreshold([1, 20, 4, 10], 22);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.BelowThreshold([1, 8, 4, 10], 11);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.BelowThreshold([1, 8, 4, 10], 10);
        Assert.Equal(false, result);
    }
}