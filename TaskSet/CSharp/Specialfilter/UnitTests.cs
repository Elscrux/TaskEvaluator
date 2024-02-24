using Xunit;
namespace Task;

public class Test_Specialfilter {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Specialfilter([5, -2, 1, -5]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Specialfilter([15, -73, 14, -15]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Specialfilter([33, -2, -3, 45, 21, 109]);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Specialfilter([43, -12, 93, 125, 121, 109]);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Specialfilter([71, -2, -33, 75, 21, 19]);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Specialfilter([1]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Specialfilter([]);
        Assert.Equal(0, result);
    }
}