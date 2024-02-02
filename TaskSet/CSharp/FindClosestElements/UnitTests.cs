using Xunit;
namespace Task;

public class Test_FindClosestElements {
    [Fact]
    public void Test_0() {
        var result = TaskClass.FindClosestElements([1.0, 2.0, 3.9, 4.0, 5.0, 2.2]);
        Assert.Equal((3.9, 4.0), result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.FindClosestElements([1.0, 2.0, 5.9, 4.0, 5.0]);
        Assert.Equal((5.0, 5.9), result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.FindClosestElements([1.0, 2.0, 3.0, 4.0, 5.0, 2.2]);
        Assert.Equal((2.0, 2.2), result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.FindClosestElements([1.0, 2.0, 3.0, 4.0, 5.0, 2.0]);
        Assert.Equal((2.0, 2.0), result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.FindClosestElements([1.1, 2.2, 3.1, 4.1, 5.1]);
        Assert.Equal((2.2, 3.1), result);
    }
}