using Xunit;
namespace Task;

public class Test_Specialfilter {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Specialfilter([]);
        Assert.Equal(0, result);
    }
}