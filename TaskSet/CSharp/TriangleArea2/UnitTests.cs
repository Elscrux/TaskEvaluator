using Xunit;
namespace Task;

public class Test_TriangleArea2 {
    [Fact]
    public void Test_0() {
        var result = TaskClass.TriangleArea2(3, 4, 5);
        Assert.Equal(6.00, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.TriangleArea2(1, 2, 10);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.TriangleArea2(4, 8, 5);
        Assert.Equal(8.18, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.TriangleArea2(2, 2, 2);
        Assert.Equal(1.73, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.TriangleArea2(1, 2, 3);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.TriangleArea2(10, 5, 7);
        Assert.Equal(16.25, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.TriangleArea2(2, 6, 3);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.TriangleArea2(1, 1, 1);
        Assert.Equal(0.43, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.TriangleArea2(2, 2, 10);
        Assert.Equal(-1, result);
    }
}