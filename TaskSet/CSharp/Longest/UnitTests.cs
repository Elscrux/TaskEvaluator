using Xunit;
namespace Task;

public class Test_Longest {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Longest([]);
        Assert.Equal(null, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Longest(["x", "y", "z"]);
        Assert.Equal("x", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Longest(["x", "yyy", "zzzz", "www", "kkkk", "abc"]);
        Assert.Equal("zzzz", result);
    }
}