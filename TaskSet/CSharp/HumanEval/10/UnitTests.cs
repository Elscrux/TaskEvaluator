using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.MakePalindrome("");
        Assert.Equal( "", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.MakePalindrome("x");
        Assert.Equal( "x", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.MakePalindrome("xyz");
        Assert.Equal( "xyzyx", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.MakePalindrome("xyx");
        Assert.Equal( "xyx", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.MakePalindrome("jerry");
        Assert.Equal( "jerryrrej", result);
    }
}