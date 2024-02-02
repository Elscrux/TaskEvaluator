using Xunit;
namespace Task;

public class Test_TriangleArea {
    [Fact]
    public void Test_0() {
        var result = TaskClass.TriangleArea(5, 3);
        Assert.Equal(7.5, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.TriangleArea(2, 2);
        Assert.Equal(2.0, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.TriangleArea(10, 8);
        Assert.Equal(40.0, result);
    }
}