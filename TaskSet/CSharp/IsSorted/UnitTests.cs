using Xunit;
namespace Task;

public class Test_IsSorted {
    [Fact]
    public void Test_0() {
        var result = TaskClass.IsSorted([5]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.IsSorted([1, 2, 3, 4, 5]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.IsSorted([1, 3, 2, 4, 5]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.IsSorted([1, 2, 3, 4, 5, 6]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.IsSorted([1, 2, 3, 4, 5, 6, 7]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.IsSorted([1, 3, 2, 4, 5, 6, 7]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.IsSorted([]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.IsSorted([1]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.IsSorted([3, 2, 1]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.IsSorted([1, 2, 2, 2, 3, 4]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.IsSorted([1, 2, 3, 3, 3, 4]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.IsSorted([1, 2, 2, 3, 3, 4]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_12() {
        var result = TaskClass.IsSorted([1, 2, 3, 4]);
        Assert.Equal(true, result);
    }
}