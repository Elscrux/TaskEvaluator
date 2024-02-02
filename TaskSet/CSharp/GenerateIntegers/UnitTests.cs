using Xunit;
namespace Task;

public class Test_GenerateIntegers {
    [Fact]
    public void Test_0() {
        var result = TaskClass.GenerateIntegers(2, 10);
        Assert.Equal([2, 4, 6, 8], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.GenerateIntegers(10, 2);
        Assert.Equal([2, 4, 6, 8], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.GenerateIntegers(132, 2);
        Assert.Equal([2, 4, 6, 8], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.GenerateIntegers(17,89);
        Assert.Equal([], result);
    }
}