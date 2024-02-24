using Xunit;
namespace Task;

public class Test_EvenOddCount {
    [Fact]
    public void Test_0() {
        var result = TaskClass.EvenOddCount(7);
        Assert.Equal((0, 1), result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.EvenOddCount(-78);
        Assert.Equal((1, 1), result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.EvenOddCount(3452);
        Assert.Equal((2, 2), result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.EvenOddCount(346211);
        Assert.Equal((3, 3), result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.EvenOddCount(-345821);
        Assert.Equal((3, 3), result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.EvenOddCount(-2);
        Assert.Equal((1, 0), result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.EvenOddCount(-45347);
        Assert.Equal((2, 3), result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.EvenOddCount(0);
        Assert.Equal((1, 0), result);
    }
}