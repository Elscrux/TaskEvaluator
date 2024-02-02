using Xunit;
namespace Task;

public class Test_Maximum {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Maximum([-3, -4, 5], 3);
        Assert.Equal([-4, -3, 5], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Maximum([4, -4, 4], 2);
        Assert.Equal([4, 4], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Maximum([-3, 2, 1, 2, -1, -2, 1], 1);
        Assert.Equal([2], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Maximum([123, -123, 20, 0 , 1, 2, -3], 3);
        Assert.Equal([2, 20, 123], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Maximum([-123, 20, 0 , 1, 2, -3], 4);
        Assert.Equal([0, 1, 2, 20], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Maximum([5, 15, 0, 3, -13, -8, 0], 7);
        Assert.Equal([-13, -8, 0, 0, 3, 5, 15], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Maximum([-1, 0, 2, 5, 3, -10], 2);
        Assert.Equal([3, 5], result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Maximum([1, 0, 5, -7], 1);
        Assert.Equal([5], result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.Maximum([4, -4], 2);
        Assert.Equal([-4, 4], result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.Maximum([-10, 10], 2);
        Assert.Equal([-10, 10], result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.Maximum([1, 2, 3, -23, 243, -400, 0], 0);
        Assert.Equal([], result);
    }
}