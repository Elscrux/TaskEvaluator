using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.HasCloseElements([1.0, 2.0, 3.9, 4.0, 5.0, 2.2], 0.3);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.HasCloseElements([1.0, 2.0, 3.9, 4.0, 5.0, 2.2], 0.05);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.HasCloseElements([1.0, 2.0, 5.9, 4.0, 5.0], 0.95);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.HasCloseElements([1.0, 2.0, 5.9, 4.0, 5.0], 0.8);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.HasCloseElements([1.0, 2.0, 3.0, 4.0, 5.0, 2.0], 0.1);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.HasCloseElements([1.1, 2.2, 3.1, 4.1, 5.1], 1.0);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.HasCloseElements([1.1, 2.2, 3.1, 4.1, 5.1], 0.5);
        Assert.Equal(false, result);
    }
}