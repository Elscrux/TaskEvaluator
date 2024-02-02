using Xunit;
namespace Task;

public class Test_FindMax {
    [Fact]
    public void Test_0() {
        var result = TaskClass.FindMax(["name", "of", "string"]);
        Assert.Equal("string", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.FindMax(["name", "enam", "game"]);
        Assert.Equal("enam", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.FindMax(["aaaaaaa", "bb", "cc"]);
        Assert.Equal("aaaaaaa", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.FindMax(["abc", "cba"]);
        Assert.Equal("abc", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.FindMax(["play", "this", "game", "of","footbott"]);
        Assert.Equal("footbott", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.FindMax(["we", "are", "gonna", "rock"]);
        Assert.Equal("gonna", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.FindMax(["we", "are", "a", "mad", "nation"]);
        Assert.Equal("nation", result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.FindMax(["this", "is", "a", "prrk"]);
        Assert.Equal("this", result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.FindMax(["b"]);
        Assert.Equal("b", result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.FindMax(["play", "play", "play"]);
        Assert.Equal("play", result);
    }
}