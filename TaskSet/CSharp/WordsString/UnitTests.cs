using Xunit;
namespace Task;

public class Test_WordsString {
    [Fact]
    public void Test_0() {
        var result = TaskClass.WordsString("One, two, three, four, five, six");
        Assert.Equal(["One", "two", "three", "four", "five", "six"], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.WordsString("Hi, my name");
        Assert.Equal(["Hi", "my", "name"], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.WordsString("One,, two, three, four, five, six,");
        Assert.Equal(["One", "two", "three", "four", "five", "six"], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.WordsString("");
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.WordsString("ahmed     , gamal");
        Assert.Equal(["ahmed", "gamal"], result);
    }
}