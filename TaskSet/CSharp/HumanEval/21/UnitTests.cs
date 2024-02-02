using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.RescaleToUnit([2.0, 49.9]);
        Assert.Equal( [0.0, 1.0], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.RescaleToUnit([100.0, 49.9]);
        Assert.Equal( [1.0, 0.0], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.RescaleToUnit([1.0, 2.0, 3.0, 4.0, 5.0]);
        Assert.Equal( [0.0, 0.25, 0.5, 0.75, 1.0], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.RescaleToUnit([2.0, 1.0, 5.0, 3.0, 4.0]);
        Assert.Equal( [0.25, 0.0, 1.0, 0.5, 0.75], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.RescaleToUnit([12.0, 11.0, 15.0, 13.0, 14.0]);
        Assert.Equal( [0.25, 0.0, 1.0, 0.5, 0.75], result);
    }
}