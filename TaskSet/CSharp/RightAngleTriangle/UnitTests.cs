using Xunit;
namespace Task;

public class Test_RightAngleTriangle {
    [Fact]
    public void Test_0() {
        var result = TaskClass.RightAngleTriangle(3, 4, 5);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.RightAngleTriangle(1, 2, 3);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.RightAngleTriangle(10, 6, 8);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.RightAngleTriangle(2, 2, 2);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.RightAngleTriangle(7, 24, 25);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.RightAngleTriangle(10, 5, 7);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.RightAngleTriangle(5, 12, 13);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.RightAngleTriangle(15, 8, 17);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.RightAngleTriangle(48, 55, 73);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.RightAngleTriangle(1, 1, 1);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.RightAngleTriangle(2, 2, 10);
        Assert.Equal(false, result);
    }
}