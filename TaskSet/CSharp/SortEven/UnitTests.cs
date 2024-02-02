using Xunit;
namespace Task;

public class Test_SortEven {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SortEven([1, 2, 3]);
        Assert.Equal([1, 2, 3], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SortEven([5, 3, -5, 2, -3, 3, 9, 0, 123, 1, -10]);
        Assert.Equal([-10, 3, -5, 2, -3, 3, 5, 0, 9, 1, 123], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SortEven([5, 8, -12, 4, 23, 2, 3, 11, 12, -10]);
        Assert.Equal([-12, 8, 3, 4, 5, 2, 12, 11, 23, -10], result);
    }
}