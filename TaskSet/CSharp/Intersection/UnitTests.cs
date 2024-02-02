using Xunit;
namespace Task;

public class Test_Intersection {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Intersection((1, 2), (2, 3));
        Assert.Equal("NO", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Intersection((-1, 1), (0, 4));
        Assert.Equal("NO", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Intersection((-3, -1), (-5, 5));
        Assert.Equal("YES", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Intersection((-2, 2), (-4, 0));
        Assert.Equal("YES", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Intersection((-11, 2), (-1, -1));
        Assert.Equal("NO", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Intersection((1, 2), (3, 5));
        Assert.Equal("NO", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Intersection((1, 2), (1, 2));
        Assert.Equal("NO", result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Intersection((-2, -2), (-3, -2));
        Assert.Equal("NO", result);
    }
}