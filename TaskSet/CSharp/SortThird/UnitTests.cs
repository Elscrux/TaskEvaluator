using Xunit;
namespace Task;

public class Test_SortThird {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SortThird([1, 2, 3]);
        Assert.Equal([1, 2, 3], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SortThird([5, 6, 3, 4, 8, 9, 2]);
        Assert.Equal([2, 6, 3, 4, 8, 9, 5], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SortThird([5, 8, 3, 4, 6, 9, 2]);
        Assert.Equal([2, 8, 3, 4, 6, 9, 5], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SortThird([5, 6, 9, 4, 8, 3, 2]);
        Assert.Equal([2, 6, 9, 4, 8, 3, 5], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SortThird([5, 6, 3, 4, 8, 9, 2, 1]);
        Assert.Equal([2, 6, 3, 4, 8, 9, 5, 1], result);
    }
}