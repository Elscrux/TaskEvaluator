using Xunit;
namespace Task;

public class Test_StrongestExtension {
    [Fact]
    public void Test_0() {
        var result = TaskClass.StrongestExtension("Watashi", ["tEN", "niNE", "eIGHt8OKe"]);
        Assert.Equal("Watashi.eIGHt8OKe", result);
    }

    [Fact]
    public void Test_1() {
        var result = TaskClass.StrongestExtension("Boku123", ["nani", "NazeDa", "YEs.WeCaNe", "32145tggg"]);
        Assert.Equal("Boku123.YEs.WeCaNe", result);
    }

    [Fact]
    public void Test_2() {
        var result = TaskClass.StrongestExtension("__YESIMHERE", ["t", "eMptY", "nothing", "zeR00", "NuLl__", "123NoooneB321"]);
        Assert.Equal("__YESIMHERE.NuLl__", result);
    }

    [Fact]
    public void Test_3() {
        var result = TaskClass.StrongestExtension("K", ["Ta", "TAR", "t234An", "cosSo"]);
        Assert.Equal("K.TAR", result);
    }

    [Fact]
    public void Test_4() {
        var result = TaskClass.StrongestExtension("__HAHA", ["Tab", "123", "781345", "-_-"]);
        Assert.Equal("__HAHA.123", result);
    }

    [Fact]
    public void Test_5() {
        var result = TaskClass.StrongestExtension("YameRore", ["HhAas", "okIWILL123", "WorkOut", "Fails", "-_-"]);
        Assert.Equal("YameRore.okIWILL123", result);
    }

    [Fact]
    public void Test_6() {
        var result = TaskClass.StrongestExtension("finNNalLLly", ["Die", "NowW", "Wow", "WoW"]);
        Assert.Equal("finNNalLLly.WoW", result);
    }

    [Fact]
    public void Test_7() {
        var result = TaskClass.StrongestExtension("_", ["Bb", "91245"]);
        Assert.Equal("_.Bb", result);
    }

    [Fact]
    public void Test_8() {
        var result = TaskClass.StrongestExtension("Sp", ["671235", "Bb"]);
        Assert.Equal("Sp.671235", result);
    }
}