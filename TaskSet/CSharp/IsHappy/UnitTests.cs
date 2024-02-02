using Xunit;
namespace Task;

public class Test_IsHappy {
    [Fact]
    public void Test_0() {
        var result = TaskClass.IsHappy("a");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.IsHappy("aa");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.IsHappy("abcd");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.IsHappy("aabb");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.IsHappy("adb");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.IsHappy("xyy");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.IsHappy("iopaxpoi");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.IsHappy("iopaxioi");
        Assert.Equal(false, result);
    }
}