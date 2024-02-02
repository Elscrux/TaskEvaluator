using Xunit;
namespace Task;

public class Test_AntiShuffle {
    [Fact]
    public void Test_0() {
        var result = TaskClass.AntiShuffle("Hi");
        Assert.Equal("Hi", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.AntiShuffle("hello");
        Assert.Equal("ehllo", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.AntiShuffle("number");
        Assert.Equal("bemnru", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.AntiShuffle("abcd");
        Assert.Equal("abcd", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.AntiShuffle("Hello World!!!");
        Assert.Equal("Hello !!!Wdlor", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.AntiShuffle("");
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.AntiShuffle("Hi. My name is Mister Robot. How are you?");
        Assert.Equal(".Hi My aemn is Meirst .Rboot How aer ?ouy", result);
    }
}