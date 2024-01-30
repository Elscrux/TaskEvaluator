using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_3_Groups_WithWhitespaces() {
        var groups = TaskClass.SeparateParenthesisGroups("( ) (( )) (( )( ))");
        Assert.Equal(["()", "(())", "(()())"], groups);
    }

    [Fact]
    public void Test_4_Groups_WithNestedGroup() {
        var groups = TaskClass.SeparateParenthesisGroups("(()()) ((())) () ((())()())");
        Assert.Equal(["(()())", "((()))", "()", "((())()())"], groups);
    }

    [Fact]
    public void Test_4_Groups() {
        var groups = TaskClass.SeparateParenthesisGroups("() (()) ((())) (((())))");
        Assert.Equal(["()", "(())", "((()))", "(((())))"], groups);
    }

    [Fact]
    public void Test_1_Group() {
        var groups = TaskClass.SeparateParenthesisGroups("(()(())((())))");
        Assert.Equal(["(()(())((())))"], groups);
    }

    [Fact]
    public void Test_3_Groups() {
        var groups = TaskClass.SeparateParenthesisGroups("( ) (( )) (( )( ))");
        Assert.Equal(["()", "(())", "(()())"], groups);
    }

    [Fact]
    public void Test_1_Group_WithWhitespaces() {
        var groups = TaskClass.SeparateParenthesisGroups("(  )");
        Assert.Equal(["()"], groups);
    }

    [Fact]
    public void Test_Empty() {
        var groups = TaskClass.SeparateParenthesisGroups("");
        Assert.Equal([], groups);
    }

    [Fact]
    public void Test_Whitespaces() {
        var groups = TaskClass.SeparateParenthesisGroups("    ");
        Assert.Equal([], groups);
    }
}