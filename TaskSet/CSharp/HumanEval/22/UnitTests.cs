using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.FilterIntegers([]);
        Assert.Equal( [], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.FilterIntegers([4, {}, [], 23.2, 9, "adasd"]);
        Assert.Equal( [4, 9], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.FilterIntegers([3, "c", 3, 3, "a", "b"]);
        Assert.Equal( [3, 3, 3], result);
    }
}