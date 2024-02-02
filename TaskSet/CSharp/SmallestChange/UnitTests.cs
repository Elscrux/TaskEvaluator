using Xunit;
namespace Task;

public class Test_SmallestChange {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SmallestChange([1,2,3,5,4,7,9,6]);
        Assert.Equal(4, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SmallestChange([1, 2, 3, 4, 3, 2, 2]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SmallestChange([1, 4, 2]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SmallestChange([1, 4, 4, 2]);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SmallestChange([1, 2, 3, 2, 1]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.SmallestChange([3, 1, 1, 3]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.SmallestChange([1]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.SmallestChange([0, 1]);
        Assert.Equal(1, result);
    }
}