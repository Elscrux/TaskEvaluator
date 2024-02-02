using Xunit;
namespace Task;

public class Test_UniqueDigits {
    [Fact]
    public void Test_0() {
        var result = TaskClass.UniqueDigits([15, 33, 1422, 1]);
        Assert.Equal([1, 15, 33], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.UniqueDigits([152, 323, 1422, 10]);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.UniqueDigits([12345, 2033, 111, 151]);
        Assert.Equal([111, 151], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.UniqueDigits([135, 103, 31]);
        Assert.Equal([31, 135], result);
    }
}