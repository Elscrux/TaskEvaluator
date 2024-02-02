using Xunit;
namespace Task;

public class Test_ValidDate {
    [Fact]
    public void Test_0() {
        var result = TaskClass.ValidDate("03-11-2000");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.ValidDate("15-01-2012");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.ValidDate("04-0-2040");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.ValidDate("06-04-2020");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.ValidDate("01-01-2007");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.ValidDate("03-32-2011");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.ValidDate("");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.ValidDate("04-31-3000");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.ValidDate("06-06-2005");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.ValidDate("21-31-2000");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_10() {
        var result = TaskClass.ValidDate("04-12-2003");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_11() {
        var result = TaskClass.ValidDate("04122003");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_12() {
        var result = TaskClass.ValidDate("20030412");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_13() {
        var result = TaskClass.ValidDate("2003-04");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_14() {
        var result = TaskClass.ValidDate("2003-04-12");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_15() {
        var result = TaskClass.ValidDate("04-2003");
        Assert.Equal(false, result);
    }
}