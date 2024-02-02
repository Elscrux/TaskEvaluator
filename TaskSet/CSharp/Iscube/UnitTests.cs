using Xunit;
namespace Task;

public class Test_Iscube {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Iscube(1729);
        Assert.Equal(false, result);
    }
}