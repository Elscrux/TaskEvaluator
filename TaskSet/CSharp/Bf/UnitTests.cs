using Xunit;
namespace Task;

public class Test_Bf {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Bf("Mars", "Earth");
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Bf("Jupiter", "Makemake");
        Assert.Equal([], result);
    }
}