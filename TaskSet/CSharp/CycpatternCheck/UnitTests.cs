using Xunit;
namespace Task;

public class Test_CycpatternCheck {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CycpatternCheck("yello","ell");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CycpatternCheck("whattup","ptut");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CycpatternCheck("efef","fee");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CycpatternCheck("abab","aabb");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CycpatternCheck("winemtt","tinem");
        Assert.Equal(true, result);
    }
}