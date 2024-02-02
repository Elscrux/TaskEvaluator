using Xunit;
namespace Task;

public class Test_Simplify {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Simplify("1/5", "5/1");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Simplify("1/5", "1/5");
        Assert.Equal(false, result);
    }
}