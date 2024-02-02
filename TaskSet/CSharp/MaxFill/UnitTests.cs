using Xunit;
namespace Task;

public class Test_MaxFill {
    [Fact]
    public void Test_0() {
        var result = TaskClass.MaxFill([[0,0,1,1], [0,0,0,0], [1,1,1,1], [0,1,1,1]], 2);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.MaxFill([[0,0,0], [0,0,0]], 5);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.MaxFill([[1,1,1,1], [1,1,1,1]], 2);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.MaxFill([[1,1,1,1], [1,1,1,1]], 9);
        Assert.Equal(2, result);
    }
}