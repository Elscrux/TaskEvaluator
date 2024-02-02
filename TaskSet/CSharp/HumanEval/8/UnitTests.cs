using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SumProduct([]);
        Assert.Equal( (0, 1), result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SumProduct([1, 1, 1]);
        Assert.Equal( (3, 1), result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SumProduct([100, 0]);
        Assert.Equal( (100, 0), result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SumProduct([3, 5, 7]);
        Assert.Equal( (3 + 5 + 7, 3 * 5 * 7), result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SumProduct([10]);
        Assert.Equal( (10, 10), result);
    }
}