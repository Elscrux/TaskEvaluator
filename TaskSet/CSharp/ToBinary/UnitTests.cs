using Xunit;
namespace Task;

public class Test_ToBinary {
    [Fact]
    public void Test_0() {
        var result = TaskClass.ToBinary(150);
        Assert.Equal("110", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.ToBinary(147);
        Assert.Equal("1100", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.ToBinary(333);
        Assert.Equal("1001", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.ToBinary(963);
        Assert.Equal("10010", result);
    }
}