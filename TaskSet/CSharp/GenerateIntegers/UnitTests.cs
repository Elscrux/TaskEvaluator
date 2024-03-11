using Xunit;
namespace Task;

public class Test_GenerateIntegers {
    [Fact]
    public void Test_0() {
        var result = TaskClass.GenerateIntegers(2, 10);
        Assert.Equal([2, 4, 6, 8], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.GenerateIntegers(10, 2);
        Assert.Equal([2, 4, 6, 8], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.GenerateIntegers(132, 2);
        Assert.Equal([2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 50, 52, 54, 56, 58, 60, 62, 64, 66, 68, 70, 72, 74, 76, 78, 80, 82, 84, 86, 88, 90, 92, 94, 96, 98, 100, 102, 104, 106, 108, 110, 112, 114, 116, 118, 120, 122, 124, 126, 128, 130], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.GenerateIntegers(17,89);
        Assert.Equal([18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 50, 52, 54, 56, 58, 60, 62, 64, 66, 68, 70, 72, 74, 76, 78, 80, 82, 84, 86, 88], result);
    }
}