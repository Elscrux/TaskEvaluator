using Xunit;
namespace Task;

public class Test_CorrectBracketing2 {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CorrectBracketing2("()");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CorrectBracketing2("(()())");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CorrectBracketing2("()()(()())()");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CorrectBracketing2("()()((()()())())(()()(()))");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CorrectBracketing2("((()())))");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.CorrectBracketing2(")(()");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.CorrectBracketing2("(");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.CorrectBracketing2("((((");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.CorrectBracketing2(")");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.CorrectBracketing2("(()");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.CorrectBracketing2("()()(()())())(()");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.CorrectBracketing2("()()(()())()))()");
        Assert.Equal(false, result);
    }
}