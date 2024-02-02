using Xunit;
namespace Task;

public class Test_CountUpTo {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CountUpTo(5);
        Assert.Equal([2,3], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CountUpTo(6);
        Assert.Equal([2,3,5], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CountUpTo(7);
        Assert.Equal([2,3,5], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CountUpTo(10);
        Assert.Equal([2,3,5,7], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CountUpTo(0);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.CountUpTo(22);
        Assert.Equal([2,3,5,7,11,13,17,19], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.CountUpTo(1);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.CountUpTo(18);
        Assert.Equal([2,3,5,7,11,13,17], result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.CountUpTo(47);
        Assert.Equal([2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43], result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.CountUpTo(101);
        Assert.Equal([2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97], result);
    }
}