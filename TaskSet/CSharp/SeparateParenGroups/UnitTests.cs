using Xunit;
namespace Task;

public class Test_SeparateParenGroups {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SeparateParenGroups("(()()) ((())) () ((())()())");
        Assert.Equal([
        "(()())", "((()))", "()", "((())()())"
    ], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SeparateParenGroups("() (()) ((())) (((())))");
        Assert.Equal([
        "()", "(())", "((()))", "(((())))"
    ], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SeparateParenGroups("(()(())((())))");
        Assert.Equal([
        "(()(())((())))"
    ], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SeparateParenGroups("( ) (( )) (( )( ))");
        Assert.Equal(["()", "(())", "(()())"], result);
    }
}