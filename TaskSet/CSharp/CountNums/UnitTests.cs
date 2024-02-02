using Xunit;
namespace Task;

public class Test_CountNums {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CountNums([]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CountNums([-1, -2, 0]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CountNums([1, 1, 2, -2, 3, 4, 5]);
        Assert.Equal(6, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CountNums([1, 6, 9, -6, 0, 1, 5]);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CountNums([1, 100, 98, -7, 1, -1]);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.CountNums([12, 23, 34, -45, -56, 0]);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.CountNums([-0, 1]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.CountNums([1]);
        Assert.Equal(1, result);
    }
}