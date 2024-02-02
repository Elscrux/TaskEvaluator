using Xunit;
namespace Task;

public class Test_GetClosestVowel {
    [Fact]
    public void Test_0() {
        var result = TaskClass.GetClosestVowel("yogurt");
        Assert.Equal("u", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.GetClosestVowel("full");
        Assert.Equal("u", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.GetClosestVowel("easy");
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.GetClosestVowel("eAsy");
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.GetClosestVowel("ali");
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.GetClosestVowel("bad");
        Assert.Equal("a", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.GetClosestVowel("most");
        Assert.Equal("o", result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.GetClosestVowel("ab");
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.GetClosestVowel("ba");
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.GetClosestVowel("quick");
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.GetClosestVowel("anime");
        Assert.Equal("i", result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.GetClosestVowel("Asia");
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_12() {
        var result = TaskClass.GetClosestVowel("Above");
        Assert.Equal("o", result);
    }
}