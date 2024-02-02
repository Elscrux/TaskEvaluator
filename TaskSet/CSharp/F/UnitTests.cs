using Xunit;
namespace Task;

public class Test_F {
    [Fact]
    public void Test_0() {
        var result = TaskClass.F(5);
        Assert.Equal([1, 2, 6, 24, 15], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.F(7);
        Assert.Equal([1, 2, 6, 24, 15, 720, 28], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.F(1);
        Assert.Equal([1], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.F(3);
        Assert.Equal([1, 2, 6], result);
    }
}