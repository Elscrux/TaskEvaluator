using Xunit;
namespace Task;

public class Test_SumSquares {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SumSquares([1,2,3]);
        Assert.Equal(14, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SumSquares([1.0,2,3]);
        Assert.Equal(14, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SumSquares([1,3,5,7]);
        Assert.Equal(84, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SumSquares([1.4,4.2,0]);
        Assert.Equal(29, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SumSquares([-2.4,1,1]);
        Assert.Equal(6, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.SumSquares([100,1,15,2]);
        Assert.Equal(10230, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.SumSquares([10000,10000]);
        Assert.Equal(200000000, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.SumSquares([-1.4,4.6,6.3]);
        Assert.Equal(75, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.SumSquares([-1.4,17.9,18.9,19.9]);
        Assert.Equal(1086, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.SumSquares([0]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.SumSquares([-1]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.SumSquares([-1,1,0]);
        Assert.Equal(2, result);
    }
}