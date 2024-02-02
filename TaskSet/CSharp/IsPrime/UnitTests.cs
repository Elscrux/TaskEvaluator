using Xunit;
namespace Task;

public class Test_IsPrime {
    [Fact]
    public void Test_0() {
        var result = TaskClass.IsPrime(6);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.IsPrime(101);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.IsPrime(11);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.IsPrime(13441);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.IsPrime(61);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.IsPrime(4);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.IsPrime(1);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.IsPrime(5);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.IsPrime(11);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.IsPrime(17);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.IsPrime(5 * 17);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.IsPrime(11 * 7);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_12() {
        var result = TaskClass.IsPrime(13441 * 19);
        Assert.Equal(false, result);
    }
}