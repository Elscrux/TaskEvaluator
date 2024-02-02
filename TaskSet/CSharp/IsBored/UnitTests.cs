using Xunit;
namespace Task;

public class Test_IsBored {
    [Fact]
    public void Test_0() {
        var result = TaskClass.IsBored("Hello world");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.IsBored("Is the sky blue?");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.IsBored("I love It !");
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.IsBored("bIt");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.IsBored("I feel good today. I will be productive. will kill It");
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.IsBored("You and I are going for a walk");
        Assert.Equal(0, result);
    }
}