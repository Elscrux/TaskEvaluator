using Xunit;
namespace Task;

public class Test_CountUpper {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CountUpper("aBCdEf");
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CountUpper("abcdefg");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CountUpper("dBBE");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CountUpper("B");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CountUpper("U");
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.CountUpper("");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.CountUpper("EEEE");
        Assert.Equal(2, result);
    }
}