using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CountDistinctCharacters("");
        Assert.Equal( 0, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CountDistinctCharacters("abcde");
        Assert.Equal( 5, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CountDistinctCharacters("abcde" + "cade" + "CADE");
        Assert.Equal( 5, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CountDistinctCharacters("aaaaAAAAaaaa");
        Assert.Equal( 1, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CountDistinctCharacters("Jerry jERRY JeRRRY");
        Assert.Equal( 5, result);
    }
}