using Xunit;
namespace Task;

public class Test_LargestPrimeFactor {
    [Fact]
    public void Test_0() {
        var result = TaskClass.LargestPrimeFactor(15);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.LargestPrimeFactor(27);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.LargestPrimeFactor(63);
        Assert.Equal(7, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.LargestPrimeFactor(330);
        Assert.Equal(11, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.LargestPrimeFactor(13195);
        Assert.Equal(29, result);
    }
}