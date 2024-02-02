using Xunit;
namespace Task;

public class Test_NumericalLetterGrade {
    [Fact]
    public void Test_0() {
        var result = TaskClass.NumericalLetterGrade([4.0, 3, 1.7, 2, 3.5]);
        Assert.Equal(["A+", "B", "C-", "C", "A-"], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.NumericalLetterGrade([1.2]);
        Assert.Equal(["D+"], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.NumericalLetterGrade([0.5]);
        Assert.Equal(["D-"], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.NumericalLetterGrade([0.0]);
        Assert.Equal(["E"], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.NumericalLetterGrade([1, 0.3, 1.5, 2.8, 3.3]);
        Assert.Equal(["D", "D-", "C-", "B", "B+"], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.NumericalLetterGrade([0, 0.7]);
        Assert.Equal(["E", "D-"], result);
    }
}