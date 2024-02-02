using Xunit;
namespace Task;

public class Test_TriplesSumToZero {
    [Fact]
    public void Test_0() {
        var result = TaskClass.TriplesSumToZero([1, 3, 5, 0]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.TriplesSumToZero([1, 3, 5, -1]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.TriplesSumToZero([1, 3, -2, 1]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.TriplesSumToZero([1, 2, 3, 7]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.TriplesSumToZero([1, 2, 5, 7]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.TriplesSumToZero([2, 4, -5, 3, 9, 7]);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.TriplesSumToZero([1]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.TriplesSumToZero([1, 3, 5, -100]);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.TriplesSumToZero([100, 3, 5, -100]);
        Assert.Equal(false, result);
    }
}