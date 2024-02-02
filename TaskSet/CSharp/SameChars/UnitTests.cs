using Xunit;
namespace Task;

public class Test_SameChars {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SameChars("eabcdzzzz", "dddzzzzzzzddeddabc");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SameChars("abcd", "dddddddabc");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SameChars("dddddddabc", "abcd");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SameChars("eabcd", "dddddddabc");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SameChars("abcd", "dddddddabcf");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.SameChars("eabcdzzzz", "dddzzzzzzzddddabc");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.SameChars("aabb", "aaccc");
        Assert.Equal(false, result);
    }
}