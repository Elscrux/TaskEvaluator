using Xunit;
namespace Task;

public class Test_StartsOneEnds {
    [Fact]
    public void Test_0() {
        var result = TaskClass.StartsOneEnds(2);
        Assert.Equal(18, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.StartsOneEnds(3);
        Assert.Equal(180, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.StartsOneEnds(4);
        Assert.Equal(1800, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.StartsOneEnds(5);
        Assert.Equal(18000, result);
    }
}