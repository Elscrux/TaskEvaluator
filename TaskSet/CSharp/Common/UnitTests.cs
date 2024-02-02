using Xunit;
namespace Task;

public class Test_Common {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Common([1, 4, 3, 34, 653, 2, 5], [5, 7, 1, 5, 9, 653, 121]);
        Assert.Equal([1, 5, 653], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Common([5, 3, 2, 8], [3, 2]);
        Assert.Equal([2, 3], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Common([4, 3, 2, 8], [3, 2, 4]);
        Assert.Equal([2, 3, 4], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Common([4, 3, 2, 8], []);
        Assert.Equal([], result);
    }
}