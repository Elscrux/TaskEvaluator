using Xunit;
namespace Task;

public class Test_Histogram {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Histogram("a b b a");
        Assert.Equal(new Dictionary<string, int> { {"a", 2}, {"b", 2} }, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Histogram("a b c a b");
        Assert.Equal(new Dictionary<string, int> { {"a", 2}, {"b", 2} }, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Histogram("a b c d g");
        Assert.Equal(new Dictionary<string, int> { {"a", 1}, {"b", 1}, {"c", 1}, {"d", 1}, {"g", 1} }, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Histogram("r t g");
        Assert.Equal(new Dictionary<string, int> { {"r", 1}, {"t", 1}, {"g", 1} }, result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Histogram("b b b b a");
        Assert.Equal(new Dictionary<string, int> { {"b", 4} }, result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Histogram("r t g");
        Assert.Equal(new Dictionary<string, int> { {"r", 1}, {"t", 1}, {"g", 1} }, result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Histogram("");
        Assert.Equal(new Dictionary<string, int> {  }, result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Histogram("a");
        Assert.Equal(new Dictionary<string, int> { {"a", 1} }, result);
    }
}