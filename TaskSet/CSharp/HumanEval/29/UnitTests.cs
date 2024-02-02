using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.FilterByPrefix([], "john");
        Assert.Equal( [], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.FilterByPrefix(["xxx", "asd", "xxy", "john doe", "xxxAAA", "xxx"], "xxx");
        Assert.Equal( ["xxx", "xxxAAA", "xxx"], result);
    }
}