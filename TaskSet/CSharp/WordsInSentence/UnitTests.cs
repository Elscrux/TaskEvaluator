using Xunit;
namespace Task;

public class Test_WordsInSentence {
    [Fact]
    public void Test_0() {
        var result = TaskClass.WordsInSentence("This is a test");
        Assert.Equal("is", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.WordsInSentence("lets go for swimming");
        Assert.Equal("go for", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.WordsInSentence("there is no place available here");
        Assert.Equal("there is no place", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.WordsInSentence("Hi I am Hussein");
        Assert.Equal("Hi am Hussein", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.WordsInSentence("go for it");
        Assert.Equal("go for it", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.WordsInSentence("here");
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.WordsInSentence("here is");
        Assert.Equal("is", result);
    }
}