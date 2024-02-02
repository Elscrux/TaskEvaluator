using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.ParseNestedParens("(()()) ((())) () ((())()())");
        Assert.Equal( [2, 3, 1, 3], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.ParseNestedParens("() (()) ((())) (((())))");
        Assert.Equal( [1, 2, 3, 4], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.ParseNestedParens("(()(())((())))");
        Assert.Equal( [4], result);
    }
}