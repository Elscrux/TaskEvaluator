using Xunit;
namespace Task;

public class Test_VowelsCount {
    [Fact]
    public void Test_0() {
        var result = TaskClass.VowelsCount("abcde");
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.VowelsCount("Alone");
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.VowelsCount("key");
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.VowelsCount("bye");
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.VowelsCount("keY");
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.VowelsCount("bYe");
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.VowelsCount("ACEDY");
        Assert.Equal(3, result);
    }
}