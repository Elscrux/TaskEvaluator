using Xunit;
namespace Task;

public class Test_SortArrayBinary {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SortArrayBinary([-2,-3,-4,-5,-6]);
        Assert.Equal([-4, -2, -6, -5, -3], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SortArrayBinary([1,0,2,3,4]);
        Assert.Equal([0, 1, 2, 4, 3], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SortArrayBinary([]);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SortArrayBinary([2,5,77,4,5,3,5,7,2,3,4]);
        Assert.Equal([2, 2, 4, 4, 3, 3, 5, 5, 5, 7, 77], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SortArrayBinary([3,6,44,12,32,5]);
        Assert.Equal([32, 3, 5, 6, 12, 44], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.SortArrayBinary([2,4,8,16,32]);
        Assert.Equal([2, 4, 8, 16, 32], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.SortArrayBinary([2,4,8,16,32]);
        Assert.Equal([2, 4, 8, 16, 32], result);
    }
}