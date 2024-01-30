using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_2023_12_31() {
        var weekday = TaskClass.GetWeekday(2023, 12, 31);
        Assert.Equal(6, weekday);
    }
}