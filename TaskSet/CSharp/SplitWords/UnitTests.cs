using Xunit;
namespace Task;

public class Test_SplitWords {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SplitWords("Hello world!");
        Assert.Equal(["Hello","world!"], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SplitWords("Hello,world!");
        Assert.Equal(["Hello","world!"], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SplitWords("Hello world,!");
        Assert.Equal(["Hello","world,!"], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SplitWords("Hello,Hello,world !");
        Assert.Equal(["Hello,Hello,world","!"], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SplitWords("abcdef");
        Assert.Equal(["3"], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.SplitWords("aaabb");
        Assert.Equal(["2"], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.SplitWords("aaaBb");
        Assert.Equal(["1"], result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.SplitWords("");
        Assert.Equal(["0"], result);
    }
}