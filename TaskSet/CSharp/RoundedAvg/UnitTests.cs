using Xunit;
namespace Task;

public class Test_RoundedAvg {
    [Fact]
    public void Test_0() {
        var result = TaskClass.RoundedAvg(1, 5);
        Assert.Equal("0b11", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.RoundedAvg(7, 13);
        Assert.Equal("0b1010", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.RoundedAvg(964,977);
        Assert.Equal("0b1111001010", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.RoundedAvg(996,997);
        Assert.Equal("0b1111100100", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.RoundedAvg(560,851);
        Assert.Equal("0b1011000010", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.RoundedAvg(185,546);
        Assert.Equal("0b101101110", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.RoundedAvg(362,496);
        Assert.Equal("0b110101101", result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.RoundedAvg(350,902);
        Assert.Equal("0b1001110010", result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.RoundedAvg(197,233);
        Assert.Equal("0b11010111", result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.RoundedAvg(7, 5);
        Assert.Equal("-1", result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.RoundedAvg(5, 1);
        Assert.Equal("-1", result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.RoundedAvg(5, 5);
        Assert.Equal("0b101", result);
    }
}