using Xunit;
namespace Task;

public class Test_Modp {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Modp(3, 5);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Modp(1101, 101);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Modp(0, 101);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Modp(3, 11);
        Assert.Equal(8, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Modp(100, 101);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Modp(30, 5);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Modp(31, 5);
        Assert.Equal(3, result);
    }
}