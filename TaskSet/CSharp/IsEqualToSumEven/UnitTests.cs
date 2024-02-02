using Xunit;
namespace Task;

public class Test_IsEqualToSumEven {
    [Fact]
    public void Test_0() {
        var result = TaskClass.IsEqualToSumEven(4);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.IsEqualToSumEven(6);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.IsEqualToSumEven(8);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.IsEqualToSumEven(10);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.IsEqualToSumEven(11);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.IsEqualToSumEven(12);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.IsEqualToSumEven(13);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.IsEqualToSumEven(16);
        Assert.Equal(true, result);
    }
}