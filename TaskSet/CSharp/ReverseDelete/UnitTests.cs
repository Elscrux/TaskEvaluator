using Xunit;
namespace Task;

public class Test_ReverseDelete {
    [Fact]
    public void Test_0() {
        var result = TaskClass.ReverseDelete("abcde","ae");
        Assert.Equal(("bcd",false), result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.ReverseDelete("abcdef", "b");
        Assert.Equal(("acdef",false), result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.ReverseDelete("abcdedcba","ab");
        Assert.Equal(("cdedc",true), result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.ReverseDelete("dwik","w");
        Assert.Equal(("dik",false), result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.ReverseDelete("a","a");
        Assert.Equal(("",true), result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.ReverseDelete("abcdedcba","");
        Assert.Equal(("abcdedcba",true), result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.ReverseDelete("abcdedcba","v");
        Assert.Equal(("abcdedcba",true), result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.ReverseDelete("vabba","v");
        Assert.Equal(("abba",true), result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.ReverseDelete("mamma", "mia");
        Assert.Equal(("", true), result);
    }
}