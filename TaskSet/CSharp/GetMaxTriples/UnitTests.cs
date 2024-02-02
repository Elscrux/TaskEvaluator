using Xunit;
namespace Task;

public class Test_GetMaxTriples {
    [Fact]
    public void Test_0() {
        var result = TaskClass.GetMaxTriples(5);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.GetMaxTriples(6);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.GetMaxTriples(10);
        Assert.Equal(36, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.GetMaxTriples(100);
        Assert.Equal(53361, result);
    }
}