using Xunit;
namespace Task;

public class Test_IntToMiniRoman {
    [Fact]
    public void Test_0() {
        var result = TaskClass.IntToMiniRoman(19);
        Assert.Equal("xix", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.IntToMiniRoman(152);
        Assert.Equal("clii", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.IntToMiniRoman(251);
        Assert.Equal("ccli", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.IntToMiniRoman(426);
        Assert.Equal("cdxxvi", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.IntToMiniRoman(500);
        Assert.Equal("d", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.IntToMiniRoman(1);
        Assert.Equal("i", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.IntToMiniRoman(4);
        Assert.Equal("iv", result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.IntToMiniRoman(43);
        Assert.Equal("xliii", result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.IntToMiniRoman(90);
        Assert.Equal("xc", result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.IntToMiniRoman(94);
        Assert.Equal("xciv", result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.IntToMiniRoman(532);
        Assert.Equal("dxxxii", result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.IntToMiniRoman(900);
        Assert.Equal("cm", result);
    }

    [Fact]
    public void Test_12() {
        var result = TaskClass.IntToMiniRoman(994);
        Assert.Equal("cmxciv", result);
    }

    [Fact]
    public void Test_13() {
        var result = TaskClass.IntToMiniRoman(1000);
        Assert.Equal("m", result);
    }
}