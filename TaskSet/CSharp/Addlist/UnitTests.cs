using Xunit;
namespace Task;

public class Test_AddList {
    [Fact]
    public void Test_0() {
        var result = TaskClass.AddList([4, 88]);
        Assert.Equal(88, result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.AddList([4, 5, 6, 7, 2, 122]);
        Assert.Equal(122, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.AddList([4, 0, 6, 7]);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.AddList([4, 4, 6, 8]);
        Assert.Equal(12, result);
    }
}