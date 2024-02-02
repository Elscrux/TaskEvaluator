using Xunit;
namespace Task;

public class Test_ByLength {
    [Fact]
    public void Test_0() {
        var result = TaskClass.ByLength([]);
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.ByLength([1, -1 , 55]);
        Assert.Equal(["One"], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.ByLength([1, -1, 3, 2]);
        Assert.Equal(["Three", "Two", "One"], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.ByLength([9, 4, 8]);
        Assert.Equal(["Nine", "Eight", "Four"], result);
    }
}