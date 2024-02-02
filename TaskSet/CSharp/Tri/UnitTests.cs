using Xunit;
namespace Task;

public class Test_Tri {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Tri(3);
        Assert.Equal([1, 3, 2.0, 8.0], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Tri(4);
        Assert.Equal([1, 3, 2.0, 8.0, 3.0], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Tri(5);
        Assert.Equal([1, 3, 2.0, 8.0, 3.0, 15.0], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Tri(6);
        Assert.Equal([1, 3, 2.0, 8.0, 3.0, 15.0, 4.0], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Tri(7);
        Assert.Equal([1, 3, 2.0, 8.0, 3.0, 15.0, 4.0, 24.0], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Tri(8);
        Assert.Equal([1, 3, 2.0, 8.0, 3.0, 15.0, 4.0, 24.0, 5.0], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Tri(9);
        Assert.Equal([1, 3, 2.0, 8.0, 3.0, 15.0, 4.0, 24.0, 5.0, 35.0], result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Tri(20);
        Assert.Equal([1, 3, 2.0, 8.0, 3.0, 15.0, 4.0, 24.0, 5.0, 35.0, 6.0, 48.0, 7.0, 63.0, 8.0, 80.0, 9.0, 99.0, 10.0, 120.0, 11.0], result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.Tri(0);
        Assert.Equal([1], result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.Tri(1);
        Assert.Equal([1, 3], result);
    }
}