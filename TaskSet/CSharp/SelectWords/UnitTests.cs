using Xunit;
namespace Task;

public class Test_SelectWords {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SelectWords("Mary had a little lamb", 4);
        Assert.Equal(["little"], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SelectWords("Mary had a little lamb", 3);
        Assert.Equal(["Mary", "lamb"], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SelectWords("simple white space", 2);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SelectWords("Hello world", 4);
        Assert.Equal(["world"], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SelectWords("Uncle sam", 3);
        Assert.Equal(["Uncle"], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.SelectWords("", 4);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.SelectWords("a b c d e f", 1);
        Assert.Equal(["b", "c", "d", "f"], result);
    }
}