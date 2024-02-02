using Xunit;
namespace Task;

public class Test_Median {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Median([3, 1, 2, 4, 5]);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Median([-10, 4, 6, 1000, 10, 20]);
        Assert.Equal(8.0, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Median([5]);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Median([6, 5]);
        Assert.Equal(5.5, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Median([8, 1, 3, 9, 9, 2, 7]);
        Assert.Equal(7, result);
    }
}