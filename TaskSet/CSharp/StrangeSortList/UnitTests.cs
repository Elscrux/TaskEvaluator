using Xunit;
namespace Task;

public class Test_StrangeSortList {
    [Fact]
    public void Test_0() {
        var result = TaskClass.StrangeSortList([1, 2, 3, 4]);
        Assert.Equal([1, 4, 2, 3], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.StrangeSortList([5, 6, 7, 8, 9]);
        Assert.Equal([5, 9, 6, 8, 7], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.StrangeSortList([1, 2, 3, 4, 5]);
        Assert.Equal([1, 5, 2, 4, 3], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.StrangeSortList([5, 6, 7, 8, 9, 1]);
        Assert.Equal([1, 9, 5, 8, 6, 7], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.StrangeSortList([5, 5, 5, 5]);
        Assert.Equal([5, 5, 5, 5], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.StrangeSortList([]);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.StrangeSortList([1,2,3,4,5,6,7,8]);
        Assert.Equal([1, 8, 2, 7, 3, 6, 4, 5], result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.StrangeSortList([0,2,2,2,5,5,-5,-5]);
        Assert.Equal([-5, 5, -5, 5, 0, 2, 2, 2], result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.StrangeSortList([111111]);
        Assert.Equal([111111], result);
    }
}