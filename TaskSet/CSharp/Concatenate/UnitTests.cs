using Xunit;
namespace Task;

public class Test_Concatenate {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Concatenate([]);
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Concatenate(["x", "y", "z"]);
        Assert.Equal("xyz", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Concatenate(["x", "y", "z", "w", "k"]);
        Assert.Equal("xyzwk", result);
    }
}