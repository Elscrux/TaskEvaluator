using Xunit;
namespace Task;

public class Test_MeanAbsoluteDeviation {
    [Fact]
    public void Test_0() {
        var result = TaskClass.MeanAbsoluteDeviation([1.0, 2.0, 3.0]);
        Assert.Equal(2.0/3.0, result, 1e-6);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.MeanAbsoluteDeviation([1.0, 2.0, 3.0, 4.0]);
        Assert.Equal(1.0, result, 1e-6);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.MeanAbsoluteDeviation([1.0, 2.0, 3.0, 4.0, 5.0]);
        Assert.Equal(6.0/5.0, result, 1e-6);
    }
}