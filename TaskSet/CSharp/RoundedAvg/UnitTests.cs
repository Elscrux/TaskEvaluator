using Xunit;
namespace Task;

public class Test_RoundedAvg {
    [Fact]
    public void Test_0() {
        var result = TaskClass.RoundedAvg(5, 1);
        Assert.Equal("-1", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.RoundedAvg(5, 5);
        Assert.Equal("0b101", result);
    }
}