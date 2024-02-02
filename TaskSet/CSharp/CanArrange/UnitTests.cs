using Xunit;
namespace Task;

public class Test_CanArrange {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CanArrange([1,2,4,3,5]);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CanArrange([1,2,4,5]);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CanArrange([1,4,2,5,6,7,8,9,10]);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CanArrange([4,8,5,7,3]);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CanArrange([]);
        Assert.Equal(-1, result);
    }
}