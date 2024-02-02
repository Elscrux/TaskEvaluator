using Xunit;
namespace Task;

public class Test_SortArray {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SortArray([5]);
        Assert.Equal([5], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SortArray([2, 4, 3, 0, 1, 5]);
        Assert.Equal([0, 1, 2, 3, 4, 5], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SortArray([2, 4, 3, 0, 1, 5, 6]);
        Assert.Equal([6, 5, 4, 3, 2, 1, 0], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SortArray([2, 1]);
        Assert.Equal([1, 2], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SortArray([15, 42, 87, 32 ,11, 0]);
        Assert.Equal([0, 11, 15, 32, 42, 87], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.SortArray([21, 14, 23, 11]);
        Assert.Equal([23, 21, 14, 11], result);
    }
}