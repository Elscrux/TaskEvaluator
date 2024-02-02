using Xunit;
namespace Task;

public class Test_EvenOddPalindrome {
    [Fact]
    public void Test_0() {
        var result = TaskClass.EvenOddPalindrome(123);
        Assert.Equal((8, 13), result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.EvenOddPalindrome(12);
        Assert.Equal((4, 6), result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.EvenOddPalindrome(3);
        Assert.Equal((1, 2), result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.EvenOddPalindrome(63);
        Assert.Equal((6, 8), result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.EvenOddPalindrome(25);
        Assert.Equal((5, 6), result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.EvenOddPalindrome(19);
        Assert.Equal((4, 6), result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.EvenOddPalindrome(9);
        Assert.Equal((4, 5), result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.EvenOddPalindrome(1);
        Assert.Equal((0, 1), result);
    }
}