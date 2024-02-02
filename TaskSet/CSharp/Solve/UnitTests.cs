using Xunit;
namespace Task;

public class Test_Solve {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Solve("AsDf");
        Assert.Equal("aSdF", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Solve("1234");
        Assert.Equal("4321", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Solve("ab");
        Assert.Equal("AB", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Solve("#a@C");
        Assert.Equal("#A@c", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Solve("#AsdfW^45");
        Assert.Equal("#aSDFw^45", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Solve("#6@2");
        Assert.Equal("2@6#", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Solve("#$a^D");
        Assert.Equal("#$A^d", result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Solve("#ccc");
        Assert.Equal("#CCC", result);
    }
}