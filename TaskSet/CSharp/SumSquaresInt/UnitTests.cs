using Xunit;
namespace Task;

public class Test_SumSquaresInt {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SumSquaresInt([1,2,3]);
        Assert.Equal(6, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SumSquaresInt([1,4,9]);
        Assert.Equal(14, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SumSquaresInt([]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SumSquaresInt([1,1,1,1,1,1,1,1,1]);
        Assert.Equal(9, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SumSquaresInt([-1,-1,-1,-1,-1,-1,-1,-1,-1]);
        Assert.Equal(-3, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.SumSquaresInt([0]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.SumSquaresInt([-1,-5,2,-1,-5]);
        Assert.Equal(-126, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.SumSquaresInt([-56,-99,1,0,-2]);
        Assert.Equal(3030, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.SumSquaresInt([-1,0,0,0,0,0,0,0,-1]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.SumSquaresInt([-16, -9, -2, 36, 36, 26, -20, 25, -40, 20, -4, 12, -26, 35, 37]);
        Assert.Equal(-14196, result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.SumSquaresInt([-1, -3, 17, -1, -15, 13, -1, 14, -14, -12, -5, 14, -14, 6, 13, 11, 16, 16, 4, 10]);
        Assert.Equal(-1448, result);
    }
}