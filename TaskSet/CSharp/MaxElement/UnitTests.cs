using Xunit;
namespace Task;

public class Test_MaxElement {
    [Fact]
    public void Test_0() {
        var result = TaskClass.MaxElement([1, 2, 3]);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.MaxElement([5, 3, -5, 2, -3, 3, 9, 0, 124, 1, -10]);
        Assert.Equal(124, result);
    }
}