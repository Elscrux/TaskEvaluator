using Xunit;
namespace Task;

public class Test_Skjkasdkd {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Skjkasdkd([0,3,2,1,3,5,7,4,5,5,5,2,181,32,4,32,3,2,32,324,4,3]);
        Assert.Equal(10, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Skjkasdkd([1,0,1,8,2,4597,2,1,3,40,1,2,1,2,4,2,5,1]);
        Assert.Equal(25, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Skjkasdkd([1,3,1,32,5107,34,83278,109,163,23,2323,32,30,1,9,3]);
        Assert.Equal(13, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Skjkasdkd([0,724,32,71,99,32,6,0,5,91,83,0,5,6]);
        Assert.Equal(11, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Skjkasdkd([0,81,12,3,1,21]);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Skjkasdkd([0,8,1,2,1,7]);
        Assert.Equal(7, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Skjkasdkd([8191]);
        Assert.Equal(19, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Skjkasdkd([8191, 123456, 127, 7]);
        Assert.Equal(19, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.Skjkasdkd([127, 97, 8192]);
        Assert.Equal(10, result);
    }
}