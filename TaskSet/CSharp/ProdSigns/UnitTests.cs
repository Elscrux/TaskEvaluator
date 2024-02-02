using Xunit;
namespace Task;

public class Test_ProdSigns {
    [Fact]
    public void Test_0() {
        var result = TaskClass.ProdSigns([0, 1]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.ProdSigns([1, 1, 1, 2, 3, -1, 1]);
        Assert.Equal(-10, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.ProdSigns([]);
        Assert.Equal(null, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.ProdSigns([2, 4,1, 2, -1, -1, 9]);
        Assert.Equal(20, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.ProdSigns([-1, 1, -1, 1]);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.ProdSigns([-1, 1, 1, 1]);
        Assert.Equal(-4, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.ProdSigns([-1, 1, 1, 0]);
        Assert.Equal(0, result);
    }
}