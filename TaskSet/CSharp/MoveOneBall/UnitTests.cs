using Xunit;
namespace Task;

public class Test_MoveOneBall {
    [Fact]
    public void Test_0() {
        var result = TaskClass.MoveOneBall([3, 4, 5, 1, 2]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.MoveOneBall([3, 5, 10, 1, 2]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.MoveOneBall([4, 3, 1, 2]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.MoveOneBall([3, 5, 4, 1, 2]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.MoveOneBall([]);
        Assert.Equal(true, result);
    }
}