using Xunit;
namespace Task;

public class Test_ChooseNum {
    [Fact]
    public void Test_0() {
        var result = TaskClass.ChooseNum(12, 15);
        Assert.Equal(14, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.ChooseNum(13, 12);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.ChooseNum(33, 12354);
        Assert.Equal(12354, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.ChooseNum(5234, 5233);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.ChooseNum(6, 29);
        Assert.Equal(28, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.ChooseNum(27, 10);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.ChooseNum(7, 7);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.ChooseNum(546, 546);
        Assert.Equal(546, result);
    }
}