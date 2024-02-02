using Xunit;
namespace Task;

public class Test_AddElements {
    [Fact]
    public void Test_0() {
        var result = TaskClass.AddElements([1,-2,-3,41,57,76,87,88,99], 3);
        Assert.Equal(-4, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.AddElements([111,121,3,4000,5,6], 2);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.AddElements([11,21,3,90,5,6,7,8,9], 4);
        Assert.Equal(125, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.AddElements([111,21,3,4000,5,6,7,8,9], 4);
        Assert.Equal(24, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.AddElements([1], 1);
        Assert.Equal(1, result);
    }
}