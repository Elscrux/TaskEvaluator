using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.ParseMusic("");
        Assert.Equal( [], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.ParseMusic("o o o o");
        Assert.Equal( [4, 4, 4, 4], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.ParseMusic(".| .| .| .|");
        Assert.Equal( [1, 1, 1, 1], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.ParseMusic("o| o| .| .| o o o o");
        Assert.Equal( [2, 2, 1, 1, 4, 4, 4, 4], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.ParseMusic("o| .| o| .| o o| o o|");
        Assert.Equal( [2, 1, 2, 1, 4, 2, 4, 2], result);
    }
}