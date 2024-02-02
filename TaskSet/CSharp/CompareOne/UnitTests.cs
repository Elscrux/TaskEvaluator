using Xunit;
namespace Task;

public class Test_CompareOne {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CompareOne(1, 2);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CompareOne(1, 2.5);
        Assert.Equal(2.5, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CompareOne(2, 3);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CompareOne(5, 6);
        Assert.Equal(6, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CompareOne(1, "2,3");
        Assert.Equal("2,3", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.CompareOne("5,1", "6");
        Assert.Equal("6", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.CompareOne("1", "2");
        Assert.Equal("2", result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.CompareOne("1", 1);
        Assert.Equal(null, result);
    }
}