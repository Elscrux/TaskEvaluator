using Xunit;
namespace Task;

public class Test_StringSequence {
    [Fact]
    public void Test_0() {
        var result = TaskClass.StringSequence(0);
        Assert.Equal("0", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.StringSequence(3);
        Assert.Equal("0 1 2 3", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.StringSequence(10);
        Assert.Equal("0 1 2 3 4 5 6 7 8 9 10", result);
    }
}