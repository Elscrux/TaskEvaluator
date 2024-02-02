using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.RemoveDuplicates([]);
        Assert.Equal( [], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.RemoveDuplicates([1, 2, 3, 4]);
        Assert.Equal( [1, 2, 3, 4], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.RemoveDuplicates([1, 2, 3, 2, 4, 3, 5]);
        Assert.Equal( [1, 4, 5], result);
    }
}