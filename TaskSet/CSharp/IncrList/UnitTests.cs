using Xunit;
namespace Task;

public class Test_IncrList {
    [Fact]
    public void Test_0() {
        var result = TaskClass.IncrList([]);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.IncrList([3, 2, 1]);
        Assert.Equal([4, 3, 2], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.IncrList([5, 2, 5, 2, 3, 3, 9, 0, 123]);
        Assert.Equal([6, 3, 6, 3, 4, 4, 10, 1, 124], result);
    }
}