using Xunit;
namespace Task;

public class Test_Add {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Add(0, 1);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Add(1, 0);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Add(2, 3);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Add(5, 7);
        Assert.Equal(12, result);
    }
}