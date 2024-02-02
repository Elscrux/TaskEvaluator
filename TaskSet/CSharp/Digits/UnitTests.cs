using Xunit;
namespace Task;

public class Test_Digits {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Digits(5);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Digits(54);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Digits(120);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Digits(5014);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Digits(98765);
        Assert.Equal(315, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Digits(5576543);
        Assert.Equal(2625, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Digits(2468);
        Assert.Equal(0, result);
    }
}