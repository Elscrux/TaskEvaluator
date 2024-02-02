using Xunit;
namespace Task;

public class Test_PrimeLength {
    [Fact]
    public void Test_0() {
        var result = TaskClass.PrimeLength("Hello");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.PrimeLength("abcdcba");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.PrimeLength("kittens");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.PrimeLength("orange");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.PrimeLength("wow");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.PrimeLength("world");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.PrimeLength("MadaM");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.PrimeLength("Wow");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.PrimeLength("");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.PrimeLength("HI");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.PrimeLength("go");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.PrimeLength("gogo");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_12() {
        var result = TaskClass.PrimeLength("aaaaaaaaaaaaaaa");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_13() {
        var result = TaskClass.PrimeLength("Madam");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_14() {
        var result = TaskClass.PrimeLength("M");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_15() {
        var result = TaskClass.PrimeLength("0");
        Assert.Equal(false, result);
    }
}