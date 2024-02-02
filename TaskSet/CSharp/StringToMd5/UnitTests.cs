using Xunit;
namespace Task;

public class Test_StringToMd5 {
    [Fact]
    public void Test_0() {
        var result = TaskClass.StringToMd5("Hello world");
        Assert.Equal("3e25960a79dbc69b674cd4ec67a72c62", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.StringToMd5("");
        Assert.Equal(null, result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.StringToMd5("A B C");
        Assert.Equal("0ef78513b0cb8cef12743f5aeb35f888", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.StringToMd5("password");
        Assert.Equal("5f4dcc3b5aa765d61d8327deb882cf99", result);
    }
}