using Xunit;
namespace Task;

public class Test_GetPositive {
    [Fact]
    public void Test_0() {
        var result = TaskClass.GetPositive([-1, -2, 4, 5, 6]);
        Assert.Equal([4, 5, 6], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.GetPositive([5, 3, -5, 2, 3, 3, 9, 0, 123, 1, -10]);
        Assert.Equal([5, 3, 2, 3, 3, 9, 123, 1], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.GetPositive([-1, -2]);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.GetPositive([]);
        Assert.Equal([], result);
    }
}