using Xunit;
namespace Task;

public class Test_MatchParens {
    [Fact]
    public void Test_0() {
        var result = TaskClass.MatchParens(["()(", ")"]);
        Assert.Equal("Yes", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.MatchParens([")", ")"]);
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.MatchParens(["(()(())", "())())"]);
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.MatchParens([")())", "(()()("]);
        Assert.Equal("Yes", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.MatchParens(["(())))", "(()())(("]);
        Assert.Equal("Yes", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.MatchParens(["()", "())"]);
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.MatchParens(["(()(", "()))()"]);
        Assert.Equal("Yes", result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.MatchParens(["((((", "((())"]);
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.MatchParens([")(()", "(()("]);
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.MatchParens([")(", ")("]);
        Assert.Equal("No", result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.MatchParens(["(", ")"]);
        Assert.Equal("Yes", result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.MatchParens([")", "("]);
        Assert.Equal("Yes", result);
    }
}