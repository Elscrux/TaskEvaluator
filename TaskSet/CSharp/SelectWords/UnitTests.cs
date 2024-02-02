using Xunit;
namespace Task;

public class Test_SelectWords {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SelectWords("a b c d e f", 1);
        Assert.Equal(["b", "c", "d", "f"], result);
    }
}