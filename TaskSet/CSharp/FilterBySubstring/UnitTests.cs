using Xunit;
namespace Task;

public class Test_FilterBySubstring {
    [Fact]
    public void Test_0() {
        var result = TaskClass.FilterBySubstring([], "john");
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.FilterBySubstring(["xxx", "asd", "xxy", "john doe", "xxxAAA", "xxx"], "xxx");
        Assert.Equal(["xxx", "xxxAAA", "xxx"], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.FilterBySubstring(["xxx", "asd", "aaaxxy", "john doe", "xxxAAA", "xxx"], "xx");
        Assert.Equal(["xxx", "aaaxxy", "xxxAAA", "xxx"], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.FilterBySubstring(["grunt", "trumpet", "prune", "gruesome"], "run");
        Assert.Equal(["grunt", "prune"], result);
    }
}