using Xunit;
namespace Task;

public class Test_OrderByPoints {
    [Fact]
    public void Test_0() {
        var result = TaskClass.OrderByPoints([1, 11, -1, -11, -12]);
        Assert.Equal([-1, -11, 1, -12, 11], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.OrderByPoints([1234,423,463,145,2,423,423,53,6,37,3457,3,56,0,46]);
        Assert.Equal([0, 2, 3, 6, 53, 423, 423, 423, 1234, 145, 37, 46, 56, 463, 3457], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.OrderByPoints([]);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.OrderByPoints([1, -11, -32, 43, 54, -98, 2, -3]);
        Assert.Equal([-3, -32, -98, -11, 1, 2, 43, 54], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.OrderByPoints([1,2,3,4,5,6,7,8,9,10,11]);
        Assert.Equal([1, 10, 2, 11, 3, 4, 5, 6, 7, 8, 9], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.OrderByPoints([0,6,6,-76,-21,23,4]);
        Assert.Equal([-76, -21, 0, 4, 23, 6, 6], result);
    }
}