using Xunit;
namespace Task;

public class Test_DecimalToBinary {
    [Fact]
    public void Test_0() {
        var result = TaskClass.DecimalToBinary(0);
        Assert.Equal("db0db", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.DecimalToBinary(32);
        Assert.Equal("db100000db", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.DecimalToBinary(103);
        Assert.Equal("db1100111db", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.DecimalToBinary(15);
        Assert.Equal("db1111db", result);
    }
}