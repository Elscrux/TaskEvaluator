using Xunit;
namespace Task;

public class Test_FizzBuzz {
    [Fact]
    public void Test_0() {
        var result = TaskClass.FizzBuzz(50);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.FizzBuzz(78);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.FizzBuzz(79);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.FizzBuzz(100);
        Assert.Equal(3, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.FizzBuzz(200);
        Assert.Equal(6, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.FizzBuzz(4000);
        Assert.Equal(192, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.FizzBuzz(10000);
        Assert.Equal(639, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.FizzBuzz(100000);
        Assert.Equal(8026, result);
    }
}