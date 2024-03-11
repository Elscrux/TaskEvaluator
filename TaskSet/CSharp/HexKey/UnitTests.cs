using Xunit;
namespace Task;

public class Test_HexKey {
    [Fact]
    public void Test_0() {
        var result = TaskClass.HexKey("AB");
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.HexKey("1077E");
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.HexKey("ABED1A33");
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.HexKey("2020");
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.HexKey("123456789ABCDEF0");
        Assert.Equal(6, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.HexKey("112233445566778899AABBCCDDEEFF00");
        Assert.Equal(12, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.HexKey("");
        Assert.Equal(0, result);
    }
}