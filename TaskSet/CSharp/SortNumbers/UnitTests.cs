using Xunit;
namespace Task;

public class Test_SortNumbers {
    [Fact]
    public void Test_0() {
        var result = TaskClass.SortNumbers("");
        Assert.Equal("", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.SortNumbers("three");
        Assert.Equal("three", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.SortNumbers("three five nine");
        Assert.Equal("three five nine", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.SortNumbers("five zero four seven nine eight");
        Assert.Equal("zero four five seven eight nine", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.SortNumbers("six five four three two one zero");
        Assert.Equal("zero one two three four five six", result);
    }
}