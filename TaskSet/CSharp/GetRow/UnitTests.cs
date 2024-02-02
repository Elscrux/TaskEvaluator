using Xunit;
namespace Task;

public class Test_GetRow {
    [Fact]
    public void Test_0() {
        var result = TaskClass.GetRow([
        [1,2,3,4,5,6],
        [1,2,3,4,1,6],
        [1,2,3,4,5,1]
    ], 1);
        Assert.Equal([(0, 0), (1, 4), (1, 0), (2, 5), (2, 0)], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.GetRow([
        [1,2,3,4,5,6],
        [1,2,3,4,5,6],
        [1,2,3,4,5,6],
        [1,2,3,4,5,6],
        [1,2,3,4,5,6],
        [1,2,3,4,5,6]
    ], 2);
        Assert.Equal([(0, 1), (1, 1), (2, 1), (3, 1), (4, 1), (5, 1)], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.GetRow([
        [1,2,3,4,5,6],
        [1,2,3,4,5,6],
        [1,1,3,4,5,6],
        [1,2,1,4,5,6],
        [1,2,3,1,5,6],
        [1,2,3,4,1,6],
        [1,2,3,4,5,1]
    ], 1);
        Assert.Equal([(0, 0), (1, 0), (2, 1), (2, 0), (3, 2), (3, 0), (4, 3), (4, 0), (5, 4), (5, 0), (6, 5), (6, 0)], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.GetRow([], 1);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.GetRow([[1]], 2);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.GetRow([[], [1], [1, 2, 3]], 3);
        Assert.Equal([(2, 2)], result);
    }
}