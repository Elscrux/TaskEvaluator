using Xunit;
namespace Task;

public class Test_XOrY {
    [Fact]
    public void Test_0() {
        var result = TaskClass.XOrY(7, 34, 12);
        Assert.Equal(34, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.XOrY(15, 8, 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.XOrY(3, 33, 5212);
        Assert.Equal(33, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.XOrY(1259, 3, 52);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.XOrY(7919, -1, 12);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.XOrY(3609, 1245, 583);
        Assert.Equal(583, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.XOrY(91, 56, 129);
        Assert.Equal(129, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.XOrY(6, 34, 1234);
        Assert.Equal(1234, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.XOrY(1, 2, 0);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.XOrY(2, 2, 0);
        Assert.Equal(2, result);
    }
}