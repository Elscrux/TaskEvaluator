using Xunit;
namespace Task;

public class Test_RemoveVowels {
    [Fact]
    public void Test_0() {
        var result = TaskClass.RemoveVowels("");
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.RemoveVowels("abcdef\nghijklm");
        Assert.Equal("bcdf\nghjklm", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.RemoveVowels("fedcba");
        Assert.Equal("fdcb", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.RemoveVowels("eeeee");
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.RemoveVowels("acBAA");
        Assert.Equal("cB", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.RemoveVowels("EcBOO");
        Assert.Equal("cB", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.RemoveVowels("ybcd");
        Assert.Equal("ybcd", result);
    }
}