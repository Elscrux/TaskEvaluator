using Xunit;
namespace Task;

public class Test_IsSimplePower {
    [Fact]
    public void Test_0() {
        var result = TaskClass.IsSimplePower(16, 2);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.IsSimplePower(143214, 16);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.IsSimplePower(4, 2);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.IsSimplePower(9, 3);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.IsSimplePower(16, 4);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.IsSimplePower(24, 2);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.IsSimplePower(128, 4);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.IsSimplePower(12, 6);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.IsSimplePower(1, 1);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.IsSimplePower(1, 12);
        Assert.Equal(true, result);
    }
}