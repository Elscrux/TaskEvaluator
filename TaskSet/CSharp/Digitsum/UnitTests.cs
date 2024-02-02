using Xunit;
namespace Task;

public class Test_Digitsum {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Digitsum("abAB");
        Assert.Equal(131, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Digitsum("abcCd");
        Assert.Equal(67, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Digitsum("helloE");
        Assert.Equal(69, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Digitsum("woArBld");
        Assert.Equal(131, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Digitsum("aAaaaXa");
        Assert.Equal(153, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Digitsum(" How are yOu?");
        Assert.Equal(151, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Digitsum("You arE Very Smart");
        Assert.Equal(327, result);
    }
}