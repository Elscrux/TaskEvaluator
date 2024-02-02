using Xunit;
namespace Task;

public class Test_IsPalindrome {
    [Fact]
    public void Test_0() {
        var result = TaskClass.IsPalindrome("");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.IsPalindrome("aba");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.IsPalindrome("aaaaa");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.IsPalindrome("zbcd");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.IsPalindrome("xywyx");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.IsPalindrome("xywyz");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.IsPalindrome("xywzx");
        Assert.Equal(false, result);
    }
}