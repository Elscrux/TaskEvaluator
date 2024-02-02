using Xunit;
namespace Task;

public class Test_Minpath {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Minpath([[1, 2, 3], [4, 5, 6], [7, 8, 9]], 3);
        Assert.Equal([1, 2, 1], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Minpath([[5, 9, 3], [4, 1, 6], [7, 8, 2]], 1);
        Assert.Equal([1], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Minpath([[1, 2, 3, 4], [5, 6, 7, 8], [9, 10, 11, 12], [13, 14, 15, 16]], 4);
        Assert.Equal([1, 2, 1, 2], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Minpath([[6, 4, 13, 10], [5, 7, 12, 1], [3, 16, 11, 15], [8, 14, 9, 2]], 7);
        Assert.Equal([1, 10, 1, 10, 1, 10, 1], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Minpath([[8, 14, 9, 2], [6, 4, 13, 15], [5, 7, 1, 12], [3, 10, 11, 16]], 5);
        Assert.Equal([1, 7, 1, 7, 1], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Minpath([[11, 8, 7, 2], [5, 16, 14, 4], [9, 3, 15, 6], [12, 13, 10, 1]], 9);
        Assert.Equal([1, 6, 1, 6, 1, 6, 1, 6, 1], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Minpath([[12, 13, 10, 1], [9, 3, 15, 6], [5, 16, 14, 4], [11, 8, 7, 2]], 12);
        Assert.Equal([1, 6, 1, 6, 1, 6, 1, 6, 1, 6, 1, 6], result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Minpath([[2, 7, 4], [3, 1, 5], [6, 8, 9]], 8);
        Assert.Equal([1, 3, 1, 3, 1, 3, 1, 3], result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.Minpath([[6, 1, 5], [3, 8, 9], [2, 7, 4]], 8);
        Assert.Equal([1, 5, 1, 5, 1, 5, 1, 5], result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.Minpath([[1, 2], [3, 4]], 10);
        Assert.Equal([1, 2, 1, 2, 1, 2, 1, 2, 1, 2], result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.Minpath([[1, 3], [3, 2]], 10);
        Assert.Equal([1, 3, 1, 3, 1, 3, 1, 3, 1, 3], result);
    }
}