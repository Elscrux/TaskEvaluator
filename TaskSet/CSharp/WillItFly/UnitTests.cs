using Xunit;
namespace Task;

public class Test_WillItFly {
    [Fact]
    public void Test_0() {
        var result = TaskClass.WillItFly([3, 2, 3], 9);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.WillItFly([1, 2], 5);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.WillItFly([3], 5);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.WillItFly([3, 2, 3], 1);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.WillItFly([1, 2, 3], 6);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.WillItFly([5], 5);
        Assert.Equal(true, result);
    }
}