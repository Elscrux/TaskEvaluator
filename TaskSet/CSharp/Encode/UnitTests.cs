using Xunit;
namespace Task;

public class Test_Encode {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Encode("TEST");
        Assert.Equal("tgst", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Encode("Mudasir");
        Assert.Equal("mWDCSKR", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Encode("YES");
        Assert.Equal("ygs", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Encode("This is a message");
        Assert.Equal("tHKS KS C MGSSCGG", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Encode("I DoNt KnOw WhAt tO WrItE");
        Assert.Equal("k dQnT kNqW wHcT Tq wRkTg", result);
    }
}