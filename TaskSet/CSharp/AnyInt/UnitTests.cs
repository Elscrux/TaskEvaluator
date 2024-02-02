using Xunit;
namespace Task;

public class Test_AnyInt {
    [Fact]
    public void Test_0() {
        var result = TaskClass.AnyInt(2, 3, 1);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.AnyInt(2.5, 2, 3);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.AnyInt(1.5, 5, 3.5);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.AnyInt(2, 6, 2);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.AnyInt(4, 2, 2);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.AnyInt(2.2, 2.2, 2.2);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.AnyInt(-4, 6, 2);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.AnyInt(2,1,1);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.AnyInt(3,4,7);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.AnyInt(3.0,4,7);
        Assert.Equal(false, result);
    }
}