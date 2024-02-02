using Xunit;
namespace Task;

public class Test_HowManyTimes {
    [Fact]
    public void Test_0() {
        var result = TaskClass.HowManyTimes("", "x");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.HowManyTimes("xyxyxyx", "x");
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.HowManyTimes("cacacacac", "cac");
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.HowManyTimes("john doe", "john");
        Assert.Equal(1, result);
    }
}