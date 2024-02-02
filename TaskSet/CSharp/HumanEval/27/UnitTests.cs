using Xunit;
namespace Task;

public class Test {
    [Fact]
    public void Test_0() {
        var result = TaskClass.FlipCase("");
        Assert.Equal( "", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.FlipCase("Hello!");
        Assert.Equal( "hELLO!", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.FlipCase("These violent delights have violent ends");
        Assert.Equal( "tHESE VIOLENT DELIGHTS HAVE VIOLENT ENDS", result);
    }
}