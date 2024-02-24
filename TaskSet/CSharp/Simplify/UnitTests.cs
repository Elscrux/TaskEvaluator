using Xunit;
namespace Task;

public class Test_Simplify {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Simplify("1/5", "5/1");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Simplify("1/6", "2/1");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Simplify("5/1", "3/1");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Simplify("7/10", "10/2");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Simplify("2/10", "50/10");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Simplify("7/2", "4/2");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Simplify("11/6", "6/1");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Simplify("2/3", "5/2");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.Simplify("5/2", "3/5");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.Simplify("2/4", "8/4");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.Simplify("2/4", "4/2");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.Simplify("1/5", "5/1");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_12() {
        var result = TaskClass.Simplify("1/5", "1/5");
        Assert.Equal(false, result);
    }
}