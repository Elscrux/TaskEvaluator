using Xunit;
namespace Task;

public class Test_Unique {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Unique([5, 3, 5, 2, 3, 3, 9, 0, 123]);
        Assert.Equal([0, 2, 3, 5, 9, 123], result);
    }
}