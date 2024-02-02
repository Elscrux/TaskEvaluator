using Xunit;
namespace Task;

public class Test_SpecialFactorial {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SpecialFactorial(4);
        Assert.Equal(288, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SpecialFactorial(5);
        Assert.Equal(34560, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SpecialFactorial(7);
        Assert.Equal(125411328000, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SpecialFactorial(1);
        Assert.Equal(1, result);
    }
}