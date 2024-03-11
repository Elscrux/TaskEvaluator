using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_2023_12_31() {
        var weekday = TaskClass.GetWeekday(2023, 12, 31);
        Assert.Equal(6, weekday);
    }

    [Fact]
    public void Test_2024_03_11() {
        var weekday = TaskClass.GetWeekday(2024, 03, 11);
        Assert.Equal(0, weekday);
    }

    [Fact]
    public void Test_2024_03_22() {
        var weekday = TaskClass.GetWeekday(2024, 03, 22);
        Assert.Equal(4, weekday);
    }
}