using Xunit;
namespace Task;

public class Test_Derivative {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Derivative([3, 1, 2, 4, 5]);
        Assert.Equal([1, 4, 12, 20], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Derivative([1, 2, 3]);
        Assert.Equal([2, 6], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Derivative([3, 2, 1]);
        Assert.Equal([2, 2], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Derivative([3, 2, 1, 0, 4]);
        Assert.Equal([2, 2, 0, 16], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Derivative([1]);
        Assert.Equal([], result);
    }
}