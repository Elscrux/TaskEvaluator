using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Strlen("");
        Assert.Equal( 0, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Strlen("x");
        Assert.Equal( 1, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Strlen("asdasnakj");
        Assert.Equal( 9, result);
    }
}