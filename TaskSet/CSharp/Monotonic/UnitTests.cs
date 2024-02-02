using Xunit;
namespace Task;

public class Test_Monotonic {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Monotonic([1, 2, 4, 10]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Monotonic([1, 2, 4, 20]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Monotonic([1, 20, 4, 10]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Monotonic([4, 1, 0, -10]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Monotonic([4, 1, 1, 0]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Monotonic([1, 2, 3, 2, 5, 60]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Monotonic([1, 2, 3, 4, 5, 60]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Monotonic([9, 9, 9, 9]);
        Assert.Equal(true, result);
    }
}