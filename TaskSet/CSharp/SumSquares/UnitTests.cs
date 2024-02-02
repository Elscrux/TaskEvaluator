using Xunit;
namespace Task;

public class Test_SumSquares {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SumSquares([-1]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SumSquares([-1,1,0]);
        Assert.Equal(2, result);
    }
}