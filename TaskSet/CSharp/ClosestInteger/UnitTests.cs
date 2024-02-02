using Xunit;
namespace Task;

public class Test_ClosestInteger {
    [Fact]
    public void Test_0() {
        var result = TaskClass.ClosestInteger("10");
        Assert.Equal(10, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.ClosestInteger("14.5");
        Assert.Equal(15, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.ClosestInteger("-15.5");
        Assert.Equal(-16, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.ClosestInteger("15.3");
        Assert.Equal(15, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.ClosestInteger("0");
        Assert.Equal(0, result);
    }
}