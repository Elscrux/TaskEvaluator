using Xunit;
namespace Task;

public class Test_Exchange {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Exchange([1, 2, 3, 4], [1, 2, 3, 4]);
        Assert.Equal("YES", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Exchange([1, 2, 3, 4], [1, 5, 3, 4]);
        Assert.Equal("NO", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Exchange([1, 2, 3, 4], [2, 1, 4, 3]);
        Assert.Equal("YES", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Exchange([5, 7, 3], [2, 6, 4]);
        Assert.Equal("YES", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Exchange([5, 7, 3], [2, 6, 3]);
        Assert.Equal("NO", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Exchange([3, 2, 6, 1, 8, 9], [3, 5, 5, 1, 1, 1]);
        Assert.Equal("NO", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Exchange([100, 200], [200, 200]);
        Assert.Equal("YES", result);
    }
}