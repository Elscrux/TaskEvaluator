using Xunit;
namespace Task;

public class Test_Intersperse {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Intersperse([], 7);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Intersperse([5, 6, 3, 2], 8);
        Assert.Equal([5, 8, 6, 8, 3, 8, 2], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Intersperse([2, 2, 2], 2);
        Assert.Equal([2, 2, 2, 2, 2], result);
    }
}