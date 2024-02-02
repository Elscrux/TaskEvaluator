using Xunit;
namespace Task;

public class Test_IsMultiplyPrime {
    [Fact]
    public void Test_0() {
        var result = TaskClass.IsMultiplyPrime(5);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.IsMultiplyPrime(30);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.IsMultiplyPrime(8);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.IsMultiplyPrime(10);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.IsMultiplyPrime(125);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.IsMultiplyPrime(3 * 5 * 7);
        Assert.Equal(true, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.IsMultiplyPrime(3 * 6 * 7);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.IsMultiplyPrime(9 * 9 * 9);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.IsMultiplyPrime(11 * 9 * 9);
        Assert.Equal(false, result);
    }

    [Fact]
    public void Test_9() {
        var result = TaskClass.IsMultiplyPrime(11 * 13 * 7);
        Assert.Equal(true, result);
    }
}