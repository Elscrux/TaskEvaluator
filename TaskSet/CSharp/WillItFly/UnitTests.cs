using Xunit;
namespace Task;

public class Test_WillItFly {
    [Fact]
    public void Test_0() {
        var result = TaskClass.WillItFly([5], 5);
        Assert.Equal(true, result);
    }
}