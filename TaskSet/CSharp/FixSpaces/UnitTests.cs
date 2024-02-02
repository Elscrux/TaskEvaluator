using Xunit;
namespace Task;

public class Test_FixSpaces {
    [Fact]
    public void Test_0() {
        var result = TaskClass.FixSpaces("Example");
        Assert.Equal("Example", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.FixSpaces("Mudasir Hanif ");
        Assert.Equal("Mudasir_Hanif_", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.FixSpaces("Yellow Yellow  Dirty  Fellow");
        Assert.Equal("Yellow_Yellow__Dirty__Fellow", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.FixSpaces("Exa   mple");
        Assert.Equal("Exa-mple", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.FixSpaces("   Exa 1 2 2 mple");
        Assert.Equal("-Exa_1_2_2_mple", result);
    }
}