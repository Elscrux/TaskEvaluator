using Xunit;
namespace Task;

public class Test_StringXor {
    [Fact]
    public void Test_0() {
        var result = TaskClass.StringXor("111000", "101010");
        Assert.Equal("010010", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.StringXor("1", "1");
        Assert.Equal("0", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.StringXor("0101", "0000");
        Assert.Equal("0101", result);
    }
}