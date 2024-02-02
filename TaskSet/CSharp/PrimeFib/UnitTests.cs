using Xunit;
namespace Task;

public class Test_PrimeFib {
    [Fact]
    public void Test_0() {
        var result = TaskClass.PrimeFib(1);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.PrimeFib(2);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.PrimeFib(3);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.PrimeFib(4);
        Assert.Equal(13, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.PrimeFib(5);
        Assert.Equal(89, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.PrimeFib(6);
        Assert.Equal(233, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.PrimeFib(7);
        Assert.Equal(1597, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.PrimeFib(8);
        Assert.Equal(28657, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.PrimeFib(9);
        Assert.Equal(514229, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.PrimeFib(10);
        Assert.Equal(433494437, result);
    }
}