using Xunit;
namespace Task;

public class Test_AllPrefixes {
    [Fact]
    public void Test_0() {
        var result = TaskClass.AllPrefixes("");
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.AllPrefixes("asdfgh");
        Assert.Equal(["a", "as", "asd", "asdf", "asdfg", "asdfgh"], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.AllPrefixes("WWW");
        Assert.Equal(["W", "WW", "WWW"], result);
    }
}