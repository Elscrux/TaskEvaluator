using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.RollingMax([]);
        Assert.Equal( [], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.RollingMax([1, 2, 3, 4]);
        Assert.Equal( [1, 2, 3, 4], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.RollingMax([4, 3, 2, 1]);
        Assert.Equal( [4, 4, 4, 4], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.RollingMax([3, 2, 3, 100, 3]);
        Assert.Equal( [3, 3, 3, 100, 100], result);
    }
}