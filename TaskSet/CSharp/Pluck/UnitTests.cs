using Xunit;
namespace Task;

public class Test_Pluck {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Pluck([1,2,3]);
        Assert.Equal([2, 1], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Pluck([]);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Pluck([5, 0, 3, 0, 4, 2]);
        Assert.Equal([0, 1], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Pluck([1, 2, 3, 0, 5, 3]);
        Assert.Equal([0, 3], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Pluck([5, 4, 8, 4 ,8]);
        Assert.Equal([4, 1], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Pluck([7, 6, 7, 1]);
        Assert.Equal([6, 1], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Pluck([7, 9, 7, 1]);
        Assert.Equal([], result);
    }
}