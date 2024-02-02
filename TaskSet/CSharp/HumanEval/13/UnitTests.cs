using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.GreatestCommonDivisor(3, 7);
        Assert.Equal( 1, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.GreatestCommonDivisor(10, 15);
        Assert.Equal( 5, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.GreatestCommonDivisor(49, 14);
        Assert.Equal( 7, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.GreatestCommonDivisor(144, 60);
        Assert.Equal( 12, result);
    }
}