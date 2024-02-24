using Xunit;
namespace Task;

public class Test_Multiply {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Multiply(148, 412);
        Assert.Equal(16, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Multiply(19, 28);
        Assert.Equal(72, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Multiply(2020, 1851);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Multiply(14,-15);
        Assert.Equal(20, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Multiply(76, 67);
        Assert.Equal(42, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Multiply(17, 27);
        Assert.Equal(49, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Multiply(0, 1);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Multiply(0, 0);
        Assert.Equal(0, result);
    }
}