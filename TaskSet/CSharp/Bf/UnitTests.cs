using Xunit;
namespace Task;

public class Test_Bf {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Bf("Jupiter", "Neptune");
        Assert.Equal(["Saturn", "Uranus"], result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Bf("Earth", "Mercury");
        Assert.Equal(["Venus"], result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Bf("Mercury", "Uranus");
        Assert.Equal(["Venus", "Earth", "Mars", "Jupiter", "Saturn"], result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Bf("Neptune", "Venus");
        Assert.Equal(["Earth", "Mars", "Jupiter", "Saturn", "Uranus"], result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Bf("Earth", "Earth");
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Bf("Mars", "Earth");
        Assert.Equal([], result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Bf("Jupiter", "Makemake");
        Assert.Equal([], result);
    }
}