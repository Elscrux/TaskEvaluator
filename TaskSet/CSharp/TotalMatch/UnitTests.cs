using Xunit;
namespace Task;

public class Test_TotalMatch {
    [Fact]
    public void Test_0() {
        var result = TaskClass.TotalMatch(["this"], []);
        Assert.Equal([], result);
    }
}