using Xunit;
namespace Task;

public class Test_IsCube {
    [Fact]
    public void Test_0() {
        var result = TaskClass.IsCube(1);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.IsCube(2);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.IsCube(-1);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.IsCube(64);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.IsCube(1000);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.IsCube(0);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.IsCube(1729);
        Assert.Equal(false, result);
    }
}