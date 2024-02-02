using Xunit;
namespace Task;

public class Test_Encrypt {
    [Fact]
    public void Test_0() {
        var result = TaskClass.Encrypt("hi");
        Assert.Equal("lm", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.Encrypt("asdfghjkl");
        Assert.Equal("ewhjklnop", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.Encrypt("gf");
        Assert.Equal("kj", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.Encrypt("et");
        Assert.Equal("ix", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.Encrypt("faewfawefaewg");
        Assert.Equal("jeiajeaijeiak", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.Encrypt("hellomyfriend");
        Assert.Equal("lippsqcjvmirh", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.Encrypt("dxzdlmnilfuhmilufhlihufnmlimnufhlimnufhfucufh");
        Assert.Equal("hbdhpqrmpjylqmpyjlpmlyjrqpmqryjlpmqryjljygyjl", result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.Encrypt("a");
        Assert.Equal("e", result);
    }
}