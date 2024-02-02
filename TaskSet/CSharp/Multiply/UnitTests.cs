using Xunit;
namespace Task;

public class Test_Multiply {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Multiply(0, 0);
        Assert.Equal(0, result);
    }
}