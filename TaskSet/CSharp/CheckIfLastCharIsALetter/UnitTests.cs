using Xunit;
namespace Task;

public class Test_CheckIfLastCharIsALetter {
    [Fact]
    public void Test_0() {
        var result = TaskClass.CheckIfLastCharIsALetter("apple");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.CheckIfLastCharIsALetter("apple pi e");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.CheckIfLastCharIsALetter("eeeee");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.CheckIfLastCharIsALetter("A");
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.CheckIfLastCharIsALetter("Pumpkin pie ");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.CheckIfLastCharIsALetter("Pumpkin pie 1");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.CheckIfLastCharIsALetter("");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.CheckIfLastCharIsALetter("eeeee e ");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.CheckIfLastCharIsALetter("apple pie");
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.CheckIfLastCharIsALetter("apple pi e ");
        Assert.Equal(false, result);
    }
}