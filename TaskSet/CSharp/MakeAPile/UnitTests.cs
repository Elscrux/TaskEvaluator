using Xunit;
namespace Task;

public class Test_MakeAPile {
    [Fact]
    public void Test_0() {
        var result = TaskClass.MakeAPile(3);
        Assert.Equal([3, 5, 7], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.MakeAPile(4);
        Assert.Equal([4,6,8,10], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.MakeAPile(5);
        Assert.Equal([5, 7, 9, 11, 13], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.MakeAPile(6);
        Assert.Equal([6, 8, 10, 12, 14, 16], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.MakeAPile(8);
        Assert.Equal([8, 10, 12, 14, 16, 18, 20, 22], result);
    }
}