using Xunit;
namespace Task;

public class Test_Minsubarraysum {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Minsubarraysum([2, 3, 4, 1, 2, 4]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Minsubarraysum([-1, -2, -3]);
        Assert.Equal(-6, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Minsubarraysum([-1, -2, -3, 2, -10]);
        Assert.Equal(-14, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Minsubarraysum([-999999999]);
        Assert.Equal(-999999999, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Minsubarraysum([0, 10, 20, 1000000]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Minsubarraysum([-1, -2, -3, 10, -5]);
        Assert.Equal(-6, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Minsubarraysum([100, -1, -2, -3, 10, -5]);
        Assert.Equal(-6, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Minsubarraysum([10, 11, 13, 8, 3, 4]);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.Minsubarraysum([100, -33, 32, -1, 0, -2]);
        Assert.Equal(-33, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.Minsubarraysum([-10]);
        Assert.Equal(-10, result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.Minsubarraysum([7]);
        Assert.Equal(7, result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.Minsubarraysum([1, -1]);
        Assert.Equal(-1, result);
    }
}