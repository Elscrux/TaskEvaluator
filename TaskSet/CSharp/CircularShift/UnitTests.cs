using Xunit;
namespace Task;

public class Test_CircularShift {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CircularShift(100, 2);
        Assert.Equal("001", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CircularShift(12, 2);
        Assert.Equal("12", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CircularShift(97, 8);
        Assert.Equal("79", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CircularShift(12, 1);
        Assert.Equal("21", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CircularShift(11, 101);
        Assert.Equal("11", result);
    }
}