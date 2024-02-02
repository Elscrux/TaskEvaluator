using Xunit;
namespace Task;

public class Test_NextSmallest {
    [Fact]
    public void Test_0() {
        var result = TaskClass.NextSmallest([1, 2, 3, 4, 5]);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.NextSmallest([5, 1, 4, 3, 2]);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.NextSmallest([]);
        Assert.Equal(null, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.NextSmallest([1, 1]);
        Assert.Equal(null, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.NextSmallest([1,1,1,1,0]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.NextSmallest([1, 1]);
        Assert.Equal(null, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.NextSmallest([-35, 34, 12, -45]);
        Assert.Equal(-35, result);
    }
}