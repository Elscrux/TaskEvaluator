using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Factorize(2);
        Assert.Equal( [2], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Factorize(4);
        Assert.Equal( [2, 2], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Factorize(8);
        Assert.Equal( [2, 2, 2], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Factorize(3 * 19);
        Assert.Equal( [3, 19], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Factorize(3 * 19 * 3 * 19);
        Assert.Equal( [3, 3, 19, 19], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Factorize(3 * 19 * 3 * 19 * 3 * 19);
        Assert.Equal( [3, 3, 3, 19, 19, 19], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Factorize(3 * 19 * 19 * 19);
        Assert.Equal( [3, 19, 19, 19], result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Factorize(3 * 2 * 3);
        Assert.Equal( [2, 3, 3], result);
    }
}