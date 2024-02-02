using Xunit;
namespace Task;

public class Test_GetOddCollatz {
    [Fact]
    public void Test_0() {
        var result = TaskClass.GetOddCollatz(14);
        Assert.Equal([1, 5, 7, 11, 13, 17], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.GetOddCollatz(5);
        Assert.Equal([1, 5], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.GetOddCollatz(12);
        Assert.Equal([1, 3, 5], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.GetOddCollatz(1);
        Assert.Equal([1], result);
    }
}