using Xunit;
namespace Task;

public class Test_LargestSmallestIntegers {
    [Fact]
    public void Test_0() {
        var result = TaskClass.LargestSmallestIntegers([2, 4, 1, 3, 5, 7]);
        Assert.Equal((null, 1), result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.LargestSmallestIntegers([2, 4, 1, 3, 5, 7, 0]);
        Assert.Equal((null, 1), result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.LargestSmallestIntegers([1, 3, 2, 4, 5, 6, -2]);
        Assert.Equal((-2, 1), result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.LargestSmallestIntegers([4, 5, 3, 6, 2, 7, -7]);
        Assert.Equal((-7, 2), result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.LargestSmallestIntegers([7, 3, 8, 4, 9, 2, 5, -9]);
        Assert.Equal((-9, 2), result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.LargestSmallestIntegers([]);
        Assert.Equal((null, null), result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.LargestSmallestIntegers([0]);
        Assert.Equal((null, null), result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.LargestSmallestIntegers([-1, -3, -5, -6]);
        Assert.Equal((-1, null), result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.LargestSmallestIntegers([-1, -3, -5, -6, 0]);
        Assert.Equal((-1, null), result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.LargestSmallestIntegers([-6, -4, -4, -3, 1]);
        Assert.Equal((-3, 1), result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.LargestSmallestIntegers([-6, -4, -4, -3, -100, 1]);
        Assert.Equal((-3, 1), result);
    }
}