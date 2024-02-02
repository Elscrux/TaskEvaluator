using Xunit;
namespace Task;

public class Test_Search {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Search([5, 5, 5, 5, 1]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Search([4, 1, 4, 1, 4, 4]);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Search([3, 3]);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Search([8, 8, 8, 8, 8, 8, 8, 8]);
        Assert.Equal(8, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Search([2, 3, 3, 2, 2]);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Search([2, 7, 8, 8, 4, 8, 7, 3, 9, 6, 5, 10, 4, 3, 6, 7, 1, 7, 4, 10, 8, 1]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Search([3, 2, 8, 2]);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Search([6, 7, 1, 8, 8, 10, 5, 8, 5, 3, 10]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.Search([8, 8, 3, 6, 5, 6, 4]);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.Search([6, 9, 6, 7, 1, 4, 7, 1, 8, 8, 9, 8, 10, 10, 8, 4, 10, 4, 10, 1, 2, 9, 5, 7, 9]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.Search([1, 9, 10, 1, 3]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.Search([6, 9, 7, 5, 8, 7, 5, 3, 7, 5, 10, 10, 3, 6, 10, 2, 8, 6, 5, 4, 9, 5, 3, 10]);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_12() {
        var result = TaskClass.Search([1]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_13() {
        var result = TaskClass.Search([8, 8, 10, 6, 4, 3, 5, 8, 2, 4, 2, 8, 4, 6, 10, 4, 2, 1, 10, 2, 1, 1, 5]);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_14() {
        var result = TaskClass.Search([2, 10, 4, 8, 2, 10, 5, 1, 2, 9, 5, 5, 6, 3, 8, 6, 4, 10]);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_15() {
        var result = TaskClass.Search([1, 6, 10, 1, 6, 9, 10, 8, 6, 8, 7, 3]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_16() {
        var result = TaskClass.Search([9, 2, 4, 1, 5, 1, 5, 2, 5, 7, 7, 7, 3, 10, 1, 5, 4, 2, 8, 4, 1, 9, 10, 7, 10, 2, 8, 10, 9, 4]);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_17() {
        var result = TaskClass.Search([2, 6, 4, 2, 8, 7, 5, 6, 4, 10, 4, 6, 3, 7, 8, 8, 3, 1, 4, 2, 2, 10, 7]);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_18() {
        var result = TaskClass.Search([9, 8, 6, 10, 2, 6, 10, 2, 7, 8, 10, 3, 8, 2, 6, 2, 3, 1]);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_19() {
        var result = TaskClass.Search([5, 5, 3, 9, 5, 6, 3, 2, 8, 5, 6, 10, 10, 6, 8, 4, 10, 7, 7, 10, 8]);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_20() {
        var result = TaskClass.Search([10]);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_21() {
        var result = TaskClass.Search([9, 7, 7, 2, 4, 7, 2, 10, 9, 7, 5, 7, 2]);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_22() {
        var result = TaskClass.Search([5, 4, 10, 2, 1, 1, 10, 3, 6, 1, 8]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_23() {
        var result = TaskClass.Search([7, 9, 9, 9, 3, 4, 1, 5, 9, 1, 2, 1, 1, 10, 7, 5, 6, 7, 6, 7, 7, 6]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_24() {
        var result = TaskClass.Search([3, 10, 10, 9, 2]);
        Assert.Equal(-1, result);
    }
}