using Xunit;
namespace Task;

public class Test_Compare {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Compare([1,2,3,4,5,1],[1,2,3,4,2,-2]);
        Assert.Equal([0,0,0,0,3,3], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Compare([0,0,0,0,0,0],[0,0,0,0,0,0]);
        Assert.Equal([0,0,0,0,0,0], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Compare([1,2,3],[-1,-2,-3]);
        Assert.Equal([2,4,6], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Compare([1,2,3,5],[-1,2,3,4]);
        Assert.Equal([2,0,0,1], result);
    }
}